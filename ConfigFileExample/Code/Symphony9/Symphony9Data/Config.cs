//----------------------------------------------------------------------------
//
//  $Workfile: Config.cs$
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
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;

namespace Symphony9Data
{
    //----------------------------------------------------------------------------
    //  Class Declarations
    //----------------------------------------------------------------------------
    //
    // Class Name: Config
    // 
    // Purpose:
    //      This stores and retrives configuration data from the xml file
    //
    //----------------------------------------------------------------------------
    public class Config
    {
        //----------------------------------------------------------------------------
        //  Class Public Consts
        //----------------------------------------------------------------------------
        public const int MOVEMENT01 = 0;  // the movement numbers
        public const int MOVEMENT02 = 1;
        public const int MOVEMENT03 = 2;
        public const int MOVEMENT04 = 3;
        public const int MOVEMENT05 = 4;

        //----------------------------------------------------------------------------
        //  Class Consts
        //----------------------------------------------------------------------------
        const string CONFIG_FILE_NAME = "Config.xml"; // this file must be in the same directory as the .dll

        const string CAM_HEIGHT_TAG = "Height";  // The XML file tags
        const string CAM_WIDTH_TAG = "Width";
        const string CONFIG_TAG = "Config";
        const string DATA_PATH_TAG = "DataPath";
        const string END_FRAME_TAG = "EndFrame";
        const string FULL_ORCH_FILE_TAG = "FullOrchFile";
        const string MAXSCRIPTS_PATH_TAG = "MaxScriptsPath";
        const string MEASURE_FILE_TAG = "MeasureFile";
        const string REPEATE_FILE_TAG = "RepeateFile";
        const string MEASURES_FILE_TAG = "MeasuresFile";
        const string MEDIA_FILE_TAG = "MediaFile";
        const string MEDIA_PATH_TAG = "MediaPath";
        const string MIDI_PATH_TAG = "MidiPath";
        const string SCENE_PATH_TAG = "ScenePath";
        const string SCRIPTS_PATH_TAG = "ScriptsPath";
        const string SELECTION_TAG = "Selection";
        const string SHOTS_FILE_TAG = "ShotFile";
        const string SHOTS_SELECTION_TAG = "ShotsSelection";
        const string START_FRAME_TAG = "StartFrame";
        const string TEMPLATE_PATH_TAG = "TemplatePath";
        const string TIMING_FILE_TAG = "TimingFile";

        //----------------------------------------------------------------------------
        //  Class Attributes 
        //----------------------------------------------------------------------------
        private string mFullOrchFile = "FullOrch.csv";
        private string mMeasureFile = "ShotsMeasures.csv";
        private string mMeasuresFile = "Measures.csv";
        private string mMediaFile = "X.png";
        private string mShotFile = "Shots.csv";
        private string mTimingFile = "Timing.csv";
        private string mTimingsFile = "Timings.csv";
        private string mRepeateFile = "Repeate.csv";

        private string mShotsSelection = "";
        private string mSelection = "";

        private string mTemplatePath = "Template\\";
        private string mDataPath = "..\\..\\Data\\";
        private string mMaxScriptsPath = "..\\..\\Scripts\\";
        private string mMediaPath = "..\\..\\Media\\";
        private string mMidiPath = "Midi\\";
        private string mScenePath = "..\\..\\Scene\\";
        private string mScriptsPath = "Scripts\\";

        private int mWidth = 1280;
        private int mStartFrame = 1;
        private int mEndFrame = 100;
        private int mHeight = 720;

