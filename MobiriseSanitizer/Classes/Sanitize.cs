using System;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;

namespace MobiriseSanitizer.Classes
{
    /// <summary>
    /// Provides utility methods to sanitise Mobirise-generated projects by
    /// cleaning tags, files, assets, directory and file names, and by adding custom comments.
    /// </summary>
    internal static class Sanitize
    {
        /// <summary>
        /// Cleans Mobirise-specific advertisement tags from all HTML files in the given project directory (non-recursive).
        /// </summary>
        /// <param name="projPath">Path to the project root directory.</param>
        /// <param name="valueShort">
        /// Reserved for future use. Currently not used within this method.
        /// </param>
        /// <param name="valueLong">
        /// Reserved for future use. Currently not used within this method.
        /// </param>
        public static void CleanTags(string projPath, string valueShort, string valueLong)
        {
            // Basic parameter validation for robustness
            if(string.IsNullOrWhiteSpace(projPath))
            {
                throw new ArgumentException("Project path must not be null, empty or whitespace.", nameof(projPath));
            }

            if(!Directory.Exists(projPath))
            {
                throw new DirectoryNotFoundException($"The specified project directory does not exist: '{projPath}'.");
            }

            // Patterns indicating Mobirise advertisement meta and footer content
            string[] removePatterns =
            {
                "<meta name=\"generator\"",
                "<!-- Site made with Mobirise Website",
                "AI Website Software</a>",
                "Drag & Drop Website Builder</a>",
                "AI Website Generator</a>",
                "Website Builder Software</a>",
                "Offline Website Builder</a>",
                "Free AI Website Creator</a>",
                "No Code Website Builder</a>",
                "AI Website Builder</a>",
                "Mobirise.com </a>"
            };

            string[] htmlFiles;
            try
            {
                htmlFiles = Directory.GetFiles(projPath, "*.html");
            }
            catch(Exception ex)
            {
                // In a library context we do not show UI here; the caller can react to this.
                throw new IOException($"Failed to enumerate HTML files in '{projPath}'.", ex);
            }

            foreach(var file in htmlFiles)
            {
                try
                {
                    var fileLines = File.ReadAllLines(file);

                    var cleaned = fileLines
                        .Where(line => !removePatterns.Any(p =>
                            line.Contains(p, StringComparison.OrdinalIgnoreCase)))
                        .ToArray();

                    File.WriteAllLines(file, cleaned);
                }
                catch(Exception ex)
                {
                    // Log and continue with the next file to avoid aborting the whole operation.
                    Debug.WriteLine($"[CleanTags] Failed to process file '{file}': {ex.Message}");
                }
            }
        }

        /// <summary>
        /// Cleans HTML files in the project, optionally removes the project file,
        /// and can add a non-draggable attribute to images.
        /// </summary>
        /// <param name="projPath">Path to the project root directory.</param>
        /// <param name="valueShort">Replacement value for short tokens such as "mbr".</param>
        /// <param name="valueLong">Replacement value for long tokens such as "mobirise" and "mobi".</param>
        /// <param name="deleteProjectFile">If true, deletes the "project.mobirise" file if present.</param>
        /// <param name="antiDragImg">If true, ensures that each <img> tag has draggable="false" when the attribute is missing.</param>
        public static void CleanFiles(
            string projPath,
            string valueShort,
            string valueLong,
            bool deleteProjectFile,
            bool antiDragImg)
        {
            if(string.IsNullOrWhiteSpace(projPath))
            {
                throw new ArgumentException("Project path must not be null, empty or whitespace.", nameof(projPath));
            }

            if(!Directory.Exists(projPath))
            {
                throw new DirectoryNotFoundException($"The specified project directory does not exist: '{projPath}'.");
            }

            // Optionally remove the Mobirise project file
            if(deleteProjectFile)
            {
                try
                {
                    string projectFile = Path.Combine(projPath, "project.mobirise");
                    if(File.Exists(projectFile))
                    {
                        File.Delete(projectFile);
                    }
                }
                catch(Exception ex)
                {
                    Debug.WriteLine($"[CleanFiles] Failed to delete project file in '{projPath}': {ex.Message}");
                    // Intentionally continue; project file removal is non-critical to HTML sanitisation.
                }
            }

            string[] htmlFiles;
            try
            {
                htmlFiles = Directory.GetFiles(projPath, "*.html", SearchOption.AllDirectories);
            }
            catch(Exception ex)
            {
                throw new IOException($"Failed to enumerate HTML files recursively in '{projPath}'.", ex);
            }

            foreach(var file in htmlFiles)
            {
                try
                {
                    string? directoryName = Path.GetDirectoryName(file) != null
                        ? new DirectoryInfo(Path.GetDirectoryName(file)!).Name
                        : null;

                    // Skip HTML files located directly within "images" folders
                    if(directoryName != null &&
                        directoryName.Equals("images", StringComparison.OrdinalIgnoreCase))
                    {
                        continue;
                    }

                    string fileContent = File.ReadAllText(file);

                    string fileContentNew = fileContent
                        .Replace("alt=\"Mobirise Website Builder\"", "alt=\"image\"", StringComparison.OrdinalIgnoreCase)
                        .Replace("mbr", valueShort, StringComparison.OrdinalIgnoreCase)
                        .Replace("mobirise", valueLong, StringComparison.OrdinalIgnoreCase)
                        .Replace("href=\"https://mobiri.se/\"", "href=\"#\"", StringComparison.OrdinalIgnoreCase)
                        .Replace("href=\"https://mobiri.se\"", "href=\"#\"", StringComparison.OrdinalIgnoreCase)
                        .Replace("mobi", valueLong, StringComparison.OrdinalIgnoreCase);

                    if(antiDragImg)
                    {
                        // Ensure each <img ...> (or self-closing <img ... />) has draggable="false"
                        // if no draggable attribute is present.
                        const string pattern = "<img(?<attrs>[^>]*?)(?<slash>/?)>";

                        fileContentNew = Regex.Replace(
                            fileContentNew,
                            pattern,
                            m =>
                            {
                                string attrs = m.Groups["attrs"].Value;
                                string slash = m.Groups["slash"].Value;

                                // If a draggable attribute is already present, leave tag unchanged.
                                return attrs.IndexOf("draggable=", StringComparison.OrdinalIgnoreCase) >= 0
                                    ? m.Value
                                    : "<img" + attrs + " draggable=\"false\"" + slash + ">";
                            },
                            RegexOptions.IgnoreCase | RegexOptions.CultureInvariant);
                    }

                    File.WriteAllText(file, fileContentNew);
                }
                catch(Exception ex)
                {
                    Debug.WriteLine($"[CleanFiles] Failed to process file '{file}': {ex.Message}");
                }
            }
        }

