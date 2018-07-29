namespace OSFBusinessSolutionsTest
{
    partial class OSFBusinessSolutionsTest
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(OSFBusinessSolutionsTest));
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.fileToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.openToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.saveToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this.exitToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.helpToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.aboutToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.lvFilteredList = new System.Windows.Forms.ListView();
            this.chType = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.chName = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.chTitle = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.chDepartment = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.gbSort = new System.Windows.Forms.GroupBox();
            this.rbTitle = new System.Windows.Forms.RadioButton();
            this.rbName = new System.Windows.Forms.RadioButton();
            this.gbDisplay = new System.Windows.Forms.GroupBox();
            this.rbMarketing = new System.Windows.Forms.RadioButton();
            this.rbIT = new System.Windows.Forms.RadioButton();
            this.rbBoxOffice = new System.Windows.Forms.RadioButton();
            this.rbAdmin = new System.Windows.Forms.RadioButton();
            this.rbAll = new System.Windows.Forms.RadioButton();
            this.menuStrip1.SuspendLayout();
            this.gbSort.SuspendLayout();
            this.gbDisplay.SuspendLayout();
            this.SuspendLayout();
            // 
            // menuStrip1
            // 
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.fileToolStripMenuItem,
            this.helpToolStripMenuItem});
            this.menuStrip1.Location = new System.Drawing.Point(0, 0);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.Size = new System.Drawing.Size(578, 24);
            this.menuStrip1.TabIndex = 0;
            this.menuStrip1.Text = "menuStrip1";
            // 
            // fileToolStripMenuItem
            // 
            this.fileToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.openToolStripMenuItem,
            this.saveToolStripMenuItem,
            this.toolStripSeparator1,
            this.exitToolStripMenuItem});
            this.fileToolStripMenuItem.Name = "fileToolStripMenuItem";
            this.fileToolStripMenuItem.Size = new System.Drawing.Size(37, 20);
            this.fileToolStripMenuItem.Text = "File";
            // 
            // openToolStripMenuItem
            // 
            this.openToolStripMenuItem.Name = "openToolStripMenuItem";
            this.openToolStripMenuItem.Size = new System.Drawing.Size(152, 22);
            this.openToolStripMenuItem.Text = "Open";
            this.openToolStripMenuItem.Click += new System.EventHandler(this.openToolStripMenuItem_Click);
            // 
            // saveToolStripMenuItem
            // 
            this.saveToolStripMenuItem.Name = "saveToolStripMenuItem";
            this.saveToolStripMenuItem.Size = new System.Drawing.Size(152, 22);
            this.saveToolStripMenuItem.Text = "Save As";
            this.saveToolStripMenuItem.Click += new System.EventHandler(this.saveToolStripMenuItem_Click);
            // 
            // toolStripSeparator1
            // 
            this.toolStripSeparator1.Name = "toolStripSeparator1";
            this.toolStripSeparator1.Size = new System.Drawing.Size(149, 6);
            // 
            // exitToolStripMenuItem
            // 
            this.exitToolStripMenuItem.Name = "exitToolStripMenuItem";
            this.exitToolStripMenuItem.Size = new System.Drawing.Size(152, 22);
            this.exitToolStripMenuItem.Text = "Exit";
            this.exitToolStripMenuItem.Click += new System.EventHandler(this.exitToolStripMenuItem_Click);
            // 
            // helpToolStripMenuItem
            // 
            this.helpToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.aboutToolStripMenuItem});
            this.helpToolStripMenuItem.Name = "helpToolStripMenuItem";
            this.helpToolStripMenuItem.Size = new System.Drawing.Size(44, 20);
            this.helpToolStripMenuItem.Text = "Help";
            // 
            // aboutToolStripMenuItem
            // 
            this.aboutToolStripMenuItem.Name = "aboutToolStripMenuItem";
            this.aboutToolStripMenuItem.Size = new System.Drawing.Size(107, 22);
            this.aboutToolStripMenuItem.Text = "About";
            this.aboutToolStripMenuItem.Click += new System.EventHandler(this.aboutToolStripMenuItem_Click);
            // 
            // lvFilteredList
            // 
            this.lvFilteredList.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.lvFilteredList.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.chType,
            this.chName,
            this.chTitle,
            this.chDepartment});
            this.lvFilteredList.Location = new System.Drawing.Point(12, 89);
            this.lvFilteredList.Name = "lvFilteredList";
            this.lvFilteredList.Size = new System.Drawing.Size(554, 223);
            this.lvFilteredList.TabIndex = 1;
            this.lvFilteredList.UseCompatibleStateImageBehavior = false;
            this.lvFilteredList.View = System.Windows.Forms.View.Details;
            // 
            // chType
            // 
            this.chType.Text = "Type";
            // 
            // chName
            // 
            this.chName.Text = "Name";
            this.chName.Width = 120;
            // 
            // chTitle
            // 
            this.chTitle.Text = "Title";
            this.chTitle.Width = 120;
            // 
            // chDepartment
            // 
            this.chDepartment.Text = "Department";
            this.chDepartment.Width = 120;
            // 
            // gbSort
            // 
            this.gbSort.Controls.Add(this.rbTitle);
            this.gbSort.Controls.Add(this.rbName);
            this.gbSort.Location = new System.Drawing.Point(12, 27);
            this.gbSort.Name = "gbSort";
            this.gbSort.Size = new System.Drawing.Size(98, 56);
            this.gbSort.TabIndex = 2;
            this.gbSort.TabStop = false;
            this.gbSort.Text = "Sort";
            // 
            // rbTitle
            // 
            this.rbTitle.AutoSize = true;
            this.rbTitle.Location = new System.Drawing.Point(6, 33);
            this.rbTitle.Name = "rbTitle";
            this.rbTitle.Size = new System.Drawing.Size(45, 17);
            this.rbTitle.TabIndex = 1;
            this.rbTitle.Text = "Title";
            this.rbTitle.UseVisualStyleBackColor = true;
            this.rbTitle.CheckedChanged += new System.EventHandler(this.rbTitle_CheckedChanged);
            // 
            // rbName
            // 
            this.rbName.AutoSize = true;
            this.rbName.Checked = true;
            this.rbName.Location = new System.Drawing.Point(6, 15);
            this.rbName.Name = "rbName";
            this.rbName.Size = new System.Drawing.Size(53, 17);
            this.rbName.TabIndex = 0;
            this.rbName.TabStop = true;
            this.rbName.Text = "Name";
            this.rbName.UseVisualStyleBackColor = true;
            this.rbName.CheckedChanged += new System.EventHandler(this.rbName_CheckedChanged);
            // 
            // gbDisplay
            // 
            this.gbDisplay.Controls.Add(this.rbAll);
            this.gbDisplay.Controls.Add(this.rbBoxOffice);
            this.gbDisplay.Controls.Add(this.rbAdmin);
            this.gbDisplay.Controls.Add(this.rbMarketing);
            this.gbDisplay.Controls.Add(this.rbIT);
            this.gbDisplay.Location = new System.Drawing.Point(183, 27);
            this.gbDisplay.Name = "gbDisplay";
            this.gbDisplay.Size = new System.Drawing.Size(383, 56);
            this.gbDisplay.TabIndex = 0;
            this.gbDisplay.TabStop = false;
            this.gbDisplay.Text = "Display";
            // 
            // rbMarketing
            // 
            this.rbMarketing.AutoSize = true;
            this.rbMarketing.Location = new System.Drawing.Point(6, 33);
            this.rbMarketing.Name = "rbMarketing";
            this.rbMarketing.Size = new System.Drawing.Size(72, 17);
            this.rbMarketing.TabIndex = 1;
            this.rbMarketing.Text = "Marketing";
            this.rbMarketing.UseVisualStyleBackColor = true;
            this.rbMarketing.CheckedChanged += new System.EventHandler(this.rbMarketing_CheckedChanged);
            // 
            // rbIT
            // 
            this.rbIT.AutoSize = true;
            this.rbIT.Checked = true;
            this.rbIT.Location = new System.Drawing.Point(6, 15);
            this.rbIT.Name = "rbIT";
            this.rbIT.Size = new System.Drawing.Size(35, 17);
            this.rbIT.TabIndex = 0;
            this.rbIT.TabStop = true;
            this.rbIT.Text = "IT";
            this.rbIT.UseVisualStyleBackColor = true;
            this.rbIT.CheckedChanged += new System.EventHandler(this.rbIT_CheckedChanged);
            // 
            // rbBoxOffice
            // 
            this.rbBoxOffice.AutoSize = true;
            this.rbBoxOffice.Location = new System.Drawing.Point(84, 33);
            this.rbBoxOffice.Name = "rbBoxOffice";
            this.rbBoxOffice.Size = new System.Drawing.Size(74, 17);
            this.rbBoxOffice.TabIndex = 3;
            this.rbBoxOffice.Text = "Box Office";
            this.rbBoxOffice.UseVisualStyleBackColor = true;
            this.rbBoxOffice.CheckedChanged += new System.EventHandler(this.rbBoxOffice_CheckedChanged);
            // 
            // rbAdmin
            // 
            this.rbAdmin.AutoSize = true;
            this.rbAdmin.Location = new System.Drawing.Point(84, 15);
            this.rbAdmin.Name = "rbAdmin";
            this.rbAdmin.Size = new System.Drawing.Size(54, 17);
            this.rbAdmin.TabIndex = 2;
            this.rbAdmin.Text = "Admin";
            this.rbAdmin.UseVisualStyleBackColor = true;
            this.rbAdmin.CheckedChanged += new System.EventHandler(this.rbAdmin_CheckedChanged);
            // 
            // rbAll
            // 
            this.rbAll.AutoSize = true;
            this.rbAll.Location = new System.Drawing.Point(162, 15);
            this.rbAll.Name = "rbAll";
            this.rbAll.Size = new System.Drawing.Size(36, 17);
            this.rbAll.TabIndex = 4;
            this.rbAll.Text = "All";
            this.rbAll.UseVisualStyleBackColor = true;
            this.rbAll.CheckedChanged += new System.EventHandler(this.rbAll_CheckedChanged);
            // 
            // OSFBusinessSolutionsTest
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(578, 319);
            this.Controls.Add(this.gbDisplay);
            this.Controls.Add(this.gbSort);
            this.Controls.Add(this.lvFilteredList);
            this.Controls.Add(this.menuStrip1);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MainMenuStrip = this.menuStrip1;
            this.Name = "OSFBusinessSolutionsTest";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "OSF Business Solutions Test";
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            this.gbSort.ResumeLayout(false);
            this.gbSort.PerformLayout();
            this.gbDisplay.ResumeLayout(false);
            this.gbDisplay.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.MenuStrip menuStrip1;
        private System.Windows.Forms.ToolStripMenuItem fileToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem openToolStripMenuItem;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
        private System.Windows.Forms.ToolStripMenuItem exitToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem helpToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem aboutToolStripMenuItem;
        private System.Windows.Forms.ListView lvFilteredList;
        private System.Windows.Forms.ColumnHeader chType;
        private System.Windows.Forms.ColumnHeader chName;
        private System.Windows.Forms.ColumnHeader chTitle;
        private System.Windows.Forms.ColumnHeader chDepartment;
        private System.Windows.Forms.GroupBox gbSort;
        private System.Windows.Forms.RadioButton rbTitle;
        private System.Windows.Forms.RadioButton rbName;
        private System.Windows.Forms.GroupBox gbDisplay;
        private System.Windows.Forms.RadioButton rbMarketing;
        private System.Windows.Forms.RadioButton rbIT;
        private System.Windows.Forms.ToolStripMenuItem saveToolStripMenuItem;
        private System.Windows.Forms.RadioButton rbAll;
        private System.Windows.Forms.RadioButton rbBoxOffice;
        private System.Windows.Forms.RadioButton rbAdmin;
    }
}

