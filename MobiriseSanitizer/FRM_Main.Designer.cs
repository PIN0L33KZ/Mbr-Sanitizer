namespace MobiriseSanitizer
{
    partial class FRM_Main
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if(disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FRM_Main));
            BTN_StartCleanUp = new Button();
            LBL_ReplaceValueShort = new Label();
            TBX_ReplaceValueShort = new TextBox();
            LBL_ReplaceValueLong = new Label();
            TBX_ReplaceValueLong = new TextBox();
            CBX_DeleteProjectFile = new CheckBox();
            GBX_ProjectPath = new GroupBox();
            BTN_OpenPath = new Button();
            BTN_SelectProjectPath = new Button();
            LBL_ProjectPath = new Label();
            GBX_Replace = new GroupBox();
            GBX_Other = new GroupBox();
            TBX_CustomComment = new TextBox();
            LBL_CustomComment = new Label();
            CBX_AntiDragImages = new CheckBox();
            LBL_Copyright = new Label();
            TSP_Main = new ToolStrip();
            TSB_ExportSettings = new ToolStripButton();
            TSB_ImportSettings = new ToolStripButton();
            BTN_DeleteAllFiles = new Button();
            GBX_ProjectPath.SuspendLayout();
            GBX_Replace.SuspendLayout();
            GBX_Other.SuspendLayout();
            TSP_Main.SuspendLayout();
            SuspendLayout();
            // 
            // BTN_StartCleanUp
            // 
            BTN_StartCleanUp.Anchor = AnchorStyles.Bottom | AnchorStyles.Right;
            BTN_StartCleanUp.Cursor = Cursors.Hand;
            BTN_StartCleanUp.Location = new Point(263, 483);
            BTN_StartCleanUp.Name = "BTN_StartCleanUp";
            BTN_StartCleanUp.Size = new Size(76, 23);
            BTN_StartCleanUp.TabIndex = 0;
            BTN_StartCleanUp.Text = "Sanitize";
            BTN_StartCleanUp.UseVisualStyleBackColor = true;
            BTN_StartCleanUp.Click += BTN_StartCleanUp_Click;
            // 
            // LBL_ReplaceValueShort
            // 
            LBL_ReplaceValueShort.AutoSize = true;
            LBL_ReplaceValueShort.Location = new Point(6, 25);
            LBL_ReplaceValueShort.Name = "LBL_ReplaceValueShort";
            LBL_ReplaceValueShort.Size = new Size(162, 15);
            LBL_ReplaceValueShort.TabIndex = 2;
            LBL_ReplaceValueShort.Text = "Value for LESS classes (mbr-*)";
            // 
            // TBX_ReplaceValueShort
            // 
            TBX_ReplaceValueShort.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            TBX_ReplaceValueShort.Location = new Point(202, 22);
            TBX_ReplaceValueShort.Name = "TBX_ReplaceValueShort";
            TBX_ReplaceValueShort.PlaceholderText = "Default: proj";
            TBX_ReplaceValueShort.Size = new Size(119, 23);
            TBX_ReplaceValueShort.TabIndex = 0;
            // 
            // LBL_ReplaceValueLong
            // 
            LBL_ReplaceValueLong.AutoSize = true;
            LBL_ReplaceValueLong.Location = new Point(6, 54);
            LBL_ReplaceValueLong.Name = "LBL_ReplaceValueLong";
            LBL_ReplaceValueLong.Size = new Size(175, 15);
            LBL_ReplaceValueLong.TabIndex = 2;
            LBL_ReplaceValueLong.Text = "Value for folders/files (mobirise)";
            // 
            // TBX_ReplaceValueLong
            // 
            TBX_ReplaceValueLong.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            TBX_ReplaceValueLong.Location = new Point(202, 51);
            TBX_ReplaceValueLong.Name = "TBX_ReplaceValueLong";
            TBX_ReplaceValueLong.PlaceholderText = "Default: project";
            TBX_ReplaceValueLong.Size = new Size(119, 23);
            TBX_ReplaceValueLong.TabIndex = 1;
            // 
            // CBX_DeleteProjectFile
            // 
            CBX_DeleteProjectFile.AutoSize = true;
            CBX_DeleteProjectFile.Checked = true;
            CBX_DeleteProjectFile.CheckState = CheckState.Checked;
            CBX_DeleteProjectFile.Location = new Point(6, 22);
            CBX_DeleteProjectFile.Name = "CBX_DeleteProjectFile";
            CBX_DeleteProjectFile.Size = new Size(172, 19);
            CBX_DeleteProjectFile.TabIndex = 0;
            CBX_DeleteProjectFile.Text = "Delete project.mobirise file?";
            CBX_DeleteProjectFile.UseVisualStyleBackColor = true;
            // 
            // GBX_ProjectPath
            // 
            GBX_ProjectPath.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            GBX_ProjectPath.Controls.Add(BTN_OpenPath);
            GBX_ProjectPath.Controls.Add(BTN_DeleteAllFiles);
            GBX_ProjectPath.Controls.Add(BTN_SelectProjectPath);
            GBX_ProjectPath.Controls.Add(LBL_ProjectPath);
            GBX_ProjectPath.Location = new Point(12, 28);
            GBX_ProjectPath.Name = "GBX_ProjectPath";
            GBX_ProjectPath.Size = new Size(327, 85);
            GBX_ProjectPath.TabIndex = 5;
            GBX_ProjectPath.TabStop = false;
            GBX_ProjectPath.Text = "Project path";
            // 
            // BTN_OpenPath
            // 
            BTN_OpenPath.Cursor = Cursors.Hand;
            BTN_OpenPath.Location = new Point(237, 45);
            BTN_OpenPath.Name = "BTN_OpenPath";
            BTN_OpenPath.Size = new Size(84, 23);
            BTN_OpenPath.TabIndex = 0;
            BTN_OpenPath.Text = "Open";
            BTN_OpenPath.UseVisualStyleBackColor = true;
            BTN_OpenPath.Click += BTN_OpenPath_Click;
            // 
            // BTN_SelectProjectPath
            // 
            BTN_SelectProjectPath.Cursor = Cursors.Hand;
            BTN_SelectProjectPath.Location = new Point(6, 45);
            BTN_SelectProjectPath.Name = "BTN_SelectProjectPath";
            BTN_SelectProjectPath.Size = new Size(84, 23);
            BTN_SelectProjectPath.TabIndex = 0;
            BTN_SelectProjectPath.Text = "Select";
            BTN_SelectProjectPath.UseVisualStyleBackColor = true;
            BTN_SelectProjectPath.Click += BTN_SelectProjectPath_Click;
            // 
            // LBL_ProjectPath
            // 
            LBL_ProjectPath.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            LBL_ProjectPath.AutoEllipsis = true;
            LBL_ProjectPath.BorderStyle = BorderStyle.Fixed3D;
            LBL_ProjectPath.Location = new Point(6, 19);
            LBL_ProjectPath.Name = "LBL_ProjectPath";
            LBL_ProjectPath.Size = new Size(315, 23);
            LBL_ProjectPath.TabIndex = 0;
            LBL_ProjectPath.Text = "Select project path...";
            // 
            // GBX_Replace
            // 
            GBX_Replace.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            GBX_Replace.Controls.Add(LBL_ReplaceValueShort);
            GBX_Replace.Controls.Add(TBX_ReplaceValueShort);
            GBX_Replace.Controls.Add(LBL_ReplaceValueLong);
            GBX_Replace.Controls.Add(TBX_ReplaceValueLong);
            GBX_Replace.Location = new Point(12, 119);
            GBX_Replace.Name = "GBX_Replace";
            GBX_Replace.Size = new Size(327, 86);
            GBX_Replace.TabIndex = 6;
            GBX_Replace.TabStop = false;
            GBX_Replace.Text = "Settings for replacing";
            // 
            // GBX_Other
            // 
            GBX_Other.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            GBX_Other.Controls.Add(TBX_CustomComment);
            GBX_Other.Controls.Add(LBL_CustomComment);
            GBX_Other.Controls.Add(CBX_AntiDragImages);
            GBX_Other.Controls.Add(CBX_DeleteProjectFile);
            GBX_Other.Location = new Point(12, 211);
            GBX_Other.Name = "GBX_Other";
            GBX_Other.Size = new Size(327, 260);
            GBX_Other.TabIndex = 7;
            GBX_Other.TabStop = false;
            GBX_Other.Text = "Other settings";
            // 
            // TBX_CustomComment
            // 
            TBX_CustomComment.Anchor = AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            TBX_CustomComment.Location = new Point(6, 96);
            TBX_CustomComment.Multiline = true;
            TBX_CustomComment.Name = "TBX_CustomComment";
            TBX_CustomComment.Size = new Size(315, 158);
            TBX_CustomComment.TabIndex = 2;
            // 
            // LBL_CustomComment
            // 
            LBL_CustomComment.AutoSize = true;
            LBL_CustomComment.Location = new Point(6, 78);
            LBL_CustomComment.Name = "LBL_CustomComment";
            LBL_CustomComment.Size = new Size(195, 15);
            LBL_CustomComment.TabIndex = 1;
            LBL_CustomComment.Text = "Custom comment before </body>:";
            // 
            // CBX_AntiDragImages
            // 
            CBX_AntiDragImages.AutoSize = true;
            CBX_AntiDragImages.Location = new Point(6, 47);
            CBX_AntiDragImages.Name = "CBX_AntiDragImages";
            CBX_AntiDragImages.Size = new Size(253, 19);
            CBX_AntiDragImages.TabIndex = 1;
            CBX_AntiDragImages.Text = "Add 'draggable=\"false\"' to all <img> Tags?";
            CBX_AntiDragImages.UseVisualStyleBackColor = true;
            // 
            // LBL_Copyright
            // 
            LBL_Copyright.Anchor = AnchorStyles.Bottom | AnchorStyles.Left;
            LBL_Copyright.AutoSize = true;
            LBL_Copyright.Font = new Font("Segoe UI", 8.25F, FontStyle.Italic);
            LBL_Copyright.ForeColor = SystemColors.ControlDark;
            LBL_Copyright.Location = new Point(12, 488);
            LBL_Copyright.Name = "LBL_Copyright";
            LBL_Copyright.Size = new Size(159, 13);
            LBL_Copyright.TabIndex = 8;
            LBL_Copyright.Text = "© PIN0L33KZ www.pinoleekz.de";
            // 
            // TSP_Main
            // 
            TSP_Main.Items.AddRange(new ToolStripItem[] { TSB_ExportSettings, TSB_ImportSettings });
            TSP_Main.Location = new Point(0, 0);
            TSP_Main.Name = "TSP_Main";
            TSP_Main.Size = new Size(351, 25);
            TSP_Main.TabIndex = 9;
            // 
            // TSB_ExportSettings
            // 
            TSB_ExportSettings.DisplayStyle = ToolStripItemDisplayStyle.Text;
            TSB_ExportSettings.Image = (Image)resources.GetObject("TSB_ExportSettings.Image");
            TSB_ExportSettings.ImageTransparentColor = Color.Magenta;
            TSB_ExportSettings.Name = "TSB_ExportSettings";
            TSB_ExportSettings.Size = new Size(44, 22);
            TSB_ExportSettings.Text = "Export";
            TSB_ExportSettings.ToolTipText = "Export from file...";
            TSB_ExportSettings.Click += TSB_ExportSettings_Click;
            // 
            // TSB_ImportSettings
            // 
            TSB_ImportSettings.DisplayStyle = ToolStripItemDisplayStyle.Text;
            TSB_ImportSettings.Image = (Image)resources.GetObject("TSB_ImportSettings.Image");
            TSB_ImportSettings.ImageTransparentColor = Color.Magenta;
            TSB_ImportSettings.Name = "TSB_ImportSettings";
            TSB_ImportSettings.Size = new Size(47, 22);
            TSB_ImportSettings.Text = "Import";
            TSB_ImportSettings.ToolTipText = "Import from file...";
            TSB_ImportSettings.Click += TSB_ImportSettings_Click;
            // 
            // BTN_DeleteAllFiles
            // 
            BTN_DeleteAllFiles.Cursor = Cursors.Hand;
            BTN_DeleteAllFiles.Location = new Point(94, 45);
            BTN_DeleteAllFiles.Name = "BTN_DeleteAllFiles";
            BTN_DeleteAllFiles.Size = new Size(84, 23);
            BTN_DeleteAllFiles.TabIndex = 0;
            BTN_DeleteAllFiles.Text = "Delete Files";
            BTN_DeleteAllFiles.UseVisualStyleBackColor = true;
            BTN_DeleteAllFiles.Click += BTN_DeleteAllFiles_Click;
            // 
            // FRM_Main
            // 
            AutoScaleDimensions = new SizeF(96F, 96F);
            AutoScaleMode = AutoScaleMode.Dpi;
            AutoSize = true;
            AutoSizeMode = AutoSizeMode.GrowAndShrink;
            ClientSize = new Size(351, 510);
            Controls.Add(TSP_Main);
            Controls.Add(BTN_StartCleanUp);
            Controls.Add(LBL_Copyright);
            Controls.Add(GBX_Other);
            Controls.Add(GBX_Replace);
            Controls.Add(GBX_ProjectPath);
            FormBorderStyle = FormBorderStyle.FixedSingle;
            Icon = (Icon)resources.GetObject("$this.Icon");
            MaximizeBox = false;
            Name = "FRM_Main";
            SizeGripStyle = SizeGripStyle.Show;
            Text = "Mobirise sanitizer";
            GBX_ProjectPath.ResumeLayout(false);
            GBX_Replace.ResumeLayout(false);
            GBX_Replace.PerformLayout();
            GBX_Other.ResumeLayout(false);
            GBX_Other.PerformLayout();
            TSP_Main.ResumeLayout(false);
            TSP_Main.PerformLayout();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion
        private Button BTN_StartCleanUp;
        private Label LBL_ReplaceValueShort;
        private TextBox TBX_ReplaceValueShort;
        private Label LBL_ReplaceValueLong;
        private TextBox TBX_ReplaceValueLong;
        private CheckBox CBX_DeleteProjectFile;
        private GroupBox GBX_ProjectPath;
        private Button BTN_SelectProjectPath;
        private Label LBL_ProjectPath;
        private GroupBox GBX_Replace;
        private GroupBox GBX_Other;
        private CheckBox CBX_AntiDragImages;
        private Label LBL_CustomComment;
        private TextBox TBX_CustomComment;
        private Label LBL_Copyright;
        private ToolStrip TSP_Main;
        private ToolStripButton TSB_ExportSettings;
        private ToolStripButton TSB_ImportSettings;
        private Button BTN_OpenPath;
        private Button BTN_DeleteAllFiles;
    }
}