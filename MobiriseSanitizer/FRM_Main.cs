using System.Diagnostics;
using System.Text.Json;
using MobiriseSanitizer.Classes;

namespace MobiriseSanitizer
{
    /// <summary>
    /// Main form of the Mobirise Sanitizer application.
    /// Provides the user interface to select a project and run the sanitizing operations.
    /// </summary>
    public partial class FRM_Main : Form
    {
        /// <summary>
        /// Holds the selected project root directory path.
        /// </summary>
        private string _projPath = string.Empty;

        /// <summary>
        /// Short replacement token (e.g. used for "mbr").
        /// </summary>
        private string _replaceValueShort = "proj";

        /// <summary>
        /// Long replacement token (e.g. used for "mobirise" / "mobi").
        /// </summary>
        private string _replaceValueLong = "project";

        /// <summary>
        /// Initializes a new instance of the <see cref="FRM_Main"/> class.
        /// Sets up the user interface and subscribes to relevant events.
        /// </summary>
        public FRM_Main()
        {
            InitializeComponent();

            // React to DPI changes to keep the layout correct on high DPI displays.
            DpiChanged += MainForm_DpiChanged;
        }

        /// <summary>
        /// Handles DPI changes of the main form and applies the suggested bounds.
        /// </summary>
        /// <param name="sender">The source of the event (main form).</param>
        /// <param name="e">Event data containing the suggested bounds.</param>
        private void MainForm_DpiChanged(object? sender, DpiChangedEventArgs e)
        {
            // Apply the system-suggested new size and position to avoid layout issues.
            Bounds = e.SuggestedRectangle;
        }

        /// <summary>
        /// Lets the user select the project root directory via a folder browser dialog.
        /// Stores the result in <see cref="_projPath"/> and updates the label on success.
        /// </summary>
        /// <param name="sender">The source of the event (button).</param>
        /// <param name="e">Event data.</param>
        private void BTN_SelectProjectPath_Click(object sender, EventArgs e)
        {
            try
            {
                using FolderBrowserDialog dirDialog = new()
                {
                    Description = "Select the root directory of your Mobirise project.",
                    RootFolder = Environment.SpecialFolder.MyDocuments,
                    ShowHiddenFiles = false,
                    ShowNewFolderButton = true
                };

                if(dirDialog.ShowDialog(this) == DialogResult.OK)
                {
                    _projPath = dirDialog.SelectedPath;
                    LBL_ProjectPath.Text = _projPath;
                }
            }
            catch(Exception ex)
            {
                Debug.WriteLine(ex);
                _ = MessageBox.Show(
                    $"An error occurred while selecting the project directory:\n\n{ex.Message}",
                    "Mobirise Sanitizer - Error",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error);
            }
        }

        /// <summary>
        /// Opens the currently selected project directory in Windows Explorer.
        /// </summary>
        /// <param name="sender">The source of the event (button).</param>
        /// <param name="e">Event data.</param>
        private void BTN_OpenPath_Click(object sender, EventArgs e)
        {
            if(string.IsNullOrWhiteSpace(_projPath) || !Directory.Exists(_projPath))
            {
                _ = MessageBox.Show(
                    "Please select a valid project directory before trying to open it.",
                    "Mobirise Sanitizer",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Warning);
                return;
            }

            try
            {
                // Use shell execute to open the folder directly.
                ProcessStartInfo psi = new()
                {
                    FileName = _projPath,
                    UseShellExecute = true
                };

                _ = Process.Start(psi);
            }
            catch(Exception ex)
            {
                Debug.WriteLine(ex);
                _ = MessageBox.Show(
                    $"The project directory could not be opened:\n\n{ex.Message}",
                    "Mobirise Sanitizer - Error",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error);
            }
        }

