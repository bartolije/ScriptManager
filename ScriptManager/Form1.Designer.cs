namespace Scriptmanager
{
    partial class Form1
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
            ((System.ComponentModel.ISupportInitialize)(this.grid_champions)).BeginInit();
            this.SuspendLayout();
            // 
            // cboChampionsList
            // 
            this.cboChampionsList.FormattingEnabled = true;
            this.cboChampionsList.Location = new System.Drawing.Point(28, 28);
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
            this.grid_champions.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.grid_champions.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.grid_champions.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.Title,
            this.Author,
            this.ForumThread,
            this.Download,
            this.DownloadUrl});
            this.grid_champions.Location = new System.Drawing.Point(12, 116);
            this.grid_champions.Name = "grid_champions";
            this.grid_champions.ReadOnly = true;
            this.grid_champions.Size = new System.Drawing.Size(506, 268);
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
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(530, 396);
            this.Controls.Add(this.grid_champions);
            this.Controls.Add(this.cboChampionsList);
            this.Name = "Form1";
            this.Text = "Form1";
            this.Load += new System.EventHandler(this.Form1_Load);
            ((System.ComponentModel.ISupportInitialize)(this.grid_champions)).EndInit();
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
    }
}

