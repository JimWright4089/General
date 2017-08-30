//----------------------------------------------------------------------------
//
//  $Workfile: Icon Builder$
//
//  $Revision: 126$
//
//  Project:    Icon Builder
//
//                            Copyright (c) 2017
//                               Jim Wright
//                            All Rights Reserved
//
//  Modification History:
//  $Log:
//  $
//
//----------------------------------------------------------------------------
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;
using System.IO;

namespace IconBuilder
{
    //----------------------------------------------------------------------------
    //  Class Declarations
    //----------------------------------------------------------------------------
    //
    // Class Name: IconBuilder
    // 
    // Purpose:
    //      Handles the main form
    //
    //----------------------------------------------------------------------------
    public partial class IconBuilder : Form
    {
        //----------------------------------------------------------------------------
        //  Class Constants 
        //----------------------------------------------------------------------------
        const int MAX_WIDTH = 0;
        const int MAX_HEIGHT = 0;
        const int MAX_OUTLINE_SIZE = 1;
        const int TEXT_X_OFFSET = 2;
        const int TEXT_Y_1_OFFSET = 3;
        const int TEXT_Y_2_OFFSET = 4;
        const int A_X_OFFSET = 5;
        const int A_Y_OFFSET = 6;
        const int TEXT_SIZE = 7;
        const int A_SIZE = 8;
        const int X_OFFSET = 9;
        const int Y_OFFSET = 10;

        const int WHITE_OFFSET = 420;
        const int CONTROL_OFFSET = 300;

        //----------------------------------------------------------------------------
        //  Class Attributes 
        //----------------------------------------------------------------------------
                         //  0     1   2   3   4    5    6    7   8   9   10
        int[] s256Consts = { 256, 20, 15, 20, 100, 111, 111, 60, 80, 100, 20 };
        int[] s128Consts = { 128, 10,  7, 10,  50,  55,  55, 30, 40, 360, 20 };
        int[] s64Consts  = {  64,  5,  4,  5,  25,  27,  27, 15, 20, 360, 150 };
        int[] s32Consts  = {  32,  2,  2,  2,  12,  13,  13,  7, 10, 360, 218 };
        int[] s16Consts  = {  16,  1, -1,  1,   6,  -1,  -1,  3, 10, 360, 252 };
        Color mIconColor = Color.ForestGreen;

        string bm256Consts = "IconBuilder.Properties.Corner256.png";
        string bm128Consts = "IconBuilder.Properties.Corner128.png";
        string bm64Consts  = "IconBuilder.Properties.Corner64.png";
        string bm32Consts  = "IconBuilder.Properties.Corner32.png";
        string bm16Consts  = "IconBuilder.Properties.Corner16.png";

        //--------------------------------------------------------------------
        // Purpose:
        //     Constructor
        //
        // Notes:
        //     None.
        //--------------------------------------------------------------------
        public IconBuilder()
        {
            InitializeComponent();
        }

        //--------------------------------------------------------------------
        // Purpose:
        //     Draws an icon to a graphic pane
        //
        // Notes:
        //     None.
        //--------------------------------------------------------------------
        private void drawIt(Graphics graph, int xOffest, int yOffset, int[] consts, string stringConst)
        {
            SolidBrush outlineBrush = new SolidBrush(mIconColor);

            SolidBrush backBrush = new SolidBrush(Color.White);
            SolidBrush whiteBrush = new SolidBrush(Color.ForestGreen);

            Font letterFont = new System.Drawing.Font(new FontFamily("Gill Sans MT Condensed"), consts[TEXT_SIZE]);
//            Font letterFont = new System.Drawing.Font(new FontFamily("Cambria"), consts[TEXT_SIZE]);

            graph.TextRenderingHint = System.Drawing.Text.TextRenderingHint.AntiAlias;

            graph.FillRectangle(outlineBrush, xOffest, yOffset, consts[MAX_WIDTH], consts[MAX_HEIGHT]);
            graph.FillRectangle(backBrush, xOffest + consts[MAX_OUTLINE_SIZE], yOffset + consts[MAX_OUTLINE_SIZE], consts[MAX_WIDTH] - ( 2 * consts[MAX_OUTLINE_SIZE] ), consts[MAX_HEIGHT] - ( 2 * consts[MAX_OUTLINE_SIZE] ));

            System.Reflection.Assembly thisExe = System.Reflection.Assembly.GetExecutingAssembly();
            System.IO.Stream file = thisExe.GetManifestResourceStream(stringConst);
            Image aImage = Image.FromStream(file);
            graph.DrawImage(aImage, xOffest + consts[A_X_OFFSET], yOffset + consts[A_Y_OFFSET]);
            if ( -1 != consts[TEXT_X_OFFSET] )
            { 
                graph.DrawString(tbFirstLine.Text, letterFont, outlineBrush, xOffest + consts[TEXT_X_OFFSET], yOffset + consts[TEXT_Y_1_OFFSET]);
                graph.DrawString(tbSecondLine.Text, letterFont, outlineBrush, xOffest + consts[TEXT_X_OFFSET], yOffset + consts[TEXT_Y_2_OFFSET]);
            }
        }

