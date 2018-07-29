//----------------------------------------------------------------------------
//
//  $Workfile: OSFBusinessSolutionsTest.cs$
//
//  $Revision: X$
//
//  Project:    OSF Business Solutions Test
//
//                            Copyright (c) 2017
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
using System.Collections.Generic;

namespace OSFBusinessSolutionsTest
{
    //----------------------------------------------------------------------------
    //  Class Declarations
    //----------------------------------------------------------------------------
    //
    // Class Name: OSFBusinessSolutionsTest
    // 
    // Purpose:
    //      This class is the User Interface for the employee list
    //
    //----------------------------------------------------------------------------
    public partial class OSFBusinessSolutionsTest : Form
    {
        //----------------------------------------------------------------------------
        //  Class Consts
        //----------------------------------------------------------------------------
        public const int DEPARTMENT_IT = 100;
        public const int DEPARTMENT_MARKETING = 200;
        public const int DEPARTMENT_ADMIN = 300;
        public const int DEPARTMENT_BOX_OFFICE = 400;
        public const int DEPARTMENT_ALL = -1;

        //----------------------------------------------------------------------------
        //  Class Attributes 
        //----------------------------------------------------------------------------
        EmployeeList mEmployeeList = new EmployeeList();
        EmployeeList mFilteredEmployees = new EmployeeList();
        int mDepartmentFilter = DEPARTMENT_IT;

        //--------------------------------------------------------------------
        // Purpose:
        //     Load in a CSV file
        //
        // Notes:
        //     return false if something went wrong
        //--------------------------------------------------------------------
        public OSFBusinessSolutionsTest()
        {
            InitializeComponent();
        }

        //--------------------------------------------------------------------
        // Purpose:
        //     Handle Exit
        //
        // Notes:
        //     none
        //--------------------------------------------------------------------
        private void exitToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        //--------------------------------------------------------------------
        // Purpose:
        //     Handle Open
        //
        // Notes:
        //     none
        //--------------------------------------------------------------------
        private void openToolStripMenuItem_Click(object sender, EventArgs e)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();

            openFileDialog.Filter = "CSV Files (.csv)|*.csv|All Files (*.*)|*.*";
            openFileDialog.FilterIndex = 1;
            openFileDialog.Multiselect = false;

            DialogResult dialogResult = openFileDialog.ShowDialog();

            if (DialogResult.OK == dialogResult)
            {
                if (false == mEmployeeList.Load(openFileDialog.FileName))
                {
                    MessageBox.Show("There was a problem in reading the CSV file \""+ 
                            openFileDialog.FileName + 
                            "\".  Please check this file to see if it formatted correctly");
                }
                else
                {
                    DisplayRows();
                }
            }
        }

        //--------------------------------------------------------------------
        // Purpose:
        //     Save the List
        //
        // Notes:
        //     none
        //--------------------------------------------------------------------
        private void saveToolStripMenuItem_Click(object sender, EventArgs e)
        {
            SaveFileDialog saveFileDialog = new SaveFileDialog();

            saveFileDialog.Filter = "CSV Files (.CSV)|*.csv";
            saveFileDialog.FilterIndex = 1;
            saveFileDialog.RestoreDirectory = true;

            if (saveFileDialog.ShowDialog() == DialogResult.OK)
            {
                if (false == mFilteredEmployees.Save(saveFileDialog.FileName))
                {
                    MessageBox.Show("There was a problem in writting the CSV file \"" +
                            saveFileDialog.FileName +
                            "\".");
                }
            }
        }

        //--------------------------------------------------------------------
        // Purpose:
        //     Handle Help
        // Notes:
        //     none
        //--------------------------------------------------------------------
        private void aboutToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Help helpDialog = new Help();

            helpDialog.ShowDialog();
        }

        //--------------------------------------------------------------------
        // Purpose:
        //     Display the rows to the screen.
        //
        // Notes:
        //     none
        //--------------------------------------------------------------------
        private void DisplayRows()
        {
            // Sort the list depending on the radio button
            if(true == rbTitle.Checked)
            {
                mEmployeeList.SortTitle();
            }
            else
            {
                mEmployeeList.SortName();
            }

            // Get the filtered list and return the actual list
            mFilteredEmployees = mEmployeeList.GetFilteredList(mDepartmentFilter);
            List<Employee> employees = mFilteredEmployees.GetList();

            // Display the list
            lvFilteredList.Items.Clear();
            for (int i = 0; i < employees.Count; i++)
            {
                ListViewItem item = new ListViewItem(employees[i].Type.ToString());
                item.SubItems.Add(employees[i].Name);
                item.SubItems.Add(employees[i].Title);
                item.SubItems.Add(employees[i].Department);
                lvFilteredList.Items.Add(item);
            }
        }

        //--------------------------------------------------------------------
        // Purpose:
        //     Radio Button Sort Name
        //
        // Notes:
        //     none
        //--------------------------------------------------------------------
        private void rbName_CheckedChanged(object sender, EventArgs e)
        {
            DisplayRows();
        }

        //--------------------------------------------------------------------
        // Purpose:
        //     Radio Button Sort Title
        //
        // Notes:
        //     none
        //--------------------------------------------------------------------
        private void rbTitle_CheckedChanged(object sender, EventArgs e)
        {
            DisplayRows();
        }

        //--------------------------------------------------------------------
        // Purpose:
        //     Filter radio Button.
        //
        // Notes:
        //     none
        //--------------------------------------------------------------------
        private void rbIT_CheckedChanged(object sender, EventArgs e)
        {
            mDepartmentFilter = DEPARTMENT_IT;
            DisplayRows();
        }

        //--------------------------------------------------------------------
        // Purpose:
        //     Filter radio Button.
        //
        // Notes:
        //     none
        //--------------------------------------------------------------------
        private void rbMarketing_CheckedChanged(object sender, EventArgs e)
        {
            mDepartmentFilter = DEPARTMENT_MARKETING;
            DisplayRows();
        }

        //--------------------------------------------------------------------
        // Purpose:
        //     Filter radio Button.
        //
        // Notes:
        //     none
        //--------------------------------------------------------------------
        private void rbAdmin_CheckedChanged(object sender, EventArgs e)
        {
            mDepartmentFilter = DEPARTMENT_ADMIN;
            DisplayRows();
        }

        //--------------------------------------------------------------------
        // Purpose:
        //     Filter radio Button.
        //
        // Notes:
        //     none
        //--------------------------------------------------------------------
        private void rbBoxOffice_CheckedChanged(object sender, EventArgs e)
        {
            mDepartmentFilter = DEPARTMENT_BOX_OFFICE;
            DisplayRows();
        }

        //--------------------------------------------------------------------
        // Purpose:
        //     Filter radio Button.
        //
        // Notes:
        //     none
        //--------------------------------------------------------------------
        private void rbAll_CheckedChanged(object sender, EventArgs e)
        {
            mDepartmentFilter = DEPARTMENT_ALL;
            DisplayRows();
        }
    }
}
