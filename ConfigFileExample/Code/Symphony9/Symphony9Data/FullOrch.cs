//----------------------------------------------------------------------------
//
//  $Workfile: FullOrch.cs$
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
using System.Collections.Generic;
using System.IO;

namespace Symphony9Data
{
    //----------------------------------------------------------------------------
    //  Class Declarations
    //----------------------------------------------------------------------------
    //
    // Class Name: FullOrch
    // 
    // Purpose:
    //      This stores and retrives data for the Orchestra
    //
    //----------------------------------------------------------------------------
    public class FullOrch
    {
        //----------------------------------------------------------------------------
        //  Class Attributes 
        //----------------------------------------------------------------------------
        private Config mConfig = new Config();
        private List<OrchPlayer> mPlayers = new List<OrchPlayer>();
        private string mVersion = "0.0";
        private string mFileDate = "0/0/0000";
        private string mMD5 = "";
        private string mFileName = "";

        //--------------------------------------------------------------------
        // Purpose:
        //     Cnstructor of the orchestra
        //
        // Notes:
        //     This class does not save anything to the config file, so
        //     it does not need the main config file
        //--------------------------------------------------------------------
        public FullOrch()
        {
            mFileName = mConfig.getFullOrchFile();
            StreamReader file = File.OpenText(mFileName);

            mMD5 = file.GetHashCode().ToString();

            string line = file.ReadLine();

            char[] delimiterChars = { ',' };

            string[] items = line.Split(delimiterChars);
            mVersion = items[1];
            mFileDate = File.GetLastWriteTime(mFileName).ToString();

            //Read header Line
            line = file.ReadLine();

            line = file.ReadLine();

            while (!file.EndOfStream)
            {
                OrchPlayer newPlayer = new OrchPlayer(line);
                mPlayers.Add(newPlayer);
                line = file.ReadLine();
            }
            file.Close();
        }

        //--------------------------------------------------------------------
        // Purpose:
        //     Return the name of the orchestra file
        //
        // Notes:
        //     None
        //--------------------------------------------------------------------
        public string getFileName()
        {
            return mFileName;
        }

        //--------------------------------------------------------------------
        // Purpose:
        //     Return the version of the orchestra file
        //
        // Notes:
        //     None
        //--------------------------------------------------------------------
        public string getVersion()
        {
            return mVersion;
        }

        //--------------------------------------------------------------------
        // Purpose:
        //     Return the hash of the orchestra file
        //
        // Notes:
        //     None
        //--------------------------------------------------------------------
        public string getMD5()
        {
            return mMD5;
        }

        //--------------------------------------------------------------------
        // Purpose:
        //     Return the date of the orchestra file
        //
        // Notes:
        //     None
        //--------------------------------------------------------------------
        public string getFileDate()
        {
            return mFileDate;
        }

        //--------------------------------------------------------------------
        // Purpose:
        //     Writes the orchestra file info to another file
        //
        // Notes:
        //     None
        //--------------------------------------------------------------------
        public void writeOrchInfo(StreamWriter outfile)
        {
            outfile.WriteLine("Orch File,Name," + mFileName + ",Date," + mFileDate + ",md5," + mMD5 + ",version," + mVersion);
        }

        //--------------------------------------------------------------------
        // Purpose:
        //     Clear the selection flag for the players
        //
        // Notes:
        //     None
        //--------------------------------------------------------------------
        public void clearSelected()
        {
            for (int i = 0; i < mPlayers.Count; i++)
            {
                mPlayers[i].setSelected(false);
            }
        }

        //--------------------------------------------------------------------
        // Purpose:
        //     Return the adj for the player
        //
        // Notes:
        //     None
        //--------------------------------------------------------------------
        public string getAdj(int num)
        {
            if ((num >= 0) && (num < mPlayers.Count))
            {
                return mPlayers[num].getAdj();
            }
            return "";
        }

        //--------------------------------------------------------------------
        // Purpose:
        //     Return the inst name for the player
        //
        // Notes:
        //     None
        //--------------------------------------------------------------------
        public string getInstStringfromID(int num)
        {
            for (int i = 0; i < mPlayers.Count; i++)
            {
                if (num == mPlayers[i].getID())
                {
                    return mPlayers[i].getInst();
                }
            }
            return "";
        }

        //--------------------------------------------------------------------
        // Purpose:
        //     Returns what the player should do in the movement
        //
        // Notes:
        //     0 - for not in the movement
        //     1 - for in the movement but waiting
        //     2 - for in themovement and playing
        //--------------------------------------------------------------------
        public int getMovefromID(int num, int move)
        {
            if (Config.MOVEMENT05 == move)
            {
                return 1;
            }

            for (int i = 0; i < mPlayers.Count; i++)
            {
                if (num == mPlayers[i].getID())
                {
                    return mPlayers[i].inMovement(move);
                }
            }
            return 0;
        }

