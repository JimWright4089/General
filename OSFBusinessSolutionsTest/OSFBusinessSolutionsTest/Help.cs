using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace OSFBusinessSolutionsTest
{
    public partial class Help : Form
    {
        public Help()
        {
            InitializeComponent();
            Image theIcon = (Image)(Properties.Resources.ResourceManager.GetObject("OSFBusinessSolutionsTest"));
            pbIcon.Image = theIcon;
            lVersion.Text = Application.ProductVersion;
        }

        private void bOK_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