        //--------------------------------------------------------------------
        // Purpose:
        //     Paint
        //
        // Notes:
        //     None.
        //--------------------------------------------------------------------
        private void IconBuilder_Paint(object sender, PaintEventArgs e)
        {
            Graphics graph = e.Graphics;

            SolidBrush black = new SolidBrush(Color.Black);
            SolidBrush white = new SolidBrush(Color.White);
            SolidBrush title = new SolidBrush(SystemColors.ActiveCaption);

            graph.FillRectangle(black, 90, 5, 430, 300);
            graph.FillRectangle(white, 500, 5, 430, 300);
            graph.FillRectangle(title, 500, 305, 430, 300);


            drawIt(graph, s256Consts[X_OFFSET], s256Consts[Y_OFFSET], s256Consts, bm256Consts);
            drawIt(graph, s128Consts[X_OFFSET], s128Consts[Y_OFFSET], s128Consts, bm128Consts);
            drawIt(graph, s64Consts[X_OFFSET], s64Consts[Y_OFFSET], s64Consts, bm64Consts);
            drawIt(graph, s32Consts[X_OFFSET], s32Consts[Y_OFFSET], s32Consts, bm32Consts);
            drawIt(graph, s16Consts[X_OFFSET], s16Consts[Y_OFFSET], s16Consts, bm16Consts);

            drawIt(graph, WHITE_OFFSET + s256Consts[X_OFFSET], s256Consts[Y_OFFSET], s256Consts, bm256Consts);
            drawIt(graph, WHITE_OFFSET + s128Consts[X_OFFSET], s128Consts[Y_OFFSET], s128Consts, bm128Consts);
            drawIt(graph, WHITE_OFFSET + s64Consts[X_OFFSET], s64Consts[Y_OFFSET], s64Consts, bm64Consts);
            drawIt(graph, WHITE_OFFSET + s32Consts[X_OFFSET], s32Consts[Y_OFFSET], s32Consts, bm32Consts);
            drawIt(graph, WHITE_OFFSET + s16Consts[X_OFFSET], s16Consts[Y_OFFSET], s16Consts, bm16Consts);

            drawIt(graph, s256Consts[X_OFFSET], CONTROL_OFFSET + s256Consts[Y_OFFSET], s256Consts, bm256Consts);
            drawIt(graph, s128Consts[X_OFFSET], CONTROL_OFFSET + s128Consts[Y_OFFSET], s128Consts, bm128Consts);
            drawIt(graph, s64Consts[X_OFFSET], CONTROL_OFFSET + s64Consts[Y_OFFSET], s64Consts, bm64Consts);
            drawIt(graph, s32Consts[X_OFFSET], CONTROL_OFFSET + s32Consts[Y_OFFSET], s32Consts, bm32Consts);
            drawIt(graph, s16Consts[X_OFFSET], CONTROL_OFFSET + s16Consts[Y_OFFSET], s16Consts, bm16Consts);

            drawIt(graph, WHITE_OFFSET + s256Consts[X_OFFSET], CONTROL_OFFSET + s256Consts[Y_OFFSET], s256Consts, bm256Consts);
            drawIt(graph, WHITE_OFFSET + s128Consts[X_OFFSET], CONTROL_OFFSET + s128Consts[Y_OFFSET], s128Consts, bm128Consts);
            drawIt(graph, WHITE_OFFSET + s64Consts[X_OFFSET], CONTROL_OFFSET + s64Consts[Y_OFFSET], s64Consts, bm64Consts);
            drawIt(graph, WHITE_OFFSET + s32Consts[X_OFFSET], CONTROL_OFFSET + s32Consts[Y_OFFSET], s32Consts, bm32Consts);
            drawIt(graph, WHITE_OFFSET + s16Consts[X_OFFSET], CONTROL_OFFSET + s16Consts[Y_OFFSET], s16Consts, bm16Consts);
        }

