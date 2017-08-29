namespace Symphony9Data
{
    partial class ShotSelection
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
            this.bOK = new System.Windows.Forms.Button();
            this.bCancel = new System.Windows.Forms.Button();
            this.lMovement = new System.Windows.Forms.Label();
            this.tbSelection = new System.Windows.Forms.TextBox();
            this.tbHeight = new System.Windows.Forms.TextBox();
            this.tbWidth = new System.Windows.Forms.TextBox();
            this.lEnd = new System.Windows.Forms.Label();
            this.lStart = new System.Windows.Forms.Label();
            this.bAddShotTimes = new System.Windows.Forms.Button();
            this.bClear = new System.Windows.Forms.Button();
            this.b180 = new System.Windows.Forms.Button();
            this.b360 = new System.Windows.Forms.Button();
            this.b540 = new System.Windows.Forms.Button();
            this.b450 = new System.Windows.Forms.Button();
            this.b2160c = new System.Windows.Forms.Button();
            this.b2160 = new System.Windows.Forms.Button();
            this.b1080 = new System.Windows.Forms.Button();
            this.b720 = new System.Windows.Forms.Button();
            this.bSingleShot = new System.Windows.Forms.Button();
            this.lbSingleShot = new System.Windows.Forms.ListBox();
            this.lbShotType = new System.Windows.Forms.ListBox();
            this.SuspendLayout();
            // 
            // bOK
            // 
            this.bOK.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.bOK.Location = new System.Drawing.Point(325, 290);
            this.bOK.Name = "bOK";
            this.bOK.Size = new System.Drawing.Size(75, 23);
            this.bOK.TabIndex = 1;
            this.bOK.Text = "OK";
            this.bOK.UseVisualStyleBackColor = true;
            this.bOK.Click += new System.EventHandler(this.bOK_Click);
            // 
            // bCancel
            // 
            this.bCancel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.bCancel.Location = new System.Drawing.Point(409, 290);
            this.bCancel.Name = "bCancel";
            this.bCancel.Size = new System.Drawing.Size(75, 23);
            this.bCancel.TabIndex = 2;
            this.bCancel.Text = "Cancel";
            this.bCancel.UseVisualStyleBackColor = true;
            this.bCancel.Click += new System.EventHandler(this.bCancel_Click);
            // 
            // lMovement
            // 
            this.lMovement.Location = new System.Drawing.Point(12, 9);
            this.lMovement.Name = "lMovement";
            this.lMovement.Size = new System.Drawing.Size(166, 22);
            this.lMovement.TabIndex = 3;
            this.lMovement.Text = "M ovement 01";
            // 
            // tbSelection
            // 
            this.tbSelection.Location = new System.Drawing.Point(286, 133);
            this.tbSelection.Multiline = true;
            this.tbSelection.Name = "tbSelection";
            this.tbSelection.Size = new System.Drawing.Size(198, 151);
            this.tbSelection.TabIndex = 4;
            // 
            // tbHeight
            // 
            this.tbHeight.Location = new System.Drawing.Point(450, 72);
            this.tbHeight.Name = "tbHeight";
            this.tbHeight.Size = new System.Drawing.Size(75, 20);
            this.tbHeight.TabIndex = 5;
            // 
            // tbWidth
            // 
            this.tbWidth.Location = new System.Drawing.Point(321, 72);
            this.tbWidth.Name = "tbWidth";
            this.tbWidth.Size = new System.Drawing.Size(75, 20);
            this.tbWidth.TabIndex = 6;
            // 
            // lEnd
            // 
            this.lEnd.AutoSize = true;
            this.lEnd.Location = new System.Drawing.Point(406, 75);
            this.lEnd.Name = "lEnd";
            this.lEnd.Size = new System.Drawing.Size(38, 13);
            this.lEnd.TabIndex = 7;
            this.lEnd.Text = "Height";
            // 
            // lStart
            // 
            this.lStart.AutoSize = true;
            this.lStart.Location = new System.Drawing.Point(286, 72);
            this.lStart.Name = "lStart";
            this.lStart.Size = new System.Drawing.Size(35, 13);
            this.lStart.TabIndex = 8;
            this.lStart.Text = "Width";
            // 
            // bAddShotTimes
            // 
            this.bAddShotTimes.Location = new System.Drawing.Point(15, 267);
            this.bAddShotTimes.Name = "bAddShotTimes";
            this.bAddShotTimes.Size = new System.Drawing.Size(75, 23);
            this.bAddShotTimes.TabIndex = 9;
            this.bAddShotTimes.Text = "Add";
            this.bAddShotTimes.UseVisualStyleBackColor = true;
            this.bAddShotTimes.Click += new System.EventHandler(this.bAddSelection_Click);
            // 
            // bClear
            // 
            this.bClear.Location = new System.Drawing.Point(286, 104);
            this.bClear.Name = "bClear";
            this.bClear.Size = new System.Drawing.Size(75, 23);
            this.bClear.TabIndex = 10;
            this.bClear.Text = "Clear";
            this.bClear.UseVisualStyleBackColor = true;
            this.bClear.Click += new System.EventHandler(this.bClear_Click);
            // 
            // b180
            // 
            this.b180.Location = new System.Drawing.Point(289, 12);
            this.b180.Name = "b180";
            this.b180.Size = new System.Drawing.Size(56, 23);
            this.b180.TabIndex = 12;
            this.b180.Text = "180";
            this.b180.UseVisualStyleBackColor = true;
            this.b180.Click += new System.EventHandler(this.b180_Click);
            // 
            // b360
            // 
            this.b360.Location = new System.Drawing.Point(352, 12);
            this.b360.Name = "b360";
            this.b360.Size = new System.Drawing.Size(56, 23);
            this.b360.TabIndex = 13;
            this.b360.Text = "360";
            this.b360.UseVisualStyleBackColor = true;
            this.b360.Click += new System.EventHandler(this.b360_Click);
            // 
            // b540
            // 
            this.b540.Location = new System.Drawing.Point(478, 12);
            this.b540.Name = "b540";
            this.b540.Size = new System.Drawing.Size(56, 23);
            this.b540.TabIndex = 15;
            this.b540.Text = "540";
            this.b540.UseVisualStyleBackColor = true;
            this.b540.Click += new System.EventHandler(this.b540_Click);
            // 
            // b450
            // 
            this.b450.Location = new System.Drawing.Point(415, 12);
            this.b450.Name = "b450";
            this.b450.Size = new System.Drawing.Size(56, 23);
            this.b450.TabIndex = 14;
            this.b450.Text = "450";
            this.b450.UseVisualStyleBackColor = true;
            this.b450.Click += new System.EventHandler(this.b450_Click);
            // 
            // b2160c
            // 
            this.b2160c.Location = new System.Drawing.Point(478, 41);
            this.b2160c.Name = "b2160c";
            this.b2160c.Size = new System.Drawing.Size(56, 23);
            this.b2160c.TabIndex = 19;
            this.b2160c.Text = "2160c";
            this.b2160c.UseVisualStyleBackColor = true;
            this.b2160c.Click += new System.EventHandler(this.b2160c_Click);
            // 
            // b2160
            // 
            this.b2160.Location = new System.Drawing.Point(415, 41);
            this.b2160.Name = "b2160";
            this.b2160.Size = new System.Drawing.Size(56, 23);
            this.b2160.TabIndex = 18;
            this.b2160.Text = "2160";
            this.b2160.UseVisualStyleBackColor = true;
            this.b2160.Click += new System.EventHandler(this.b2160_Click);
            // 
            // b1080
            // 
            this.b1080.Location = new System.Drawing.Point(352, 41);
            this.b1080.Name = "b1080";
            this.b1080.Size = new System.Drawing.Size(56, 23);
            this.b1080.TabIndex = 17;
            this.b1080.Text = "1080";
            this.b1080.UseVisualStyleBackColor = true;
            this.b1080.Click += new System.EventHandler(this.b1080_Click);
            // 
            // b720
            // 
            this.b720.Location = new System.Drawing.Point(289, 41);
            this.b720.Name = "b720";
            this.b720.Size = new System.Drawing.Size(56, 23);
            this.b720.TabIndex = 16;
            this.b720.Text = "720";
            this.b720.UseVisualStyleBackColor = true;
            this.b720.Click += new System.EventHandler(this.b720_Click);
            // 
            // bSingleShot
            // 
            this.bSingleShot.Location = new System.Drawing.Point(138, 267);
            this.bSingleShot.Name = "bSingleShot";
            this.bSingleShot.Size = new System.Drawing.Size(75, 23);
            this.bSingleShot.TabIndex = 20;
            this.bSingleShot.Text = "Add";
            this.bSingleShot.UseVisualStyleBackColor = true;
            this.bSingleShot.Click += new System.EventHandler(this.bSingleShot_Click);
            // 
            // lbSingleShot
            // 
            this.lbSingleShot.FormattingEnabled = true;
            this.lbSingleShot.Location = new System.Drawing.Point(138, 41);
            this.lbSingleShot.Name = "lbSingleShot";
            this.lbSingleShot.Size = new System.Drawing.Size(120, 225);
            this.lbSingleShot.TabIndex = 21;
            // 
            // lbShotType
            // 
            this.lbShotType.FormattingEnabled = true;
            this.lbShotType.Location = new System.Drawing.Point(12, 41);
            this.lbShotType.Name = "lbShotType";
            this.lbShotType.Size = new System.Drawing.Size(120, 225);
            this.lbShotType.TabIndex = 22;
            // 
            // ShotSelection
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(558, 329);
            this.Controls.Add(this.lbShotType);
            this.Controls.Add(this.lbSingleShot);
            this.Controls.Add(this.bSingleShot);
            this.Controls.Add(this.b2160c);
            this.Controls.Add(this.b2160);
            this.Controls.Add(this.b1080);
            this.Controls.Add(this.b720);
            this.Controls.Add(this.b540);
            this.Controls.Add(this.b450);
            this.Controls.Add(this.b360);
            this.Controls.Add(this.b180);
            this.Controls.Add(this.bClear);
            this.Controls.Add(this.bAddShotTimes);
            this.Controls.Add(this.lStart);
            this.Controls.Add(this.lEnd);
            this.Controls.Add(this.tbWidth);
            this.Controls.Add(this.tbHeight);
            this.Controls.Add(this.tbSelection);
            this.Controls.Add(this.lMovement);
            this.Controls.Add(this.bCancel);
            this.Controls.Add(this.bOK);
            this.Name = "ShotSelection";
            this.Text = "Selection";
            this.Load += new System.EventHandler(this.Selection_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button bOK;
        private System.Windows.Forms.Button bCancel;
        private System.Windows.Forms.Label lMovement;
        private System.Windows.Forms.TextBox tbSelection;
        private System.Windows.Forms.TextBox tbHeight;
        private System.Windows.Forms.TextBox tbWidth;
        private System.Windows.Forms.Label lEnd;
        private System.Windows.Forms.Label lStart;
        private System.Windows.Forms.Button bAddShotTimes;
        private System.Windows.Forms.Button bClear;
        private System.Windows.Forms.Button b180;
        private System.Windows.Forms.Button b360;
        private System.Windows.Forms.Button b540;
        private System.Windows.Forms.Button b450;
        private System.Windows.Forms.Button b2160c;
        private System.Windows.Forms.Button b2160;
        private System.Windows.Forms.Button b1080;
        private System.Windows.Forms.Button b720;
        private System.Windows.Forms.Button bSingleShot;
        private System.Windows.Forms.ListBox lbSingleShot;
        private System.Windows.Forms.ListBox lbShotType;
    }
}