        //--------------------------------------------------------------------
        // Purpose:
        //     Get the chair number for the player
        //
        // Notes:
        //     None
        //--------------------------------------------------------------------
        public int getNumfromID(int num)
        {
            for (int i = 0; i < mPlayers.Count; i++)
            {
                if (num == mPlayers[i].getID())
                {
                    return mPlayers[i].getNum();
                }
            }
            return 0;
        }

        //--------------------------------------------------------------------
        // Purpose:
        //     Get the number of players in the ochestra
        //
        // Notes:
        //     None
        //--------------------------------------------------------------------
        public int getNumPlayers()
        {
            return mPlayers.Count;
        }

        //--------------------------------------------------------------------
        // Purpose:
        //     Return the player object for a player
        //
        // Notes:
        //     None
        //--------------------------------------------------------------------
        public OrchPlayer getPlayerFromID(int num)
        {
            if ((num >= 0) && (num < mPlayers.Count))
            {
                return mPlayers[num];
            }
            return null;
        }

        //--------------------------------------------------------------------
        // Purpose:
        //     Return the string represtenting the player
        //
        // Notes:
        //     None
        //--------------------------------------------------------------------
        public string getPlayerString(int num)
        {
            if ((num >= 0) && (num < mPlayers.Count))
            {
                return mPlayers[num].ToString();
            }
            return "";
        }

        //--------------------------------------------------------------------
        // Purpose:
        //     Return the string represtenting the player
        //
        // Notes:
        //     None
        //--------------------------------------------------------------------
        public string getPlayerStringfromID(int num)
        {
            for (int i = 0; i < mPlayers.Count; i++)
            {
                if (num == mPlayers[i].getID())
                {
                    return mPlayers[i].ToString();
                }
            }

            return "";
        }

        //--------------------------------------------------------------------
        // Purpose:
        //     Get the random seed for the player
        //
        // Notes:
        //     None
        //--------------------------------------------------------------------
        public int getSeedfromID(int num)
        {
            for (int i = 0; i < mPlayers.Count; i++)
            {
                if (num == mPlayers[i].getID())
                {
                    return mPlayers[i].getSeed();
                }
            }
            return 0;
        }

        //--------------------------------------------------------------------
        // Purpose:
        //     Return the selected array
        //
        // Notes:
        //     None
        //--------------------------------------------------------------------
        public int[] getSelected()
        {
            int[] array = null;
            int count = 0;
            int index = 0;

            for (int i = 0; i < mPlayers.Count; i++)
            {
                if (true == mPlayers[i].getSelected())
                {
                    count++;
                }
            }

            array = new int[count];
            for (int i = 0; i < mPlayers.Count; i++)
            {
                if (true == mPlayers[i].getSelected())
                {
                    array[index] = mPlayers[i].getID();
                    index++;
                }
            }

            return array;
        }

        //--------------------------------------------------------------------
        // Purpose:
        //     Get the instrument type for the player
        //
        // Notes:
        //     None
        //--------------------------------------------------------------------
        public string getTypeStringfromID(int num)
        {
            for (int i = 0; i < mPlayers.Count; i++)
            {
                if (num == mPlayers[i].getID())
                {
                    return mPlayers[i].getType();
                }
            }
            return "";
        }

        //--------------------------------------------------------------------
        // Purpose:
        //     Select the full orchestra
        //
        // Notes:
        //     None
        //--------------------------------------------------------------------
        public void selectAll()
        {
            for (int i = 0; i < mPlayers.Count; i++)
            {
                mPlayers[i].setSelected(true);
            }
        }

        //--------------------------------------------------------------------
        // Purpose:
        //     Select the full orchestra who are playing in the movement
        //
        // Notes:
        //     None
        //--------------------------------------------------------------------
        public void selectAll(int move)
        {
            for (int i = 0; i < mPlayers.Count; i++)
            {
                if (0 != mPlayers[i].inMovement(move))
                {
                    mPlayers[i].setSelected(true);
                }
            }
        }

        //--------------------------------------------------------------------
        // Purpose:
        //     Select the all of the instruments in the orch
        //
        // Notes:
        //     None
        //--------------------------------------------------------------------
        public void selectInst(string inst)
        {
            for (int i = 0; i < mPlayers.Count; i++)
            {
                if (inst == mPlayers[i].getInst())
                {
                    mPlayers[i].setSelected(true);
                }
            }
        }

        //--------------------------------------------------------------------
        // Purpose:
        //     Select the instrument types who are playing in the movement
        //
        // Notes:
        //     None
        //--------------------------------------------------------------------
        public void selectType(string type)
        {
            for (int i = 0; i < mPlayers.Count; i++)
            {
                if (type == mPlayers[i].getType())
                {
                    mPlayers[i].setSelected(true);
                }
            }
        }
    }