        //--------------------------------------------------------------------
        // Purpose:
        //     Changes to a text box
        //
        // Notes:
        //     None.
        //--------------------------------------------------------------------
        private void tbFirstLine_TextChanged(object sender, EventArgs e)
        {
            this.Invalidate();
        }

        //--------------------------------------------------------------------
        // Purpose:
        //     Changes to a text box
        //
        // Notes:
        //     None.
        //--------------------------------------------------------------------
        private void tbSecondLine_TextChanged(object sender, EventArgs e)
        {
            this.Invalidate();
        }

        //--------------------------------------------------------------------
        // Purpose:
        //     Select a different color
        //
        // Notes:
        //     None.
        //--------------------------------------------------------------------
        private void bColor_Click(object sender, EventArgs e)
        {
            ColorDialog colorDialog = new ColorDialog();
            if(DialogResult.OK == colorDialog.ShowDialog())
            {
                mIconColor = colorDialog.Color;
                this.Invalidate();
            }
        }

        //--------------------------------------------------------------------
        // Purpose:
        //     Save the icon
        //
        // Notes:
        //     None.
        //--------------------------------------------------------------------
        private void bSave_Click(object sender, EventArgs e)
        {
            SaveFileDialog saveFileDialog = new SaveFileDialog();

            saveFileDialog.Filter = "Icon Files (.ico)|*.ico|All Files (*.*)|*.*";
            saveFileDialog.FilterIndex = 1;
            saveFileDialog.RestoreDirectory = true;

            if ( saveFileDialog.ShowDialog() == DialogResult.OK )
            {
                SaveIcon(saveFileDialog.FileName);
            }
        }

        //--------------------------------------------------------------------
        // Purpose:
        //     Save the icon
        //
        // Notes:
        //     None.
        //--------------------------------------------------------------------
        private void SaveIcon(string fileName)
        {
            Bitmap newBitmap256 = new Bitmap(s256Consts[MAX_WIDTH], s256Consts[MAX_HEIGHT], System.Drawing.Imaging.PixelFormat.Format24bppRgb);
            Bitmap newBitmap128 = new Bitmap(s128Consts[MAX_WIDTH], s128Consts[MAX_HEIGHT], System.Drawing.Imaging.PixelFormat.Format24bppRgb);
            Bitmap newBitmap064 = new Bitmap(s64Consts[MAX_WIDTH],  s64Consts[MAX_HEIGHT], System.Drawing.Imaging.PixelFormat.Format24bppRgb);
            Bitmap newBitmap032 = new Bitmap(s32Consts[MAX_WIDTH],  s32Consts[MAX_HEIGHT], System.Drawing.Imaging.PixelFormat.Format24bppRgb);
            Bitmap newBitmap016 = new Bitmap(s16Consts[MAX_WIDTH],  s16Consts[MAX_HEIGHT], System.Drawing.Imaging.PixelFormat.Format24bppRgb);

            Graphics graph256 = Graphics.FromImage(newBitmap256);
            Graphics graph128 = Graphics.FromImage(newBitmap128);
            Graphics graph064 = Graphics.FromImage(newBitmap064);
            Graphics graph032 = Graphics.FromImage(newBitmap032);
            Graphics graph016 = Graphics.FromImage(newBitmap016);

            drawIt(graph256, 0, 0, s256Consts, bm256Consts);
            drawIt(graph128, 0, 0, s128Consts, bm128Consts);
            drawIt(graph064, 0, 0, s64Consts, bm64Consts);
            drawIt(graph032, 0, 0, s32Consts, bm32Consts);
            drawIt(graph016, 0, 0, s16Consts, bm16Consts);

            // We need to save the bitmaps inorder to read them in as a raw binary file
            newBitmap256.Save("NewIcon256.bmp", System.Drawing.Imaging.ImageFormat.Bmp);
            newBitmap128.Save("NewIcon128.bmp", System.Drawing.Imaging.ImageFormat.Bmp);
            newBitmap064.Save("NewIcon64.bmp", System.Drawing.Imaging.ImageFormat.Bmp);
            newBitmap032.Save("NewIcon32.bmp", System.Drawing.Imaging.ImageFormat.Bmp);
            newBitmap016.Save("NewIcon16.bmp", System.Drawing.Imaging.ImageFormat.Bmp);

            // Create the icon file and add five icon to it
            jimsIcon newIcon =  new jimsIcon(5);
            newIcon.add("NewIcon256.bmp");
            newIcon.add("NewIcon128.bmp");
            newIcon.add("NewIcon64.bmp");
            newIcon.add("NewIcon32.bmp");
            newIcon.add("NewIcon16.bmp");

            newIcon.save(fileName);
        }