        //--------------------------------------------------------------------
        // Purpose:
        //     Constructor, load in the file.  If the file is not found the 
        //     defaults from above are used.
        //
        // Notes:
        //     None.
        //--------------------------------------------------------------------
        public Config()
        {
            XmlDocument doc = new XmlDocument();
            doc.Load(CONFIG_FILE_NAME);
            XmlNode node = doc.FirstChild;
            XmlNode childNode = node.FirstChild;
            while (null != childNode)
            {
                if (CAM_WIDTH_TAG == childNode.Name)
                {
                    int.TryParse(childNode.InnerText, out mWidth);
                }
                if (CAM_HEIGHT_TAG == childNode.Name)
                {
                    int.TryParse(childNode.InnerText, out mHeight);
                }
                if (SHOTS_SELECTION_TAG == childNode.Name)
                {
                    mShotsSelection = childNode.InnerText;
                }
                if (MEASURE_FILE_TAG == childNode.Name)
                {
                    mMeasureFile = childNode.InnerText;
                }
                if (REPEATE_FILE_TAG == childNode.Name)
                {
                    mRepeateFile = childNode.InnerText;
                }
                if (MEASURES_FILE_TAG == childNode.Name)
                {
                    mMeasuresFile = childNode.InnerText;
                }
                if (MEDIA_FILE_TAG == childNode.Name)
                {
                    mMediaFile = childNode.InnerText;
                }
                if (MEDIA_PATH_TAG == childNode.Name)
                {
                    mMediaPath = childNode.InnerText;
                }
                if (SHOTS_FILE_TAG == childNode.Name)
                {
                    mShotFile = childNode.InnerText;
                }
                if (START_FRAME_TAG == childNode.Name)
                {
                    int.TryParse(childNode.InnerText, out mStartFrame);
                }
                if (END_FRAME_TAG == childNode.Name)
                {
                    int.TryParse(childNode.InnerText, out mEndFrame);
                }
                if (SELECTION_TAG == childNode.Name)
                {
                    mSelection = childNode.InnerText;
                }
                if (MAXSCRIPTS_PATH_TAG == childNode.Name)
                {
                    mMaxScriptsPath = childNode.InnerText;
                }
                if (MIDI_PATH_TAG == childNode.Name)
                {
                    mMidiPath = childNode.InnerText;
                }
                if (TEMPLATE_PATH_TAG == childNode.Name)
                {
                    mTemplatePath = childNode.InnerText;
                }
                if (SCRIPTS_PATH_TAG == childNode.Name)
                {
                    mScriptsPath = childNode.InnerText;
                }
                if (TIMING_FILE_TAG == childNode.Name)
                {
                    mTimingFile = childNode.InnerText;
                }
                if (SCENE_PATH_TAG == childNode.Name)
                {
                    mScenePath = childNode.InnerText;
                }
                if (DATA_PATH_TAG == childNode.Name)
                {
                    mDataPath = childNode.InnerText;
                }
                if (FULL_ORCH_FILE_TAG == childNode.Name)
                {
                    mFullOrchFile = childNode.InnerText;
                }
                childNode = childNode.NextSibling;
            }
        }

