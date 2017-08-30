//----------------------------------------------------------------------------
//
//  $Workfile: ColorLocator.cpp$
//
//  $Revision: X$
//
//  Project:    Open CV example
//
//                            Copyright (c) 2017
//                               James A Wright
//                            All Rights Reserved
//
//  Modification History:
//  $Log:
//  $
//
//  Notes:
//     This is a tool for tuning a camera
//
//----------------------------------------------------------------------------
#include <iostream>
#include "opencv2/highgui/highgui.hpp"
#include "opencv2/imgproc/imgproc.hpp"

using namespace cv;
using namespace std;

//----------------------------------------------------------------------------
//  File Locals
//----------------------------------------------------------------------------
int mMorphSize = 7;
vector<vector<Point> > mContours;
vector<Vec4i> mHierarchy;
Rect mRectangle;
Mat mSourceImage;

//--------------------------------------------------------------------
// Purpose:
//     Main entry point
//
// Notes:
//     None.
//--------------------------------------------------------------------
int main(int argc, char** argv)
{
    VideoCapture cap(2); //capture the video from web cam
    
    // Display the properties of the web cam
    printf("CV_CAP_PROP_POS_MSEC     :%f\n", cap.get(CV_CAP_PROP_POS_MSEC));
    printf("CV_CAP_PROP_POS_FRAMES   :%f\n", cap.get(CV_CAP_PROP_POS_FRAMES));
    printf("CV_CAP_PROP_POS_AVI_RATIO:%f\n", cap.get(CV_CAP_PROP_POS_AVI_RATIO));
    printf("CV_CAP_PROP_FRAME_WIDTH  :%f\n", cap.get(CV_CAP_PROP_FRAME_WIDTH));
    printf("CV_CAP_PROP_FRAME_HEIGHT :%f\n", cap.get(CV_CAP_PROP_FRAME_HEIGHT));
    printf("CV_CAP_PROP_FPS          :%f\n", cap.get(CV_CAP_PROP_FPS));
    printf("CV_CAP_PROP_FOURCC       :%f\n", cap.get(CV_CAP_PROP_FOURCC));
    printf("CV_CAP_PROP_FRAME_COUNT  :%f\n", cap.get(CV_CAP_PROP_FRAME_COUNT));
    printf("CV_CAP_PROP_FORMAT       :%f\n", cap.get(CV_CAP_PROP_FORMAT));
    printf("CV_CAP_PROP_MODE         :%f\n", cap.get(CV_CAP_PROP_MODE));

    printf("CV_CAP_PROP_BRIGHTNESS   :%f\n", cap.get(CV_CAP_PROP_BRIGHTNESS));
    printf("CV_CAP_PROP_CONTRAST     :%f\n", cap.get(CV_CAP_PROP_CONTRAST));
    printf("CV_CAP_PROP_SATURATION   :%f\n", cap.get(CV_CAP_PROP_SATURATION));
    printf("CV_CAP_PROP_HUE          :%f\n", cap.get(CV_CAP_PROP_HUE));
    printf("CV_CAP_PROP_GAIN         :%f\n", cap.get(CV_CAP_PROP_GAIN));
    printf("CV_CAP_PROP_EXPOSURE     :%f\n", cap.get(CV_CAP_PROP_EXPOSURE));
    printf("CV_CAP_PROP_CONVERT_RGB  :%f\n", cap.get(CV_CAP_PROP_CONVERT_RGB));
    printf("CV_CAP_PROP_WHITE_BALANCE_BLUE_U:%f\n", cap.get(CV_CAP_PROP_WHITE_BALANCE_BLUE_U));

    printf("CV_CAP_PROP_RECTIFICATION:%f\n", cap.get(CV_CAP_PROP_RECTIFICATION));
    printf("CV_CAP_PROP_MONOCHROME   :%f\n", cap.get(CV_CAP_PROP_MONOCHROME));
    printf("CV_CAP_PROP_SHARPNESS    :%f\n", cap.get(CV_CAP_PROP_SHARPNESS));
    printf("CV_CAP_PROP_AUTO_EXPOSURE:%f\n", cap.get(CV_CAP_PROP_AUTO_EXPOSURE));
    printf("CV_CAP_PROP_GAMMA        :%f\n", cap.get(CV_CAP_PROP_GAMMA));
    printf("CV_CAP_PROP_TEMPERATURE  :%f\n", cap.get(CV_CAP_PROP_TEMPERATURE));

    printf("CV_CAP_PROP_TRIGGER      :%f\n", cap.get(CV_CAP_PROP_TRIGGER));
    printf("CV_CAP_PROP_TRIGGER_DELAY:%f\n", cap.get(CV_CAP_PROP_TRIGGER_DELAY));
    printf("CV_CAP_PROP_WHITE_BALANCE_RED_V:%f\n", cap.get(CV_CAP_PROP_WHITE_BALANCE_RED_V));
    printf("CV_CAP_PROP_ZOOM         :%f\n", cap.get(CV_CAP_PROP_ZOOM));
    printf("CV_CAP_PROP_FOCUS        :%f\n", cap.get(CV_CAP_PROP_FOCUS));
    printf("CV_CAP_PROP_GUID         :%f\n", cap.get(CV_CAP_PROP_GUID));
    printf("CV_CAP_PROP_ISO_SPEED    :%f\n", cap.get(CV_CAP_PROP_ISO_SPEED));
    printf("CV_CAP_PROP_MAX_DC1394   :%f\n", cap.get(CV_CAP_PROP_MAX_DC1394));
    printf("CV_CAP_PROP_BACKLIGHT    :%f\n", cap.get(CV_CAP_PROP_BACKLIGHT));
    printf("CV_CAP_PROP_PAN          :%f\n", cap.get(CV_CAP_PROP_PAN));
    printf("CV_CAP_PROP_TILT         :%f\n", cap.get(CV_CAP_PROP_TILT));
    printf("CV_CAP_PROP_ROLL         :%f\n", cap.get(CV_CAP_PROP_ROLL));
    printf("CV_CAP_PROP_IRIS         :%f\n", cap.get(CV_CAP_PROP_IRIS));
    printf("CV_CAP_PROP_SETTINGS     :%f\n", cap.get(CV_CAP_PROP_SETTINGS));

    // Uncoment the following to just display the properties
    //while(true);
    
    // Set the properties for the camera
    cap.set(CV_CAP_PROP_BRIGHTNESS,128.000000);
    cap.set(CV_CAP_PROP_CONTRAST, 32.000000);
    cap.set(CV_CAP_PROP_SATURATION, 32.000000);
    cap.set(CV_CAP_PROP_GAIN, 128.000000);
    cap.set(CV_CAP_PROP_SHARPNESS, 24.000000);
    cap.set(CV_CAP_PROP_WHITE_BALANCE_BLUE_U, 4000.000000);
    cap.set(CV_CAP_PROP_EXPOSURE, -5.0);
    
    // Can't open the camera
    if (!cap.isOpened())  // if not success, exit program
    {
        cout << "Cannot open the web cam" << endl;
        return -1;
    }

    namedWindow("Control", CV_WINDOW_AUTOSIZE); //create a window called "Control"

    // Tracker values
    int iLowH = 35;
    int iHighH = 255;

    int iLowS = 117;
    int iHighS = 255;

    int iLowV = 89;
    int iHighV = 188;

    //Create trackbars in "Control" window
    cvCreateTrackbar("LowH", "Control", &iLowH, 255); //Hue (0 - 179)
    cvCreateTrackbar("HighH", "Control", &iHighH, 255);

    cvCreateTrackbar("LowS", "Control", &iLowS, 255); //Saturation (0 - 255)
    cvCreateTrackbar("HighS", "Control", &iHighS, 255);

    cvCreateTrackbar("LowV", "Control", &iLowV, 255); //Value (0 - 255)
    cvCreateTrackbar("HighV", "Control", &iHighV, 255);

    bool bSuccess = cap.read(mSourceImage); // read a new frame from video

    Size matSize;
    matSize.width = mSourceImage.cols;
    matSize.height = mSourceImage.rows;
   
    // Idle loop
    while (true)
    {
        // set the exposure as short as posible
        cap.set(CV_CAP_PROP_EXPOSURE, -8.0);

        bool bSuccess = cap.read(mSourceImage); // read a new frame from video

        if (!bSuccess) //if not success, break loop
        {
            cout << "Cannot read a frame from video stream" << endl;
            break;
        }

        Mat imageInHueSatVal;

        // Convert the captured frame from BGR to HSV
        // I like working in Hue Sat Value
        cvtColor(mSourceImage, imageInHueSatVal, COLOR_BGR2HSV); 

        Mat imgThresholded;
        Mat imgThresholdedCopy;

        inRange(imageInHueSatVal, Scalar(iLowH, iLowS, iLowV), 
            Scalar(iHighH, iHighS, iHighV), imgThresholded); //Threshold the image
        
        //morphological opening (remove small objects from the foreground)
        erode(imgThresholded, imgThresholded, 
            getStructuringElement(MORPH_ELLIPSE, Size(mMorphSize, mMorphSize)));
        dilate(imgThresholded, imgThresholded, 
            getStructuringElement(MORPH_ELLIPSE, Size(mMorphSize, mMorphSize)));

        //morphological closing (fill small holes in the foreground)
        dilate(imgThresholded, imgThresholded, 
            getStructuringElement(MORPH_ELLIPSE, Size(mMorphSize, mMorphSize)));
        erode(imgThresholded, imgThresholded, 
            getStructuringElement(MORPH_ELLIPSE, Size(mMorphSize, mMorphSize)));
        
        // finding the contours corupts the image passed in
        imgThresholded.copyTo(imgThresholdedCopy);

        findContours(imgThresholdedCopy, mContours, mHierarchy, 
            CV_RETR_TREE, CV_CHAIN_APPROX_SIMPLE, Point(0, 0));

        double largestArea = -1;
        int largestCont = -1;

        for (int i = 0; i< mContours.size(); i++)
        {
            double area = contourArea(mContours.at(i));

            if (area > largestArea)
            {
                largestArea = area;
                largestCont = i;
            }
        }

        // Are there contours
        if (-1 != largestCont)
        {
            mRectangle = boundingRect(mContours.at(largestCont));
            approxPolyDP(Mat(mContours.at(largestCont)), mContours.at(largestCont), 3, true);

            Scalar color = Scalar(255, 255, 255);
            drawContours(mSourceImage, mContours, largestCont, color, 2, 8, mHierarchy, 0, Point());
            Scalar color2 = Scalar(255, 128, 255);
            rectangle(mSourceImage, mRectangle, color2, 2);
        }

        // Did we find the biggest contour, display it
        if (-1 != largestCont)
        {
            mRectangle = boundingRect(mContours.at(largestCont));
            approxPolyDP(Mat(mContours.at(largestCont)), mContours.at(largestCont), 3, true);
            printf("X:%4d Y:%4d W:%4d H:%4d A:%6.1f\n", 
                mRectangle.x, mRectangle.y, mRectangle.width, mRectangle.height, largestArea);
        }

        // Show the images
        imshow("Thresholded Image", imgThresholded); //show the thresholded image
        imshow("Original", mSourceImage); //show the original image

        //wait for 'esc' key press for 30ms. If 'esc' key is pressed, break loop
        if (waitKey(30) == 27) 
        {
            cout << "esc key is pressed by user" << endl;
            break;
        }
    }

    return 0;
}