        //--------------------------------------------------------------------
        // Purpose:
        //     Test code to parse the icon file
        //
        // Notes:
        //     None.
        //--------------------------------------------------------------------
        private void bTest_Click(object sender, EventArgs e)
        {
            jimsIcon newIcon = new jimsIcon();

            newIcon.load("F:\\star\\1432_PSU\\Prototype\\tools\\IconBuilder\\IconBuilder\\TestIcon.ico");
        }
    }

    //----------------------------------------------------------------------------
    //  Class Declarations
    //----------------------------------------------------------------------------
    //
    // Class Name: jimsIcon
    // 
    // Purpose:
    //      Handles reading and writting the ico file
    //
    //----------------------------------------------------------------------------
    class jimsIcon
    {
        //----------------------------------------------------------------------------
        //  Class Attributes 
        //----------------------------------------------------------------------------
        jimsIconHeader mHeader = new jimsIconHeader();
        List<jimsIconEntry> mEntries = new List<jimsIconEntry>();
        int mLocation=0;

        //--------------------------------------------------------------------
        // Purpose:
        //     Empty Constructor
        //
        // Notes:
        //     None.
        //--------------------------------------------------------------------
        public jimsIcon()
        {

        }

        //--------------------------------------------------------------------
        // Purpose:
        //     Constructor for building the icon file
        //
        // Notes:
        //     None.
        //--------------------------------------------------------------------
        public jimsIcon(int count)
        {
            mHeader = new jimsIconHeader(count);
            //Need to get this to the first data block
            mLocation = (16*count)+6;
        }

        //--------------------------------------------------------------------
        // Purpose:
        //     Add a new bitmap file to the icon file
        //
        // Notes:
        //     None.
        //--------------------------------------------------------------------
        public void add(string fileName)
        {
            jimsIconEntry newEntry = new jimsIconEntry(fileName,mLocation);
            mLocation += newEntry.getLength();
            mEntries.Add(newEntry);
        }

        //--------------------------------------------------------------------
        // Purpose:
        //     Load the ico file
        //
        // Notes:
        //     None.
        //--------------------------------------------------------------------
        public void load(string fileName)
        {
            BinaryReader theFile = new BinaryReader(File.Open(fileName, FileMode.Open));

            mHeader.load(theFile);

            for(int i=0;i<mHeader.Count();i++)
            {
                jimsIconEntry newEntry = new jimsIconEntry();
                newEntry.load(theFile);
                mEntries.Add(newEntry);
            }

            for ( int i=0; i < mHeader.Count(); i++ )
            {
                mEntries[i].loadData(theFile);
            }

            theFile.Close();
        }