        //--------------------------------------------------------------------
        // Purpose:
        //     Saves the file to the new config file.
        //
        // Notes:
        //     This must be called from the thing that is using the config file.
        //--------------------------------------------------------------------
        public void save()
        {
            XmlDocument doc = new XmlDocument();

            XmlNode nodeCONFIG_TAG = doc.CreateNode("element", CONFIG_TAG, "");
            doc.AppendChild(nodeCONFIG_TAG);

            XmlNode nodeDATA_PATH_TAG = doc.CreateNode("element", DATA_PATH_TAG, "");
            nodeDATA_PATH_TAG.InnerText = mDataPath;
            nodeCONFIG_TAG.AppendChild(nodeDATA_PATH_TAG);

            XmlNode nodeEND_FRAME_TAG = doc.CreateNode("element", END_FRAME_TAG, "");
            nodeEND_FRAME_TAG.InnerText = mEndFrame.ToString();
            nodeCONFIG_TAG.AppendChild(nodeEND_FRAME_TAG);

            XmlNode nodeFULL_ORCH_FILE_TAG = doc.CreateNode("element", FULL_ORCH_FILE_TAG, "");
            nodeFULL_ORCH_FILE_TAG.InnerText = mFullOrchFile;
            nodeCONFIG_TAG.AppendChild(nodeFULL_ORCH_FILE_TAG);

            XmlNode nodeHEIGHT_TAG = doc.CreateNode("element", CAM_HEIGHT_TAG, "");
            nodeHEIGHT_TAG.InnerText = mHeight.ToString();
            nodeCONFIG_TAG.AppendChild(nodeHEIGHT_TAG);

            XmlNode nodeMAXSCRIPTS_PATH_TAG = doc.CreateNode("element", MAXSCRIPTS_PATH_TAG, "");
            nodeMAXSCRIPTS_PATH_TAG.InnerText = mMaxScriptsPath;
            nodeCONFIG_TAG.AppendChild(nodeMAXSCRIPTS_PATH_TAG);

            XmlNode nodeMEASUREFILE_TAG = doc.CreateNode("element", MEASURE_FILE_TAG, "");
            nodeMEASUREFILE_TAG.InnerText = mMeasureFile;
            nodeCONFIG_TAG.AppendChild(nodeMEASUREFILE_TAG);

            XmlNode nodeREPEATEFILE_TAG = doc.CreateNode("element", REPEATE_FILE_TAG, "");
            nodeREPEATEFILE_TAG.InnerText = mRepeateFile;
            nodeCONFIG_TAG.AppendChild(nodeREPEATEFILE_TAG);

            XmlNode nodeMEASURESFILE_TAG = doc.CreateNode("element", MEASURES_FILE_TAG, "");
            nodeMEASURESFILE_TAG.InnerText = mMeasuresFile;
            nodeCONFIG_TAG.AppendChild(nodeMEASURESFILE_TAG);

            XmlNode nodeMEDIA_FILE_TAG = doc.CreateNode("element", MEDIA_FILE_TAG, "");
            nodeMEDIA_FILE_TAG.InnerText = mMediaFile;
            nodeCONFIG_TAG.AppendChild(nodeMEDIA_FILE_TAG);

            XmlNode nodeMEDIA_PATH_TAG = doc.CreateNode("element", MEDIA_PATH_TAG, "");
            nodeMEDIA_PATH_TAG.InnerText = mMediaPath;
            nodeCONFIG_TAG.AppendChild(nodeMEDIA_PATH_TAG);

            XmlNode nodeMIDI_PATH_TAG = doc.CreateNode("element", MIDI_PATH_TAG, "");
            nodeMIDI_PATH_TAG.InnerText = mMidiPath;
            nodeCONFIG_TAG.AppendChild(nodeMIDI_PATH_TAG);

            XmlNode nodeSCENE_PATH_TAG = doc.CreateNode("element", SCENE_PATH_TAG, "");
            nodeSCENE_PATH_TAG.InnerText = mScenePath;
            nodeCONFIG_TAG.AppendChild(nodeSCENE_PATH_TAG);

            XmlNode nodeSCRIPTS_PATH_TAG = doc.CreateNode("element", SCRIPTS_PATH_TAG, "");
            nodeSCRIPTS_PATH_TAG.InnerText = mScriptsPath;
            nodeCONFIG_TAG.AppendChild(nodeSCRIPTS_PATH_TAG);

            XmlNode nodeSELECTION_TAG = doc.CreateNode("element", SELECTION_TAG, "");
            nodeSELECTION_TAG.InnerText = mSelection;
            nodeCONFIG_TAG.AppendChild(nodeSELECTION_TAG);

            XmlNode nodeSHOTSFILE_TAG = doc.CreateNode("element", SHOTS_FILE_TAG, "");
            nodeSHOTSFILE_TAG.InnerText = mShotFile;
            nodeCONFIG_TAG.AppendChild(nodeSHOTSFILE_TAG);

            XmlNode nodeSHOTSSELECTION_TAG = doc.CreateNode("element", SHOTS_SELECTION_TAG, "");
            nodeSHOTSSELECTION_TAG.InnerText = mShotsSelection;
            nodeCONFIG_TAG.AppendChild(nodeSHOTSSELECTION_TAG);

            XmlNode nodeSTART_FRAME_TAG = doc.CreateNode("element", START_FRAME_TAG, "");
            nodeSTART_FRAME_TAG.InnerText = mStartFrame.ToString();
            nodeCONFIG_TAG.AppendChild(nodeSTART_FRAME_TAG);

            XmlNode nodeTEMPLATE_PATH_TAG = doc.CreateNode("element", TEMPLATE_PATH_TAG, "");
            nodeTEMPLATE_PATH_TAG.InnerText = mTemplatePath;
            nodeCONFIG_TAG.AppendChild(nodeTEMPLATE_PATH_TAG);

            XmlNode nodeTIMING_FILE_TAG = doc.CreateNode("element", TIMING_FILE_TAG, "");
            nodeTIMING_FILE_TAG.InnerText = mTimingFile;
            nodeCONFIG_TAG.AppendChild(nodeTIMING_FILE_TAG);

            XmlNode nodeWIDTH_TAG = doc.CreateNode("element", CAM_WIDTH_TAG, "");
            nodeWIDTH_TAG.InnerText = mWidth.ToString();
            nodeCONFIG_TAG.AppendChild(nodeWIDTH_TAG);

            doc.Save(CONFIG_FILE_NAME);
        }