        /// <summary>
        /// Starts the sanitising process for the selected Mobirise project.
        /// Performs input validation and locks the UI while processing.
        /// </summary>
        /// <param name="sender">The source of the event (button).</param>
        /// <param name="e">Event data.</param>
        private void BTN_StartCleanUp_Click(object sender, EventArgs e)
        {
            // Ask the user for confirmation before making project-wide changes.
            DialogResult confirmResult = MessageBox.Show(
                "This tool will sanitise your Mobirise project. It will remove comments, meta tags, image alt texts, links to Mobirise and advertisement banners from all pages. It will also rename any directory or file that contains 'Mobirise'. Continue?",
                "Mobirise Sanitizer",
                MessageBoxButtons.YesNo,
                MessageBoxIcon.Question);

            if(confirmResult != DialogResult.Yes)
            {
                return;
            }

            // Validate the project path before proceeding.
            if(string.IsNullOrWhiteSpace(_projPath))
            {
                _ = MessageBox.Show(
                    "Please select your project's root directory before starting the clean-up.",
                    "Mobirise Sanitizer",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error);
                return;
            }

            if(!Directory.Exists(_projPath))
            {
                _ = MessageBox.Show(
                    "The selected project root directory does not exist. Please choose a valid path.",
                    "Mobirise Sanitizer",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error);
                return;
            }

            // Apply optional replacement values entered by the user, if provided.
            if(!string.IsNullOrWhiteSpace(TBX_ReplaceValueShort?.Text))
            {
                _replaceValueShort = TBX_ReplaceValueShort.Text.Trim();
            }

            if(!string.IsNullOrWhiteSpace(TBX_ReplaceValueLong?.Text))
            {
                _replaceValueLong = TBX_ReplaceValueLong.Text.Trim();
            }

            // Lock the UI while the clean-up runs to prevent re-entry and accidental interaction.
            Enabled = false;
            Cursor = Cursors.WaitCursor;

            try
            {
                // Perform the sanitising steps. Each method should have its own internal safety checks.
                Sanitize.CleanTags(_projPath, _replaceValueShort, _replaceValueLong);
                Sanitize.CleanFiles(
                    _projPath,
                    _replaceValueShort,
                    _replaceValueLong,
                    CBX_DeleteProjectFile.Checked,
                    CBX_AntiDragImages.Checked);
                Sanitize.CleanAssets(_projPath, _replaceValueShort, _replaceValueLong);
                Sanitize.CleanDirFileNames(_projPath, _replaceValueShort, _replaceValueLong);

                if(!string.IsNullOrWhiteSpace(TBX_CustomComment?.Text))
                {
                    Sanitize.AddCustomComment(_projPath, TBX_CustomComment.Text);
                }

                // Inform the user that the process has completed successfully.
                _ = MessageBox.Show(
                    "The project has been sanitised successfully.",
                    "Mobirise Sanitizer",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Information);
            }
            catch(Exception ex)
            {
                // Single, centralised error handler for sanitising errors.
                Debug.WriteLine(ex);
                _ = MessageBox.Show(
                    $"An error occurred while sanitising the project:\n\n{ex.Message}",
                    "Mobirise Sanitizer - Error",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error);
            }
            finally
            {
                // Always restore the UI state, even if an exception occurred.
                Enabled = true;
                Cursor = Cursors.Default;
            }
        }

        /// <summary>
        /// Collects the current UI values and triggers the export of settings to a JSON file.
        /// </summary>
        /// <param name="sender">The source of the event (toolbar button).</param>
        /// <param name="e">Event data.</param>
        private void TSB_ExportSettings_Click(object sender, EventArgs e)
        {
            AppSettings settings = new()
            {
                ProjPath = LBL_ProjectPath.Text,
                ValueShort = TBX_ReplaceValueShort.Text,
                ValueLong = TBX_ReplaceValueLong.Text,
                DeleteProjectFile = CBX_DeleteProjectFile.Checked,
                AntiDragImages = CBX_AntiDragImages.Checked,
                CustomComment = TBX_CustomComment.Text
            };

            ExportSettings(settings, this);
        }

        /// <summary>
        /// Imports settings from a JSON file and applies them to the user interface.
        /// </summary>
        /// <param name="sender">The source of the event (toolbar button).</param>
        /// <param name="e">Event data.</param>
        private void TSB_ImportSettings_Click(object sender, EventArgs e)
        {
            AppSettings? settings = ImportSettings(this);

            // User cancelled or an error occurred.
            if(settings is null)
            {
                return;
            }

            // Apply imported settings and also update the internal project path.
            _projPath = settings.ProjPath ?? string.Empty;
            LBL_ProjectPath.Text = settings.ProjPath ?? string.Empty;
            TBX_ReplaceValueShort.Text = string.IsNullOrWhiteSpace(settings.ValueShort) ? "proj" : settings.ValueShort;
            TBX_ReplaceValueLong.Text = string.IsNullOrWhiteSpace(settings.ValueLong) ? "project" : settings.ValueLong;
            CBX_DeleteProjectFile.Checked = settings.DeleteProjectFile;
            CBX_AntiDragImages.Checked = settings.AntiDragImages;
            TBX_CustomComment.Text = settings.CustomComment ?? string.Empty;
        }