        //--------------------------------------------------------------------
        // Purpose:
        //     Save the ico file
        //
        // Notes:
        //     None.
        //--------------------------------------------------------------------
        public void save(string fileName)
        {
            BinaryWriter file = new BinaryWriter(File.Open(fileName, FileMode.Create));

            mHeader.save(file);

            for ( int i=0; i < mHeader.Count(); i++ )
            {
                mEntries[i].save(file);
            }

            for ( int i=0; i < mHeader.Count(); i++ )
            {
                mEntries[i].saveData(file);
            }

            file.Close();
        }
    }

    //----------------------------------------------------------------------------
    //  Class Declarations
    //----------------------------------------------------------------------------
    //
    // Class Name: jimsIconHeader
    // 
    // Purpose:
    //      Handles reading and writting the ico file header
    //
    //----------------------------------------------------------------------------
    class jimsIconHeader
    {
        //----------------------------------------------------------------------------
        //  Class Attributes 
        //----------------------------------------------------------------------------
        Int16 mReserved;
        Int16 mType;
        Int16 mCount;

        //--------------------------------------------------------------------
        // Purpose:
        //     Empty Constructor
        //
        // Notes:
        //     None.
        //--------------------------------------------------------------------
        public jimsIconHeader()
        {
            mReserved = 0;
            mType = 1;
            mCount = 0;
        }

        //--------------------------------------------------------------------
        // Purpose:
        //     Constructor for building the ico file
        //
        // Notes:
        //     None.
        //--------------------------------------------------------------------
        public jimsIconHeader(int count)
        {
            mReserved = 0;
            mType = 1;
            mCount = (Int16)count;
        }

        //--------------------------------------------------------------------
        // Purpose:
        //     Load in an ico file
        //
        // Notes:
        //     None.
        //--------------------------------------------------------------------
        public void load(BinaryReader theFile)
        {
            mReserved = theFile.ReadInt16();
            mType = theFile.ReadInt16();
            mCount = theFile.ReadInt16();
        }

        //--------------------------------------------------------------------
        // Purpose:
        //     Save the ico file
        //
        // Notes:
        //     None.
        //--------------------------------------------------------------------
        public void save(BinaryWriter file)
        {
            file.Write(mReserved);
            file.Write(mType);
            file.Write(mCount);
        }

        //--------------------------------------------------------------------
        // Purpose:
        //     Return the count
        //
        // Notes:
        //     None.
        //--------------------------------------------------------------------
        public Int16 Count()
        {
            return mCount;
        }
    }

    //----------------------------------------------------------------------------
    //  Class Declarations
    //----------------------------------------------------------------------------
    //
    // Class Name: jimsIconEntry
    // 
    // Purpose:
    //      Handles reading and writting the ico file entry (sub icon)
    //
    //----------------------------------------------------------------------------
    class jimsIconEntry
    {
        //----------------------------------------------------------------------------
        //  Class Consts
        //----------------------------------------------------------------------------
        const int MAX_BITMAP_HEADER = 14;

        //----------------------------------------------------------------------------
        //  Class Attributes 
        //----------------------------------------------------------------------------
        byte mWidth=0;
        byte mHeight=0;
        byte mColorCount=0;
        byte mReserved=0;
        Int16 mPlanes=0;
        Int16 mBitCount=0;
        Int32 mBytesInRes=0;
        Int32 mImageOffset=0;
        byte[] mData;

        //--------------------------------------------------------------------
        // Purpose:
        //     Empty Constructor
        //
        // Notes:
        //     None.
        //--------------------------------------------------------------------
        public jimsIconEntry()
        {

        }

        //--------------------------------------------------------------------
        // Purpose:
        //     The 'mask' part of our ico file will be all pixels are on.
        //     We need to tell how many extra entries we need after the 
        //     raw bitmap data.  This tells us how many.
        //
        // Notes:
        //     None.
        //--------------------------------------------------------------------
        private int extraBytes(int height)
        {
            int extra = 63;

            switch(height)
            {
                case ( 0x00 ):
                    extra = 8191;
                    break;
                case ( 0x100 ):
                    extra = 8191;
                    break;
                case ( 0x80 ):
                    extra = 2047;
                    break;
                case ( 0x40 ):
                    extra = 511;
                    break;
                case ( 0x20 ):
                    extra = 127;
                    break;
                case ( 0x10 ):
                    extra = 63;
                    break;
            }

            return extra;
        }

