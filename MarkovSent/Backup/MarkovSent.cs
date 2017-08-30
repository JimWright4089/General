using System;
using System.Drawing;
using System.Collections;
using System.Collections.Specialized;
using System.ComponentModel;
using System.Windows.Forms;
using System.Data;

namespace MarkovSent
{
  /// <summary>
  /// Summary description for Form1.
  /// </summary>
  public class FormMarkovSent : System.Windows.Forms.Form
  {
    private System.Windows.Forms.Button buttonRemove;
    private System.Windows.Forms.Button buttonGenerate;
    private System.Windows.Forms.Button buttonAdd;
    private System.Windows.Forms.TextBox textBoxSent;
    private System.Windows.Forms.ListBox listBoxSent;
    private System.Windows.Forms.Label labelSent;
    private System.Windows.Forms.Button buttonProbTable;
    private System.Windows.Forms.Button buttonTestMatrix;
    /// <summary>
    /// Required designer variable.
    /// </summary>
    private System.ComponentModel.Container components = null;

    public FormMarkovSent()
    {
      //
      // Required for Windows Form Designer support
      //
      InitializeComponent();
    }

    /// <summary>
    /// Clean up any resources being used.
    /// </summary>
    protected override void Dispose( bool disposing )
    {
      if( disposing )
      {
        if (components != null) 
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
      System.Resources.ResourceManager resources = new System.Resources.ResourceManager(typeof(FormMarkovSent));
      this.textBoxSent = new System.Windows.Forms.TextBox();
      this.listBoxSent = new System.Windows.Forms.ListBox();
      this.labelSent = new System.Windows.Forms.Label();
      this.buttonRemove = new System.Windows.Forms.Button();
      this.buttonGenerate = new System.Windows.Forms.Button();
      this.buttonAdd = new System.Windows.Forms.Button();
      this.buttonProbTable = new System.Windows.Forms.Button();
      this.buttonTestMatrix = new System.Windows.Forms.Button();
      this.SuspendLayout();
      // 
      // textBoxSent
      // 
      this.textBoxSent.Location = new System.Drawing.Point(8, 168);
      this.textBoxSent.Name = "textBoxSent";
      this.textBoxSent.Size = new System.Drawing.Size(360, 20);
      this.textBoxSent.TabIndex = 0;
      this.textBoxSent.Text = "Hi there.";
      // 
      // listBoxSent
      // 
      this.listBoxSent.Location = new System.Drawing.Point(8, 8);
      this.listBoxSent.Name = "listBoxSent";
      this.listBoxSent.Size = new System.Drawing.Size(368, 147);
      this.listBoxSent.TabIndex = 4;
      // 
      // labelSent
      // 
      this.labelSent.Location = new System.Drawing.Point(16, 224);
      this.labelSent.Name = "labelSent";
      this.labelSent.Size = new System.Drawing.Size(352, 23);
      this.labelSent.TabIndex = 5;
      // 
      // buttonRemove
      // 
      this.buttonRemove.Location = new System.Drawing.Point(384, 8);
      this.buttonRemove.Name = "buttonRemove";
      this.buttonRemove.Size = new System.Drawing.Size(72, 32);
      this.buttonRemove.TabIndex = 2;
      this.buttonRemove.Text = "Remove";
      this.buttonRemove.Click += new System.EventHandler(this.buttonRemove_Click);
      // 
      // buttonGenerate
      // 
      this.buttonGenerate.Location = new System.Drawing.Point(384, 216);
      this.buttonGenerate.Name = "buttonGenerate";
      this.buttonGenerate.Size = new System.Drawing.Size(72, 32);
      this.buttonGenerate.TabIndex = 3;
      this.buttonGenerate.Text = "Generate";
      this.buttonGenerate.Click += new System.EventHandler(this.buttonGenerate_Click);
      // 
      // buttonAdd
      // 
      this.buttonAdd.Location = new System.Drawing.Point(384, 168);
      this.buttonAdd.Name = "buttonAdd";
      this.buttonAdd.Size = new System.Drawing.Size(72, 32);
      this.buttonAdd.TabIndex = 1;
      this.buttonAdd.Text = "Add";
      this.buttonAdd.Click += new System.EventHandler(this.buttonAdd_Click);
      // 
      // buttonProbTable
      // 
      this.buttonProbTable.Location = new System.Drawing.Point(384, 48);
      this.buttonProbTable.Name = "buttonProbTable";
      this.buttonProbTable.Size = new System.Drawing.Size(72, 32);
      this.buttonProbTable.TabIndex = 6;
      this.buttonProbTable.Text = "Prob\' Table";
      this.buttonProbTable.Click += new System.EventHandler(this.buttonProbTable_Click);
      // 
      // buttonTestMatrix
      // 
      this.buttonTestMatrix.Location = new System.Drawing.Point(384, 96);
      this.buttonTestMatrix.Name = "buttonTestMatrix";
      this.buttonTestMatrix.Size = new System.Drawing.Size(72, 32);
      this.buttonTestMatrix.TabIndex = 7;
      this.buttonTestMatrix.Text = "Test Mat";
      this.buttonTestMatrix.Visible = false;
      this.buttonTestMatrix.Click += new System.EventHandler(this.buttonTestMatrix_Click);
      // 
      // FormMarkovSent
      // 
      this.AutoScaleBaseSize = new System.Drawing.Size(5, 13);
      this.ClientSize = new System.Drawing.Size(464, 253);
      this.Controls.Add(this.buttonTestMatrix);
      this.Controls.Add(this.buttonProbTable);
      this.Controls.Add(this.labelSent);
      this.Controls.Add(this.listBoxSent);
      this.Controls.Add(this.textBoxSent);
      this.Controls.Add(this.buttonRemove);
      this.Controls.Add(this.buttonGenerate);
      this.Controls.Add(this.buttonAdd);
      this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
      this.Name = "FormMarkovSent";
      this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
      this.Text = "Markov Sentences";
      this.Load += new System.EventHandler(this.FormMarkovSent_Load);
      this.ResumeLayout(false);

    }
		#endregion

    /// <summary>
    /// The main entry point for the application.
    /// </summary>
    [STAThread]
    /******************************************************************
    ** Main
    ** 
    *******************************************************************/
    static void Main() 
    {
      Application.Run(new FormMarkovSent());
    }

    /******************************************************************
    ** buttonAdd_Click
    ** 
    *******************************************************************/
    private void buttonAdd_Click(object sender, System.EventArgs e)
    {
      listBoxSent.Items.Add(textBoxSent.Lines[0]);
      CMarkovSentences.GetInstance().AddString(textBoxSent.Lines[0]);
      textBoxSent.Clear();
      textBoxSent.Focus();
    }

    /******************************************************************
    ** buttonRemove_Click
    ** 
    *******************************************************************/
    private void buttonRemove_Click(object sender, System.EventArgs e)
    {
      int row_number;
      
      /*
      ** Find the row and delete it if there is a row
      */
      row_number = listBoxSent.SelectedIndex;
      if(row_number != -1)
      {
        CMarkovSentences.GetInstance().RemoveString(listBoxSent.Items[row_number].ToString());
        listBoxSent.Items.RemoveAt(row_number);
      }
    }

    /******************************************************************
    ** buttonGenerate_Click
    ** 
    *******************************************************************/
    private void buttonGenerate_Click(object sender, System.EventArgs e)
    {
      string return_string = CMarkovSentences.GetInstance().GenerateString();
      labelSent.Text = return_string;
    }

    /******************************************************************
    ** buttonProbTable_Click
    ** 
    *******************************************************************/
    private void buttonProbTable_Click(object sender, System.EventArgs e)
    {
      FormProbabilityTable new_prob_form = new FormProbabilityTable();

      new_prob_form.Show();
    }

    private void buttonTestMatrix_Click(object sender, System.EventArgs e)
    {
      {
        float[,] m_a = new float[3,2];
        float[,] m_b = new float[2,3];
        float[,] m_c;
        
        m_a[0,0] =  1;
        m_a[1,0] =  0;
        m_a[2,0] = -2;
        m_a[0,1] =  0;
        m_a[1,1] =  3;
        m_a[2,1] = -1;
        
        m_b[0,0] =  0;
        m_b[1,0] =  3;
        m_b[0,1] = -2;
        m_b[1,1] = -1;
        m_b[0,2] =  0;
        m_b[1,2] =  4;
        
        m_c = MathUtil.MatrixMultiplication(m_a,m_b);
      }      
      {
        float[,] m_a = new float[2,2];
        float[,] m_b = new float[2,2];
        float[,] m_c;
          
        m_a[0,0] =  0.2f;
        m_a[1,0] =  0.8f;
        m_a[0,1] =  0.3f;
        m_a[1,1] =  0.7f;
          
        m_b[0,0] =  0.2f;
        m_b[1,0] =  0.8f;
        m_b[0,1] =  0.3f;
        m_b[1,1] =  0.7f;
          
        m_c = MathUtil.MatrixMultiplication(m_a,m_b);
        m_a = m_c;
      }      
    }

    private void FormMarkovSent_Load(object sender, System.EventArgs e)
    {
    
    }
  }
}
