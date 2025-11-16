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
            GBX_Path = new GroupBox();
            BTN_SelectProjectPath = new Button();
            LBL_MbrProjPath = new Label();
            GBX_Replace = new GroupBox();
            GBX_Other = new GroupBox();
            TBX_CustomComment = new TextBox();
            LBL_CustomComment = new Label();
            CBX_AntiDragImages = new CheckBox();
            LBL_Copyright = new Label();
            GBX_Path.SuspendLayout();
            GBX_Replace.SuspendLayout();
            GBX_Other.SuspendLayout();
            SuspendLayout();
            // 
            // BTN_StartCleanUp
            // 
            BTN_StartCleanUp.Anchor = AnchorStyles.Bottom | AnchorStyles.Right;
            BTN_StartCleanUp.Cursor = Cursors.Hand;
            BTN_StartCleanUp.Location = new Point(263, 468);
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
            CBX_DeleteProjectFile.Location = new Point(6, 22);
            CBX_DeleteProjectFile.Name = "CBX_DeleteProjectFile";
            CBX_DeleteProjectFile.Size = new Size(172, 19);
            CBX_DeleteProjectFile.TabIndex = 0;
            CBX_DeleteProjectFile.Text = "Delete project.mobirise file?";
            CBX_DeleteProjectFile.UseVisualStyleBackColor = true;
            // 
            // GBX_Path
            // 
            GBX_Path.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            GBX_Path.Controls.Add(BTN_SelectProjectPath);
            GBX_Path.Controls.Add(LBL_MbrProjPath);
            GBX_Path.Location = new Point(12, 12);
            GBX_Path.Name = "GBX_Path";
            GBX_Path.Size = new Size(327, 85);
            GBX_Path.TabIndex = 5;
            GBX_Path.TabStop = false;
            GBX_Path.Text = "Project path";
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
            // LBL_MbrProjPath
            // 
            LBL_MbrProjPath.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            LBL_MbrProjPath.AutoEllipsis = true;
            LBL_MbrProjPath.BorderStyle = BorderStyle.Fixed3D;
            LBL_MbrProjPath.Location = new Point(6, 19);
            LBL_MbrProjPath.Name = "LBL_MbrProjPath";
            LBL_MbrProjPath.Size = new Size(315, 23);
            LBL_MbrProjPath.TabIndex = 0;
            LBL_MbrProjPath.Text = "Select project path...";
            // 
            // GBX_Replace
            // 
            GBX_Replace.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            GBX_Replace.Controls.Add(LBL_ReplaceValueShort);
            GBX_Replace.Controls.Add(TBX_ReplaceValueShort);
            GBX_Replace.Controls.Add(LBL_ReplaceValueLong);
            GBX_Replace.Controls.Add(TBX_ReplaceValueLong);
            GBX_Replace.Location = new Point(12, 103);
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
            GBX_Other.Location = new Point(12, 195);
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
            LBL_Copyright.Location = new Point(12, 473);
            LBL_Copyright.Name = "LBL_Copyright";
            LBL_Copyright.Size = new Size(159, 13);
            LBL_Copyright.TabIndex = 8;
            LBL_Copyright.Text = "© PIN0L33KZ www.pinoleekz.de";
            // 
            // FRM_Main
            // 
            AutoScaleDimensions = new SizeF(96F, 96F);
            AutoScaleMode = AutoScaleMode.Dpi;
            AutoSize = true;
            AutoSizeMode = AutoSizeMode.GrowAndShrink;
            ClientSize = new Size(351, 495);
            Controls.Add(BTN_StartCleanUp);
            Controls.Add(LBL_Copyright);
            Controls.Add(GBX_Other);
            Controls.Add(GBX_Replace);
            Controls.Add(GBX_Path);
            FormBorderStyle = FormBorderStyle.FixedSingle;
            Icon = (Icon)resources.GetObject("$this.Icon");
            MaximizeBox = false;
            Name = "FRM_Main";
            SizeGripStyle = SizeGripStyle.Show;
            Text = "Mobirise sanitizer";
            GBX_Path.ResumeLayout(false);
            GBX_Replace.ResumeLayout(false);
            GBX_Replace.PerformLayout();
            GBX_Other.ResumeLayout(false);
            GBX_Other.PerformLayout();
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
        private GroupBox GBX_Path;
        private Button BTN_SelectProjectPath;
        private Label LBL_MbrProjPath;
        private GroupBox GBX_Replace;
        private GroupBox GBX_Other;
        private CheckBox CBX_AntiDragImages;
        private Label LBL_CustomComment;
        private TextBox TBX_CustomComment;
        private Label LBL_Copyright;
    }
}