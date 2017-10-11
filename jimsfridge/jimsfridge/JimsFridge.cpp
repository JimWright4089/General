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

#include <asm/ioctl.h>
#include <chrono>
#include <ctime>
#include <errno.h>
#include <fcntl.h>
#include <fstream>
#include <iostream>
#include <linux/i2c-dev.h>
#include <memory.h>
#include <mosquitto.h>
#include <random>
#include <stdio.h>
#include <stdint.h>
#include <stdlib.h>
#include <string.h>
#include <string>
#include <sys/time.h>
#include <sys/ioctl.h>
#include <sys/mman.h>
#include <sys/types.h>
#include <sys/stat.h>
#include <unistd.h>
#include <vector>
#include <wiringPi.h>
#include <wiringPiI2C.h>
#include <signal.h>
#include <stdarg.h>
#include <getopt.h>
#include "StopWatch.h"

using namespace std;
using namespace chrono;

//----------------------------------------------------------------------------
//  Local functions
//----------------------------------------------------------------------------
static void CtrlCHandler(int signum);
static void SetupHandlers(void);
bool GetDoorState(int door);
bool Debounce(uint8_t pin, uint8_t* curState, uint8_t* lastState, uint32_t* lastTime); 
double ReadTempSensor(int filePointer);
void InitI2C();
void InitSayings(const char* fileName, vector<string>& list);
void Tweet(string text);
void TweetDoor(string text);
void TweetDoorClosed(string door, uint32_t time, double temp);
double CelsiusToFahrenheit(double temp);
string GetTemp(double temp);
void MQTTPublish(char* topic, bool packet);
void MQTTPublish(char* topic, uint32_t packet);
void MQTTPublish(char* topic, int32_t packet);
void MQTTPublish(char* topic, double packet);
void MQTTPublish(char* topic, uint8_t* packet, uint8_t packetLen);
void MQTTPublish(char* topic, string packet);
void ConnectCallback(struct mosquitto *mosq, void *obj, int rc);
void DisconnectCallback(struct mosquitto *mosq, void *obj, int result);
void MessageCallback(struct mosquitto *mosq, void *obj,
  const struct mosquitto_message *msg);

//----------------------------------------------------------------------------
//  Old C defines for the file name stuff
//----------------------------------------------------------------------------
#define SAYINGS_FILE "/home/pi/samba/jimsfridge/sayings.txt"
#define FRIDGE_DOOR_OPEN_SAYINGS_FILE "/home/pi/samba/jimsfridge/FridgeDoorOpen.txt"
#define FREEZER_DOOR_OPEN_SAYINGS_FILE "/home/pi/samba/jimsfridge/FridgeDoorOpen.txt"
#define MQTT_FREEZER_DOOR (char*)"freezer/door"
#define MQTT_FREEZER_TEMP (char*)"freezer/temp"
#define MQTT_FRIDGE_DOOR  (char*)"fridge/door"
#define MQTT_FRIDGE_TEMP  (char*)"fridge/temp"
#define MQTT_BORED_TIME   (char*)"time/bored"
#define MQTT_LOCKOUT_TIME (char*)"time/lockout"
#define MQTT_DOOR_TIME    (char*)"time/door"
#define MQTT_LAST_TWEET   (char*)"tweet/last"
#define MQTT_WEB_TWEET    (char*)"tweet/web"

//----------------------------------------------------------------------------
//  File constants
//----------------------------------------------------------------------------
const uint8_t  FRIDGE_DOOR  = 18;
const uint8_t  FREEZER_DOOR = 17;
const uint8_t  DOOR_CLOSED  =  0;
const uint8_t  DOOR_OPEN    =  1;
const uint16_t BUTTON_DEBOUNCE = 20;
const uint8_t  I2C_WAIT_TIME = 100;
//const int32_t MAX_BORED_TIME = 960; // seconds
//const int32_t MIN_BORED_TIME = 480; // seconds
const uint32_t MAX_BORED_TIME = 28800; // seconds
const uint32_t MIN_BORED_TIME = 7200; // seconds
const uint32_t MAX_DOOR_WAIT = 900; // 15 seconds
const uint32_t MAX_TWEET_WAIT = 300; // 5 seconds
const uint32_t MAX_MQTT_WAIT = 200; // 100 ms
const uint16_t MAX_DOOR_OPEN_CHANCE = 100; // Don't tweet all the time if there are under 100 entries in the door open list
const uint8_t mFridgeAddress = 0x49;  // i2C addresses
const uint8_t mFreezerAddress = 0x48;
const uint32_t MAX_ID_LEN = 50;