        //--------------------------------------------------------------------
        // Purpose:
        //     Returns the string representing the movement name
        //
        // Notes:
        //     None.
        //--------------------------------------------------------------------
        public string getMovementName(int move)
        {
            switch (move)
            {
                case Symphony9Data.Config.MOVEMENT01:
                    return "Movement01";
                case Symphony9Data.Config.MOVEMENT02:
                    return "Movement02";
                case Symphony9Data.Config.MOVEMENT03:
                    return "Movement03";
                case Symphony9Data.Config.MOVEMENT04:
                    return "Movement04";
                case Symphony9Data.Config.MOVEMENT05:
                    return "Movement05";
            }
            return "";
        }

        //--------------------------------------------------------------------
        // Purpose:
        //     Returns the path and file to the Orchestra File.
        //
        // Notes:
        //     None.
        //--------------------------------------------------------------------
        public string getFullOrchFile()
        {
            return mDataPath + mFullOrchFile;
        }


        //--------------------------------------------------------------------
        // Purpose:
        //     Return the start frame
        //
        // Notes:
        //     None.
        //--------------------------------------------------------------------
        public int getStartFrame()
        {
            return mStartFrame;
        }

        //--------------------------------------------------------------------
        // Purpose:
        //     Set the start frame from an integer
        //
        // Notes:
        //     None.
        //--------------------------------------------------------------------
        public void setStartFrame(int startFrame)
        {
            mStartFrame = startFrame;
        }

        //--------------------------------------------------------------------
        // Purpose:
        //     Set the start frame from a string
        //
        // Notes:
        //     None.
        //--------------------------------------------------------------------
        public void setStartFrame(string startFrame)
        {
            int.TryParse(startFrame, out mStartFrame);
        }

        //--------------------------------------------------------------------
        // Purpose:
        //     Return the end frame
        //
        // Notes:
        //     None.
        //--------------------------------------------------------------------
        public int getEndFrame()
        {
            return mEndFrame;
        }

        //--------------------------------------------------------------------
        // Purpose:
        //     Set the end frame from an integer
        //
        // Notes:
        //     None.
        //--------------------------------------------------------------------
        public void setEndFrame(int endFrame)
        {
            mEndFrame = endFrame;
        }

        //--------------------------------------------------------------------
        // Purpose:
        //     Set the end frame from a string
        //
        // Notes:
        //     None.
        //--------------------------------------------------------------------
        public void setEndFrame(string endFrame)
        {
            int.TryParse(endFrame, out mEndFrame);
        }

        //--------------------------------------------------------------------
        // Purpose:
        //     Return the frame height
        //
        // Notes:
        //     None.
        //--------------------------------------------------------------------
        public int getHeight()
        {
            return mHeight;
        }

        //--------------------------------------------------------------------
        // Purpose:
        //     Sets the height of the frame from an integer
        //
        // Notes:
        //     None.
        //--------------------------------------------------------------------
        public void setHeight(int height)
        {
            mHeight = height;
        }

        //--------------------------------------------------------------------
        // Purpose:
        //     Sets the height of the frame from a string
        //
        // Notes:
        //     None.
        //--------------------------------------------------------------------
        public void setHeight(string height)
        {
            int.TryParse(height, out mHeight);
        }