        /// <summary>
        /// Cleans asset text files within the "assets" directory by replacing Mobirise-related tokens.
        /// Non-text files and the "images" directory subtree are ignored.
        /// </summary>
        /// <param name="projPath">Path to the project root directory.</param>
        /// <param name="valueShort">Replacement value for short tokens such as "mbr".</param>
        /// <param name="valueLong">Replacement value for long tokens such as "mobirise" and "mobi".</param>
        public static void CleanAssets(string projPath, string valueShort, string valueLong)
        {
            if(string.IsNullOrWhiteSpace(projPath))
            {
                throw new ArgumentException("Project path must not be null, empty or whitespace.", nameof(projPath));
            }

            if(!Directory.Exists(projPath))
            {
                throw new DirectoryNotFoundException($"The specified project directory does not exist: '{projPath}'.");
            }

            string assetPath = Path.Combine(projPath, "assets");

            if(!Directory.Exists(assetPath))
            {
                // In some projects there may be no "assets" folder; treat this as non-fatal.
                Debug.WriteLine($"[CleanAssets] Asset directory does not exist: '{assetPath}'.");
                return;
            }

            // File extensions which are treated as text-based resources
            string[] textExtensions =
            {
                ".html",
                ".htm",
                ".css",
                ".less",
                ".txt",
                ".js"
            };

            string[] files;
            try
            {
                files = Directory.GetFiles(assetPath, "*.*", SearchOption.AllDirectories);
            }
            catch(Exception ex)
            {
                throw new IOException($"Failed to enumerate files in asset directory '{assetPath}'.", ex);
            }

            foreach(var file in files)
            {
                try
                {
                    string? directoryName = Path.GetDirectoryName(file) != null
                        ? new DirectoryInfo(Path.GetDirectoryName(file)!).Name
                        : null;

                    // Ignore the "images" directory entirely
                    if(directoryName != null &&
                        directoryName.Equals("images", StringComparison.OrdinalIgnoreCase))
                    {
                        continue;
                    }

                    // Only modify known text-based files
                    string ext = Path.GetExtension(file);
                    if(!textExtensions.Contains(ext, StringComparer.OrdinalIgnoreCase))
                    {
                        continue;
                    }

                    string fileContent = File.ReadAllText(file);

                    string fileContentNew = fileContent
                        .Replace("mbr", valueShort, StringComparison.OrdinalIgnoreCase)
                        .Replace("mobirise", valueLong, StringComparison.OrdinalIgnoreCase)
                        .Replace("mobi", valueLong, StringComparison.OrdinalIgnoreCase);

                    // Only write back if something has actually changed
                    if(!string.Equals(fileContent, fileContentNew, StringComparison.Ordinal))
                    {
                        File.WriteAllText(file, fileContentNew);
                    }
                }
                catch(Exception ex)
                {
                    Debug.WriteLine($"[CleanAssets] Failed to process asset file '{file}': {ex.Message}");
                }
            }
        }

