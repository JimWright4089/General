//----------------------------------------------------------------------------
//
//  $Workfile: JimsFridge.cpp$
//
//  $Revision: X$
//
//  Project:    JimsFridge
//
//                            Copyright (c) 2017
//                                Jim Wright
//                            All Rights Reserved
//
//  Modification History:
//  $Log:
//  $
//
//----------------------------------------------------------------------------
// compile: g++ JimsFridge.cpp -o JimsFridge -lwiringPi -lstdc++

#include <memory.h>
#include <unistd.h>
#include <errno.h>
#include <fcntl.h>
#include <sys/time.h>
#include <sys/ioctl.h>
#include <asm/ioctl.h>
#include <wiringPi.h>
#include <wiringPiI2C.h>
#include <linux/i2c-dev.h>
#include <stdio.h>
#include <iostream>
#include <string>
#include <vector>
#include <fstream>
#include <ctime>
#include <random>
#include <chrono>

using namespace std;
using namespace chrono;

//----------------------------------------------------------------------------
//  Type defs
//----------------------------------------------------------------------------
typedef unsigned char  uint8;
typedef unsigned short uint16;
typedef unsigned long  uint32;
typedef char  sint8;
typedef short sint16;
typedef long  sint32;

//----------------------------------------------------------------------------
//  Local functions
//----------------------------------------------------------------------------
bool GetDoorState(int door);
bool Debounce(uint8 pin, uint8* curState, uint8* lastState, uint32* lastTime); 
uint32 GetTime();
uint32 GetTimeInSeconds();
double ReadTempSensor(int filePointer);
void InitI2C();
void InitSayings(const char* fileName, vector<string>& list);
void Tweet(string text);
void TweetDoorClosed(string door, uint32 time, double temp);
double CelsiusToFahrenheit(double temp);
string GetTemp(double temp);

//----------------------------------------------------------------------------
//  Old C defines for the file name stuff
//----------------------------------------------------------------------------
#define SAYINGS_FILE "/home/pi/samba/jimsfridge/sayings.txt"
#define FRIDGE_DOOR_OPEN_SAYINGS_FILE "/home/pi/samba/jimsfridge/FridgeDoorOpen.txt"
#define FREEZER_DOOR_OPEN_SAYINGS_FILE "/home/pi/samba/jimsfridge/FridgeDoorOpen.txt"

//----------------------------------------------------------------------------
//  File constants
//----------------------------------------------------------------------------
const uint8  FRIDGE_DOOR  = 18;
const uint8  FREEZER_DOOR = 17;
const uint8  DOOR_CLOSED  =  0;
const uint8  DOOR_OPEN    =  1;
const uint16 BUTTON_DEBOUNCE = 20;
const uint8  I2C_WAIT_TIME = 100;
const uint32 MAX_BORED_TIME = 28800;
const uint32 MIN_BORED_TIME =  7200;
const uint16 MAX_DOOR_OPEN_CHANCE = 100; // Don't tweet all the time if there are under 100 entries in the door open list
const int mFridgeAddress = 0x49;  // i2C addresses
const int mFreezerAddress = 0x48;

//----------------------------------------------------------------------------
//  File constants
//----------------------------------------------------------------------------
// Door debounce vars
uint8  mFridgeDoorState = DOOR_CLOSED;
uint8  mFreezerDoorState = DOOR_CLOSED;
uint8  mLastFridgeDoorState = DOOR_CLOSED;
uint8  mLastFreezerDoorState = DOOR_CLOSED;
uint32 mLastFridgeDoorTime = 0;
uint32 mLastFreezerDoorTime = 0;

uint32 mNextBoredTweet = 0;       // time the next bored tweet happens
int mFileFridge = 0;              // Fridge I2C file
int mFileFreezer = 0;             // Freezer I2C file
double mFridgeTemp = 0.0;         // Temperatures
double mFreezerTemp = 0.0;
uint32 mOpenFridgeTime = 0;       // Time the doors were open
uint32 mOpenFreezerTime = 0;

// Sayings list
vector<string> mBoredSayings;
vector<string> mFreezerDoorOpenSayings;
vector<string> mFridgeDoorOpenSayings;