        /// <summary>
        /// Exports the given <see cref="AppSettings"/> instance as a JSON file
        /// using a <see cref="SaveFileDialog"/>.
        /// </summary>
        /// <param name="settings">The settings to export. Must not be null.</param>
        /// <param name="owner">Optional owner window for the dialog.</param>
        private void ExportSettings(AppSettings? settings, IWin32Window? owner = null)
        {
            if(settings is null)
            {
                _ = MessageBox.Show(
                    "The settings could not be exported because they are null.",
                    "Mobirise Sanitizer - Error",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error);
                return;
            }

            string initialDirectory = Directory.Exists(LBL_ProjectPath.Text)
                ? LBL_ProjectPath.Text
                : Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments);

            using SaveFileDialog dialog = new()
            {
                Title = "Einstellungen exportieren",
                Filter = "JSON-Dateien (*.json)|*.json|Alle Dateien (*.*)|*.*",
                InitialDirectory = initialDirectory,
                DefaultExt = "json",
                AddExtension = true,
                OverwritePrompt = true,
                FileName = "settings.json"
            };

            if(dialog.ShowDialog(owner) != DialogResult.OK)
            {
                return;
            }

            try
            {
                JsonSerializerOptions options = new()
                {
                    WriteIndented = true
                };

                string json = JsonSerializer.Serialize(settings, options);
                File.WriteAllText(dialog.FileName, json);
            }
            catch(Exception ex)
            {
                Debug.WriteLine(ex);
                _ = MessageBox.Show(
                    $"The settings could not be exported:\n\n{ex.Message}",
                    "Mobirise Sanitizer - Error",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error);
            }
        }

        /// <summary>
        /// Imports settings from a JSON file selected via an <see cref="OpenFileDialog"/>.
        /// </summary>
        /// <param name="owner">Optional owner window for the dialog.</param>
        /// <returns>
        /// An <see cref="AppSettings"/> instance on success, or <c>null</c> if the user cancels
        /// or an error occurs.
        /// </returns>
        private AppSettings? ImportSettings(IWin32Window? owner = null)
        {
            string initialDirectory = Directory.Exists(LBL_ProjectPath.Text)
                ? LBL_ProjectPath.Text
                : Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments);

            using OpenFileDialog dialog = new()
            {
                Title = "Einstellungen importieren",
                Filter = "JSON-Dateien (*.json)|*.json|Alle Dateien (*.*)|*.*",
                InitialDirectory = initialDirectory,
                Multiselect = false
            };

            if(dialog.ShowDialog(owner) != DialogResult.OK)
            {
                return null;
            }