//----------------------------------------------------------------------------
//  File constants
//----------------------------------------------------------------------------
// Door debounce vars
uint8_t  mFridgeDoorState = DOOR_CLOSED;
uint8_t  mFreezerDoorState = DOOR_CLOSED;
uint8_t  mLastFridgeDoorState = DOOR_CLOSED;
uint8_t  mLastFreezerDoorState = DOOR_CLOSED;
uint32_t mLastFridgeDoorTime = 0;
uint32_t mLastFreezerDoorTime = 0;

int mFileFridge = 0;              // Fridge I2C file
int mFileFreezer = 0;             // Freezer I2C file
double mFridgeTemp = 0.0;         // Temperatures
double mFreezerTemp = 0.0;
uint32_t mOpenFridgeTime = 0;       // Time the doors were open
uint32_t mOpenFreezerTime = 0;
StopWatch mDoorStopWatch(MAX_DOOR_WAIT, true);
bool mMQTTTweet = false;
struct mosquitto *mMqttServer;
bool mRunning = true;
string mLastTweet = "";

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
  int messageID;
  char mqttID[MAX_ID_LEN];
  int returnCode=0;
  

  // Load in and init all the stuff
  InitSayings(SAYINGS_FILE, mBoredSayings);
  InitSayings(FRIDGE_DOOR_OPEN_SAYINGS_FILE, mFreezerDoorOpenSayings);
  InitSayings(FREEZER_DOOR_OPEN_SAYINGS_FILE, mFridgeDoorOpenSayings);
  StopWatch tweetStopWatch(MAX_TWEET_WAIT, true);
  StopWatch boredStopWatch(true);
  StopWatch mqttStopWatch(MAX_MQTT_WAIT);

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

  usleep(50000000);
  mFridgeTemp = ReadTempSensor(mFileFridge);
  mFreezerTemp = ReadTempSensor(mFileFreezer);

  snprintf(mqttID, 50, "JimsFridge01");
  mMqttServer = mosquitto_new(mqttID, true, NULL);
  printf("mosq:%d\n", mMqttServer);
  mosquitto_connect_callback_set(mMqttServer, ConnectCallback);
  mosquitto_disconnect_callback_set(mMqttServer, DisconnectCallback);
  mosquitto_message_callback_set(mMqttServer, MessageCallback);

  returnCode = mosquitto_connect(mMqttServer, "127.0.0.1", 1883, 600);
  printf("Connect:%d\n", returnCode);
  returnCode = mosquitto_subscribe(mMqttServer, &messageID, MQTT_WEB_TWEET, 0);
  printf("tweet:%d\n", returnCode);

  SetupHandlers();

  Tweet("Jim's Fridge SW V2.01 Online Freezer:"+
    GetTemp(mFreezerTemp)+" Fridge:"+GetTemp(mFridgeTemp));

  boredStopWatch.SetTime(boredDist(generator));
  boredStopWatch.Reset();

  // End on control C
  while(true == mRunning)
  {
    returnCode = mosquitto_loop(mMqttServer, 1, 1);

    mFridgeTemp = ReadTempSensor(mFileFridge);
    mFreezerTemp = ReadTempSensor(mFileFreezer);

    bool fridgeChange = Debounce(FRIDGE_DOOR, &mFridgeDoorState, &mLastFridgeDoorState, &mLastFridgeDoorTime);
    bool freezerChange = Debounce(FREEZER_DOOR, &mFreezerDoorState, &mLastFreezerDoorState, &mLastFreezerDoorTime);

    // Check the MQTT timer to see if it's time to set numbers
    if (true == mqttStopWatch.IsExpired())
    {
      // If the tweet lockout is expired
      if (true == tweetStopWatch.IsExpired())
      {
        // Check if the Tweet is set.
        if (true == mMQTTTweet)
        {
          Tweet("Hi from Jim's Fridge! Freezer:" + GetTemp(mFreezerTemp) + " Fridge:" + GetTemp(mFridgeTemp));
          mMQTTTweet = false;
          MQTTPublish(MQTT_WEB_TWEET, mMQTTTweet);
          tweetStopWatch.Reset();
        }
      }
      else
      {
        mMQTTTweet = false;
        MQTTPublish(MQTT_WEB_TWEET, mMQTTTweet);
      }

      MQTTPublish(MQTT_FREEZER_DOOR, (uint32_t)mFreezerDoorState);
      MQTTPublish(MQTT_FRIDGE_DOOR, (uint32_t)mFridgeDoorState);
      MQTTPublish(MQTT_FREEZER_TEMP, mFreezerTemp);
      MQTTPublish(MQTT_FRIDGE_TEMP, mFridgeTemp);
      MQTTPublish(MQTT_LAST_TWEET, mLastTweet);
      MQTTPublish(MQTT_BORED_TIME, boredStopWatch.GetTimeLeft());
      MQTTPublish(MQTT_LOCKOUT_TIME, tweetStopWatch.GetTimeLeft());
      MQTTPublish(MQTT_DOOR_TIME, mDoorStopWatch.GetTimeLeft());

      mqttStopWatch.Reset();
    }

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
        mOpenFridgeTime = StopWatch::Now();
        printf("Open Fridge time:%d",mOpenFridgeTime);

        string saying = mFridgeDoorOpenSayings.at(fridgeSayingsDist(generator));
        if(mFridgeDoorOpenSayings.size() > doorOpenTweetChance(generator))
        {
          TweetDoor(saying);
        }
      }
    }

    // Check the Freezer door
    if(true == freezerChange)
    {
      if(DOOR_CLOSED == mFreezerDoorState)
      {
        TweetDoorClosed("Freezer", mOpenFreezerTime, mFreezerTemp);
      }
      else
      {
        mOpenFreezerTime = StopWatch::Now();
        printf("Open Freezer time:%d",mOpenFreezerTime);

        string saying = mFreezerDoorOpenSayings.at(freezerSayingsDist(generator));
        if(mFreezerDoorOpenSayings.size() > doorOpenTweetChance(generator))
        {
          TweetDoor(saying);
        }
      }
    }

    if(true == boredStopWatch.IsExpired())
    {
      boredStopWatch.SetTime(boredDist(generator));
      boredStopWatch.Reset();
      mLastTweet = mBoredSayings.at(sayingsDist(generator));
      Tweet(mLastTweet);
      printf("Fridge:%f Freezer=%f Fridge:%d %d Freezer:%d %d\n",
        mFridgeTemp, mFreezerTemp,
        fridgeChange, mFridgeDoorState,
        freezerChange, mFreezerDoorState);
    }
    usleep(10000);
  }

  mosquitto_disconnect(mMqttServer);
  mosquitto_destroy(mMqttServer);
  mosquitto_lib_cleanup();
  return 0;
}

