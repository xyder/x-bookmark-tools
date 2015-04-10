namespace XDBookmarkTools.Views
{
    partial class MainView
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
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(MainView));
            this.OpenDataBaseButton = new System.Windows.Forms.Button();
            this.logBox = new System.Windows.Forms.RichTextBox();
            this.buttonFindDupes = new System.Windows.Forms.Button();
            this.splitContainer1 = new System.Windows.Forms.SplitContainer();
            this.mainTreeView = new BrightIdeasSoftware.TreeListView();
            this.titleColMTV = ((BrightIdeasSoftware.OLVColumn)(new BrightIdeasSoftware.OLVColumn()));
            this.mainSmallImageList = new System.Windows.Forms.ImageList(this.components);
            this.editContainerPanel = new System.Windows.Forms.Panel();
            this.mainContentsView = new BrightIdeasSoftware.FastObjectListView();
            this.titleColMCV = ((BrightIdeasSoftware.OLVColumn)(new BrightIdeasSoftware.OLVColumn()));
            this.locationColMCV = ((BrightIdeasSoftware.OLVColumn)(new BrightIdeasSoftware.OLVColumn()));
            this.databaseLabel = new System.Windows.Forms.Label();
            this.textBox1 = new System.Windows.Forms.TextBox();
            this.infoLabel = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).BeginInit();
            this.splitContainer1.Panel1.SuspendLayout();
            this.splitContainer1.Panel2.SuspendLayout();
            this.splitContainer1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.mainTreeView)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.mainContentsView)).BeginInit();
            this.SuspendLayout();
            // 
            // OpenDataBaseButton
            // 
            this.OpenDataBaseButton.AutoSize = true;
            this.OpenDataBaseButton.Location = new System.Drawing.Point(367, 12);
            this.OpenDataBaseButton.Name = "OpenDataBaseButton";
            this.OpenDataBaseButton.Size = new System.Drawing.Size(26, 23);
            this.OpenDataBaseButton.TabIndex = 0;
            this.OpenDataBaseButton.Text = "...";
            this.OpenDataBaseButton.UseVisualStyleBackColor = true;
            this.OpenDataBaseButton.Click += new System.EventHandler(this.OpenDataBaseButton_Click);
            // 
            // logBox
            // 
            this.logBox.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.logBox.Location = new System.Drawing.Point(12, 532);
            this.logBox.Name = "logBox";
            this.logBox.Size = new System.Drawing.Size(772, 96);
            this.logBox.TabIndex = 2;
            this.logBox.Text = "";
            // 
            // buttonFindDupes
            // 
            this.buttonFindDupes.Location = new System.Drawing.Point(12, 63);
            this.buttonFindDupes.Name = "buttonFindDupes";
            this.buttonFindDupes.Size = new System.Drawing.Size(88, 23);
            this.buttonFindDupes.TabIndex = 3;
            this.buttonFindDupes.Text = "Find Duplicates";
            this.buttonFindDupes.UseVisualStyleBackColor = true;
            this.buttonFindDupes.Click += new System.EventHandler(this.ButtonFindDupes_Click);
            // 
            // splitContainer1
            // 
            this.splitContainer1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.splitContainer1.Location = new System.Drawing.Point(12, 92);
            this.splitContainer1.Name = "splitContainer1";
            // 
            // splitContainer1.Panel1
            // 
            this.splitContainer1.Panel1.Controls.Add(this.mainTreeView);
            // 
            // splitContainer1.Panel2
            // 
            this.splitContainer1.Panel2.Controls.Add(this.editContainerPanel);
            this.splitContainer1.Panel2.Controls.Add(this.mainContentsView);
            this.splitContainer1.Size = new System.Drawing.Size(772, 434);
            this.splitContainer1.SplitterDistance = 248;
            this.splitContainer1.TabIndex = 5;
            // 
            // mainTreeView
            // 
            this.mainTreeView.AllColumns.Add(this.titleColMTV);
            this.mainTreeView.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.titleColMTV});
            this.mainTreeView.Cursor = System.Windows.Forms.Cursors.Default;
            this.mainTreeView.Dock = System.Windows.Forms.DockStyle.Fill;
            this.mainTreeView.FullRowSelect = true;
            this.mainTreeView.HideSelection = false;
            this.mainTreeView.Location = new System.Drawing.Point(0, 0);
            this.mainTreeView.MultiSelect = false;
            this.mainTreeView.Name = "mainTreeView";
            this.mainTreeView.OwnerDraw = true;
            this.mainTreeView.ShowGroups = false;
            this.mainTreeView.Size = new System.Drawing.Size(248, 434);
            this.mainTreeView.SmallImageList = this.mainSmallImageList;
            this.mainTreeView.TabIndex = 1;
            this.mainTreeView.UseCompatibleStateImageBehavior = false;
            this.mainTreeView.View = System.Windows.Forms.View.Details;
            this.mainTreeView.VirtualMode = true;
            this.mainTreeView.CellRightClick += new System.EventHandler<BrightIdeasSoftware.CellRightClickEventArgs>(this.mainTreeView_CellRightClick);
            this.mainTreeView.SelectionChanged += new System.EventHandler(this.mainTreeView_SelectionChanged);
            this.mainTreeView.ItemActivate += new System.EventHandler(this.mainTreeView_ItemActivate);
            this.mainTreeView.MouseDown += new System.Windows.Forms.MouseEventHandler(this.mainTreeView_MouseDown);
            // 
            // titleColMTV
            // 
            this.titleColMTV.AspectName = "Title";
            this.titleColMTV.FillsFreeSpace = true;
            this.titleColMTV.Text = "Title";
            // 
            // mainSmallImageList
            // 
            this.mainSmallImageList.ImageStream = ((System.Windows.Forms.ImageListStreamer)(resources.GetObject("mainSmallImageList.ImageStream")));
            this.mainSmallImageList.TransparentColor = System.Drawing.Color.Transparent;
            this.mainSmallImageList.Images.SetKeyName(0, "folder");
            this.mainSmallImageList.Images.SetKeyName(1, "bkm");
            // 
            // editContainerPanel
            // 
            this.editContainerPanel.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.editContainerPanel.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.editContainerPanel.Location = new System.Drawing.Point(0, 309);
            this.editContainerPanel.Margin = new System.Windows.Forms.Padding(0, 3, 0, 3);
            this.editContainerPanel.Name = "editContainerPanel";
            this.editContainerPanel.Size = new System.Drawing.Size(520, 125);
            this.editContainerPanel.TabIndex = 1;
            // 
            // mainContentsView
            // 
            this.mainContentsView.AllColumns.Add(this.titleColMCV);
            this.mainContentsView.AllColumns.Add(this.locationColMCV);
            this.mainContentsView.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.mainContentsView.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.titleColMCV,
            this.locationColMCV});
            this.mainContentsView.Cursor = System.Windows.Forms.Cursors.Default;
            this.mainContentsView.FullRowSelect = true;
            this.mainContentsView.HideSelection = false;
            this.mainContentsView.Location = new System.Drawing.Point(0, 0);
            this.mainContentsView.Margin = new System.Windows.Forms.Padding(0, 3, 0, 0);
            this.mainContentsView.Name = "mainContentsView";
            this.mainContentsView.ShowGroups = false;
            this.mainContentsView.Size = new System.Drawing.Size(520, 306);
            this.mainContentsView.SmallImageList = this.mainSmallImageList;
            this.mainContentsView.TabIndex = 0;
            this.mainContentsView.UseCompatibleStateImageBehavior = false;
            this.mainContentsView.UseHyperlinks = true;
            this.mainContentsView.View = System.Windows.Forms.View.Details;
            this.mainContentsView.VirtualMode = true;
            this.mainContentsView.CellRightClick += new System.EventHandler<BrightIdeasSoftware.CellRightClickEventArgs>(this.mainContentsView_CellRightClick);
            this.mainContentsView.SelectionChanged += new System.EventHandler(this.mainContentsView_SelectionChanged);
            this.mainContentsView.ItemActivate += new System.EventHandler(this.MainContentsView_ItemActivate);
            this.mainContentsView.MouseDown += new System.Windows.Forms.MouseEventHandler(this.mainContentsView_MouseDown);
            // 
            // titleColMCV
            // 
            this.titleColMCV.AspectName = "Title";
            this.titleColMCV.FillsFreeSpace = true;
            this.titleColMCV.Groupable = false;
            this.titleColMCV.Text = "Title";
            this.titleColMCV.Width = 112;
            // 
            // locationColMCV
            // 
            this.locationColMCV.AspectName = "Location";
            this.locationColMCV.FillsFreeSpace = true;
            this.locationColMCV.Hyperlink = true;
            this.locationColMCV.Text = "Location";
            // 
            // databaseLabel
            // 
            this.databaseLabel.AutoSize = true;
            this.databaseLabel.Location = new System.Drawing.Point(12, 17);
            this.databaseLabel.Name = "databaseLabel";
            this.databaseLabel.Size = new System.Drawing.Size(56, 13);
            this.databaseLabel.TabIndex = 6;
            this.databaseLabel.Text = "Database:";
            // 
            // textBox1
            // 
            this.textBox1.Location = new System.Drawing.Point(74, 14);
            this.textBox1.Name = "textBox1";
            this.textBox1.Size = new System.Drawing.Size(287, 20);
            this.textBox1.TabIndex = 7;
            // 
            // infoLabel
            // 
            this.infoLabel.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.infoLabel.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.infoLabel.Location = new System.Drawing.Point(399, 12);
            this.infoLabel.Name = "infoLabel";
            this.infoLabel.Padding = new System.Windows.Forms.Padding(2);
            this.infoLabel.Size = new System.Drawing.Size(385, 74);
            this.infoLabel.TabIndex = 8;
            this.infoLabel.Text = "label1";
            this.infoLabel.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // MainView
            // 
            this.AllowDrop = true;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(797, 640);
            this.Controls.Add(this.infoLabel);
            this.Controls.Add(this.textBox1);
            this.Controls.Add(this.databaseLabel);
            this.Controls.Add(this.splitContainer1);
            this.Controls.Add(this.buttonFindDupes);
            this.Controls.Add(this.logBox);
            this.Controls.Add(this.OpenDataBaseButton);
            this.Name = "MainView";
            this.Text = "Form1";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.MainView_FormClosing);
            this.DragDrop += new System.Windows.Forms.DragEventHandler(this.MainView_DragDrop);
            this.DragEnter += new System.Windows.Forms.DragEventHandler(this.MainView_DragEnter);
            this.splitContainer1.Panel1.ResumeLayout(false);
            this.splitContainer1.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).EndInit();
            this.splitContainer1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.mainTreeView)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.mainContentsView)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button OpenDataBaseButton;
        private BrightIdeasSoftware.TreeListView mainTreeView;
        private System.Windows.Forms.RichTextBox logBox;
        private System.Windows.Forms.Button buttonFindDupes;
        private System.Windows.Forms.SplitContainer splitContainer1;
        private BrightIdeasSoftware.FastObjectListView mainContentsView;
        private BrightIdeasSoftware.OLVColumn titleColMTV;
        private BrightIdeasSoftware.OLVColumn titleColMCV;
        private BrightIdeasSoftware.OLVColumn locationColMCV;
        private System.Windows.Forms.ImageList mainSmallImageList;
        private System.Windows.Forms.Panel editContainerPanel;
        private System.Windows.Forms.Label databaseLabel;
        private System.Windows.Forms.TextBox textBox1;
        private System.Windows.Forms.Label infoLabel;
    }
}

