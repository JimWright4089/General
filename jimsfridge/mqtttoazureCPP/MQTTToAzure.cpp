//----------------------------------------------------------------------------
//
//  $Workfile: MQTTToAzure.cpp$
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
double CelsiusToFahrenheit(double temp);
void MQTTPublish(char* topic, bool packet);
void MQTTPublish(char* topic, uint32_t packet);
void MQTTPublish(char* topic, double packet);
void MQTTPublish(char* topic, uint8_t* packet, uint8_t packetLen);
void MQTTPublish(char* topic, string packet);
void MQTTConvert(uint8_t* packet, uint8_t packetLen, uint8_t* text, uint8_t textLen);
void MQTTConvertUInt32(uint8_t* packet, uint8_t packetLen, uint32_t &number);
void MQTTConvertInt32(uint8_t* packet, uint8_t packetLen, int32_t &number);
void MQTTConvert(uint8_t* packet, uint8_t packetLen, double &number);
void MQTTConvertBool(uint8_t* packet, uint8_t packetLen, uint8_t &boolean);
void ConnectCallback(struct mosquitto *mosq, void *obj, int rc);
void DisconnectCallback(struct mosquitto *mosq, void *obj, int result);
void MessageCallback(struct mosquitto *mosq, void *obj,
  const struct mosquitto_message *msg);

//----------------------------------------------------------------------------
//  Old C defines for the file name stuff
//----------------------------------------------------------------------------
#define MQTT_FREEZER_DOOR (char*)"freezer/door"
#define MQTT_FREEZER_TEMP (char*)"freezer/temp"
#define MQTT_FRIDGE_DOOR  (char*)"fridge/door"
#define MQTT_FRIDGE_TEMP  (char*)"fridge/temp"
#define MQTT_BORED_TIME   (char*)"time/bored"
#define MQTT_LOCKOUT_TIME (char*)"time/lockout"
#define MQTT_DOOR_TIME    (char*)"time/door"
#define MQTT_LAST_TWEET   (char*)"tweet/last"
#define MQTT_WEB_TWEET    (char*)"tweet/web"
#define MQTT_FREEZER_ALL  (char*)"freezer/#"
#define MQTT_FRIDGE_ALL   (char*)"fridge/#"
#define MQTT_TIME_ALL     (char*)"time/#"
#define MQTT_TWEET_ALL    (char*)"tweet/#"

//----------------------------------------------------------------------------
//  File constants
//----------------------------------------------------------------------------
const uint32_t MAX_AZURE_WAIT = 1000; // 1 seconds
const uint32_t MAX_DISPLAY_WAIT = 2000; // 2 seconds
const uint32_t MAX_ID_LEN = 50;
const uint32_t MAX_TWEET_LEN = 141;

