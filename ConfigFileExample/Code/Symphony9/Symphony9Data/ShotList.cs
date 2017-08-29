using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace Symphony9Data
{

    public class ShotList
    {

        Config mConfig = new Config();
        int mMove;
        List<SingleShot> mShots = new List<SingleShot>();

        public ShotList()
        {
            mMove = Config.MOVEMENT01;
            loadShotList();
        }

        public void clearSelected()
        {
            for (int i = 0; i < mShots.Count; i++)
            {
                mShots[i].setSelected(false);
            }
        }

        public int getNumShots()
        {
            return mShots.Count;
        }

        public int[] getSelected()
        {
            int[] array = null;
            int count = 0;
            int index = 0;

            for (int i = 0; i < mShots.Count; i++)
            {
                if (true == mShots[i].getSelected())
                {
                    count++;
                }
            }

            array = new int[count];
            for (int i = 0; i < mShots.Count; i++)
            {
                if (true == mShots[i].getSelected())
                {
                    array[index] = mShots[i].getID();
                    index++;
                }
            }

            return array;
        }

        public string getSelectedString()
        {
            int[] array = getSelected();
            string selected = "";
            bool first = true;

            for (int i = 0; i < array.Length; i++)
            {
                if (true == first)
                {
                    selected += array[i].ToString();
                    first = false;
                }
                else
                {
                    selected += ", " + array[i].ToString();
                }
            }

            return selected;
        }

        public double[] getShotArrayFromID(int num)
        {
            double[] array = new double[2];
            for (int i = 0; i < mShots.Count; i++)
            {
                if (num == mShots[i].getID())
                {
                    array[0] = mShots[i].getFrameStart();
                    array[1] = mShots[i].getFrameEnd();
                }
            }
            return array;
        }

        public string getShotCameraFromID(int num)
        {
            for (int i = 0; i < mShots.Count; i++)
            {
                if (num == mShots[i].getID())
                {
                    return mShots[i].getCamera();
                }
            }
            return "";
        }

        public string getShotFramesFromID(int num)
        {
            for (int i = 0; i < mShots.Count; i++)
            {
                if (num == mShots[i].getID())
                {
                    return mShots[i].getFrameStart().ToString() + "-" + mShots[i].getFrameEnd().ToString();
                }
            }
            return "";
        }

        public string getShotID(int num)
        {
            if ((num >= 0) && (num < mShots.Count))
            {
                return mShots[num].getID().ToString();
            }
            return "";
        }

        public string getShotName(int num)
        {
            if ((num >= 0) && (num < mShots.Count))
            {
                return mShots[num].getShotName();
            }
            return "";
        }

        private void loadShotList()
        {
            mShots = new List<SingleShot>();
            StreamReader file = File.OpenText(mConfig.getShotFileName(mMove));

            string line = file.ReadLine();

            while (!file.EndOfStream)
            {
                line = file.ReadLine();
                SingleShot newShot = new SingleShot(line);
                mShots.Add(newShot);
            }
        }

        public void selectAll()
        {
            for (int i = 0; i < mShots.Count; i++)
            {
                mShots[i].setSelected(true);
            }
        }

        public void selectedID(int num)
        {
            for (int i = 0; i < mShots.Count; i++)
            {
                if (num == mShots[i].getID())
                {
                    mShots[i].setSelected(true);
                }
            }
        }

        public void selectType(string theType)
        {
            for (int i = 0; i < mShots.Count; i++)
            {
                if (theType == mShots[i].getShotName())
                {
                    mShots[i].setSelected(true);
                }
            }
        }

        public void setMove(int move, Config config)
        {
            mMove = move;
            mConfig = config;
            loadShotList();
        }
    }

    public class SingleShot
    {

        string mCamera;
        double mFrameEnd;
        double mFrameStart;
        int mID;
        bool mSelected;
        string mShotName;

        public SingleShot(string text)
        {
            char[] delimiterChars = { ',' };

            string[] items = text.Split(delimiterChars);

            int.TryParse(items[0], out mID);
            mShotName = items[1];
            double.TryParse(items[2], out mFrameStart);
            double.TryParse(items[3], out mFrameEnd);
            mCamera = items[4];

            mSelected = false;
        }

        public string getCamera()
        {
            return mCamera;
        }

        public double getFrameEnd()
        {
            return mFrameEnd;
        }

        public double getFrameStart()
        {
            return mFrameStart;
        }

        public int getID()
        {
            return mID;
        }

        public bool getSelected()
        {
            return mSelected;
        }

        public string getShotName()
        {
            return mShotName;
        }

        public void setSelected(bool selected)
        {
            mSelected = selected;
        }

        public override string ToString()
        {
            return mID.ToString() + "-" + mShotName;
        }
    }
}
