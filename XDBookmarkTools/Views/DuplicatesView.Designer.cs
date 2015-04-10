namespace XDBookmarkTools.Views
{
    partial class DuplicatesView
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
            this.checkBox1 = new System.Windows.Forms.CheckBox();
            this.checkBox2 = new System.Windows.Forms.CheckBox();
            this.mainTreeView = new BrightIdeasSoftware.TreeListView();
            this.titleColMTV = ((BrightIdeasSoftware.OLVColumn)(new BrightIdeasSoftware.OLVColumn()));
            this.dupeNrColMTV = ((BrightIdeasSoftware.OLVColumn)(new BrightIdeasSoftware.OLVColumn()));
            this.olvColumn2 = ((BrightIdeasSoftware.OLVColumn)(new BrightIdeasSoftware.OLVColumn()));
            this.olvColumn1 = ((BrightIdeasSoftware.OLVColumn)(new BrightIdeasSoftware.OLVColumn()));
            this.checkBox3 = new System.Windows.Forms.CheckBox();
            this.textBox1 = new System.Windows.Forms.TextBox();
            this.button1 = new System.Windows.Forms.Button();
            this.button2 = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.mainTreeView)).BeginInit();
            this.SuspendLayout();
            // 
            // checkBox1
            // 
            this.checkBox1.AutoSize = true;
            this.checkBox1.Location = new System.Drawing.Point(12, 35);
            this.checkBox1.Name = "checkBox1";
            this.checkBox1.Size = new System.Drawing.Size(177, 17);
            this.checkBox1.TabIndex = 0;
            this.checkBox1.Text = "Ignore protocol (http, https, etc).";
            this.checkBox1.UseVisualStyleBackColor = true;
            // 
            // checkBox2
            // 
            this.checkBox2.AutoSize = true;
            this.checkBox2.Location = new System.Drawing.Point(12, 58);
            this.checkBox2.Name = "checkBox2";
            this.checkBox2.Size = new System.Drawing.Size(280, 17);
            this.checkBox2.TabIndex = 1;
            this.checkBox2.Text = "Ignore GET parameters (everything after the \"?\" sign).";
            this.checkBox2.UseVisualStyleBackColor = true;
            // 
            // mainTreeView
            // 
            this.mainTreeView.AllColumns.Add(this.titleColMTV);
            this.mainTreeView.AllColumns.Add(this.dupeNrColMTV);
            this.mainTreeView.AllColumns.Add(this.olvColumn2);
            this.mainTreeView.AllColumns.Add(this.olvColumn1);
            this.mainTreeView.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.mainTreeView.CheckBoxes = true;
            this.mainTreeView.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.titleColMTV,
            this.dupeNrColMTV,
            this.olvColumn2,
            this.olvColumn1});
            this.mainTreeView.Cursor = System.Windows.Forms.Cursors.Default;
            this.mainTreeView.HierarchicalCheckboxes = true;
            this.mainTreeView.Location = new System.Drawing.Point(12, 102);
            this.mainTreeView.Name = "mainTreeView";
            this.mainTreeView.OwnerDraw = true;
            this.mainTreeView.ShowGroups = false;
            this.mainTreeView.ShowImagesOnSubItems = true;
            this.mainTreeView.Size = new System.Drawing.Size(696, 451);
            this.mainTreeView.TabIndex = 2;
            this.mainTreeView.UseCompatibleStateImageBehavior = false;
            this.mainTreeView.UseHyperlinks = true;
            this.mainTreeView.View = System.Windows.Forms.View.Details;
            this.mainTreeView.VirtualMode = true;
            this.mainTreeView.HyperlinkClicked += new System.EventHandler<BrightIdeasSoftware.HyperlinkClickedEventArgs>(this.mainTreeView_HyperlinkClicked);
            this.mainTreeView.ItemActivate += new System.EventHandler(this.mainTreeView_ItemActivate);
            // 
            // titleColMTV
            // 
            this.titleColMTV.AspectName = "Title";
            this.titleColMTV.FillsFreeSpace = true;
            this.titleColMTV.Text = "Title";
            // 
            // dupeNrColMTV
            // 
            this.dupeNrColMTV.AspectName = "DupeNr";
            this.dupeNrColMTV.Text = "Duplicates";
            this.dupeNrColMTV.Width = 69;
            // 
            // olvColumn2
            // 
            this.olvColumn2.AspectName = "PathString";
            this.olvColumn2.FillsFreeSpace = true;
            this.olvColumn2.Hyperlink = true;
            this.olvColumn2.Text = "Path";
            // 
            // olvColumn1
            // 
            this.olvColumn1.AspectName = "Id";
            // 
            // checkBox3
            // 
            this.checkBox3.AutoSize = true;
            this.checkBox3.Location = new System.Drawing.Point(12, 12);
            this.checkBox3.Name = "checkBox3";
            this.checkBox3.Size = new System.Drawing.Size(155, 17);
            this.checkBox3.TabIndex = 3;
            this.checkBox3.Text = "Restrict search to directory:";
            this.checkBox3.UseVisualStyleBackColor = true;
            // 
            // textBox1
            // 
            this.textBox1.Location = new System.Drawing.Point(184, 9);
            this.textBox1.Name = "textBox1";
            this.textBox1.Size = new System.Drawing.Size(282, 20);
            this.textBox1.TabIndex = 4;
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(472, 7);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(24, 24);
            this.button1.TabIndex = 5;
            this.button1.Text = "...";
            this.button1.UseVisualStyleBackColor = true;
            // 
            // button2
            // 
            this.button2.Location = new System.Drawing.Point(672, 73);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(36, 23);
            this.button2.TabIndex = 6;
            this.button2.Text = "GO!";
            this.button2.UseVisualStyleBackColor = true;
            // 
            // DuplicatesView
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(720, 565);
            this.Controls.Add(this.button2);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.textBox1);
            this.Controls.Add(this.checkBox3);
            this.Controls.Add(this.mainTreeView);
            this.Controls.Add(this.checkBox2);
            this.Controls.Add(this.checkBox1);
            this.Name = "DuplicatesView";
            this.Text = "DuplicatesView";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.DuplicatesView_FormClosing);
            ((System.ComponentModel.ISupportInitialize)(this.mainTreeView)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.CheckBox checkBox1;
        private System.Windows.Forms.CheckBox checkBox2;
        public BrightIdeasSoftware.TreeListView mainTreeView;
        private System.Windows.Forms.CheckBox checkBox3;
        private System.Windows.Forms.TextBox textBox1;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.Button button2;
        public BrightIdeasSoftware.OLVColumn titleColMTV;
        private BrightIdeasSoftware.OLVColumn olvColumn2;
        public BrightIdeasSoftware.OLVColumn dupeNrColMTV;
        private BrightIdeasSoftware.OLVColumn olvColumn1;
    }
}