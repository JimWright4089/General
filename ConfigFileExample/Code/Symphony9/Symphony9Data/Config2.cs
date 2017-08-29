using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;

namespace Symphony9Data
{
    public class Config
    {
        const string CONFIG_FILE_NAME = "Config.xml";
        const string DATA_PATH_TAG = "DataPath";
        const string FULL_ORCH_FILE_TAG = "FullOrchFile";
        const string CONFIG_TAG = "Config";
        const string SCRIPTS_PATH_TAG = "ScriptsPath";
        const string SCENE_PATH_TAG = "ScenePath";
        const string MEDIA_PATH_TAG = "MediaPath";
        const string TIMING_FILE_TAG = "TimingFile";
        const string MAXSCRIPTS_PATH_TAG = "MaxScriptsPath";
        const string TEMPLATE_PATH_TAG = "TemplatePath";
        const string MIDI_PATH_TAG = "MidiPath";
        const string START_FRAME_TAG = "StartFrame";
        const string END_FRAME_TAG = "EndFrame";
        const string SELECTION_TAG = "Selection";
        const string MEASURE_FILE_TAG = "MeasureFile";
        const string MEDIA_FILE_TAG = "MediaFile";
        const string SHOTS_FILE_TAG = "ShotFile";
        const string SHOTS_SELECTION_TAG = "ShotsSelection";
        const string CAM_WIDTH_TAG = "Width";
        const string CAM_HEIGHT_TAG = "Height";
 
        public const int MOVEMENT01 = 0;
        public const int MOVEMENT02 = 1;
        public const int MOVEMENT03 = 2;
        public const int MOVEMENT04 = 3;

        private string mDataPath = "..\\..\\Data\\";
        private string mFullOrchFile = "FullOrch.csv";
        private string mTimingsFile = "Timings.csv";
        private string mMaxScriptsPath = "..\\..\\Scripts\\";
        private string mScenePath = "..\\..\\Scene\\";
        private string mMediaPath = "..\\..\\Media\\";
        private string mTimingFile = "Timing.csv";
        private string mScriptsPath = "Scripts\\";
        private string mMidiPath = "Midi\\";
        private string mTemplatePath = "Template\\";
        private string mSelection = "";
        private string mShotsSelection = "";
        private string mMeasureFile = "ShotsMeasures.csv";
        private string mShotFile = "Shots.csv";
        private string mMediaFile = "X.png";
        private int mWidth = 1280;
        private int mHeight = 720;

        private int mStartFrame = 1;
        private int mEndFrame = 100;

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

