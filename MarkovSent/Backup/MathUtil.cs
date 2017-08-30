using System;

namespace MarkovSent
{
	/// <summary>
	/// Summary description for MathUtil.
	/// </summary>
	public class MathUtil
	{
	  const int MATH_RETURN_OK                   = 0;
	  const int MATH_RETURN_MATIX_NOT_COMPATABLE = -1;
	
	  private static int m_math_error;
	
		public MathUtil()
		{
		}
		
		static public int GetMathError()
		{
		  return m_math_error;
		}
		
		static public float[,] MatrixMultiplication(float[,] matrix_a, float[,] matrix_b)
		{
		  int i,j,m;
		  float[,] new_matrix;
		  
		  if((matrix_a.GetLength(0) != matrix_b.GetLength(1)) || (matrix_a.GetLength(1) != matrix_b.GetLength(0)))
		  {
		    m_math_error = MATH_RETURN_MATIX_NOT_COMPATABLE;
		    return matrix_a;
		  }
		
		  new_matrix = new float[matrix_b.GetLength(0),matrix_a.GetLength(1)];
		
		  for(i=0;i<matrix_a.GetLength(1);i++)
		  {
        for(j=0;j<matrix_b.GetLength(0);j++)
        {
		      new_matrix[j,i] = 0;
		      
		      for(m=0;m<matrix_a.GetLength(0);m++)
		      {
            new_matrix[j,i] += matrix_a[m,i]*matrix_b[j,m];
          }
        }
		  
		  }
		
		  return new_matrix;
		}
	}
}
