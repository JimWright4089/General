//----------------------------------------------------------------------------
//
//  $Workfile: Selection.cs$
//
//  $Revision: X$
//
//  Project:    Symphony 9 Data
//
//                            Copyright (c) 2015
//                               James A Wright
//                            All Rights Reserved
//
//  Modification History:
//  $Log:
//  $
//
//----------------------------------------------------------------------------
using System;
using System.Windows.Forms;

namespace Symphony9Data
{
    //----------------------------------------------------------------------------
    //  Class Declarations
    //----------------------------------------------------------------------------
    //
    // Class Name: Selection
    // 
    // Purpose:
    //      This displays the player viewer.  It's used in the CS tool and 
    //      the max scripts.
    //
    //----------------------------------------------------------------------------
    public partial class Selection : Form
    {
        //----------------------------------------------------------------------------
        //  Class Attributes 
        //----------------------------------------------------------------------------
        int mMovement = 0;
        public DialogResult mResult;
        Config mConfig = new Config();
        FullOrch mFullOrch = new FullOrch();

        //--------------------------------------------------------------------
        // Purpose:
        //     Cnstructor of the form
        //
        // Notes:
        //     None.
        //--------------------------------------------------------------------
        public Selection()
        {
            InitializeComponent();
        }

        //--------------------------------------------------------------------
        // Purpose:
        //     Sets the config file for the dialog.  
        //
        // Notes:
        //     This alows the caller to pass in their param file.
        //     This form will save the player selection and the frames
        //--------------------------------------------------------------------
        public void setConfig(Config config)
        {
            mConfig = config;
        }

        //--------------------------------------------------------------------
        // Purpose:
        //     Display the form, and return the selection int array when we close
        //
        // Notes:
        //     None.
        //--------------------------------------------------------------------
        public int[] show(int move)
        {
            mMovement = move;
            this.ShowDialog();

            return mConfig.getSelectionInts();
        }

        //--------------------------------------------------------------------
        // Purpose:
        //     Find a node in the tree
        //
        // Notes:
        //     None.
        //--------------------------------------------------------------------
        TreeNode findNode(TreeNode node, string name)
        {
            for (int i = 0; i < node.Nodes.Count;i++)
            {
                if (name == node.Nodes[i].Text)
                {
                    return node.Nodes[i];
                }
            }

            return null;
        }

        //--------------------------------------------------------------------
        // Purpose:
        //     Loads the form
        //
        // Notes:
        //     None.
        //--------------------------------------------------------------------
        private void Selection_Load(object sender, EventArgs e)
        {
            lMovement.Text = mConfig.getMovementName(mMovement);

            // Draw the tree veiw for the movement
            tvOrch.BeginUpdate();
            tvOrch.Nodes.Clear();
            TreeNode allNodes = tvOrch.Nodes.Add("All");
            
            for (int i = 0; i < mFullOrch.getNumPlayers(); i++)
            {
                OrchPlayer curPlayer = mFullOrch.getPlayerFromID(i);
                if(0 != curPlayer.inMovement(mMovement))
                {
                    string type = curPlayer.getType();
                    string inst = curPlayer.getInst();

                    TreeNode childNode = findNode(allNodes, type);
                    if (null == childNode)
                    {
                        childNode = allNodes.Nodes.Add(allNodes.Text, type);
                    }
                    
                    TreeNode instNode = findNode(childNode, inst);
                    if (null == instNode)
                    {
                        childNode.Nodes.Add(childNode.Text, inst);
                    }
                    
                }
            }

            // Fill in the text boxes with what is saved from the config file
            tbEnd.Text = mConfig.getEndFrame().ToString();
            tbStart.Text = mConfig.getStartFrame().ToString();
            tbSelection.Text = mConfig.getSelection();

            tvOrch.EndUpdate();
        }

        //--------------------------------------------------------------------
        // Purpose:
        //     Saves everything to the config file.
        //
        // Notes:
        //     None.
        //--------------------------------------------------------------------
        private void bOK_Click(object sender, EventArgs e)
        {
            this.Close();

            mConfig.setEndFrame(tbEnd.Text);
            mConfig.setSelection(tbSelection.Text);
            mConfig.setStartFrame(tbStart.Text);
            mConfig.save();

            mResult = DialogResult.OK;
        }

        //--------------------------------------------------------------------
        // Purpose:
        //     Cancel the dialog.  
        //
        // Notes:
        //     This will set the dialog result that the call has to check
        //--------------------------------------------------------------------
        private void bCancel_Click(object sender, EventArgs e)
        {
            this.Close();
            mResult = DialogResult.Cancel;
        }

        //--------------------------------------------------------------------
        // Purpose:
        //     Add a group to the selection text box
        //
        // Notes:
        //     None.
        //--------------------------------------------------------------------
        private void bAddSelection_Click(object sender, EventArgs e)
        {
            mFullOrch.clearSelected();

            if (null != tvOrch.SelectedNode)
            {
                if ("All" == tvOrch.SelectedNode.Text)
                {
                    mFullOrch.selectAll(mMovement);
                }
                else
                {
                    if ("All" == tvOrch.SelectedNode.Parent.Name)
                    {
                        mFullOrch.selectInst(tvOrch.SelectedNode.Text);
                    }
                    else
                    {
                        mFullOrch.selectType(tvOrch.SelectedNode.Text);
                    }
                }
            }

            int[] selected = mFullOrch.getSelected();

            for(int i=0;i<selected.Length;i++)
            {
                tbSelection.Text += ", " + selected[i].ToString();
            }
        }

        //--------------------------------------------------------------------
        // Purpose:
        //     Clear the selection
        //
        // Notes:
        //     None.
        //--------------------------------------------------------------------
        private void bClear_Click(object sender, EventArgs e)
        {
            tbSelection.Text = "";
        }
    }
}