    //----------------------------------------------------------------------------
    //  Class Declarations
    //----------------------------------------------------------------------------
    //
    // Class Name: OrchPlayer
    // 
    // Purpose:
    //      Storage class for a player
    //
    //----------------------------------------------------------------------------
    public class OrchPlayer
    {
        //----------------------------------------------------------------------------
        //  Class Attributes 
        //----------------------------------------------------------------------------
        private int mID;            // The number ID of the players
        private string mInstName;   // The fields to select the player in a max file
        private int mNum;
        private int mPart;
        private string mType;       // What instrument are they playing

        private string mAdj;       // The adjective of the player
        private int mSeed;         // the random seed of the player

        private int mMove01;       // Which movement and what do they do in the movement
        private int mMove02;
        private int mMove03;
        private int mMove04;

        private int mClothsColor;  // Display properties of the player
        private string mFace;
        private int mHair;
        private int mHairColor;
        private string mSex;
        private int mSkin;

        private bool mSelected;    // If the player in currently in the selection

        //--------------------------------------------------------------------
        // Purpose:
        //     Constructor, the line is the line from the file.
        //
        // Notes:
        //     None.
        //--------------------------------------------------------------------
        public OrchPlayer(string text)
        {
            char[] delimiterChars = { ' ', ',', '.', ':', '\t' };

            string[] items = text.Split(delimiterChars);

            int.TryParse(items[0], out mID);
            mInstName = items[1];
            int.TryParse(items[2], out mPart);
            int.TryParse(items[3], out mNum);
            int.TryParse(items[4], out mSeed);
            mType = items[5];
            int.TryParse(items[6], out mMove01);
            int.TryParse(items[7], out mMove02);
            int.TryParse(items[8], out mMove03);
            int.TryParse(items[9], out mMove04);
            mSex = items[10];
            mFace = items[11];
            int.TryParse(items[12], out mHair);
            int.TryParse(items[13], out mSkin);
            int.TryParse(items[14], out mHairColor);
            int.TryParse(items[15], out mClothsColor);
            mAdj = items[16];
        }

        //--------------------------------------------------------------------
        // Purpose:
        //     Returns the Id of the player
        //
        // Notes:
        //     None.
        //--------------------------------------------------------------------
        public int getID()
        {
            return mID;
        }

        //--------------------------------------------------------------------
        // Purpose:
        //     Returns the 4 char instrument ID
        //
        // Notes:
        //     None.
        //--------------------------------------------------------------------
        public string getInst()
        {
            return mInstName;
        }

        //--------------------------------------------------------------------
        // Purpose:
        //     Returns the number of the player
        //
        // Notes:
        //     None.
        //--------------------------------------------------------------------
        public int getNum()
        {
            return mNum;
        }

        //--------------------------------------------------------------------
        // Purpose:
        //     Returns the type of the player
        //
        // Notes:
        //     None.
        //--------------------------------------------------------------------
        public string getType()
        {
            return mType;
        }

        //--------------------------------------------------------------------
        // Purpose:
        //     Returns the ToString for the player
        //
        // Notes:
        //     None.
        //--------------------------------------------------------------------
        public override string ToString()
        {
            return mInstName + "-" + mPart.ToString() + "-" + mNum.ToString();
        }

        //--------------------------------------------------------------------
        // Purpose:
        //     Get the adjative for the player
        //
        // Notes:
        //     None.
        //--------------------------------------------------------------------
        public string getAdj()
        {
            return mAdj;
        }

        //--------------------------------------------------------------------
        // Purpose:
        //     Get the seed for the player
        //
        // Notes:
        //     None.
        //--------------------------------------------------------------------
        public int getSeed()
        {
            return mSeed;
        }

        //--------------------------------------------------------------------
        // Purpose:
        //     Return if the player is selected
        //
        // Notes:
        //     None.
        //--------------------------------------------------------------------
        public bool getSelected()
        {
            return mSelected;
        }

        //--------------------------------------------------------------------
        // Purpose:
        //     Get what the player does in a movement
        //
        // Notes:
        //     None.
        //--------------------------------------------------------------------
        public int inMovement(int move)
        {
            switch (move)
            {
                case Symphony9Data.Config.MOVEMENT01:
                    return mMove01;

                case Symphony9Data.Config.MOVEMENT02:
                    return mMove02;

                case Symphony9Data.Config.MOVEMENT03:
                    return mMove03;

                case Symphony9Data.Config.MOVEMENT04:
                    return mMove04;
            }
            return -1;
        }

        //--------------------------------------------------------------------
        // Purpose:
        //     Sets the selected for the player 
        //
        // Notes:
        //     None.
        //--------------------------------------------------------------------
        public void setSelected(bool selected)
        {
            mSelected = selected;
        }
    }
}