            try
            {
                if(!File.Exists(dialog.FileName))
                {
                    _ = MessageBox.Show(
                        "The selected settings file does not exist.",
                        "Mobirise Sanitizer - Error",
                        MessageBoxButtons.OK,
                        MessageBoxIcon.Error);
                    return null;
                }

                string json = File.ReadAllText(dialog.FileName);

                if(string.IsNullOrWhiteSpace(json))
                {
                    _ = MessageBox.Show(
                        "The selected settings file is empty.",
                        "Mobirise Sanitizer - Error",
                        MessageBoxButtons.OK,
                        MessageBoxIcon.Error);
                    return null;
                }

                AppSettings? settings = JsonSerializer.Deserialize<AppSettings>(json);

                if(settings is null)
                {
                    _ = MessageBox.Show(
                        "The settings file could not be deserialized. It may be invalid or corrupted.",
                        "Mobirise Sanitizer - Error",
                        MessageBoxButtons.OK,
                        MessageBoxIcon.Error);
                }

                return settings;
            }
            catch(JsonException ex)
            {
                // Specific handling for JSON issues.
                Debug.WriteLine(ex);
                _ = MessageBox.Show(
                    $"The settings file contains invalid JSON:\n\n{ex.Message}",
                    "Mobirise Sanitizer - JSON Error",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error);
                return null;
            }
            catch(Exception ex)
            {
                Debug.WriteLine(ex);
                _ = MessageBox.Show(
                    $"The settings could not be imported:\n\n{ex.Message}",
                    "Mobirise Sanitizer - Error",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error);
                return null;
            }
        }

        /// <summary>
        /// Deletes all files (and subdirectories) in the currently selected project directory
        /// after double confirmation. This is intended to allow a clean re-publish from Mobirise.
        /// </summary>
        /// <param name="sender">The source of the event (button).</param>
        /// <param name="e">Event data.</param>
        private void BTN_DeleteAllFiles_Click(object sender, EventArgs e)
        {
            // Basic validation of project path first.
            if(string.IsNullOrWhiteSpace(_projPath) || !Directory.Exists(_projPath))
            {
                _ = MessageBox.Show(
                    "Please select a valid project directory before deleting files.",
                    "Mobirise Sanitizer",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Warning);
                return;
            }

            // Safety check: prevent accidental deletion of a drive root (e.g. C:\).
            try
            {
                string normalizedPath = Path.GetFullPath(_projPath);
                string root = Path.GetPathRoot(normalizedPath) ?? string.Empty;

                if(string.Equals(
                        normalizedPath.TrimEnd(Path.DirectorySeparatorChar),
                        root.TrimEnd(Path.DirectorySeparatorChar),
                        StringComparison.OrdinalIgnoreCase))
                {
                    _ = MessageBox.Show(
                        "Refusing to delete files in a root directory. Please select the specific project folder instead.",
                        "Mobirise Sanitizer - Safety Check",
                        MessageBoxButtons.OK,
                        MessageBoxIcon.Error);
                    return;
                }
            }
            catch(Exception ex)
            {
                Debug.WriteLine(ex);
                _ = MessageBox.Show(
                    $"The project path could not be validated:\n\n{ex.Message}",
                    "Mobirise Sanitizer - Error",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error);
                return;
            }

            // First confirmation.
            DialogResult result1 = MessageBox.Show(
                "Do you want to delete ALL files in the selected project directory? " +
                "This is used in order to re-publish your project clean!",
                "Mobirise Sanitizer - Delete all files",
                MessageBoxButtons.YesNo,
                MessageBoxIcon.Warning);

            if(result1 != DialogResult.Yes)
            {
                return;
            }

            // Second, stronger confirmation with explicit path.
            DialogResult result2 = MessageBox.Show(
               $"This will permanently delete ALL files and subdirectories in:\n\n{_projPath}\n\n" +
               "Are you absolutely sure you want to continue?",
               "Mobirise Sanitizer - Final Confirmation",
               MessageBoxButtons.YesNo,
               MessageBoxIcon.Warning);

            if(result2 != DialogResult.Yes)
            {
                return;
            }

            Enabled = false;
            Cursor = Cursors.WaitCursor;

            try
            {
                // Delete all files.
                string[] files = Directory.GetFiles(_projPath, "*", SearchOption.AllDirectories);
                foreach(string file in files)
                {
                    try
                    {
                        File.Delete(file);
                    }
                    catch(Exception exFile)
                    {
                        Debug.WriteLine(exFile);
                        // We do not abort the whole operation, but we inform the user afterwards.
                    }
                }

                // Delete all subdirectories (bottom-up, so children first).
                string[] dirs = Directory.GetDirectories(_projPath, "*", SearchOption.AllDirectories);
                Array.Reverse(dirs);
                foreach(string dir in dirs)
                {
                    try
                    {
                        Directory.Delete(dir, true);
                    }
                    catch(Exception exDir)
                    {
                        Debug.WriteLine(exDir);
                    }
                }

                _ = MessageBox.Show(
                    "All files in the project directory have been deleted. You can now re-publish your project from Mobirise.",
                    "Mobirise Sanitizer",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Information);
            }
            catch(Exception ex)
            {
                Debug.WriteLine(ex);
                _ = MessageBox.Show(
                    $"An error occurred while deleting files:\n\n{ex.Message}",
                    "Mobirise Sanitizer - Error",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error);
            }
            finally
            {
                Enabled = true;
                Cursor = Cursors.Default;
            }
        }
    }
}