        //--------------------------------------------------------------------
        // Purpose:
        //     Constructor for reading in a file
        //
        // Notes:
        //     None.
        //--------------------------------------------------------------------
        public jimsIconEntry(string fileName, int count)
        {
            mImageOffset = count;

            BinaryReader theFile = new BinaryReader(File.Open(fileName, FileMode.Open));

            //--------------------------------------------------------------------
            // Strip out the 14 byte header off of the bitmap file.
            //--------------------------------------------------------------------
            for ( int i=0; i < MAX_BITMAP_HEADER; i++ )
            {
                theFile.ReadByte();
            }

            mBytesInRes = (Int32) theFile.BaseStream.Length - MAX_BITMAP_HEADER;

            //--------------------------------------------------------------------
            // Read in the bitmap file
            //--------------------------------------------------------------------
            byte[] data = new byte[mBytesInRes];

            for ( int i=0; i < mBytesInRes; i++ )
            {
                data[i] = theFile.ReadByte();
            }

            mColorCount = 0;
            mPlanes = 0;
            mReserved = 0;

            mWidth = data[4];
            mHeight = data[8];
            mBitCount = data[14];

            //--------------------------------------------------------------------
            // Add the extra bytes to the data
            //--------------------------------------------------------------------
            mBytesInRes += extraBytes(mHeight);

            mData = new byte[mBytesInRes];

            for(int i=0;i<mBytesInRes;i++)
            {
                mData[i] = 0x00;
            }

            //--------------------------------------------------------------------
            // Double up the height of the bitmap for the mask
            //--------------------------------------------------------------------
            int height = ( data[9] << 8 ) + data[8];
            height *= 2;
            data[9] = (byte) ( height >> 8 );
            data[8] = (byte) ( height & 0xFF );

            //--------------------------------------------------------------------
            // Fill the data attribute
            //--------------------------------------------------------------------
            for ( int i=0; i < data.Length; i++ )
            {
                mData[i] = data[i];
            }

            theFile.Close();
        }

        //--------------------------------------------------------------------
        // Purpose:
        //     Return the length
        //
        // Notes:
        //     None.
        //--------------------------------------------------------------------
        public int getLength()
        {
            return mBytesInRes;
        }

        //--------------------------------------------------------------------
        // Purpose:
        //     load the .ico file header
        //
        // Notes:
        //     None.
        //--------------------------------------------------------------------
        public void load(BinaryReader theFile)
        {
            mWidth = theFile.ReadByte();
            mHeight = theFile.ReadByte();
            mColorCount = theFile.ReadByte();
            mReserved = theFile.ReadByte();

            mPlanes = theFile.ReadInt16();
            mBitCount = theFile.ReadInt16();
            mBytesInRes = theFile.ReadInt32();
            mImageOffset = theFile.ReadInt32();
        }

        //--------------------------------------------------------------------
        // Purpose:
        //     Load the .ico data, the data block is in a diffent place than the
        //     section header.
        //
        // Notes:
        //     None.
        //--------------------------------------------------------------------
        public void loadData(BinaryReader theFile)
        {
            mData = new Byte[mBytesInRes];

            for(int i=0;i<mBytesInRes;i++)
            {
                mData[i] = theFile.ReadByte();
            }
        }

        //--------------------------------------------------------------------
        // Purpose:
        //     save the .ico header
        //
        // Notes:
        //     None.
        //--------------------------------------------------------------------
        public void save(BinaryWriter file)
        {
            file.Write(mWidth);
            file.Write(mHeight);
            file.Write(mColorCount);
            file.Write(mReserved);

            file.Write(mPlanes);
            file.Write(mBitCount);
            file.Write(mBytesInRes);
            file.Write(mImageOffset);
        }

        //--------------------------------------------------------------------
        // Purpose:
        //     save the .ico data
        //
        // Notes:
        //     None.
        //--------------------------------------------------------------------
        public void saveData(BinaryWriter file)
        {
            for(int i=0;i<mData.Length;i++)
            {
                file.Write(mData[i]);
            }
        }
    }
}
