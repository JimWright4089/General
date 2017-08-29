namespace Symphony9Data
{
    partial class Selection
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
            this.tvOrch = new System.Windows.Forms.TreeView();
            this.bOK = new System.Windows.Forms.Button();
            this.bCancel = new System.Windows.Forms.Button();
            this.lMovement = new System.Windows.Forms.Label();
            this.tbSelection = new System.Windows.Forms.TextBox();
            this.tbEnd = new System.Windows.Forms.TextBox();
            this.tbStart = new System.Windows.Forms.TextBox();
            this.lEnd = new System.Windows.Forms.Label();
            this.lStart = new System.Windows.Forms.Label();
            this.bAddSelection = new System.Windows.Forms.Button();
            this.bClear = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // tvOrch
            // 
            this.tvOrch.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left)));
            this.tvOrch.Location = new System.Drawing.Point(12, 34);
            this.tvOrch.Name = "tvOrch";
            this.tvOrch.Size = new System.Drawing.Size(166, 283);
            this.tvOrch.TabIndex = 0;
            // 
            // bOK
            // 
            this.bOK.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.bOK.Location = new System.Drawing.Point(226, 294);
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
            this.bCancel.Location = new System.Drawing.Point(310, 294);
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
            this.tbSelection.Location = new System.Drawing.Point(187, 119);
            this.tbSelection.Multiline = true;
            this.tbSelection.Name = "tbSelection";
            this.tbSelection.Size = new System.Drawing.Size(198, 169);
            this.tbSelection.TabIndex = 4;
            // 
            // tbEnd
            // 
            this.tbEnd.Location = new System.Drawing.Point(187, 64);
            this.tbEnd.Name = "tbEnd";
            this.tbEnd.Size = new System.Drawing.Size(100, 20);
            this.tbEnd.TabIndex = 5;
            // 
            // tbStart
            // 
            this.tbStart.Location = new System.Drawing.Point(187, 25);
            this.tbStart.Name = "tbStart";
            this.tbStart.Size = new System.Drawing.Size(100, 20);
            this.tbStart.TabIndex = 6;
            // 
            // lEnd
            // 
            this.lEnd.AutoSize = true;
            this.lEnd.Location = new System.Drawing.Point(184, 48);
            this.lEnd.Name = "lEnd";
            this.lEnd.Size = new System.Drawing.Size(26, 13);
            this.lEnd.TabIndex = 7;
            this.lEnd.Text = "End";
            // 
            // lStart
            // 
            this.lStart.AutoSize = true;
            this.lStart.Location = new System.Drawing.Point(184, 9);
            this.lStart.Name = "lStart";
            this.lStart.Size = new System.Drawing.Size(29, 13);
            this.lStart.TabIndex = 8;
            this.lStart.Text = "Start";
            // 
            // bAddSelection
            // 
            this.bAddSelection.Location = new System.Drawing.Point(187, 90);
            this.bAddSelection.Name = "bAddSelection";
            this.bAddSelection.Size = new System.Drawing.Size(75, 23);
            this.bAddSelection.TabIndex = 9;
            this.bAddSelection.Text = "Add";
            this.bAddSelection.UseVisualStyleBackColor = true;
            this.bAddSelection.Click += new System.EventHandler(this.bAddSelection_Click);
            // 
            // bClear
            // 
            this.bClear.Location = new System.Drawing.Point(268, 90);
            this.bClear.Name = "bClear";
            this.bClear.Size = new System.Drawing.Size(75, 23);
            this.bClear.TabIndex = 10;
            this.bClear.Text = "Clear";
            this.bClear.UseVisualStyleBackColor = true;
            this.bClear.Click += new System.EventHandler(this.bClear_Click);
            // 
            // Selection
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(397, 329);
            this.Controls.Add(this.bClear);
            this.Controls.Add(this.bAddSelection);
            this.Controls.Add(this.lStart);
            this.Controls.Add(this.lEnd);
            this.Controls.Add(this.tbStart);
            this.Controls.Add(this.tbEnd);
            this.Controls.Add(this.tbSelection);
            this.Controls.Add(this.lMovement);
            this.Controls.Add(this.bCancel);
            this.Controls.Add(this.bOK);
            this.Controls.Add(this.tvOrch);
            this.Name = "Selection";
            this.Text = "Selection";
            this.Load += new System.EventHandler(this.Selection_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TreeView tvOrch;
        private System.Windows.Forms.Button bOK;
        private System.Windows.Forms.Button bCancel;
        private System.Windows.Forms.Label lMovement;
        private System.Windows.Forms.TextBox tbSelection;
        private System.Windows.Forms.TextBox tbEnd;
        private System.Windows.Forms.TextBox tbStart;
        private System.Windows.Forms.Label lEnd;
        private System.Windows.Forms.Label lStart;
        private System.Windows.Forms.Button bAddSelection;
        private System.Windows.Forms.Button bClear;
    }
}