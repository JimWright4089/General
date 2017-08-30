using System;
using System.Collections;
using System.Collections.Specialized;
using System.Text;

namespace MarkovSent
{
  public class CMarkovSentences
  {
    /******************************************************************
    **
    ** Const
    ** 
    *******************************************************************/
    const string CONST_END_OF_STRING     = "<EOS>";
    const string CONST_START_OF_STRING   = "<BOS>";
    const string CONST_NO_STRING_ENTERED = "<No strings entered>";
    
    const int    RETURN_OK               = 0;
    const int    RETURN_STRING_NOT_FOUND = -1;
    const int    RETURN_NO_LETTER_FOUND  = -2;

    /******************************************************************
    **
    ** Attributes
    ** 
    *******************************************************************/
    private static CMarkovSentences m_single_instance;
    private static Object           m_instance_lock = typeof(CMarkovSentences);
    private ArrayList               m_strings       = new ArrayList(); 
    private IDictionary             m_letters       = new Hashtable();

    /******************************************************************
    ** Constructor
    **
    ** Clear the string list and the letter dictionary
    ** 
    *******************************************************************/
    private CMarkovSentences()
    {
      IDictionary     letter_entries;

      m_strings.Clear();
      m_letters.Clear();

      /*
      ** We need to add the EOS entry to the letter array
      */
      letter_entries = new Hashtable();
      letter_entries.Add(CONST_END_OF_STRING,1);
      m_letters.Add(CONST_END_OF_STRING,letter_entries);

    }

    /******************************************************************
    ** AddALetter
    **
    ** Add a letter or add 1 to the count of a letter/next letter 
    ** hash table.   
    ** 
    *******************************************************************/
    private void AddALetter(string the_letter, string the_next_letter)
    {
      IDictionary     letter_entries;
      int              letter_count;

      /*
      ** Find the letter that we are on
      */
      letter_entries = (Hashtable) m_letters[the_letter];

      if(letter_entries != null)
      {
        /*
        ** We had an entry for the letter we are on check to see
        ** if we have an entry on the next letter
        */
        if(letter_entries[the_next_letter] != null)
        {
          /*
          ** Add 1 to the count of letter/next letter pair
          */
          letter_count = (int) letter_entries[the_next_letter];
          letter_entries[the_next_letter] = (letter_count+1);
        }
        else
        {
          /*
          ** Add a new next letter entry
          */
          letter_entries.Add(the_next_letter,1);
        }
      }
      else
      {
        /*
        ** We need to add a new entry
        */
        letter_entries = new Hashtable();
        letter_entries.Add(the_next_letter,1);
        m_letters.Add(the_letter,letter_entries);
      }

    }

    /******************************************************************
    ** AddLetters
    **
    ** Spin through the string adding pairs to the letters dictionary 
    **    
    ** 
    *******************************************************************/
    private void AddLetters(string new_string)
    {
      string cur_letter;
      string next_letter;
      int    i;

      /*
      ** Begin with the begin of string "char"
      */
      cur_letter = CONST_START_OF_STRING;
      for(i=0;i<new_string.Length;i++)
      {
        next_letter = new_string[i].ToString();
        AddALetter(cur_letter, next_letter);
        cur_letter = next_letter;
      }

      /*
      ** The last pair has to have the end of string tag as the next letter
      */
      next_letter = CONST_END_OF_STRING;
      AddALetter(cur_letter, next_letter);
    }

    /******************************************************************
    ** AddString
    **
    ** The method that is called to add a string and update our
    ** letter dictionary   
    ** 
    *******************************************************************/
    public void AddString(string new_string)
    {
      m_strings.Add(new_string);
      AddLetters(new_string);
    }

    /******************************************************************
    ** BuildProbArray
    **
    ** Build and return the prob array for the next letter
    ** 
    *******************************************************************/
    private float[,] BuildProbArray()
    {
      int          max_number_of_letters = m_letters.Count;
      float[,]     letter_array = new float[max_number_of_letters,max_number_of_letters];
      int          i,j;
      string[]     letter_str_array;
      IDictionary  letter_entries;
      int          total_letter_count;
      int          letter_count;

      letter_str_array = GetProbMatrixKey();

      for(i=0;i<letter_str_array.GetLength(0);i++)
      {
        if(m_letters[letter_str_array[i]] != null)
        {
          letter_entries = (Hashtable) m_letters[letter_str_array[i]];
          total_letter_count = 0;
          IDictionaryEnumerator letter_entries_enum = letter_entries.GetEnumerator();

          while(letter_entries_enum.MoveNext())
          {
            total_letter_count += (int)letter_entries_enum.Value;
          }

          for(j=0;j<letter_str_array.GetLength(0);j++)
          {
            if(letter_entries[letter_str_array[j]] != null)
            {
              /*
              ** Find the fraction
              */
              letter_count = (int) letter_entries[letter_str_array[j]];
              if(total_letter_count == 0)
              {
                letter_array[i,j] = (float)0.0;
              }
              else
              {
                letter_array[i,j] = (float)letter_count/(float)total_letter_count;
              }
            }
          }
        }
      }
      return letter_array;
    }

