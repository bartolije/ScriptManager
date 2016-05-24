namespace ScriptManager
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(ScriptManagerForm));
            this.cboChampionsList = new System.Windows.Forms.ComboBox();
            this.grid_champions = new System.Windows.Forms.DataGridView();
            this.openFileBol = new System.Windows.Forms.OpenFileDialog();
            this.tabControl = new System.Windows.Forms.TabControl();
            this.tabMain = new System.Windows.Forms.TabPage();
            this.lDownloadDetails = new System.Windows.Forms.Label();
            this.lDownload = new System.Windows.Forms.Label();
            this.downloadBar = new System.Windows.Forms.ProgressBar();
            this.cboCategoryList = new System.Windows.Forms.ComboBox();
            this.tabManage = new System.Windows.Forms.TabPage();
            this.btnMoveToLoaded = new System.Windows.Forms.Button();
            this.btnMoveToNotLoaded = new System.Windows.Forms.Button();
            this.listScriptsNotLoaded = new System.Windows.Forms.ListBox();
            this.listScriptsLoaded = new System.Windows.Forms.ListBox();
            this.tabSettings = new System.Windows.Forms.TabPage();
            this.groupLanguage = new System.Windows.Forms.GroupBox();
            this.lblLanguage = new System.Windows.Forms.Label();
            this.cboLanguage = new System.Windows.Forms.ComboBox();
            this.groupDebug = new System.Windows.Forms.GroupBox();
            this.cbReplaceScript = new System.Windows.Forms.CheckBox();
            this.cbDebug = new System.Windows.Forms.CheckBox();
            this.groupBasic = new System.Windows.Forms.GroupBox();
            this.cbAddAutoupdate = new System.Windows.Forms.CheckBox();
            this.cbMoveScripts = new System.Windows.Forms.CheckBox();
            this.tabLogs = new System.Windows.Forms.TabPage();
            this.tbLogs = new System.Windows.Forms.TextBox();
            this.tabAbout = new System.Windows.Forms.TabPage();
            this.lAuthor = new System.Windows.Forms.Label();
            this.Title = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.PaidScript = new System.Windows.Forms.DataGridViewCheckBoxColumn();
            this.Author = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.SujetForum = new System.Windows.Forms.DataGridViewLinkColumn();
            this.Download = new System.Windows.Forms.DataGridViewButtonColumn();
            this.DownloadUrl = new System.Windows.Forms.DataGridViewTextBoxColumn();
            ((System.ComponentModel.ISupportInitialize)(this.grid_champions)).BeginInit();
            this.tabControl.SuspendLayout();
            this.tabMain.SuspendLayout();
            this.tabManage.SuspendLayout();
            this.tabSettings.SuspendLayout();
            this.groupLanguage.SuspendLayout();
            this.groupDebug.SuspendLayout();
            this.groupBasic.SuspendLayout();
            this.tabLogs.SuspendLayout();
            this.tabAbout.SuspendLayout();
            this.SuspendLayout();
            // 
            // cboChampionsList
            // 
            resources.ApplyResources(this.cboChampionsList, "cboChampionsList");
            this.cboChampionsList.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.SuggestAppend;
            this.cboChampionsList.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems;
            this.cboChampionsList.FormattingEnabled = true;
            this.cboChampionsList.Name = "cboChampionsList";
            this.cboChampionsList.SelectedIndexChanged += new System.EventHandler(this.cboChampionsList_SelectedIndexChanged);
            // 
            // grid_champions
            // 
            resources.ApplyResources(this.grid_champions, "grid_champions");
            this.grid_champions.AllowUserToAddRows = false;
            this.grid_champions.AllowUserToDeleteRows = false;
            this.grid_champions.AllowUserToOrderColumns = true;
            this.grid_champions.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.grid_champions.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.grid_champions.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.Title,
            this.PaidScript,
            this.Author,
            this.SujetForum,
            this.Download,
            this.DownloadUrl});
            this.grid_champions.Name = "grid_champions";
            this.grid_champions.ReadOnly = true;
            this.grid_champions.CellClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.grid_champions_CellClick);
            // 
            // openFileBol
            // 
            this.openFileBol.FileName = "openFileBol";
            resources.ApplyResources(this.openFileBol, "openFileBol");
            // 
            // tabControl
            // 
            resources.ApplyResources(this.tabControl, "tabControl");
            this.tabControl.Controls.Add(this.tabMain);
            this.tabControl.Controls.Add(this.tabManage);
            this.tabControl.Controls.Add(this.tabSettings);
            this.tabControl.Controls.Add(this.tabLogs);
            this.tabControl.Controls.Add(this.tabAbout);
            this.tabControl.Name = "tabControl";
            this.tabControl.SelectedIndex = 0;
            // 
            // tabMain
            // 
            resources.ApplyResources(this.tabMain, "tabMain");
            this.tabMain.Controls.Add(this.lDownloadDetails);
            this.tabMain.Controls.Add(this.lDownload);
            this.tabMain.Controls.Add(this.downloadBar);
            this.tabMain.Controls.Add(this.cboCategoryList);
            this.tabMain.Controls.Add(this.grid_champions);
            this.tabMain.Controls.Add(this.cboChampionsList);
            this.tabMain.Name = "tabMain";
            this.tabMain.UseVisualStyleBackColor = true;
            // 
            // lDownloadDetails
            // 
            resources.ApplyResources(this.lDownloadDetails, "lDownloadDetails");
            this.lDownloadDetails.Name = "lDownloadDetails";
            // 
            // lDownload
            // 
            resources.ApplyResources(this.lDownload, "lDownload");
            this.lDownload.Name = "lDownload";
            // 
            // downloadBar
            // 
            resources.ApplyResources(this.downloadBar, "downloadBar");
            this.downloadBar.Name = "downloadBar";
            // 
            // cboCategoryList
            // 
            resources.ApplyResources(this.cboCategoryList, "cboCategoryList");
            this.cboCategoryList.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.SuggestAppend;
            this.cboCategoryList.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems;
            this.cboCategoryList.FormattingEnabled = true;
            this.cboCategoryList.Name = "cboCategoryList";
            this.cboCategoryList.SelectedIndexChanged += new System.EventHandler(this.cboCategoryList_SelectedIndexChanged);
            // 
            // tabManage
            // 
            resources.ApplyResources(this.tabManage, "tabManage");
            this.tabManage.Controls.Add(this.btnMoveToLoaded);
            this.tabManage.Controls.Add(this.btnMoveToNotLoaded);
            this.tabManage.Controls.Add(this.listScriptsNotLoaded);
            this.tabManage.Controls.Add(this.listScriptsLoaded);
            this.tabManage.Name = "tabManage";
            this.tabManage.UseVisualStyleBackColor = true;
            // 
            // btnMoveToLoaded
            // 
            resources.ApplyResources(this.btnMoveToLoaded, "btnMoveToLoaded");
            this.btnMoveToLoaded.Name = "btnMoveToLoaded";
            this.btnMoveToLoaded.UseVisualStyleBackColor = true;
            this.btnMoveToLoaded.Click += new System.EventHandler(this.btnMoveToLoaded_Click);
            // 
            // btnMoveToNotLoaded
            // 
            resources.ApplyResources(this.btnMoveToNotLoaded, "btnMoveToNotLoaded");
            this.btnMoveToNotLoaded.Name = "btnMoveToNotLoaded";
            this.btnMoveToNotLoaded.UseVisualStyleBackColor = true;
            this.btnMoveToNotLoaded.Click += new System.EventHandler(this.btnMoveToNotLoaded_Click);
            // 
            // listScriptsNotLoaded
            // 
            resources.ApplyResources(this.listScriptsNotLoaded, "listScriptsNotLoaded");
            this.listScriptsNotLoaded.FormattingEnabled = true;
            this.listScriptsNotLoaded.Name = "listScriptsNotLoaded";
            // 
            // listScriptsLoaded
            // 
            resources.ApplyResources(this.listScriptsLoaded, "listScriptsLoaded");
            this.listScriptsLoaded.FormattingEnabled = true;
            this.listScriptsLoaded.Name = "listScriptsLoaded";
            // 
            // tabSettings
            // 
            resources.ApplyResources(this.tabSettings, "tabSettings");
            this.tabSettings.Controls.Add(this.groupLanguage);
            this.tabSettings.Controls.Add(this.groupDebug);
            this.tabSettings.Controls.Add(this.groupBasic);
            this.tabSettings.Name = "tabSettings";
            this.tabSettings.UseVisualStyleBackColor = true;
            // 
            // groupLanguage
            // 
            resources.ApplyResources(this.groupLanguage, "groupLanguage");
            this.groupLanguage.Controls.Add(this.lblLanguage);
            this.groupLanguage.Controls.Add(this.cboLanguage);
            this.groupLanguage.Name = "groupLanguage";
            this.groupLanguage.TabStop = false;
            // 
            // lblLanguage
            // 
            resources.ApplyResources(this.lblLanguage, "lblLanguage");
            this.lblLanguage.Name = "lblLanguage";
            // 
            // cboLanguage
            // 
            resources.ApplyResources(this.cboLanguage, "cboLanguage");
            this.cboLanguage.FormattingEnabled = true;
            this.cboLanguage.Name = "cboLanguage";
            this.cboLanguage.SelectedIndexChanged += new System.EventHandler(this.cboLanguage_SelectedIndexChanged);
            // 
            // groupDebug
            // 
            resources.ApplyResources(this.groupDebug, "groupDebug");
            this.groupDebug.Controls.Add(this.cbReplaceScript);
            this.groupDebug.Controls.Add(this.cbDebug);
            this.groupDebug.Name = "groupDebug";
            this.groupDebug.TabStop = false;
            // 
            // cbReplaceScript
            // 
            resources.ApplyResources(this.cbReplaceScript, "cbReplaceScript");
            this.cbReplaceScript.Checked = true;
            this.cbReplaceScript.CheckState = System.Windows.Forms.CheckState.Checked;
            this.cbReplaceScript.Name = "cbReplaceScript";
            this.cbReplaceScript.UseVisualStyleBackColor = true;
            this.cbReplaceScript.CheckedChanged += new System.EventHandler(this.cbReplaceScript_CheckedChanged);
            // 
            // cbDebug
            // 
            resources.ApplyResources(this.cbDebug, "cbDebug");
            this.cbDebug.Checked = true;
            this.cbDebug.CheckState = System.Windows.Forms.CheckState.Checked;
            this.cbDebug.Name = "cbDebug";
            this.cbDebug.UseVisualStyleBackColor = true;
            this.cbDebug.CheckedChanged += new System.EventHandler(this.cbDebug_CheckedChanged);
            // 
            // groupBasic
            // 
            resources.ApplyResources(this.groupBasic, "groupBasic");
            this.groupBasic.Controls.Add(this.cbAddAutoupdate);
            this.groupBasic.Controls.Add(this.cbMoveScripts);
            this.groupBasic.Name = "groupBasic";
            this.groupBasic.TabStop = false;
            // 
            // cbAddAutoupdate
            // 
            resources.ApplyResources(this.cbAddAutoupdate, "cbAddAutoupdate");
            this.cbAddAutoupdate.Checked = true;
            this.cbAddAutoupdate.CheckState = System.Windows.Forms.CheckState.Checked;
            this.cbAddAutoupdate.Name = "cbAddAutoupdate";
            this.cbAddAutoupdate.UseVisualStyleBackColor = true;
            // 
            // cbMoveScripts
            // 
            resources.ApplyResources(this.cbMoveScripts, "cbMoveScripts");
            this.cbMoveScripts.Checked = true;
            this.cbMoveScripts.CheckState = System.Windows.Forms.CheckState.Checked;
            this.cbMoveScripts.Name = "cbMoveScripts";
            this.cbMoveScripts.UseVisualStyleBackColor = true;
            this.cbMoveScripts.CheckedChanged += new System.EventHandler(this.cbMoveScripts_CheckedChanged);
            // 
            // tabLogs
            // 
            resources.ApplyResources(this.tabLogs, "tabLogs");
            this.tabLogs.Controls.Add(this.tbLogs);
            this.tabLogs.Name = "tabLogs";
            this.tabLogs.UseVisualStyleBackColor = true;
            // 
            // tbLogs
            // 
            resources.ApplyResources(this.tbLogs, "tbLogs");
            this.tbLogs.Name = "tbLogs";
            this.tbLogs.ReadOnly = true;
            // 
            // tabAbout
            // 
            resources.ApplyResources(this.tabAbout, "tabAbout");
            this.tabAbout.Controls.Add(this.lAuthor);
            this.tabAbout.Name = "tabAbout";
            this.tabAbout.UseVisualStyleBackColor = true;
            // 
            // lAuthor
            // 
            resources.ApplyResources(this.lAuthor, "lAuthor");
            this.lAuthor.Name = "lAuthor";
            // 
            // Title
            // 
            resources.ApplyResources(this.Title, "Title");
            this.Title.Name = "Title";
            this.Title.ReadOnly = true;
            // 
            // PaidScript
            // 
            resources.ApplyResources(this.PaidScript, "PaidScript");
            this.PaidScript.Name = "PaidScript";
            this.PaidScript.ReadOnly = true;
            // 
            // Author
            // 
            resources.ApplyResources(this.Author, "Author");
            this.Author.Name = "Author";
            this.Author.ReadOnly = true;
            // 
            // SujetForum
            // 
            resources.ApplyResources(this.SujetForum, "SujetForum");
            this.SujetForum.Name = "SujetForum";
            this.SujetForum.ReadOnly = true;
            this.SujetForum.Resizable = System.Windows.Forms.DataGridViewTriState.True;
            this.SujetForum.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.Automatic;
            this.SujetForum.Text = "Forum Thread";
            // 
            // Download
            // 
            resources.ApplyResources(this.Download, "Download");
            this.Download.Name = "Download";
            this.Download.ReadOnly = true;
            this.Download.Resizable = System.Windows.Forms.DataGridViewTriState.False;
            this.Download.Text = "Download";
            this.Download.UseColumnTextForButtonValue = true;
            // 
            // DownloadUrl
            // 
            resources.ApplyResources(this.DownloadUrl, "DownloadUrl");
            this.DownloadUrl.Name = "DownloadUrl";
            this.DownloadUrl.ReadOnly = true;
            // 
            // ScriptManagerForm
            // 
            resources.ApplyResources(this, "$this");
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.tabControl);
            this.Name = "ScriptManagerForm";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.ScriptManagerForm_FormClosing);
            this.Load += new System.EventHandler(this.Form1_Load);
            this.Leave += new System.EventHandler(this.ScriptManagerForm_Leave);
            ((System.ComponentModel.ISupportInitialize)(this.grid_champions)).EndInit();
            this.tabControl.ResumeLayout(false);
            this.tabMain.ResumeLayout(false);
            this.tabMain.PerformLayout();
            this.tabManage.ResumeLayout(false);
            this.tabSettings.ResumeLayout(false);
            this.groupLanguage.ResumeLayout(false);
            this.groupLanguage.PerformLayout();
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
        private System.Windows.Forms.ComboBox cboCategoryList;
        private System.Windows.Forms.CheckBox cbAddAutoupdate;
        private System.Windows.Forms.ProgressBar downloadBar;
        private System.Windows.Forms.Label lDownload;
        private System.Windows.Forms.Label lDownloadDetails;
        private System.Windows.Forms.GroupBox groupLanguage;
        private System.Windows.Forms.Label lblLanguage;
        private System.Windows.Forms.ComboBox cboLanguage;
        private System.Windows.Forms.TabPage tabManage;
        private System.Windows.Forms.ListBox listScriptsNotLoaded;
        private System.Windows.Forms.ListBox listScriptsLoaded;
        private System.Windows.Forms.Button btnMoveToLoaded;
        private System.Windows.Forms.Button btnMoveToNotLoaded;
        private System.Windows.Forms.DataGridViewTextBoxColumn Title;
        private System.Windows.Forms.DataGridViewCheckBoxColumn PaidScript;
        private System.Windows.Forms.DataGridViewTextBoxColumn Author;
        private System.Windows.Forms.DataGridViewLinkColumn SujetForum;
        private System.Windows.Forms.DataGridViewButtonColumn Download;
        private System.Windows.Forms.DataGridViewTextBoxColumn DownloadUrl;
    }
}

