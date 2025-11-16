using System;
using System.IO;
using System.Windows.Forms;
using MobiriseSanitizer.Classes;

namespace MobiriseSanitizer
{
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

        public FRM_Main()
        {
            InitializeComponent();

            // React to DPI changes to keep the layout correct on high DPI displays.
            DpiChanged += MainForm_DpiChanged;
        }

        /// <summary>
        /// Handles DPI changes of the main form and applies the suggested bounds.
        /// </summary>
        private void MainForm_DpiChanged(object? sender, DpiChangedEventArgs e)
        {
            // Apply the system-suggested new size and position to avoid layout issues.
            Bounds = e.SuggestedRectangle;
        }

        /// <summary>
        /// Lets the user select the project root directory via a folder browser dialog.
        /// </summary>
        private void BTN_SelectProjectPath_Click(object sender, EventArgs e)
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
                LBL_MbrProjPath.Text = _projPath;
            }
        }

        /// <summary>
        /// Starts the sanitising process for the selected Mobirise project.
        /// </summary>
        private void BTN_StartCleanUp_Click(object sender, EventArgs e)
        {
            // Ask the user for confirmation before making project-wide changes.
            var confirmResult = MessageBox.Show(
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
                MessageBox.Show(
                    "Please select your project's root directory before starting the clean-up.",
                    "Mobirise Sanitizer",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error);
                return;
            }

            if(!Directory.Exists(_projPath))
            {
                MessageBox.Show(
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
                try
                {
                    // Perform the sanitising steps. Each method has its own internal safety checks.
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
                }
                catch(Exception ex)
                {
                    // Inform the user about any sanitising error and stop further processing.
                    MessageBox.Show(
                        $"An error occurred while sanitising the project:\n\n{ex.Message}",
                        "Mobirise Sanitizer - Error",
                        MessageBoxButtons.OK,
                        MessageBoxIcon.Error);
                    return;
                }

                // Inform the user that the process has completed successfully.
                MessageBox.Show(
                    "The project has been sanitised successfully.",
                    "Mobirise Sanitizer",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Information);
            }
            catch(Exception ex)
            {
                // Failsafe catch for unexpected errors at event handler level.
                MessageBox.Show(
                    $"An unexpected error occurred:\n\n{ex.Message}",
                    "Mobirise Sanitizer - Unexpected Error",
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
    }
}