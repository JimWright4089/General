using System;

namespace MarkovSent
{
	/// <summary>
	/// Summary description for Util.
	/// </summary>
	public class Util
	{
		public Util()
		{
			//
			// TODO: Add constructor logic here
			//
		}
		
		public static string[] SortString(string[] my_string)
		{
		  int    i,j;
		  string temp_string;
		  
		  for(i=0;i<my_string.GetLength(0);i++)
		  {
        for(j=0;j<my_string.GetLength(0)-i-1;j++)
        {
		      if(my_string[j].CompareTo(my_string[j+1])>0)
		      {
		        temp_string = my_string[j];
		        my_string[j] = my_string[j+1];
		        my_string[j+1] = temp_string;
		      }
        }		  
		  }
		  return my_string;
		}
	}
}
