//----------------------------------------------------------------------------
//
//  $Workfile: HuMoments.cpp$
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
//     This is a tool created mostly by modifying the example code
//
//----------------------------------------------------------------------------
#include "opencv2/highgui/highgui.hpp"
#include "opencv2/imgproc/imgproc.hpp"
#include <iostream>
#include <stdio.h>
#include <stdlib.h>

using namespace cv;
using namespace std;

//----------------------------------------------------------------------------
//  File Locals
//----------------------------------------------------------------------------
Mat mSourceImage; 
Mat mGreyScaleFromSource;
int mThresholdValue = 100;
int mMaxThreshold = 255;
RNG mRandomNumber(12345);

//----------------------------------------------------------------------------
//  Local function headers
//----------------------------------------------------------------------------
void thresholdTrackbarCallback(int, void*);

//--------------------------------------------------------------------
// Purpose:
//     Main entry point
//
// Notes:
//     None.
//--------------------------------------------------------------------
int main(int argc, char** argv)
{
    /// Load source image and convert it to gray
    mSourceImage = imread("triangle.bmp", 1);
//    mSourceImage = imread("cone2.jpg", 1);

    /// Convert image to gray and blur it
    cvtColor(mSourceImage, mGreyScaleFromSource, CV_BGR2GRAY);
    blur(mGreyScaleFromSource, mGreyScaleFromSource, Size(5, 5));

    /// Create Window
    char* source_window = "Source";
    namedWindow(source_window, CV_WINDOW_AUTOSIZE);
    imshow(source_window, mSourceImage);

    createTrackbar(" Canny thresh:", "Source", &mThresholdValue, mMaxThreshold, thresholdTrackbarCallback);
    thresholdTrackbarCallback(0, 0);

    /// Wait for a key.  The trackbar does all the work
    waitKey(0);
    return(0);
}

//--------------------------------------------------------------------
// Purpose:
//     Main entry point
//
// Notes:
//     None.
//--------------------------------------------------------------------
void thresholdTrackbarCallback(int, void*)
{
    Mat canny_output;
    vector<vector<Point> > contours;
    vector<Vec4i> hierarchy;

    /// Detect edges using canny
    Canny(mGreyScaleFromSource, canny_output, mThresholdValue, mThresholdValue * 2, 3);
    /// Find contours
    findContours(canny_output, contours, hierarchy, 
        CV_RETR_TREE, CV_CHAIN_APPROX_SIMPLE, Point(0, 0));

    /// Get the moments
    vector<Moments> mu(contours.size());
    for (int i = 0; i < contours.size(); i++)
    {
        mu[i] = moments(contours[i], false);
    }

    ///  Get the mass centers:
    vector<Point2f> mc(contours.size());
    for (int i = 0; i < contours.size(); i++)
    {
        mc[i] = Point2f((float)(mu[i].m10 / mu[i].m00), (float)(mu[i].m01 / mu[i].m00));
    }

    /// Draw contours
    Mat drawing = Mat::zeros(canny_output.size(), CV_8UC3);
    for (int i = 0; i< contours.size(); i++)
    {
        Scalar color = Scalar(mRandomNumber.uniform(0, 255), 
            mRandomNumber.uniform(0, 255), mRandomNumber.uniform(0, 255));
        drawContours(drawing, contours, i, color, 2, 8, hierarchy, 0, Point());
        circle(drawing, mc[i], 4, color, -1, 8, 0);
    }

    /// Show in a window
    namedWindow("Grey", CV_WINDOW_AUTOSIZE);
    imshow("Grey", mGreyScaleFromSource);

    /// Show in a window
    namedWindow("Canny", CV_WINDOW_AUTOSIZE);
    imshow("Canny", canny_output);

    /// Show in a window
    namedWindow("Contours", CV_WINDOW_AUTOSIZE);
    imshow("Contours", drawing);

    /// Calculate the area with the moments 00 and compare with the result of the OpenCV function
    printf("\t Info: Area and Contour Length \n");
    for (int i = 0; i< contours.size(); i++)
    {
        printf(" * Contour[%d] - Area (M_00) = %.2f - Area OpenCV: %.2f - Length: %.2f \n", 
            i, mu[i].m00, contourArea(contours[i]), arcLength(contours[i], true));
        Scalar color = Scalar(mRandomNumber.uniform(0, 255), 
            mRandomNumber.uniform(0, 255), mRandomNumber.uniform(0, 255));
        drawContours(drawing, contours, i, color, 2, 8, hierarchy, 0, Point());
        circle(drawing, mc[i], 4, color, -1, 8, 0);
    }
}