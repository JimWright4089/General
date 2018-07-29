//----------------------------------------------------------------------------
//
//  $Workfile: Employee.cs$
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

namespace OSFBusinessSolutionsTest
{
    //----------------------------------------------------------------------------
    //  Class Declarations
    //----------------------------------------------------------------------------
    //
    // Class Name: Employee
    // 
    // Purpose:
    //      This class handles the functions for dealing with a single employee
    //
    //----------------------------------------------------------------------------
    class Employee
    {
        //----------------------------------------------------------------------------
        //  Class Consts
        //----------------------------------------------------------------------------
        const int FIELD_TYPE_INDEX = 0;
        const int FIELD_NAME_INDEX = 1;
        const int FIELD_TITLE_INDEX = 2;
        const int FIELD_DEPARTMENT_INDEX = 3;

        //----------------------------------------------------------------------------
        //  Class Attributes 
        //----------------------------------------------------------------------------
        int mType = 0;
        string mName = "";
        string mTitle = "";
        string mDepartment = "";

        //--------------------------------------------------------------------
        // Purpose:
        //     Constructor.
        //
        // Notes:
        //     None.
        //--------------------------------------------------------------------
        public Employee()
        {
            // No body
        }

        //--------------------------------------------------------------------
        // Purpose:
        //     Takes a line from the CSV file and parses it into the employee
        //
        // Notes:
        //     Returns false is the parse did not work
        //--------------------------------------------------------------------
        public bool Load(string line)
        {
            bool returnValue = true;

            try
            {
                char[] deliminators = { ',' };
                string[] lineParts = line.Split(deliminators);

                // Set the attributes
                mType = int.Parse(lineParts[FIELD_TYPE_INDEX]);
                // Trim the strings
                mName = lineParts[FIELD_NAME_INDEX];
                mName = mName.Trim();
                mTitle = lineParts[FIELD_TITLE_INDEX];
                mTitle = mTitle.Trim();
                mDepartment = lineParts[FIELD_DEPARTMENT_INDEX];
                mDepartment = mDepartment.Trim();
            }
            catch (Exception)
            {
                returnValue = false;
            }

            return returnValue;
        }

        //--------------------------------------------------------------------
        // Purpose:
        //     Override the ToString method
        //
        // Notes:
        //     none
        //--------------------------------------------------------------------
        public override string ToString()
        {
            string returnString = "";

            returnString = mType.ToString();
            returnString += ", ";
            returnString += mName;
            returnString += ", ";
            returnString += mTitle;
            returnString += ", ";
            returnString += mDepartment;

            return returnString;
        }

        //--------------------------------------------------------------------
        // Purpose:
        //     Get/Set Department
        //
        // Notes:
        //     none
        //--------------------------------------------------------------------
        public string Department
        {
            get
            {
                return mDepartment;
            }
            set
            {
                mDepartment = value;
            }
        }

        //--------------------------------------------------------------------
        // Purpose:
        //     Get/Set Name
        //
        // Notes:
        //     none
        //--------------------------------------------------------------------
        public string Name
        {
            get
            {
                return mName;
            }
            set
            {
                mName = value;
            }
        }

        //--------------------------------------------------------------------
        // Purpose:
        //     Get/Set Title
        //
        // Notes:
        //     none
        //--------------------------------------------------------------------
        public string Title
        {
            get
            {
                return mTitle;
            }
            set
            {
                mTitle = value;
            }
        }

        //--------------------------------------------------------------------
        // Purpose:
        //     Get/Set Type
        //
        // Notes:
        //     none
        //--------------------------------------------------------------------
        public int Type
        {
            get
            {
                return mType;
            }
            set
            {
                mType = value;
            }
        }
    }
}