        //--------------------------------------------------------------------
        // Purpose:
        //     Return the frame width
        //
        // Notes:
        //     None.
        //--------------------------------------------------------------------
        public int getWidth()
        {
            return mWidth;
        }

        //--------------------------------------------------------------------
        // Purpose:
        //     Sets the width of the frame from an integer
        //
        // Notes:
        //     None.
        //--------------------------------------------------------------------
        public void setWidth(int width)
        {
            mWidth = width;
        }

        //--------------------------------------------------------------------
        // Purpose:
        //     Sets the width of the frame from a string
        //
        // Notes:
        //     None.
        //--------------------------------------------------------------------
        public void setWidth(string width)
        {
            int.TryParse(width, out mWidth);
        }

        //--------------------------------------------------------------------
        // Purpose:
        //     Gets the selection of orchestra members in a string form
        //
        // Notes:
        //     None.
        //--------------------------------------------------------------------
        public string getSelection()
        {
            return mSelection;
        }

        //--------------------------------------------------------------------
        // Purpose:
        //     Gets the selection of orchestra members in an array of ints
        //
        // Notes:
        //     None.
        //--------------------------------------------------------------------
        public int[] getSelectionInts()
        {
            if ("" == mSelection)
            {
                return null;
            }

            char[] delimiterChars = { ' ', ',', '.', ':', '\t' };

            string[] items = mSelection.Split(delimiterChars);
            int[] ints = new int[items.Length];

            for (int i = 0; i < items.Length; i++)
            {
                int.TryParse(items[i], out ints[i]);
            }

            return ints;
        }

        //--------------------------------------------------------------------
        // Purpose:
        //     Sets the selection of orchestra members
        //
        // Notes:
        //     This trims the spaces and removes any leading commas
        //--------------------------------------------------------------------
        public void setSelection(string select)
        {
            mSelection = select;
            mSelection = mSelection.Replace(" ", "");
            if (',' == mSelection[0])
            {
                mSelection = mSelection.Substring(1);
            }
        }

        //--------------------------------------------------------------------
        // Purpose:
        //     Gets the camera shot selection in a string form
        //
        // Notes:
        //     None.
        //--------------------------------------------------------------------
        public string getShotsSelection()
        {
            return mShotsSelection;
        }

        //--------------------------------------------------------------------
        // Purpose:
        //     Gets the camera shot selection in an array of ints
        //
        // Notes:
        //     None.
        //--------------------------------------------------------------------
        public int[] getShotSelectionInts()
        {
            if ("" == mShotsSelection)
            {
                return null;
            }

            char[] delimiterChars = { ' ', ',', '.', ':', '\t' };

            string[] items = mShotsSelection.Split(delimiterChars);
            int[] ints = new int[items.Length];

            for (int i = 0; i < items.Length; i++)
            {
                int.TryParse(items[i], out ints[i]);
            }

            return ints;
        }

        //--------------------------------------------------------------------
        // Purpose:
        //     Sets the selection of camera shots
        //
        // Notes:
        //     This trims the spaces and removes any leading commas
        //--------------------------------------------------------------------
        public void setShotsSelection(string select)
        {
            mShotsSelection = select;
            mShotsSelection = mShotsSelection.Replace(" ", "");
            if ((mShotsSelection.Length > 0) && (',' == mShotsSelection[0]))
            {
                mShotsSelection = mShotsSelection.Substring(1);
            }
        }

        //--------------------------------------------------------------------
        // Purpose:
        //     Returns the measure files name for the movement
        //
        // Notes:
        //     None.
        //--------------------------------------------------------------------
        public string getMeasureFileName(int move)
        {
            return mDataPath + getMovementName(move) + mScriptsPath + getMovementName(move) + mMeasureFile;
        }

        //--------------------------------------------------------------------
        // Purpose:
        //     Returns the measure file name
        //
        // Notes:
        //     None.
        //--------------------------------------------------------------------
        public string getMeasuresFileName(int move)
        {
            return mDataPath + getMovementName(move) + mMidiPath + getMovementName(move) + mMeasuresFile;
        }