    /******************************************************************
    ** GenerateString
    **
    ** The method builds a fun sentance from the letter dictionary
    ** not really anything to do with a Markov prob' but fun.
    ** 
    *******************************************************************/
    public string GenerateString()
    {
      string      the_return_string="";
      string      cur_letter;
      IDictionary  letter_entries;
      int         total_letter_count;
      int         rand_letter;
      Random      rand_generator = new Random();

      /*
      ** If there isn't any strings we can't build a sentence
      */
      if(m_strings.Count == 0)
      {
        the_return_string = CONST_NO_STRING_ENTERED;
      }
      else
      {
        /*
        ** Start at the begining and run until we hit the end of the string
        */
        cur_letter = CONST_START_OF_STRING;
        while(cur_letter != CONST_END_OF_STRING)
        {
          /*
          ** We have at least one string, find the current letter entry
          ** count the total number of next letters that we could have
          */
          letter_entries = (Hashtable) m_letters[cur_letter];
          total_letter_count = 0;
          IDictionaryEnumerator letter_entries_enum = letter_entries.GetEnumerator();

          while(letter_entries_enum.MoveNext())
          {
            total_letter_count += (int)letter_entries_enum.Value;
          }

          /*
          ** Take a rand char from the total and find that char
          */
          rand_letter = rand_generator.Next(total_letter_count)+1;
          letter_entries_enum.Reset();
          while(letter_entries_enum.MoveNext())
          {
            rand_letter -= (int)letter_entries_enum.Value;
            if(rand_letter < 1)
            {
              cur_letter = (string)letter_entries_enum.Key;

              /*
              ** Don't add the end of string char
              */
              if(cur_letter != CONST_END_OF_STRING)
              {
                the_return_string += cur_letter;
              }
              break;
            }
          }
        }
      }
      return the_return_string;
    }

    /******************************************************************
    ** GetInstance
    **
    ** The singleton method
    ** 
    *******************************************************************/
    public static CMarkovSentences GetInstance()
    {
      lock (m_instance_lock)
      {
        if (m_single_instance == null)
        {
          m_single_instance = new CMarkovSentences();
        }
        return m_single_instance;
      }
    }

    /******************************************************************
    ** GetProbMatrix
    **
    ** Get the probability matrix for a iteration
    ** 
    *******************************************************************/
    public float[,] GetProbMatrix(int iteration)
    {
      float[,] letter_array      = BuildProbArray();
      float[,] letter_array_mult = BuildProbArray();
      int i;
      
      for(i=0;i<iteration;i++)
      {
        letter_array = MathUtil.MatrixMultiplication(letter_array,letter_array_mult);
      }

      return letter_array;
    }

    /******************************************************************
    ** GetProbMatrixKey
    **
    ** Get the probability matrix key (letters)
    ** 
    *******************************************************************/
    public string[] GetProbMatrixKey()
    {
      int     max_number_of_letters = m_letters.Count;
      string[] letter_array = new string[max_number_of_letters];
      int i=0;

      IDictionaryEnumerator letter_entries_enum = m_letters.GetEnumerator();

      while(letter_entries_enum.MoveNext())
      {
        letter_array[i] = letter_entries_enum.Key.ToString();
        i++;
      }

      letter_array = Util.SortString(letter_array);

      return letter_array;
    }

    /******************************************************************
    ** RemoveALetter
    **
    ** Remove a letter/next letter pair.  We can get the count down to 
    ** zero and leave the pair in.
    ** 
    *******************************************************************/
    private int RemoveALetter(string the_letter, string the_next_letter)
    {
      IDictionary     letter_entries;
      int              letter_count;
      int             return_value=RETURN_OK;

      /*
      ** Get the entry, if the entries are not there return an error
      */
      letter_entries = (Hashtable) m_letters[the_letter];
      if(letter_entries != null)
      {
        if(letter_entries[the_next_letter] != null)
        {
          /*
          ** found and reduce the count to zero (and not negative)
          */
          letter_count = (int) letter_entries[the_next_letter];
          if(letter_count > 0)
          {
            letter_entries[the_next_letter] = (letter_count-1);
          }
          else
          {
            return_value = RETURN_NO_LETTER_FOUND;
          }
        }
        else
        {
          return_value = RETURN_NO_LETTER_FOUND;
        }
      }
      else
      {
        return_value = RETURN_NO_LETTER_FOUND;
      }
      return return_value;
    }

    /******************************************************************
    ** RemoveLetters
    **
    ** Spin through the string removing letter/next letter pairs.
    ** 
    *******************************************************************/
    private void RemoveLetters(string new_string)
    {
      string cur_letter;
      string next_letter;
      int    i;

      cur_letter = CONST_START_OF_STRING;
      for(i=0;i<new_string.Length;i++)
      {
        next_letter = new_string[i].ToString();
        RemoveALetter(cur_letter, next_letter);
        cur_letter = next_letter;
      }

      next_letter = CONST_END_OF_STRING;
      RemoveALetter(cur_letter, next_letter);
    }

    /******************************************************************
    ** RemoveString
    **
    ** Public method for removing a string and letter/next letter pair.
    ** 
    *******************************************************************/
    public int RemoveString(string remove_string)
    {
      int i;
      bool found = false;

      for(i=0;i<m_strings.Count;i++)
      {
        /*
        ** Make sure that string was processed before
        */
        if(remove_string == (string)m_strings[i])
        {
          found = true;
          RemoveLetters(remove_string);
          m_strings.RemoveAt(i);
          break;
        }
      }

      if(false == found)
      {
        return RETURN_STRING_NOT_FOUND;
      }
      return RETURN_OK;
    }
  }
}
