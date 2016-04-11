namespace Scriptmanager
{
    partial class ScriptManagerForm
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
            if (disposing && (components != null))
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
            this.cboChampionsList = new System.Windows.Forms.ComboBox();
            this.grid_champions = new System.Windows.Forms.DataGridView();
            this.Title = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Author = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.ForumThread = new System.Windows.Forms.DataGridViewLinkColumn();
            this.Download = new System.Windows.Forms.DataGridViewButtonColumn();
            this.DownloadUrl = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.openFileBol = new System.Windows.Forms.OpenFileDialog();
            this.tabControl = new System.Windows.Forms.TabControl();
            this.tabMain = new System.Windows.Forms.TabPage();
            this.tabSettings = new System.Windows.Forms.TabPage();
            this.groupDebug = new System.Windows.Forms.GroupBox();
            this.cbReplaceScript = new System.Windows.Forms.CheckBox();
            this.cbDebug = new System.Windows.Forms.CheckBox();
            this.groupBasic = new System.Windows.Forms.GroupBox();
            this.cbMoveScripts = new System.Windows.Forms.CheckBox();
            this.tabLogs = new System.Windows.Forms.TabPage();
            this.tbLogs = new System.Windows.Forms.TextBox();
            this.tabAbout = new System.Windows.Forms.TabPage();
            this.lAuthor = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.grid_champions)).BeginInit();
            this.tabControl.SuspendLayout();
            this.tabMain.SuspendLayout();
            this.tabSettings.SuspendLayout();
            this.groupDebug.SuspendLayout();
            this.groupBasic.SuspendLayout();
            this.tabLogs.SuspendLayout();
            this.tabAbout.SuspendLayout();
            this.SuspendLayout();
            // 
            // cboChampionsList
            // 
            this.cboChampionsList.FormattingEnabled = true;
            this.cboChampionsList.Location = new System.Drawing.Point(6, 6);
            this.cboChampionsList.Name = "cboChampionsList";
            this.cboChampionsList.Size = new System.Drawing.Size(121, 21);
            this.cboChampionsList.TabIndex = 0;
            this.cboChampionsList.Text = "Select an champion";
            this.cboChampionsList.SelectedIndexChanged += new System.EventHandler(this.cboChampionsList_SelectedIndexChanged);
            // 
            // grid_champions
            // 
            this.grid_champions.AllowUserToAddRows = false;
            this.grid_champions.AllowUserToDeleteRows = false;
            this.grid_champions.AllowUserToOrderColumns = true;
            this.grid_champions.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.grid_champions.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.grid_champions.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.grid_champions.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.Title,
            this.Author,
            this.ForumThread,
            this.Download,
            this.DownloadUrl});
            this.grid_champions.Location = new System.Drawing.Point(6, 57);
            this.grid_champions.Name = "grid_champions";
            this.grid_champions.ReadOnly = true;
            this.grid_champions.Size = new System.Drawing.Size(508, 231);
            this.grid_champions.TabIndex = 1;
            this.grid_champions.CellClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.grid_champions_CellClick);
            // 
            // Title
            // 
            this.Title.HeaderText = "Title";
            this.Title.Name = "Title";
            this.Title.ReadOnly = true;
            this.Title.Width = 160;
            // 
            // Author
            // 
            this.Author.HeaderText = "Author";
            this.Author.Name = "Author";
            this.Author.ReadOnly = true;
            // 
            // ForumThread
            // 
            this.ForumThread.HeaderText = "Forum Thread";
            this.ForumThread.Name = "ForumThread";
            this.ForumThread.ReadOnly = true;
            this.ForumThread.Resizable = System.Windows.Forms.DataGridViewTriState.True;
            this.ForumThread.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.Automatic;
            this.ForumThread.Text = "Forum Thread";
            this.ForumThread.UseColumnTextForLinkValue = true;
            // 
            // Download
            // 
            this.Download.HeaderText = "Download";
            this.Download.MinimumWidth = 2;
            this.Download.Name = "Download";
            this.Download.ReadOnly = true;
            this.Download.Resizable = System.Windows.Forms.DataGridViewTriState.False;
            this.Download.Text = "Download";
            this.Download.UseColumnTextForButtonValue = true;
            // 
            // DownloadUrl
            // 
            this.DownloadUrl.HeaderText = "DownloadUrl";
            this.DownloadUrl.Name = "DownloadUrl";
            this.DownloadUrl.ReadOnly = true;
            this.DownloadUrl.Visible = false;
            // 
            // openFileBol
            // 
            this.openFileBol.FileName = "openFileBol";
            // 
            // tabControl
            // 
            this.tabControl.AccessibleName = "Main";
            this.tabControl.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.tabControl.Controls.Add(this.tabMain);
            this.tabControl.Controls.Add(this.tabSettings);
            this.tabControl.Controls.Add(this.tabLogs);
            this.tabControl.Controls.Add(this.tabAbout);
            this.tabControl.Location = new System.Drawing.Point(12, 64);
            this.tabControl.Name = "tabControl";
            this.tabControl.SelectedIndex = 0;
            this.tabControl.Size = new System.Drawing.Size(527, 320);
            this.tabControl.TabIndex = 2;
            // 
            // tabMain
            // 
            this.tabMain.Controls.Add(this.grid_champions);
            this.tabMain.Controls.Add(this.cboChampionsList);
            this.tabMain.Location = new System.Drawing.Point(4, 22);
            this.tabMain.Name = "tabMain";
            this.tabMain.Padding = new System.Windows.Forms.Padding(3);
            this.tabMain.Size = new System.Drawing.Size(519, 294);
            this.tabMain.TabIndex = 0;
            this.tabMain.Text = "Main";
            this.tabMain.UseVisualStyleBackColor = true;
            // 
            // tabSettings
            // 
            this.tabSettings.Controls.Add(this.groupDebug);
            this.tabSettings.Controls.Add(this.groupBasic);
            this.tabSettings.Location = new System.Drawing.Point(4, 22);
            this.tabSettings.Name = "tabSettings";
            this.tabSettings.Padding = new System.Windows.Forms.Padding(3);
            this.tabSettings.Size = new System.Drawing.Size(519, 294);
            this.tabSettings.TabIndex = 2;
            this.tabSettings.Text = "Settings";
            this.tabSettings.UseVisualStyleBackColor = true;
            // 
            // groupDebug
            // 
            this.groupDebug.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.groupDebug.Controls.Add(this.cbReplaceScript);
            this.groupDebug.Controls.Add(this.cbDebug);
            this.groupDebug.Location = new System.Drawing.Point(308, 6);
            this.groupDebug.Name = "groupDebug";
            this.groupDebug.Size = new System.Drawing.Size(205, 156);
            this.groupDebug.TabIndex = 6;
            this.groupDebug.TabStop = false;
            this.groupDebug.Text = "Debug";
            // 
            // cbReplaceScript
            // 
            this.cbReplaceScript.AutoSize = true;
            this.cbReplaceScript.CheckAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.cbReplaceScript.Checked = true;
            this.cbReplaceScript.CheckState = System.Windows.Forms.CheckState.Checked;
            this.cbReplaceScript.Location = new System.Drawing.Point(57, 42);
            this.cbReplaceScript.Name = "cbReplaceScript";
            this.cbReplaceScript.Size = new System.Drawing.Size(126, 17);
            this.cbReplaceScript.TabIndex = 5;
            this.cbReplaceScript.Text = "Replace script if exist";
            this.cbReplaceScript.UseVisualStyleBackColor = true;
            this.cbReplaceScript.CheckedChanged += new System.EventHandler(this.cbReplaceScript_CheckedChanged);
            // 
            // cbDebug
            // 
            this.cbDebug.AutoSize = true;
            this.cbDebug.CheckAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.cbDebug.Checked = true;
            this.cbDebug.CheckState = System.Windows.Forms.CheckState.Checked;
            this.cbDebug.Location = new System.Drawing.Point(97, 19);
            this.cbDebug.Name = "cbDebug";
            this.cbDebug.Size = new System.Drawing.Size(86, 17);
            this.cbDebug.TabIndex = 4;
            this.cbDebug.Text = "Logs actions";
            this.cbDebug.UseVisualStyleBackColor = true;
            this.cbDebug.CheckedChanged += new System.EventHandler(this.cbDebug_CheckedChanged);
            // 
            // groupBasic
            // 
            this.groupBasic.Controls.Add(this.cbMoveScripts);
            this.groupBasic.Location = new System.Drawing.Point(6, 6);
            this.groupBasic.Name = "groupBasic";
            this.groupBasic.Size = new System.Drawing.Size(211, 156);
            this.groupBasic.TabIndex = 5;
            this.groupBasic.TabStop = false;
            this.groupBasic.Text = "Basics";
            // 
            // cbMoveScripts
            // 
            this.cbMoveScripts.AutoSize = true;
            this.cbMoveScripts.Checked = true;
            this.cbMoveScripts.CheckState = System.Windows.Forms.CheckState.Checked;
            this.cbMoveScripts.Location = new System.Drawing.Point(6, 19);
            this.cbMoveScripts.Name = "cbMoveScripts";
            this.cbMoveScripts.Size = new System.Drawing.Size(171, 17);
            this.cbMoveScripts.TabIndex = 3;
            this.cbMoveScripts.Text = "Move script when downloaded";
            this.cbMoveScripts.UseVisualStyleBackColor = true;
            this.cbMoveScripts.CheckedChanged += new System.EventHandler(this.cbMoveScripts_CheckedChanged);
            // 
            // tabLogs
            // 
            this.tabLogs.AutoScroll = true;
            this.tabLogs.Controls.Add(this.tbLogs);
            this.tabLogs.Location = new System.Drawing.Point(4, 22);
            this.tabLogs.Name = "tabLogs";
            this.tabLogs.Padding = new System.Windows.Forms.Padding(3);
            this.tabLogs.Size = new System.Drawing.Size(519, 294);
            this.tabLogs.TabIndex = 1;
            this.tabLogs.Text = "Logs";
            this.tabLogs.UseVisualStyleBackColor = true;
            // 
            // tbLogs
            // 
            this.tbLogs.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.tbLogs.Location = new System.Drawing.Point(6, 6);
            this.tbLogs.Multiline = true;
            this.tbLogs.Name = "tbLogs";
            this.tbLogs.ReadOnly = true;
            this.tbLogs.Size = new System.Drawing.Size(507, 282);
            this.tbLogs.TabIndex = 0;
            // 
            // tabAbout
            // 
            this.tabAbout.Controls.Add(this.lAuthor);
            this.tabAbout.Location = new System.Drawing.Point(4, 22);
            this.tabAbout.Name = "tabAbout";
            this.tabAbout.Size = new System.Drawing.Size(519, 294);
            this.tabAbout.TabIndex = 3;
            this.tabAbout.Text = "About";
            this.tabAbout.UseVisualStyleBackColor = true;
            // 
            // lAuthor
            // 
            this.lAuthor.AutoSize = true;
            this.lAuthor.Font = new System.Drawing.Font("Microsoft Sans Serif", 30F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lAuthor.Location = new System.Drawing.Point(148, 248);
            this.lAuthor.Name = "lAuthor";
            this.lAuthor.Size = new System.Drawing.Size(214, 46);
            this.lAuthor.TabIndex = 0;
            this.lAuthor.Text = "FreakyBart";
            // 
            // ScriptManagerForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(551, 396);
            this.Controls.Add(this.tabControl);
            this.Name = "ScriptManagerForm";
            this.Text = "Script Manager";
            this.Load += new System.EventHandler(this.Form1_Load);
            this.Leave += new System.EventHandler(this.ScriptManagerForm_Leave);
            ((System.ComponentModel.ISupportInitialize)(this.grid_champions)).EndInit();
            this.tabControl.ResumeLayout(false);
            this.tabMain.ResumeLayout(false);
            this.tabSettings.ResumeLayout(false);
            this.groupDebug.ResumeLayout(false);
            this.groupDebug.PerformLayout();
            this.groupBasic.ResumeLayout(false);
            this.groupBasic.PerformLayout();
            this.tabLogs.ResumeLayout(false);
            this.tabLogs.PerformLayout();
            this.tabAbout.ResumeLayout(false);
            this.tabAbout.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.ComboBox cboChampionsList;
        private System.Windows.Forms.DataGridView grid_champions;
        private System.Windows.Forms.DataGridViewTextBoxColumn Title;
        private System.Windows.Forms.DataGridViewTextBoxColumn Author;
        private System.Windows.Forms.DataGridViewLinkColumn ForumThread;
        private System.Windows.Forms.DataGridViewButtonColumn Download;
        private System.Windows.Forms.DataGridViewTextBoxColumn DownloadUrl;
        private System.Windows.Forms.OpenFileDialog openFileBol;
        private System.Windows.Forms.TabControl tabControl;
        private System.Windows.Forms.TabPage tabMain;
        private System.Windows.Forms.TabPage tabLogs;
        private System.Windows.Forms.TextBox tbLogs;
        private System.Windows.Forms.TabPage tabSettings;
        private System.Windows.Forms.CheckBox cbDebug;
        private System.Windows.Forms.CheckBox cbMoveScripts;
        private System.Windows.Forms.GroupBox groupDebug;
        private System.Windows.Forms.GroupBox groupBasic;
        private System.Windows.Forms.CheckBox cbReplaceScript;
        private System.Windows.Forms.TabPage tabAbout;
        private System.Windows.Forms.Label lAuthor;
    }
}