        //--------------------------------------------------------------------
        // Purpose:
        //     Returns the name of the media file for the movement
        //
        // Notes:
        //     None.
        //--------------------------------------------------------------------
        public string getMediaName(int move)
        {
            return mMediaPath + getMovementName(move) + "\\" + getMovementName(move) + mMediaFile;
        }

        //--------------------------------------------------------------------
        // Purpose:
        //     Returns the name that will be saved for a shot
        //
        // Notes:
        //     None.
        //--------------------------------------------------------------------
        public string getMediaName(int move, int shot)
        {
            return mMediaPath + getMovementName(move) + "\\shot" + shot.ToString("D3") + "\\" + getMovementName(move) + mMediaFile;
        }

        //--------------------------------------------------------------------
        // Purpose:
        //     Returns the path to the media save directory
        //
        // Notes:
        //     None.
        //--------------------------------------------------------------------
        public string getMediaPath(int move, int shot)
        {
            return mMediaPath + getMovementName(move) + "\\shot" + shot.ToString("D3") + "\\";
        }

        //--------------------------------------------------------------------
        // Purpose:
        //     Returns the midi file for the players name
        //
        // Notes:
        //     None.
        //--------------------------------------------------------------------
        public string getMidiName(string inst, int move)
        {
            return mDataPath + getMovementName(move) + mMidiPath + getMovementName(move) + inst + ".mid";
        }

        //--------------------------------------------------------------------
        // Purpose:
        //     Returns the players CVS entry with the notes and times in it
        //
        // Notes:
        //     None.
        //--------------------------------------------------------------------
        public string getPlayCSVName(string name, int move)
        {
            return mDataPath + getMovementName(move) + mScriptsPath + getMovementName(move) + name + "play.csv";
        }

        //--------------------------------------------------------------------
        // Purpose:
        //     Returns the scene name for the player in the movement directory
        //
        // Notes:
        //     None.
        //--------------------------------------------------------------------
        public string getSceneName(string name, int move)
        {
            return mScenePath + getMovementName(move) + "\\" + name + ".max";
        }

        //--------------------------------------------------------------------
        // Purpose:
        //     Returns the scene name for the player from the template dir
        //
        // Notes:
        //     None.
        //--------------------------------------------------------------------
        public string getSceneTemplateName(string name)
        {
            return mScenePath + mTemplatePath + name + ".max";
        }

        //--------------------------------------------------------------------
        // Purpose:
        //     Returns the players script file name
        //
        // Notes:
        //     None.
        //--------------------------------------------------------------------
        public string getScriptName(string name, int move)
        {
            return mDataPath + getMovementName(move) + mScriptsPath + getMovementName(move) + name + ".script";
        }

        //--------------------------------------------------------------------
        // Purpose:
        //     Return the shot file for the movement
        //
        // Notes:
        //     None.
        //--------------------------------------------------------------------
        public string getShotFileName(int move)
        {
            return mDataPath + getMovementName(move) + mScriptsPath + getMovementName(move) + mShotFile;
        }

        //--------------------------------------------------------------------
        // Purpose:
        //     Returns the movements timing file name
        //
        // Notes:
        //     None.
        //--------------------------------------------------------------------
        public string getTimingName(int move)
        {
            return mDataPath + getMovementName(move) + mMidiPath + getMovementName(move) + "Timing.csv";
        }

        //--------------------------------------------------------------------
        // Purpose:
        //     Returns the movement repeate file name
        //
        // Notes:
        //     None.
        //--------------------------------------------------------------------
        public string getRepeateName(int move)
        {
            return mDataPath + getMovementName(move) + mMidiPath + getMovementName(move) + "Repeate.csv";
        }

        //--------------------------------------------------------------------
        // Purpose:
        //     Returns the timing file
        //
        // Notes:
        //     The timing file controls the wiggle of the players
        //--------------------------------------------------------------------
        public string getTimingsFile()
        {
            return mDataPath + mTimingsFile;
        }
    }
}