static void CtrlCHandler(int signum)
{
  printf("ctrl c\n");
  (void)(signum);
  mRunning = false;
}

static void SetupHandlers(void)
{
  struct sigaction sa;
  
  sa.sa_handler = CtrlCHandler;

  sigaction(SIGINT, &sa, NULL);
  sigaction(SIGTERM, &sa, NULL);
}

//----------------------------------------------------------------------------
//  Purpose:
//      Debound the door switch
//
//  Notes:
//      None
//
//----------------------------------------------------------------------------
bool Debounce(uint8_t pin, uint8_t* curState, uint8_t* lastState, uint32_t* lastTime)
{
  // read the state of the switch into a local variable:
  uint8_t reading = digitalRead(pin);
  bool stateChanged = false;

  // check to see if you just pressed the button
  // (i.e. the input went from LOW to HIGH),  and you've waited
  // long enough since the last press to ignore any noise:

  // If the switch changed, due to noise or pressing:
  if (reading != *lastState)
  {
    // reset the debouncing timer
    *lastTime = StopWatch::Now();
  }

  if ((StopWatch::Now() - *lastTime) > BUTTON_DEBOUNCE)
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
//
//
//  Notes:
//      None
//
//----------------------------------------------------------------------------
double ReadTempSensor(int filePointer)
{
  uint8_t data[2];
  uint8_t length;

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
  mLastTweet = text;
  cout << comandToSend << endl;
  system(comandToSend.c_str());
}

//----------------------------------------------------------------------------
//  Purpose:
//      Send a tweet to the command line if the door timer has passed
//
//  Notes:
//      None
//
//----------------------------------------------------------------------------
void TweetDoor(string text)
{
  if (true == mDoorStopWatch.IsExpired())
  {
    Tweet(text);
    mDoorStopWatch.Reset();
  }
}

//----------------------------------------------------------------------------
//  Purpose:
//     Tweet out the door closing
//
//  Notes:
//      None
//
//----------------------------------------------------------------------------
void TweetDoorClosed(string door, uint32_t time, double temp)
{
  double length = StopWatch::Now() - time;

  length /= 1000;
  char timeData[10];
  sprintf(timeData,"%5.2f",length);
  string saying = "The " +door +" Door is closed, it was open for "+
	string(timeData)+
	" seconds, and is at "+
	GetTemp(temp)+
	" degrees";
  TweetDoor(saying);
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

//----------------------------------------------------------------------------
//  Purpose:
//     Convert the uint32 then publish the message
//
//  Notes:
//      None
//
//----------------------------------------------------------------------------
void MQTTPublish(char* topic, uint32_t packet)
{
  int length = sizeof(uint32_t);
  uint8_t data[length];

  memcpy(data, &packet, length);
  MQTTPublish(topic, data, length);

}

//----------------------------------------------------------------------------
//  Purpose:
//     Convert the int32 then publish the message
//
//  Notes:
//      None
//
//----------------------------------------------------------------------------
void MQTTPublish(char* topic, int32_t packet)
{
  int length = sizeof(int32_t);
  uint8_t data[length];

  memcpy(data, &packet, length);
  MQTTPublish(topic, data, length);

}

//----------------------------------------------------------------------------
//  Purpose:
//     Convert the double then publish the message
//
//  Notes:
//      None
//
//----------------------------------------------------------------------------
void MQTTPublish(char* topic, double packet)
{
  int length = sizeof(double);
  uint8_t data[length];

  memcpy(data, &packet, length);
  MQTTPublish(topic, data, length);

}

//----------------------------------------------------------------------------
//  Purpose:
//     Convert the bool then publish the message
//
//  Notes:
//      None
//
//----------------------------------------------------------------------------
void MQTTPublish(char* topic, bool packet)
{
  if (true == packet)
  {
    MQTTPublish(topic, (uint8_t*)"true", 4);
  }
  else
  {
    MQTTPublish(topic, (uint8_t*)"false", 5);
  }
}

//----------------------------------------------------------------------------
//  Purpose:
//     Convert the string then publish the message
//
//  Notes:
//      None
//
//----------------------------------------------------------------------------
void MQTTPublish(char* topic, string packet)
{
  uint32_t length = (uint32_t)packet.length();
  uint8_t data[length];

  memcpy(data,packet.c_str(),length);

  MQTTPublish(topic, data, length);
}

//----------------------------------------------------------------------------
//  Purpose:
//     Publish the message
//
//  Notes:
//      None
//
//----------------------------------------------------------------------------
void MQTTPublish(char* topic, uint8_t* packet, uint8_t packetLen)
{
  int returnCode = mosquitto_publish(mMqttServer, NULL, 
    topic, packetLen, packet, 0, 0);

  if (0 != returnCode)
  {
    char data[10];
    sprintf(data, "%d", returnCode);
    string saying = "Jim! The MQTT publish failed with :" + string(data);
    TweetDoor(saying);
  }
}

//----------------------------------------------------------------------------
//  Purpose:
//     Callback to connect
//
//  Notes:
//      None
//
//----------------------------------------------------------------------------
void ConnectCallback(struct mosquitto *mosq, void *obj, int rc)
{
  printf("rc: %d\n", rc);
}

//----------------------------------------------------------------------------
//  Purpose:
//     Callback to disconnect
//
//  Notes:
//      None
//
//----------------------------------------------------------------------------
void DisconnectCallback(struct mosquitto *mosq, void *obj, int result)
{
  Tweet("Jim! the MQTT server was disconnected");
}

//----------------------------------------------------------------------------
//  Purpose:
//     Callback for a message
//
//  Notes:
//      We only are subscribed to one thing
//
//----------------------------------------------------------------------------
void MessageCallback(struct mosquitto *mosq, void *obj,
  const struct mosquitto_message *msg)
{
  char* ptrChar = (char*)msg->payload;
  char theChar = ptrChar[0];

  if (0x01 == theChar)
  {
    mMQTTTweet = true;
  }
  if ('1' == theChar)
  {
    mMQTTTweet = true;
  }
  if ('T' == theChar)
  {
    mMQTTTweet = true;
  }
  if ('t' == theChar)
  {
    mMQTTTweet = true;
  }
}

