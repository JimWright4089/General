using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace Symphony9Data
{
    public class Timings
    {
        List<Timing> mTimings = new List<Timing>();
        Config mConfig = new Config();
        private string mVersion = "0.0";
        private string mFileDate = "0/0/0000";
        private string mMD5 = "";
        private string mFileName = "";

        public Timings()
        {
            mFileName = mConfig.getTimingsFile();
            StreamReader file = File.OpenText(mFileName);

            mMD5 = file.GetHashCode().ToString();

            string line = file.ReadLine();

            char[] delimiterChars = { ',' };

            string[] items = line.Split(delimiterChars);
            mVersion = items[1];
            mFileDate = File.GetLastWriteTime(mFileName).ToString();

            // Read heading 
            line = file.ReadLine();

            line = file.ReadLine();

            while(!file.EndOfStream)
            {
                Timing newTiming = new Timing(line);
                mTimings.Add(newTiming);
                line = file.ReadLine();
            }
        }

        public string getVersion()
        {
            return mVersion;
        }

        public string getMD5()
        {
            return mMD5;
        }

        public string getFileDate()
        {
            return mFileDate;
        }

        public void writeOrchInfo(StreamWriter outfile)
        {
            outfile.WriteLine("Timing File,Name," + mFileName + ",Date," + mFileDate + ",md5," + mMD5 + ",version," + mVersion);
        }

        public string getString()
        {
            return "Timing File,Name," + mFileName + ",Date," + mFileDate + ",md5," + mMD5 + ",version," + mVersion;
        }

        public Timing findTiming(string name)
        {
            Timing returnValue = mTimings[0];
            for(int i=0;i<mTimings.Count;i++)
            {
                if(name == mTimings[i].mName)
                {
                    return mTimings[i];
                }
            }
            return returnValue;
        }

    }

    public class Timing
    {
        public const int mSSNeutral = 8;
        public const int mRNeutral = 8;
        public const int mFBNeutral = 4;

        public string mName = "";
        public int mRestToGet = 0;
        public int mGetToBreath = 0;
        public int mPlayToBreath = 0;
        public int mBreathTime = 0;
        public int mBreathPosLow = 0;
        public int mBreathPosHigh = 0;
        public int mRestPosLow = 0;
        public int mRestPosHigh = 0;
        public int mPlayingBack = 0;
        public int mPlayingFore = 0;
        public int mStartSway = 0;
        public int mSwayFrames = 0;
        public double mSSAmp = 0.0;
        public double mSSFreq = 0.0;
        public double mSSStart = 0.0;
        public double mFBAmp = 0.0;
        public double mFBFreq = 0.0;
        public double mFBStart = 0.0;
        public double mRAmp = 0.0;
        public double mRFreq = 0.0;
        public double mRStart = 0.0;

        public Random mRand = new Random();
        public int mBreath1 = 0;
        public int mBreath2 = 0;

        public Timing(int seed)
        {
            mRand = new Random(seed*100);
        }

        public Timing()
        {
            mRand = new Random();
        }

        public Timing(string line)
        {
            char[] delimiterChars = { ' ', ',', ':', '\t' };

            string[] items = line.Split(delimiterChars);

            mName = items[0];
            int.TryParse(items[1], out mRestToGet);
            int.TryParse(items[2], out mGetToBreath);
            int.TryParse(items[3], out mPlayToBreath);
            int.TryParse(items[4], out mBreathTime);
            int.TryParse(items[5], out mBreathPosLow);
            int.TryParse(items[6], out mBreathPosHigh);
            int.TryParse(items[7], out mRestPosLow);
            int.TryParse(items[8], out mRestPosHigh);
            int.TryParse(items[9], out mPlayingBack);
            int.TryParse(items[10], out mPlayingFore);
            int.TryParse(items[11], out mStartSway);
            int.TryParse(items[12], out mSwayFrames);
            double.TryParse(items[13], out mSSAmp);
            double.TryParse(items[14], out mSSFreq);
            double.TryParse(items[15], out mSSStart);
            double.TryParse(items[16], out mFBAmp);
            double.TryParse(items[17], out mFBFreq);
            double.TryParse(items[18], out mFBStart);
            double.TryParse(items[19], out mRAmp);
            double.TryParse(items[20], out mRFreq);
            double.TryParse(items[21], out mRStart);
        }

        public void setSeed(int seed)
        {
            mRand = new Random(seed*100);
        }

        public int getfidgitAmountOfTime()
        {
            return mRand.Next(500, 850);
        }

        public int getNextBreath()
        {
            mBreath1 = mRand.Next(mBreathPosLow, mBreathPosHigh);
            return mBreath1;
        }

        public int getNextRest()
        {
            mBreath1 = getNextBreath();
            mBreath2 = getNextBreath();
            return mBreath1 +
                    mGetToBreath +
                    mRestToGet +
                    mRand.Next(mRestPosLow, mRestPosHigh) +
                    mRestToGet +
                    mGetToBreath +
                    mBreath2;
        }

        public int getFidgitPercent()
        {
            if(mRand.Next(1,2000)>1598)
            {
                return 1;
            }
            return 0;
        }

        public int getFidgitWaitTime()
        {
            return mRand.Next(300, 1700);
        }

        public int getFacePercent()
        {
            if (mRand.Next(1, 200) > 185)
            {
                return 1;
            }
            return 0;
        }

        public int getWhichFacePercent()
        {
            return mRand.Next(1, 10);
        }


        public int getWhichFidgit()
        {
            return mRand.Next(1, 32);
        }

        public int getR(int frame)
        {
            double time = ((double)frame) / 20;

            double value = mRAmp * Math.Sin(mRStart + (mRFreq * time));

            return (int)value+mRNeutral;
        }

        public int getSS(int frame)
        {
            double time = ((double)frame) / 20;

            double value = mSSAmp * Math.Sin(mSSStart + (mSSFreq * time));

            return (int)value+mSSNeutral;
        }

        public int getFB(int frame)
        {
            double time = ((double)frame) / 20;

            double value = mFBAmp * Math.Sin(mFBStart + (mFBFreq * time));

            return (int)value+mFBNeutral;
        }
    
    }
}