//----------------------------------------------------------------------------
//  Purpose:
//      Main entry point
//
//  Notes:
//      None
//
//----------------------------------------------------------------------------
int main(void)
{
  // Load in and init all the stuff
  InitSayings(SAYINGS_FILE, mBoredSayings);
  InitSayings(FRIDGE_DOOR_OPEN_SAYINGS_FILE, mFreezerDoorOpenSayings);
  InitSayings(FREEZER_DOOR_OPEN_SAYINGS_FILE, mFridgeDoorOpenSayings);

  wiringPiSetup();
  wiringPiSetupGpio();
  InitI2C();
  pinMode (FRIDGE_DOOR, INPUT) ;
  pinMode (FREEZER_DOOR, INPUT) ;

  // Setup all of the random numbers
  unsigned seed = std::chrono::system_clock::now().time_since_epoch().count();
  std::default_random_engine generator (seed);

  std::uniform_int_distribution<int> boredDist(MIN_BORED_TIME,MAX_BORED_TIME);
  std::uniform_int_distribution<int> sayingsDist(0,mBoredSayings.size()-1);
  std::uniform_int_distribution<int> fridgeSayingsDist(0,mFridgeDoorOpenSayings.size()-1);
  std::uniform_int_distribution<int> freezerSayingsDist(0,mFreezerDoorOpenSayings.size()-1);
  std::uniform_int_distribution<int> doorOpenTweetChance(0,MAX_DOOR_OPEN_CHANCE);

  usleep(500000);
  mFridgeTemp = ReadTempSensor(mFileFridge);
  mFreezerTemp = ReadTempSensor(mFileFreezer);

  Tweet("Jim's Fridge SW V2.00 Online Freezer:"+GetTemp(mFreezerTemp)+" Fridge:"+GetTemp(mFridgeTemp));


  /*
  for (int n=0; n<mFridgeDoorOpenSayings.size(); ++n)
  {
        cout << mFridgeDoorOpenSayings.at( n ) << endl;
  }
  */

  // Set the random tweet time
  mNextBoredTweet = (GetTimeInSeconds()) + boredDist(generator);

  // Never end
  while(true)
  {
    mFridgeTemp = ReadTempSensor(mFileFridge);
    mFreezerTemp = ReadTempSensor(mFileFreezer);

    bool fridgeChange = Debounce(FRIDGE_DOOR, &mFridgeDoorState, &mLastFridgeDoorState, &mLastFridgeDoorTime);
    bool freezerChange = Debounce(FREEZER_DOOR, &mFreezerDoorState, &mLastFreezerDoorState, &mLastFreezerDoorTime);

    // Check the Fridge door
    // These two chunks are very much the same, but the paramters into a common
    // function would be too many for the cost
    if(true == fridgeChange)
    {
        //printf("Fridge:%d\n",mFridgeDoorState);

        if(DOOR_CLOSED == mFridgeDoorState)
        {
          TweetDoorClosed("Fridge", mOpenFridgeTime, mFridgeTemp);
        }
        else
        {
          mOpenFridgeTime = GetTime();
		  printf("Open Fridge time:%d",mOpenFridgeTime);

          string saying = mFridgeDoorOpenSayings.at(fridgeSayingsDist(generator));
	  if(mFridgeDoorOpenSayings.size() > doorOpenTweetChance(generator))
	  {
            Tweet(saying);
          }
        }
    }

    // Check the Freezer door
    if(true == freezerChange)
    {
	//printf("Freezer:%d\n",mFreezerDoorState);

	if(DOOR_CLOSED == mFreezerDoorState)
        {
          TweetDoorClosed("Freezer", mOpenFreezerTime, mFreezerTemp);
        }
        else
        {
          mOpenFreezerTime = GetTime();
		  printf("Open Freezer time:%d",mOpenFreezerTime);

          string saying = mFreezerDoorOpenSayings.at(freezerSayingsDist(generator));
          if(mFreezerDoorOpenSayings.size() > doorOpenTweetChance(generator))
          {
            Tweet(saying);
          }
        }
    }

    if((GetTimeInSeconds()) > mNextBoredTweet)
    {
      mNextBoredTweet = (GetTimeInSeconds()) + boredDist(generator);
      string saying = mBoredSayings.at(sayingsDist(generator));
      Tweet(saying);
      printf("Fridge:%f Freezer=%f Fridge:%d %d Freezer:%d %d\n",
			mFridgeTemp,mFreezerTemp,
			fridgeChange,mFridgeDoorState,
			freezerChange,mFreezerDoorState);
    }

    // Roll over pick a new time
    if((mNextBoredTweet - GetTimeInSeconds()) > MAX_BORED_TIME)
    {
      mNextBoredTweet = (GetTimeInSeconds()) + boredDist(generator);
    }

    usleep(10000);
  }
  return 0;
}

//----------------------------------------------------------------------------
//  Purpose:
//      Debound the door switch
//
//  Notes:
//      None
//
//----------------------------------------------------------------------------
bool Debounce(uint8 pin, uint8* curState, uint8* lastState, uint32* lastTime)
{
  // read the state of the switch into a local variable:
  uint8 reading = digitalRead(pin);
  bool stateChanged = false;

  // check to see if you just pressed the button
  // (i.e. the input went from LOW to HIGH),  and you've waited
  // long enough since the last press to ignore any noise:

  // If the switch changed, due to noise or pressing:
  if (reading != *lastState)
  {
    // reset the debouncing timer
    *lastTime = GetTime();
  }

  if ((GetTime() - *lastTime) > BUTTON_DEBOUNCE)
  {
    // whatever the reading is at, it's been there for longer
    // than the debounce delay, so take it as the actual current state:

    // if the button state has changed:
    if (reading != *curState)
    {
      *curState = reading;
      stateChanged = true;
    }
  }

  *lastState = reading;
  return(stateChanged);
}