//----------------------------------------------------------------------------
//  File constants
//----------------------------------------------------------------------------
struct mosquitto *mMqttServer;
bool mRunning = true;
uint32_t mFreezerDoor = 0;
uint32_t mFridgeDoor  = 0;
double   mFreezerTemp = 0.0;
double   mFridgeTemp  = 0.0;
int32_t  mBoredTime   = 0;
int32_t  mDoorTime    = 0;
int32_t  mLockoutTime = 0;
uint8_t  mWebTweet    = 0;
char     mLastTweet[MAX_TWEET_LEN];

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

  StopWatch azureStopWatch(MAX_AZURE_WAIT);
  StopWatch displayStopWatch(MAX_DISPLAY_WAIT);

  snprintf(mqttID, 50, "MQTTToAzure");
  mMqttServer = mosquitto_new(mqttID, true, NULL);
  printf("mosq:%d\n", mMqttServer);
  mosquitto_connect_callback_set(mMqttServer, ConnectCallback);
  mosquitto_disconnect_callback_set(mMqttServer, DisconnectCallback);
  mosquitto_message_callback_set(mMqttServer, MessageCallback);

  returnCode = mosquitto_connect(mMqttServer, "127.0.0.1", 1883, 600);
  printf("Connect:%d\n", returnCode);
  returnCode = mosquitto_subscribe(mMqttServer, &messageID, MQTT_FREEZER_ALL, 0);
  printf("%s:%d\n", MQTT_FREEZER_ALL, returnCode);
  returnCode = mosquitto_subscribe(mMqttServer, &messageID, MQTT_FRIDGE_ALL, 0);
  printf("%s:%d\n", MQTT_FRIDGE_ALL, returnCode);
  returnCode = mosquitto_subscribe(mMqttServer, &messageID, MQTT_TIME_ALL, 0);
  printf("%s:%d\n", MQTT_TIME_ALL, returnCode);
  returnCode = mosquitto_subscribe(mMqttServer, &messageID, MQTT_TWEET_ALL, 0);
  printf("%s:%d\n", MQTT_TWEET_ALL, returnCode);

  SetupHandlers();

  displayStopWatch.Reset();
  azureStopWatch.Reset();

  // End on control C
  while(true == mRunning)
  {
    returnCode = mosquitto_loop(mMqttServer, 1, 1);

    // Check the MQTT timer to see if it's time to set numbers
    if (true == azureStopWatch.IsExpired())
    {
      azureStopWatch.Reset();
    }

    if (true == displayStopWatch.IsExpired())
    {
      printf("Display\n");
      
      printf("%s:%d\n", MQTT_FREEZER_DOOR, mFreezerDoor);
      printf("%s:%d\n", MQTT_FRIDGE_DOOR, mFridgeDoor);
      printf("%s:%d\n", MQTT_BORED_TIME, mBoredTime);
      printf("%s:%d\n", MQTT_LOCKOUT_TIME, mLockoutTime);
      printf("%s:%d\n", MQTT_DOOR_TIME, mDoorTime);
      printf("%s:%d\n", MQTT_WEB_TWEET, mWebTweet);
      printf("%s:%f\n", MQTT_FREEZER_TEMP, mFreezerTemp);
      printf("%s:%f\n", MQTT_FRIDGE_TEMP, mFridgeTemp);
      printf("%s:%s\n", MQTT_LAST_TWEET, mLastTweet);
      
      displayStopWatch.Reset();
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
}

void MQTTConvert(uint8_t* packet, uint8_t packetLen, uint8_t* text, uint8_t textLen)
{
  
  int length = MAX_TWEET_LEN;
  if (packetLen < length)
  {
    length = packetLen;
  }

  if (length > 0)
  {
    memcpy(text, packet, length);
  }
}

void MQTTConvertUInt32(uint8_t* packet, uint8_t packetLen, uint32_t &number)
{
  uint32_t temp = 0;

  if (packetLen >= sizeof(uint32_t))
  {
    memcpy(&temp, packet, sizeof(uint32_t));
  }
  number = temp;
}

void MQTTConvertInt32(uint8_t* packet, uint8_t packetLen, int32_t &number)
{
  int32_t temp = 0;

  if (packetLen >= sizeof(int32_t))
  {
    memcpy(&temp, packet, sizeof(int32_t));
  }
  number = temp;
}

void MQTTConvert(uint8_t* packet, uint8_t packetLen, double &number)
{
  double temp = 0;

  if (packetLen >= sizeof(double))
  {
    memcpy(&temp, packet, sizeof(double));
  }
  number = temp;
}

void MQTTConvertBool(uint8_t* packet, uint8_t packetLen, uint8_t &boolean)
{
  boolean=0;

  if (packetLen > 0)
  {
    if ('t' == packet[0])
    {
      boolean = 1;
    }
    if ('T' == packet[0])
    {
      boolean = 1;
    }
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
  printf("Jim! the MQTT server was disconnected");
}

//----------------------------------------------------------------------------
//  Purpose:
//     Callback for a message
//
//  Notes:
//      None
//
//----------------------------------------------------------------------------
void MessageCallback(struct mosquitto *mosq, void *obj,
  const struct mosquitto_message *msg)
{
  if (0 == strncmp(msg->topic, MQTT_FREEZER_DOOR,
    strnlen(MQTT_FREEZER_DOOR,MAX_ID_LEN)))
  {
    MQTTConvertUInt32((uint8_t*)msg->payload, msg->payloadlen, mFreezerDoor);
  }

  if (0 == strncmp(msg->topic, MQTT_FREEZER_TEMP,
    strnlen(MQTT_FREEZER_TEMP, MAX_ID_LEN)))
  {
    MQTTConvert((uint8_t*)msg->payload, msg->payloadlen, mFreezerTemp);
  }

  if (0 == strncmp(msg->topic, MQTT_FRIDGE_DOOR,
    strnlen(MQTT_FRIDGE_DOOR, MAX_ID_LEN)))
  {
    MQTTConvertUInt32((uint8_t*)msg->payload, msg->payloadlen, mFridgeDoor);
  }

  if (0 == strncmp(msg->topic, MQTT_FRIDGE_TEMP,
    strnlen(MQTT_FRIDGE_TEMP, MAX_ID_LEN)))
  {
    MQTTConvert((uint8_t*)msg->payload, msg->payloadlen, mFridgeTemp);
  }

  if (0 == strncmp(msg->topic, MQTT_LOCKOUT_TIME,
    strnlen(MQTT_LOCKOUT_TIME, MAX_ID_LEN)))
  {
    MQTTConvertInt32((uint8_t*)msg->payload, msg->payloadlen, mLockoutTime);
  }

  if (0 == strncmp(msg->topic, MQTT_BORED_TIME,
    strnlen(MQTT_BORED_TIME, MAX_ID_LEN)))
  {
    MQTTConvertInt32((uint8_t*)msg->payload, msg->payloadlen, mBoredTime);
  }

  if (0 == strncmp(msg->topic, MQTT_DOOR_TIME,
    strnlen(MQTT_DOOR_TIME, MAX_ID_LEN)))
  {
    MQTTConvertInt32((uint8_t*)msg->payload, msg->payloadlen, mDoorTime);
  }

  if (0 == strncmp(msg->topic, MQTT_LAST_TWEET,
    strnlen(MQTT_LAST_TWEET, MAX_ID_LEN)))
  {
    MQTTConvert((uint8_t*)msg->payload, msg->payloadlen, 
      (uint8_t*)mLastTweet, MAX_TWEET_LEN);
  }

  if (0 == strncmp(msg->topic, MQTT_WEB_TWEET,
    strnlen(MQTT_WEB_TWEET, MAX_ID_LEN)))
  {
    MQTTConvertBool((uint8_t*)msg->payload, msg->payloadlen, mWebTweet);
  }
}