        public void save()
        {
            XmlDocument doc = new XmlDocument();

            XmlNode nodeCONFIG_TAG = doc.CreateNode("element", CONFIG_TAG, "");
            doc.AppendChild(nodeCONFIG_TAG);

            XmlNode nodeWIDTH_TAG = doc.CreateNode("element", CAM_WIDTH_TAG, "");
            nodeWIDTH_TAG.InnerText = mWidth.ToString();
            nodeCONFIG_TAG.AppendChild(nodeWIDTH_TAG);

            XmlNode nodeHEIGHT_TAG = doc.CreateNode("element", CAM_HEIGHT_TAG, "");
            nodeHEIGHT_TAG.InnerText = mHeight.ToString();
            nodeCONFIG_TAG.AppendChild(nodeHEIGHT_TAG);

            XmlNode nodeSTART_FRAME_TAG = doc.CreateNode("element", START_FRAME_TAG, "");
            nodeSTART_FRAME_TAG.InnerText = mStartFrame.ToString();
            nodeCONFIG_TAG.AppendChild(nodeSTART_FRAME_TAG);

            XmlNode nodeSELECTION_TAG = doc.CreateNode("element", SELECTION_TAG, "");
            nodeSELECTION_TAG.InnerText = mSelection;
            nodeCONFIG_TAG.AppendChild(nodeSELECTION_TAG);

            XmlNode nodeSHOTSSELECTION_TAG = doc.CreateNode("element", SHOTS_SELECTION_TAG, "");
            nodeSHOTSSELECTION_TAG.InnerText = mShotsSelection;
            nodeCONFIG_TAG.AppendChild(nodeSHOTSSELECTION_TAG);

            XmlNode nodeSHOTSFILE_TAG = doc.CreateNode("element", SHOTS_FILE_TAG, "");
            nodeSHOTSFILE_TAG.InnerText = mShotFile;
            nodeCONFIG_TAG.AppendChild(nodeSHOTSFILE_TAG);

            XmlNode nodeMEASUREFILE_TAG = doc.CreateNode("element", MEASURE_FILE_TAG, "");
            nodeMEASUREFILE_TAG.InnerText = mMeasureFile;
            nodeCONFIG_TAG.AppendChild(nodeMEASUREFILE_TAG);

            XmlNode nodeEND_FRAME_TAG = doc.CreateNode("element", END_FRAME_TAG, "");
            nodeEND_FRAME_TAG.InnerText = mEndFrame.ToString();
            nodeCONFIG_TAG.AppendChild(nodeEND_FRAME_TAG);

            XmlNode nodeMAXSCRIPTS_PATH_TAG = doc.CreateNode("element",MAXSCRIPTS_PATH_TAG,"");
            nodeMAXSCRIPTS_PATH_TAG.InnerText = mMaxScriptsPath;
            nodeCONFIG_TAG.AppendChild(nodeMAXSCRIPTS_PATH_TAG);

            XmlNode nodeMIDI_PATH_TAG = doc.CreateNode("element",MIDI_PATH_TAG,"");
            nodeMIDI_PATH_TAG.InnerText = mMidiPath;
            nodeCONFIG_TAG.AppendChild(nodeMIDI_PATH_TAG);

            XmlNode nodeTEMPLATE_PATH_TAG = doc.CreateNode("element",TEMPLATE_PATH_TAG,"");
            nodeTEMPLATE_PATH_TAG.InnerText = mTemplatePath;
            nodeCONFIG_TAG.AppendChild(nodeTEMPLATE_PATH_TAG);

            XmlNode nodeSCRIPTS_PATH_TAG = doc.CreateNode("element",SCRIPTS_PATH_TAG,"");
            nodeSCRIPTS_PATH_TAG.InnerText = mScriptsPath;
            nodeCONFIG_TAG.AppendChild(nodeSCRIPTS_PATH_TAG);

            XmlNode nodeTIMING_FILE_TAG = doc.CreateNode("element",TIMING_FILE_TAG,"");
            nodeTIMING_FILE_TAG.InnerText = mTimingFile;
            nodeCONFIG_TAG.AppendChild(nodeTIMING_FILE_TAG);

            XmlNode nodeSCENE_PATH_TAG = doc.CreateNode("element", SCENE_PATH_TAG, "");
            nodeSCENE_PATH_TAG.InnerText = mScenePath;
            nodeCONFIG_TAG.AppendChild(nodeSCENE_PATH_TAG);

            XmlNode nodeMEDIA_PATH_TAG = doc.CreateNode("element", MEDIA_PATH_TAG, "");
            nodeMEDIA_PATH_TAG.InnerText = mMediaPath;
            nodeCONFIG_TAG.AppendChild(nodeMEDIA_PATH_TAG);

            XmlNode nodeMEDIA_FILE_TAG = doc.CreateNode("element", MEDIA_FILE_TAG, "");
            nodeMEDIA_FILE_TAG.InnerText = mMediaFile;
            nodeCONFIG_TAG.AppendChild(nodeMEDIA_FILE_TAG);

            XmlNode nodeDATA_PATH_TAG = doc.CreateNode("element", DATA_PATH_TAG, "");
            nodeDATA_PATH_TAG.InnerText = mDataPath;
            nodeCONFIG_TAG.AppendChild(nodeDATA_PATH_TAG);

            XmlNode nodeFULL_ORCH_FILE_TAG = doc.CreateNode("element", FULL_ORCH_FILE_TAG, "");
            nodeFULL_ORCH_FILE_TAG.InnerText = mFullOrchFile;
            nodeCONFIG_TAG.AppendChild(nodeFULL_ORCH_FILE_TAG);

            doc.Save(CONFIG_FILE_NAME);
        }