//----------------------------------------------------------------------------
//  Purpose:
//      Returns the time in milliseconds
//
//  Notes:
//      None
//
//----------------------------------------------------------------------------
uint32 GetTime()
{
  milliseconds ms = duration_cast< milliseconds >(system_clock::now().time_since_epoch());

  return ms.count();
}

//----------------------------------------------------------------------------
//  Purpose:
//      Returns the time in milliseconds
//
//  Notes:
//      None
//
//----------------------------------------------------------------------------
uint32 GetTimeInSeconds()
{
  return GetTime()/1000;
}

//----------------------------------------------------------------------------
//  Purpose:
//
//
//  Notes:
//      None
//
//----------------------------------------------------------------------------
double ReadTempSensor(int filePointer)
{
  uint8 data[2];
  uint8 length;

//  usleep(25000);

  // a 0x00 means ge tthe temperature
  data[0] = 0x00;

  //----- WRITE BYTES -----
  length = 1;
  if (write(filePointer, data, length) != length)
  {
    printf("Failed to write to the i2c bus.\n");
  }

  usleep(I2C_WAIT_TIME);

  //----- READ BYTES -----
  length = 2;
  if (read(filePointer, data, length) != length)
  {
    printf("Failed to read from the i2c bus.\n");
  }
  else
  {
    // The temperature is in a 12 bit int.
    short intTemp = data[0]<<8;
    intTemp += data[1];
    intTemp /=32;
    // move the decimal
    return ((double)intTemp)*0.0625;
  }
  return 0.0;
}

//----------------------------------------------------------------------------
//  Purpose:
//
//
//  Notes:
//      None
//
//----------------------------------------------------------------------------
void InitI2C()
{
  char *filename = (char*)"/dev/i2c-1";

  //----- OPEN THE I2C BUS -----
  if ((mFileFridge = open(filename, O_RDWR)) < 0)
  {
    printf("Failed to open the fridge i2c bus");
    return;
  }

  if (ioctl(mFileFridge, I2C_SLAVE, mFridgeAddress) < 0)
  {
    printf("Failed to acquire bus access and/or talk to fridge temp sensor.\n");
    return;
  }

  if ((mFileFreezer = open(filename, O_RDWR)) < 0)
  {
    printf("Failed to open the freezer i2c bus");
    return;
  }

  if (ioctl(mFileFreezer, I2C_SLAVE, mFreezerAddress) < 0)
  {
    printf("Failed to acquire bus access and/or talk to freezer temp sensor.\n");
    return;
  }
}

//----------------------------------------------------------------------------
//  Purpose:
//      Load in the saying file
//
//  Notes:
//      None
//
//----------------------------------------------------------------------------
void InitSayings(const char* fileName, vector<string>& list)
{
  ifstream sayingsFile(fileName);

  if(!sayingsFile)
  {
    cout << "Cannot open input file.\n";
    return;
  }

  // The saysing file all end with "fin'" and then the character count lines,
  // we want to load everything up to fin.
  string saying;
  while(getline( sayingsFile, saying ))
  {
    if("fin'" == saying.substr(0,4))
    {
      break;
    }
    list.push_back(saying);
  }

  sayingsFile.close();
}

//----------------------------------------------------------------------------
//  Purpose:
//      Send a tweet to the command line
//
//  Notes:
//      None
//
//----------------------------------------------------------------------------
void Tweet(string text)
{
  string comandToSend = "python /home/pi/samba/jimsfridge/tweetaline.py \"" + text + "\"";

  cout << comandToSend << endl;
  system(comandToSend.c_str());
}

//----------------------------------------------------------------------------
//  Purpose:
//     Tweet out the door closing
//
//  Notes:
//      None
//
//----------------------------------------------------------------------------
void TweetDoorClosed(string door, uint32 time, double temp)
{
  double length = GetTime() - time;

  length /= 1000;
  char timeData[10];
  sprintf(timeData,"%5.2f",length);
  string saying = "The " +door +" Door is closed, is was open for "+
	string(timeData)+
	" seconds, and is at "+
	GetTemp(temp)+
	" degrees";
  Tweet(saying);
}

//----------------------------------------------------------------------------
//  Purpose:
//     Convert temps
//
//  Notes:
//      None
//
//----------------------------------------------------------------------------
double CelsiusToFahrenheit(double temp)
{
  return temp * 9.0/5.0 + 32.0;
}

//----------------------------------------------------------------------------
//  Purpose:
//     Convert temps
//
//  Notes:
//      None
//
//----------------------------------------------------------------------------
string GetTemp(double temp)
{
  char tempData[100];
  sprintf(tempData,"%5.2fF (%5.2fC)",CelsiusToFahrenheit(temp),temp);
  return string(tempData);
}

