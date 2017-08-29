using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Symphony9Data
{
    public partial class ShotSelection : Form
    {
        int mMovement = 0;
        public DialogResult mResult;
        Config mConfig = new Config();
        ShotList mShotList = new ShotList();

        public ShotSelection()
        {
            InitializeComponent();
        }

        public void setConfig(Config config)
        {
            mConfig = config;
        }

        public int[] show(int move)
        {
            mMovement = move;
            mShotList.setMove(move,mConfig);
            this.ShowDialog();

            return mConfig.getShotSelectionInts();
        }

        private void Selection_Load(object sender, EventArgs e)
        {
            lMovement.Text = mConfig.getMovementName(mMovement);

            lbShotType.Items.Clear();
            lbSingleShot.Items.Clear();

            for (int i = 0; i < mShotList.getNumShots(); i++)
            {
                string id = mShotList.getShotID(i);
                string name = mShotList.getShotName(i);
                string fullName = id + "-" + name;
                bool found = false;

                lbSingleShot.Items.Add(fullName);

                for(int j=0;j<lbShotType.Items.Count;j++)
                {
                    if(name == lbShotType.Items[j].ToString())
                    {
                        found = true;
                        break;
                    }
                }

                if(false == found)
                {
                    lbShotType.Items.Add(name);
                }

            }

            tbHeight.Text = mConfig.getHeight().ToString();
            tbWidth.Text = mConfig.getWidth().ToString();
            tbSelection.Text = mConfig.getShotsSelection();
        }

        private void bOK_Click(object sender, EventArgs e)
        {
            this.Close();
            mConfig.setShotsSelection(tbSelection.Text);
            mConfig.save();

            mResult = DialogResult.OK;
        }

        private void bCancel_Click(object sender, EventArgs e)
        {
            this.Close();
            mResult = DialogResult.Cancel;
        }

        private void bAddSelection_Click(object sender, EventArgs e)
        {
            if (-1 != lbShotType.SelectedIndex)
            {
                string name = lbShotType.Items[lbShotType.SelectedIndex].ToString();

                mShotList.selectType(name);

                tbSelection.Text = mShotList.getSelectedString();
            }
        }

        private void bSingleShot_Click(object sender, EventArgs e)
        {
            if(-1 != lbSingleShot.SelectedIndex)
            {
                string name = lbSingleShot.Items[lbSingleShot.SelectedIndex].ToString();
                char[] delimiterChars = { '-' };

                string[] items = name.Split(delimiterChars);
                int id = 0;

                int.TryParse(items[0], out id);

                mShotList.selectedID(id);

                tbSelection.Text = mShotList.getSelectedString();
            }
        }


        private void bClear_Click(object sender, EventArgs e)
        {
            tbSelection.Text = "";
            mShotList.clearSelected();
        }

        private void b180_Click(object sender, EventArgs e)
        {
            mConfig.setWidth(320);
            mConfig.setHeight(180);

            tbHeight.Text = "180";
            tbWidth.Text = "320";
        }

        private void b360_Click(object sender, EventArgs e)
        {
            mConfig.setWidth(640);
            mConfig.setHeight(360);

            tbHeight.Text = "360";
            tbWidth.Text = "640";
        }

        private void b450_Click(object sender, EventArgs e)
        {
            mConfig.setWidth(800);
            mConfig.setHeight(450);

            tbHeight.Text = "450";
            tbWidth.Text = "800";
        }

        private void b540_Click(object sender, EventArgs e)
        {
            mConfig.setWidth(960);
            mConfig.setHeight(540);

            tbHeight.Text = "540";
            tbWidth.Text = "960";
        }

        private void b720_Click(object sender, EventArgs e)
        {
            mConfig.setWidth(1280);
            mConfig.setHeight(720);

            tbHeight.Text = "720";
            tbWidth.Text = "1280";
        }

        private void b1080_Click(object sender, EventArgs e)
        {
            mConfig.setWidth(1920);
            mConfig.setHeight(1080);

            tbHeight.Text = "1080";
            tbWidth.Text = "1920";
        }

        private void b2160_Click(object sender, EventArgs e)
        {
            mConfig.setWidth(3840);
            mConfig.setHeight(2160);

            tbHeight.Text = "2160";
            tbWidth.Text = "3840";
        }

        private void b2160c_Click(object sender, EventArgs e)
        {
            mConfig.setWidth(4096);
            mConfig.setHeight(2160);

            tbHeight.Text = "2160";
            tbWidth.Text = "4096";
        }
    }
}
