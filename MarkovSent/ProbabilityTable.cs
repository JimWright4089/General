using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using System.Windows.Forms;

namespace MarkovSent
{
	/// <summary>
	/// Summary description for Form1.
	/// </summary>
	public class FormProbabilityTable : System.Windows.Forms.Form
	{
    private System.Windows.Forms.ComboBox comboBoxProb;
    private System.Windows.Forms.Label label2;
    private System.Windows.Forms.ListView listViewProb;
    private System.Windows.Forms.ColumnHeader columnHeader1;
    private System.Windows.Forms.ColumnHeader columnHeader2;
    private System.Windows.Forms.ColumnHeader columnHeader3;
    private System.Windows.Forms.ColumnHeader columnHeader4;
    private System.Windows.Forms.ColumnHeader columnHeader5;
    private System.Windows.Forms.ColumnHeader columnHeader6;
		/// <summary>
		/// Required designer variable.
		/// </summary>
		private System.ComponentModel.Container components = null;

		public FormProbabilityTable()
		{
			//
			// Required for Windows Form Designer support
			//
			InitializeComponent();

			//
			// TODO: Add any constructor code after InitializeComponent call
			//
		}

		/// <summary>
		/// Clean up any resources being used.
		/// </summary>
		protected override void Dispose( bool disposing )
		{
			if( disposing )
			{
				if(components != null)
				{
					components.Dispose();
				}
			}
			base.Dispose( disposing );
		}

		#region Windows Form Designer generated code
		/// <summary>
		/// Required method for Designer support - do not modify
		/// the contents of this method with the code editor.
		/// </summary>
		private void InitializeComponent()
		{
      System.Resources.ResourceManager resources = new System.Resources.ResourceManager(typeof(FormProbabilityTable));
      this.comboBoxProb = new System.Windows.Forms.ComboBox();
      this.label2 = new System.Windows.Forms.Label();
      this.listViewProb = new System.Windows.Forms.ListView();
      this.columnHeader1 = new System.Windows.Forms.ColumnHeader();
      this.columnHeader2 = new System.Windows.Forms.ColumnHeader();
      this.columnHeader3 = new System.Windows.Forms.ColumnHeader();
      this.columnHeader4 = new System.Windows.Forms.ColumnHeader();
      this.columnHeader5 = new System.Windows.Forms.ColumnHeader();
      this.columnHeader6 = new System.Windows.Forms.ColumnHeader();
      this.SuspendLayout();
      // 
      // comboBoxProb
      // 
      this.comboBoxProb.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
      this.comboBoxProb.Items.AddRange(new object[] {
                                                      "Next Letter",
                                                      "Two Letters away",
                                                      "Three Letters away",
                                                      "Four letters away",
                                                      "Five Letters away",
                                                      "Six letters away",
                                                      "Seven letters away",
                                                      "Eight letters away",
                                                      "Nine letters away"});
      this.comboBoxProb.Location = new System.Drawing.Point(160, 8);
      this.comboBoxProb.Name = "comboBoxProb";
      this.comboBoxProb.Size = new System.Drawing.Size(128, 21);
      this.comboBoxProb.TabIndex = 0;
      this.comboBoxProb.SelectedIndexChanged += new System.EventHandler(this.comboBoxProb_SelectedIndexChanged);
      // 
      // label2
      // 
      this.label2.Location = new System.Drawing.Point(88, 8);
      this.label2.Name = "label2";
      this.label2.Size = new System.Drawing.Size(72, 16);
      this.label2.TabIndex = 3;
      this.label2.Text = "The Prob on:";
      this.label2.Click += new System.EventHandler(this.label2_Click);
      // 
      // listViewProb
      // 
      this.listViewProb.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
        | System.Windows.Forms.AnchorStyles.Left) 
        | System.Windows.Forms.AnchorStyles.Right)));
      this.listViewProb.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
                                                                                   this.columnHeader1,
                                                                                   this.columnHeader2,
                                                                                   this.columnHeader3,
                                                                                   this.columnHeader4,
                                                                                   this.columnHeader5,
                                                                                   this.columnHeader6});
      this.listViewProb.GridLines = true;
      this.listViewProb.Location = new System.Drawing.Point(8, 40);
      this.listViewProb.Name = "listViewProb";
      this.listViewProb.Size = new System.Drawing.Size(480, 320);
      this.listViewProb.TabIndex = 4;
      this.listViewProb.View = System.Windows.Forms.View.Details;
      // 
      // FormProbabilityTable
      // 
      this.AutoScaleBaseSize = new System.Drawing.Size(5, 13);
      this.ClientSize = new System.Drawing.Size(504, 373);
      this.Controls.Add(this.listViewProb);
      this.Controls.Add(this.label2);
      this.Controls.Add(this.comboBoxProb);
      this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
      this.Name = "FormProbabilityTable";
      this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
      this.Text = "Probability Table";
      this.Load += new System.EventHandler(this.FormProbabilityTable_Load);
      this.ResumeLayout(false);

    }
		#endregion

    private void label1_Click(object sender, System.EventArgs e)
    {
    
    }

    private void FormProbabilityTable_Load(object sender, System.EventArgs e)
    {
      float[,] prob_table;
      string[] prob_table_key;
      int i,j;

      comboBoxProb.Text = comboBoxProb.Items[0].ToString();
      prob_table     = CMarkovSentences.GetInstance().GetProbMatrix((int)comboBoxProb.SelectedIndex);
      prob_table_key =  CMarkovSentences.GetInstance().GetProbMatrixKey();

      listViewProb.Clear();

      listViewProb.Columns.Add("starting ltr",70,HorizontalAlignment.Center);
      for(i=0;i<prob_table_key.GetLength(0);i++)
      {
        listViewProb.Columns.Add(prob_table_key[i],50,HorizontalAlignment.Center);
      }

      for(i=0;i<prob_table_key.GetLength(0);i++)
      {
        ListViewItem lvi = new ListViewItem(prob_table_key[i]);
        
        for(j=0;j<prob_table_key.GetLength(0);j++)
        {
          lvi.SubItems.Add(prob_table[i,j].ToString("0.###"));
        }
        listViewProb.Items.Add(lvi);
      }
    }

    private void comboBoxProb_SelectedIndexChanged(object sender, System.EventArgs e)
    {
      float[,] prob_table;
      string[] prob_table_key;
      int i,j;

      prob_table     = CMarkovSentences.GetInstance().GetProbMatrix((int)comboBoxProb.SelectedIndex);
      prob_table_key =  CMarkovSentences.GetInstance().GetProbMatrixKey();

      listViewProb.Clear();

      listViewProb.Columns.Add("starting ltr",70,HorizontalAlignment.Center);
      for(i=0;i<prob_table_key.GetLength(0);i++)
      {
        listViewProb.Columns.Add(prob_table_key[i],50,HorizontalAlignment.Center);
      }

      for(i=0;i<prob_table_key.GetLength(0);i++)
      {
        ListViewItem lvi = new ListViewItem(prob_table_key[i]);
        
        for(j=0;j<prob_table_key.GetLength(0);j++)
        {
          lvi.SubItems.Add(prob_table[i,j].ToString("0.###"));
        }
        listViewProb.Items.Add(lvi);
      }
    }

    private void label2_Click(object sender, System.EventArgs e)
    {
    
    }
	}
}