        public string getSelection()
        {
            return mSelection;
        }

        public string getShotsSelection()
        {
            return mShotsSelection;
        }

        public int[] getSelectionInts()
        {
            if("" == mSelection)
            {
                return null;
            }

            char[] delimiterChars = { ' ', ',', '.', ':', '\t' };

            string[] items = mSelection.Split(delimiterChars);
            int[] ints = new int[items.Length];

            for (int i = 0; i < items.Length;i++)
            {
                int.TryParse(items[i], out ints[i]);
            }

            return ints;
        }

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

        public void setSelection(string select)
        {
            mSelection = select;
            mSelection = mSelection.Replace(" ", "");
            if (',' == mSelection[0])
            {
                mSelection = mSelection.Substring(1);
            }
        }

        public void setShotsSelection(string select)
        {
            mShotsSelection = select;
            mShotsSelection = mShotsSelection.Replace(" ", "");
            if ((mShotsSelection.Length > 0)&&(',' == mShotsSelection[0]))
            {
                mShotsSelection = mShotsSelection.Substring(1);
            }
        }

        public int getStartFrame()
        {
            return mStartFrame;
        }

        public int getEndFrame()
        {
            return mEndFrame;
        }

        public int getWidth()
        {
            return mWidth;
        }

        public int getHeight()
        {
            return mHeight;
        }

        public void setEndFrame(int endFrame)
        {
            mEndFrame = endFrame;
        }

        public void setStartFrame(int startFrame)
        {
            mStartFrame = startFrame;
        }

        public void setEndFrame(string endFrame)
        {
            int.TryParse(endFrame, out mEndFrame);
        }

        public void setStartFrame(string startFrame)
        {
            int.TryParse(startFrame, out mStartFrame);
        }

        public void setWidth(int width)
        {
            mWidth = width;
        }

        public void setHeight(int height)
        {
            mHeight = height;
        }

        public void setWidth(string width)
        {
            int.TryParse(width, out mWidth);
        }

        public void setHeight(string height)
        {
            int.TryParse(height, out mHeight);
        }

 
        public string getFullOrchFile()
        {
            return mDataPath + mFullOrchFile;
        }

        public string getTimingsFile()
        {
            return mDataPath + mTimingsFile;
        }

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
            }
            return "";
        }

        public string getMidiName(string inst, int move)
        {
            return mDataPath + getMovementName(move) + mMidiPath + getMovementName(move) + inst + ".mid";
        }

        public string getMediaName(int move)
        {
            return mMediaPath + getMovementName(move) + "\\" + getMovementName(move) + mMediaFile;
        }

        public string getPlayCSVName(string name, int move)
        {
            return mDataPath + getMovementName(move) + mScriptsPath + getMovementName(move) + name + "play.csv";
        }

        public string getScriptName(string name, int move)
        {
            return mDataPath + getMovementName(move) + mScriptsPath + getMovementName(move) + name + ".script";
        }

        public string getSceneTemplateName(string name)
        {
            return mScenePath + mTemplatePath + name + ".max";
        }

        public string getSceneName(string name, int move)
        {
            return mScenePath + getMovementName(move) + "\\" + name + ".max";
        }

        public string getTimingName(int move)
        {
            return mDataPath + getMovementName(move) + mMidiPath + getMovementName(move) + "Timing.csv";
        }

        public string getMeasureFileName(int move)
        {
            return mDataPath + getMovementName(move) + mScriptsPath + getMovementName(move) + mMeasureFile;
        }
        
        public string getShotFileName(int move)
        {
            return mDataPath + getMovementName(move) + mScriptsPath + getMovementName(move) + mShotFile;
        }

    }
}
