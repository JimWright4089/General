namespace IconBuilder
{
    partial class IconBuilder
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
            if ( disposing && ( components != null ) )
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(IconBuilder));
            this.tbFirstLine = new System.Windows.Forms.TextBox();
            this.tbSecondLine = new System.Windows.Forms.TextBox();
            this.bColor = new System.Windows.Forms.Button();
            this.bSave = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // tbFirstLine
            // 
            this.tbFirstLine.Location = new System.Drawing.Point(12, 12);
            this.tbFirstLine.Name = "tbFirstLine";
            this.tbFirstLine.Size = new System.Drawing.Size(75, 20);
            this.tbFirstLine.TabIndex = 0;
            this.tbFirstLine.Text = "Jims";
            this.tbFirstLine.TextChanged += new System.EventHandler(this.tbFirstLine_TextChanged);
            // 
            // tbSecondLine
            // 
            this.tbSecondLine.Location = new System.Drawing.Point(12, 38);
            this.tbSecondLine.Name = "tbSecondLine";
            this.tbSecondLine.Size = new System.Drawing.Size(75, 20);
            this.tbSecondLine.TabIndex = 1;
            this.tbSecondLine.TextChanged += new System.EventHandler(this.tbSecondLine_TextChanged);
            // 
            // bColor
            // 
            this.bColor.BackColor = System.Drawing.SystemColors.Control;
            this.bColor.ForeColor = System.Drawing.SystemColors.ControlText;
            this.bColor.Location = new System.Drawing.Point(12, 64);
            this.bColor.Name = "bColor";
            this.bColor.Size = new System.Drawing.Size(75, 23);
            this.bColor.TabIndex = 2;
            this.bColor.Text = "Color";
            this.bColor.UseVisualStyleBackColor = false;
            this.bColor.Click += new System.EventHandler(this.bColor_Click);
            // 
            // bSave
            // 
            this.bSave.BackColor = System.Drawing.SystemColors.Control;
            this.bSave.ForeColor = System.Drawing.SystemColors.ControlText;
            this.bSave.Location = new System.Drawing.Point(12, 121);
            this.bSave.Name = "bSave";
            this.bSave.Size = new System.Drawing.Size(75, 23);
            this.bSave.TabIndex = 3;
            this.bSave.Text = "Save";
            this.bSave.UseVisualStyleBackColor = false;
            this.bSave.Click += new System.EventHandler(this.bSave_Click);
            // 
            // IconBuilder
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.Control;
            this.ClientSize = new System.Drawing.Size(942, 625);
            this.Controls.Add(this.bSave);
            this.Controls.Add(this.bColor);
            this.Controls.Add(this.tbSecondLine);
            this.Controls.Add(this.tbFirstLine);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "IconBuilder";
            this.Text = "Icon Builder";
            this.Paint += new System.Windows.Forms.PaintEventHandler(this.IconBuilder_Paint);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox tbFirstLine;
        private System.Windows.Forms.TextBox tbSecondLine;
        private System.Windows.Forms.Button bColor;
        private System.Windows.Forms.Button bSave;
    }
}