        /// <summary>
        /// Renames directories and files inside the "assets" folder by replacing Mobirise-related tokens
        /// in their names. The "images" directory subtree is ignored.
        /// </summary>
        /// <param name="projPath">Path to the project root directory.</param>
        /// <param name="valueShort">Replacement value for short tokens such as "mbr".</param>
        /// <param name="valueLong">Replacement value for long tokens such as "mobirise".</param>
        public static void CleanDirFileNames(string projPath, string valueShort, string valueLong)
        {
            if(string.IsNullOrWhiteSpace(projPath))
            {
                throw new ArgumentException("Project path must not be null, empty or whitespace.", nameof(projPath));
            }

            if(!Directory.Exists(projPath))
            {
                throw new DirectoryNotFoundException($"The specified project directory does not exist: '{projPath}'.");
            }

            string assetPath = Path.Combine(projPath, "assets");

            if(!Directory.Exists(assetPath))
            {
                Debug.WriteLine($"[CleanDirFileNames] Asset directory does not exist: '{assetPath}'.");
                return;
            }

            // Local recursive function to process directories safely.
            void ProcessDirectory(string dir)
            {
                // Ignore any directory named "images" completely
                if(string.Equals(Path.GetFileName(dir), "images", StringComparison.OrdinalIgnoreCase))
                {
                    return;
                }

                // Rename files in the current directory
                foreach(var file in Directory.GetFiles(dir))
                {
                    try
                    {
                        string fileName = Path.GetFileName(file);

                        string newFileName = fileName
                            .Replace("mbr", valueShort, StringComparison.OrdinalIgnoreCase)
                            .Replace("mobirise", valueLong, StringComparison.OrdinalIgnoreCase);

                        if(!string.Equals(fileName, newFileName, StringComparison.Ordinal))
                        {
                            string newPath = Path.Combine(Path.GetDirectoryName(file)!, newFileName);
                            File.Move(file, newPath);
                        }
                    }
                    catch(Exception ex)
                    {
                        Debug.WriteLine($"[CleanDirFileNames] Failed to rename file '{file}': {ex.Message}");
                    }
                }

                // Process and potentially rename subdirectories
                foreach(var subDir in Directory.GetDirectories(dir))
                {
                    string dirName = Path.GetFileName(subDir);

                    string newDirName = dirName
                        .Replace("mbr", valueShort, StringComparison.OrdinalIgnoreCase)
                        .Replace("mobirise", valueLong, StringComparison.OrdinalIgnoreCase);

                    string targetDir = subDir;

                    try
                    {
                        // If the directory name needs to be changed
                        if(!string.Equals(dirName, newDirName, StringComparison.Ordinal))
                        {
                            string parent = Path.GetDirectoryName(subDir)!;
                            targetDir = Path.Combine(parent, newDirName);

                            Directory.Move(subDir, targetDir);
                        }
                    }
                    catch(Exception ex)
                    {
                        Debug.WriteLine($"[CleanDirFileNames] Failed to rename directory '{subDir}': {ex.Message}");
                        // If renaming fails, continue to process the original directory name if possible.
                        targetDir = subDir;
                    }

                    // Continue recursion with the (possibly renamed) directory
                    ProcessDirectory(targetDir);
                }
            }

            try
            {
                ProcessDirectory(assetPath);
            }
            catch(Exception ex)
            {
                Debug.WriteLine($"[CleanDirFileNames] Failed to process asset directory '{assetPath}': {ex.Message}");
            }
        }

        /// <summary>
        /// Adds a custom HTML comment block before the closing &lt;/body&gt; tag
        /// in all HTML files under the specified project directory.
        /// </summary>
        /// <param name="projPath">Path to the project root directory.</param>
        /// <param name="commentText">
        /// Text to be placed inside the HTML comment block. The text is inserted as-is.
        /// </param>
        public static void AddCustomComment(string projPath, string commentText)
        {
            if(string.IsNullOrWhiteSpace(projPath))
            {
                throw new ArgumentException("Project path must not be null, empty or whitespace.", nameof(projPath));
            }

            if(!Directory.Exists(projPath))
            {
                throw new DirectoryNotFoundException($"The specified project directory does not exist: '{projPath}'.");
            }

            if(commentText is null)
            {
                throw new ArgumentNullException(nameof(commentText), "Comment text must not be null.");
            }

            string[] htmlFiles;
            try
            {
                htmlFiles = Directory.GetFiles(projPath, "*.html", SearchOption.AllDirectories);
            }
            catch(Exception ex)
            {
                throw new IOException($"Failed to enumerate HTML files recursively in '{projPath}'.", ex);
            }

            foreach(var file in htmlFiles)
            {
                try
                {
                    string html = File.ReadAllText(file);

                    // If there is no </body> tag, skip the file to avoid producing invalid HTML.
                    if(!Regex.IsMatch(html, "</body>", RegexOptions.IgnoreCase | RegexOptions.CultureInvariant))
                    {
                        continue;
                    }

                    // Build the comment block with line breaks for readability
                    string commentBlock =
                        "<!--" + Environment.NewLine +
                        commentText + Environment.NewLine +
                        "-->" + Environment.NewLine;

                    // Insert the comment directly before the closing </body> tag (case-insensitive)
                    string result = Regex.Replace(
                        html,
                        "</body>",
                        commentBlock + "</body>",
                        RegexOptions.IgnoreCase | RegexOptions.CultureInvariant);

                    File.WriteAllText(file, result);
                }
                catch(Exception ex)
                {
                    Debug.WriteLine($"[AddCustomComment] Failed to add comment to file '{file}': {ex.Message}");
                }
            }
        }
    }
}