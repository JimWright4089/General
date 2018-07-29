//----------------------------------------------------------------------------
//
//  $Workfile: EmployeeList.cs$
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
using System.Collections.Generic;
using System.IO;

namespace OSFBusinessSolutionsTest
{
    //----------------------------------------------------------------------------
    //  Class Declarations
    //----------------------------------------------------------------------------
    //
    // Class Name: EmployeeList
    // 
    // Purpose:
    //      This class handles the functions for dealing with a group of employees
    //
    //----------------------------------------------------------------------------
    class EmployeeList
    {
        //----------------------------------------------------------------------------
        //  Class Attributes 
        //----------------------------------------------------------------------------
        List<Employee> mEmployeeList = new List<Employee>();

        //--------------------------------------------------------------------
        // Purpose:
        //     Constructor.
        //
        // Notes:
        //     None.
        //--------------------------------------------------------------------
        public EmployeeList()
        {
            // No body
        }

        //--------------------------------------------------------------------
        // Purpose:
        //     Return the list
        //
        // Notes:
        //     None.
        //--------------------------------------------------------------------
        public List<Employee> GetList()
        {
            return mEmployeeList;
        }

        //--------------------------------------------------------------------
        // Purpose:
        //     Load in a CSV file
        //
        // Notes:
        //     return false if something went wrong
        //--------------------------------------------------------------------
        public bool Load(string filename)
        {
            bool returnValue = true;
            string line = "";

            try
            {
                TextReader csvFile = File.OpenText(filename);

                // Read in the file unless a parse error happened
                while (((line = csvFile.ReadLine()) != null) && (true == returnValue))
                {
                    Employee newEmployee = new Employee();
                    returnValue = newEmployee.Load(line);
                    mEmployeeList.Add(newEmployee);
                }
            }
            catch (Exception)
            {
                returnValue = false;
            }
            return returnValue;
        }

        //--------------------------------------------------------------------
        // Purpose:
        //     Load in a CSV file
        //
        // Notes:
        //     return false if something went wrong
        //--------------------------------------------------------------------
        public bool Save(string filename)
        {
            bool returnValue = true;

            try
            {
                System.IO.StreamWriter file = new System.IO.StreamWriter(filename);
                for (int i = 0; i < mEmployeeList.Count; i++)
                {
                    file.WriteLine(mEmployeeList[i].ToString());
                }
                file.Close();
            }
            catch (Exception)
            {
                returnValue = false;
            }

            return returnValue;
        }

        //--------------------------------------------------------------------
        // Purpose:
        //     Sort the list by name
        //
        // Notes:
        //     none
        //--------------------------------------------------------------------
        public void SortName()
        {
            mEmployeeList.Sort(
                delegate (Employee e1, Employee e2)
                {
                    return e1.Name.CompareTo(e2.Name);
                }
            );
        }

        //--------------------------------------------------------------------
        // Purpose:
        //     Sort the list by title
        //
        // Notes:
        //     none
        //--------------------------------------------------------------------
        public void SortTitle()
        {
            mEmployeeList.Sort(
                delegate (Employee e1, Employee e2)
                {
                    return e1.Title.CompareTo(e2.Title);
                }
            );
        }

        //--------------------------------------------------------------------
        // Purpose:
        //     Return a list filtered by type
        //
        // Notes:
        //     none
        //--------------------------------------------------------------------
        public EmployeeList GetFilteredList(int type)
        {
            EmployeeList returnList = new EmployeeList();

            for (int i = 0; i < mEmployeeList.Count; i++)
            {
                // The type or if the filter is all then return everything
                if ((type == mEmployeeList[i].Type) || (OSFBusinessSolutionsTest.DEPARTMENT_ALL == type))
                {
                    returnList.Add(mEmployeeList[i]);
                }
            }
            return returnList;
        }

        //--------------------------------------------------------------------
        // Purpose:
        //     Add an employee
        //
        // Notes:
        //     none
        //--------------------------------------------------------------------
        public void Add(Employee newEmployee)
        {
            mEmployeeList.Add(newEmployee);
        }
    }
}
