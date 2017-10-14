//----------------------------------------------------------------------------
//
//  $Workfile: MQTTToAWS.c$
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
#include <stdio.h>
#include <stdlib.h>
#include <ctype.h>
#include <unistd.h>
#include <memory.h>
#include <mosquitto.h>

#include <signal.h>
#include <memory.h>
#include <sys/time.h>
#include <limits.h>
#include <time.h>

#include "aws_iot_log.h"
#include "aws_iot_version.h"
#include "aws_iot_mqtt_interface.h"
#include "aws_iot_config.h"

//----------------------------------------------------------------------------
//  Local functions
//----------------------------------------------------------------------------
static void CtrlCHandler(int signum);
static void SetupHandlers(void);
void MQTTConvertText(uint8_t* packet, uint8_t packetLen, uint8_t* text, uint8_t textLen);
void MQTTConvertUInt32(uint8_t* packet, uint8_t packetLen, uint32_t* number);
void MQTTConvertInt32(uint8_t* packet, uint8_t packetLen, int32_t* number);
void MQTTConvertDouble(uint8_t* packet, uint8_t packetLen, double* number);
void MQTTConvertBool(uint8_t* packet, uint8_t packetLen, uint8_t* boolean);
void MQTTConnectCallback(struct mosquitto *mosq, void *obj, int rc);
void MQTTDisconnectCallback(struct mosquitto *mosq, void *obj, int result);
void MQTTMessageCallback(struct mosquitto *mosq, void *obj,
  const struct mosquitto_message *msg);
int  AWSCallbackHandler(MQTTCallbackParams params);
void AWSDisconnectCallbackHandler(void);
void MQTTPublishInt32(char* topic, int32_t packet);
void MQTTPublishUInt32(char* topic, uint32_t packet);
void MQTTPublishDouble(char* topic, double packet);
void MQTTPublishBool(char* topic, bool packet);
void MQTTJimsPublish(char* topic, uint8_t* packet, uint8_t packetLen);

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
#define MAX_ID_LEN         50
#define MAX_TWEET_LEN     141


//----------------------------------------------------------------------------
//  Global Variables
//----------------------------------------------------------------------------
char gCertDirectory[PATH_MAX + 1] = "/home/pi/certs";
char gHostAddress[255] = AWS_IOT_MQTT_HOST;
uint32_t gPort = AWS_IOT_MQTT_PORT;
uint32_t gPublishCount = 0;

int gRunning = 1;
struct mosquitto *gMqttServer;
uint32_t gFreezerDoor = 0;
uint32_t gFridgeDoor = 0;
double   gFreezerTemp = 0.0;
double   gFridgeTemp = 0.0;
int32_t  gBoredTime = 0;
int32_t  gDoorTime = 0;
int32_t  gLockoutTime = 0;
bool     gWebTweet = false;
bool     gOldWebTweet = false;
char     gLastTweet[MAX_TWEET_LEN];

//----------------------------------------------------------------------------
//  Purpose:
//     Convert the uint32 then publish the message
//
//  Notes:
//      None
//
//----------------------------------------------------------------------------
void MQTTPublishInt32(char* topic, int32_t packet)
{
  int length = 13;
  uint8_t data[length];
  sprintf(data, "%d", packet);

  MQTTJimsPublish(topic, data, length);
}

//----------------------------------------------------------------------------
//  Purpose:
//     Convert the uint32 then publish the message
//
//  Notes:
//      None
//
//----------------------------------------------------------------------------
void MQTTPublishUInt32(char* topic, uint32_t packet)
{
  int length = 13;
  uint8_t data[length];
  sprintf(data,"%d",packet);

  MQTTJimsPublish(topic, data, length);
}

//----------------------------------------------------------------------------
//  Purpose:
//     Convert the double then publish the message
//
//  Notes:
//      None
//
//----------------------------------------------------------------------------
void MQTTPublishDouble(char* topic, double packet)
{
  int length = 20;
  uint8_t data[length];
  sprintf(data, "%f", packet);

  MQTTJimsPublish(topic, data, length);
}

//----------------------------------------------------------------------------
//  Purpose:
//     Convert the bool then publish the message
//
//  Notes:
//      None
//
//----------------------------------------------------------------------------
void MQTTPublishBool(char* topic, bool packet)
{
  if (true == packet)
  {
    MQTTJimsPublish(topic, (uint8_t*)"true", 4);
  }
  else
  {
    MQTTJimsPublish(topic, (uint8_t*)"false", 5);
  }
}

//----------------------------------------------------------------------------
//  Purpose:
//     Publish the message
//
//  Notes:
//      None
//
//----------------------------------------------------------------------------
void MQTTJimsPublish(char* topic, uint8_t* packet, uint8_t packetLen)
{
  char cPayload[100];
  MQTTMessageParams Msg = MQTTMessageParamsDefault;
  MQTTPublishParams Params = MQTTPublishParamsDefault;

  Msg.qos = QOS_0;
  Msg.pPayload = (void *)cPayload;

  Params.pTopic = topic;

  sprintf(cPayload, "%s", packet);
  Msg.pPayload = (void *)cPayload;

  Msg.PayloadLen = strlen(cPayload) + 1;
  Params.MessageParams = Msg;
  int returnCode = aws_iot_mqtt_publish(&Params);
}

//----------------------------------------------------------------------------
//  Purpose:
//     Idle loops
//
//  Notes:
//      None
//
//----------------------------------------------------------------------------
int main(int argc, char** argv)
{
  IoT_Error_t rc = NONE_ERROR;
  int32_t i = 0;
  bool infinitePublishFlag = true;

  char rootCA[PATH_MAX + 1];
  char clientCRT[PATH_MAX + 1];
  char clientKey[PATH_MAX + 1];
  char CurrentWD[PATH_MAX + 1];
  char cafileName[] = AWS_IOT_ROOT_CA_FILENAME;
  char clientCRTName[] = AWS_IOT_CERTIFICATE_FILENAME;
  char clientKeyName[] = AWS_IOT_PRIVATE_KEY_FILENAME;

  int messageID;
  char mqttID[MAX_ID_LEN];
  int returnCode = 0;
  time_t sendTime;
  
  // wait to make sure mosquitto is up and running
  sleep(30);

  // Connect to the MQTT server
  snprintf(mqttID, 50, "MQTTToAzure");
  gMqttServer = mosquitto_new(mqttID, true, NULL);
  printf("mosq:%d\n", gMqttServer);
  mosquitto_connect_callback_set(gMqttServer,    MQTTConnectCallback);
  mosquitto_disconnect_callback_set(gMqttServer, MQTTDisconnectCallback);
  mosquitto_message_callback_set(gMqttServer,    MQTTMessageCallback);

  // Subscribe to the Fridge messages
  returnCode = mosquitto_connect(gMqttServer, "127.0.0.1", 1883, 600);
  printf("Connect:%d\n", returnCode);
  returnCode = mosquitto_subscribe(gMqttServer, &messageID, MQTT_FREEZER_ALL, 0);
  printf("%s:%d\n", MQTT_FREEZER_ALL, returnCode);
  returnCode = mosquitto_subscribe(gMqttServer, &messageID, MQTT_FRIDGE_ALL, 0);
  printf("%s:%d\n", MQTT_FRIDGE_ALL, returnCode);
  returnCode = mosquitto_subscribe(gMqttServer, &messageID, MQTT_TIME_ALL, 0);
  printf("%s:%d\n", MQTT_TIME_ALL, returnCode);
  returnCode = mosquitto_subscribe(gMqttServer, &messageID, MQTT_TWEET_ALL, 0);
  printf("%s:%d\n", MQTT_TWEET_ALL, returnCode);

  // Set up the control C stuff
  SetupHandlers();

  // Now for AWS
  INFO("\nAWS IoT SDK Version %d.%d.%d-%s\n", VERSION_MAJOR, VERSION_MINOR, VERSION_PATCH, VERSION_TAG);

  // GEt the certificates
  getcwd(CurrentWD, sizeof(CurrentWD));
  sprintf(rootCA, "%s/%s", gCertDirectory, cafileName);
  sprintf(clientCRT, "%s/%s", gCertDirectory, clientCRTName);
  sprintf(clientKey, "%s/%s", gCertDirectory, clientKeyName);

  MQTTConnectParams connectParams = MQTTConnectParamsDefault;

  connectParams.KeepAliveInterval_sec = 10;
  connectParams.isCleansession = true;
  connectParams.MQTTVersion = MQTT_3_1_1;
  connectParams.pClientID = "CSDK-test-device";
  connectParams.pHostURL = gHostAddress;
  connectParams.port = gPort;
  connectParams.isWillMsgPresent = false;
  connectParams.pRootCALocation = rootCA;
  connectParams.pDeviceCertLocation = clientCRT;
  connectParams.pDevicePrivateKeyLocation = clientKey;
  connectParams.mqttCommandTimeout_ms = 2000;
  connectParams.tlsHandshakeTimeout_ms = 5000;
  connectParams.isSSLHostnameVerify = true; // ensure this is set to true for production
  connectParams.disconnectHandler = AWSDisconnectCallbackHandler;

  INFO("Connecting...");
  rc = aws_iot_mqtt_connect(&connectParams);
  if (NONE_ERROR != rc) {
    ERROR("Error(%d) connecting to %s:%d", rc, connectParams.pHostURL, connectParams.port);
  }

  rc = aws_iot_mqtt_autoreconnect_set_status(true);
  if (NONE_ERROR != rc) {
    ERROR("Unable to set Auto Reconnect to true - %d", rc);
    return rc;
  }

  // Subscribe to the web tweet
  MQTTSubscribeParams subParams = MQTTSubscribeParamsDefault;
  subParams.mHandler = AWSCallbackHandler;
  subParams.pTopic = MQTT_WEB_TWEET;
  subParams.qos = QOS_0;

  INFO("Subscribing...");
  rc = aws_iot_mqtt_subscribe(&subParams);
  if (NONE_ERROR != rc) {
    ERROR("Error subscribing");
  }

  sendTime = time(NULL);

  // Start the loops
  while ((1 == gRunning)&&
    (NETWORK_ATTEMPTING_RECONNECT == rc || 
     RECONNECT_SUCCESSFUL         == rc || 
     NONE_ERROR                   == rc)
    && (gPublishCount > 0 || infinitePublishFlag)) 
  {
    returnCode = mosquitto_loop(gMqttServer, 1, 1);

    // Every ten seconds or so send the messages
    if ((time(NULL) - sendTime) > 10)
    {
      printf("Display\n");

      printf("%s:%d\n", MQTT_FREEZER_DOOR, gFreezerDoor);
      printf("%s:%d\n", MQTT_FRIDGE_DOOR, gFridgeDoor);
      printf("%s:%d\n", MQTT_BORED_TIME, gBoredTime);
      printf("%s:%d\n", MQTT_LOCKOUT_TIME, gLockoutTime);
      printf("%s:%d\n", MQTT_DOOR_TIME, gDoorTime);
      printf("%s:%d\n", MQTT_WEB_TWEET, gWebTweet);
      printf("%s:%f\n", MQTT_FREEZER_TEMP, gFreezerTemp);
      printf("%s:%f\n", MQTT_FRIDGE_TEMP, gFridgeTemp);
      printf("%s:%s\n", MQTT_LAST_TWEET, gLastTweet);

      MQTTPublishUInt32(MQTT_FREEZER_DOOR, gFreezerDoor);
      MQTTPublishUInt32(MQTT_FRIDGE_DOOR, gFridgeDoor);
      MQTTPublishDouble(MQTT_FREEZER_TEMP, gFreezerTemp);
      MQTTPublishDouble(MQTT_FRIDGE_TEMP, gFridgeTemp);
      MQTTPublishInt32(MQTT_BORED_TIME, gBoredTime);
      MQTTPublishInt32(MQTT_LOCKOUT_TIME, gLockoutTime);
      MQTTPublishInt32(MQTT_DOOR_TIME, gDoorTime);
      MQTTJimsPublish(MQTT_LAST_TWEET, gLastTweet, strlen(gLastTweet));

      sendTime = time(NULL);
    }

    //Max time the yield function will wait for read messages
    rc = aws_iot_mqtt_yield(100);
    if (NETWORK_ATTEMPTING_RECONNECT == rc) 
    {
      INFO("-->sleep");
      sleep(1);
      // If the client is attempting to reconnect we will skip the rest of the loop.
      continue;
    }
    
    // IF the tweet flag is set send it back to the fridge
    if (gOldWebTweet != gWebTweet)
    {
      if (true == gWebTweet)
      {
        mosquitto_publish(gMqttServer, NULL,
          MQTT_WEB_TWEET, 4, (uint8_t*)"true", 0, 0);
      }
      else
      {
        mosquitto_publish(gMqttServer, NULL,
          MQTT_WEB_TWEET, 5, (uint8_t*)"false", 0, 0);
      }
      gOldWebTweet = gWebTweet;
    }
  }

  // Clean up
  mosquitto_disconnect(gMqttServer);
  mosquitto_destroy(gMqttServer);
  mosquitto_lib_cleanup();

  if (NONE_ERROR != rc) {
    ERROR("An error occurred in the loop.\n");
  }
  else {
    INFO("Publish done\n");
  }

  return rc;
}

//----------------------------------------------------------------------------
//  Purpose:
//     Control C handler
//
//  Notes:
//      None
//
//----------------------------------------------------------------------------
static void CtrlCHandler(int signum)
{
  printf("ctrl c\n");
  (void)(signum);
  gRunning = 0;
}

//----------------------------------------------------------------------------
//  Purpose:
//     Setup the control C handler
//
//  Notes:
//      None
//
//----------------------------------------------------------------------------
static void SetupHandlers(void)
{
  struct sigaction sa;

  sa.sa_handler = CtrlCHandler;

  sigaction(SIGINT, &sa, NULL);
  sigaction(SIGTERM, &sa, NULL);
}

//----------------------------------------------------------------------------
//  Purpose:
//     Convert a text
//
//  Notes:
//      None
//
//----------------------------------------------------------------------------
void MQTTConvertText(uint8_t* packet, uint8_t packetLen, uint8_t* text, uint8_t textLen)
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

//----------------------------------------------------------------------------
//  Purpose:
//     Convert a UInt32
//
//  Notes:
//      None
//
//----------------------------------------------------------------------------
void MQTTConvertUInt32(uint8_t* packet, uint8_t packetLen, uint32_t* number)
{
  uint32_t temp = 0;

  if (packetLen >= sizeof(uint32_t))
  {
    memcpy(&temp, packet, sizeof(uint32_t));
  }
  *number = temp;
}

//----------------------------------------------------------------------------
//  Purpose:
//     Convert a Int32
//
//  Notes:
//      None
//
//----------------------------------------------------------------------------
void MQTTConvertInt32(uint8_t* packet, uint8_t packetLen, int32_t* number)
{
  int32_t temp = 0;

  if (packetLen >= sizeof(int32_t))
  {
    memcpy(&temp, packet, sizeof(int32_t));
  }
  *number = temp;
}

//----------------------------------------------------------------------------
//  Purpose:
//     Convert a double
//
//  Notes:
//      None
//
//----------------------------------------------------------------------------
void MQTTConvertDouble(uint8_t* packet, uint8_t packetLen, double* number)
{
  double temp = 0;

  if (packetLen >= sizeof(double))
  {
    memcpy(&temp, packet, sizeof(double));
  }
  *number = temp;
}

//----------------------------------------------------------------------------
//  Purpose:
//     Convert a bool
//
//  Notes:
//      None
//
//----------------------------------------------------------------------------
void MQTTConvertBool(uint8_t* packet, uint8_t packetLen, uint8_t* boolean)
{
  *boolean = 0;

  if (packetLen > 0)
  {
    if ('t' == packet[0])
    {
      *boolean = 1;
    }
    if ('T' == packet[0])
    {
      *boolean = 1;
    }
  }
}

//----------------------------------------------------------------------------
//  Purpose:
//     Callback Handler
//
//  Notes:
//      None
//
//----------------------------------------------------------------------------
int AWSCallbackHandler(MQTTCallbackParams params)
{
  if (0 == strncmp(MQTT_WEB_TWEET, params.pTopicName,
    strnlen(MQTT_WEB_TWEET,MAX_ID_LEN)-1))
  {
    char* data = (char*)params.MessageParams.pPayload;
    gWebTweet = false;
    if ('1' == data[0])
    {
      gWebTweet = true;
    }
    if ('t' == data[0])
    {
      gWebTweet = true;
    }
    if ('T' == data[0])
    {
      gWebTweet = true;
    }
  }

  return 0;
}

//----------------------------------------------------------------------------
//  Purpose:
//     Callback to disconnect
//
//  Notes:
//      None
//
//----------------------------------------------------------------------------
void AWSDisconnectCallbackHandler(void)
{
  WARN("MQTT Disconnect");
  IoT_Error_t rc = NONE_ERROR;
  if (aws_iot_is_autoreconnect_enabled())
  {
    INFO("Auto Reconnect is enabled, Reconnecting attempt will start now");
  }
  else
  {
    WARN("Auto Reconnect not enabled. Starting manual reconnect...");
    rc = aws_iot_mqtt_attempt_reconnect();
    if (RECONNECT_SUCCESSFUL == rc)
    {
      WARN("Manual Reconnect Successful");
    }
    else
    {
      WARN("Manual Reconnect Failed - %d", rc);
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
void MQTTConnectCallback(struct mosquitto *mosq, void *obj, int rc)
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
void MQTTDisconnectCallback(struct mosquitto *mosq, void *obj, int result)
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
void MQTTMessageCallback(struct mosquitto *mosq, void *obj,
  const struct mosquitto_message *msg)
{
  if (0 == strncmp(msg->topic, MQTT_FREEZER_DOOR,
    strnlen(MQTT_FREEZER_DOOR, MAX_ID_LEN)))
  {
    MQTTConvertUInt32((uint8_t*)msg->payload, msg->payloadlen, &gFreezerDoor);
  }

  if (0 == strncmp(msg->topic, MQTT_FREEZER_TEMP,
    strnlen(MQTT_FREEZER_TEMP, MAX_ID_LEN)))
  {
    MQTTConvertDouble((uint8_t*)msg->payload, msg->payloadlen, &gFreezerTemp);
  }

  if (0 == strncmp(msg->topic, MQTT_FRIDGE_DOOR,
    strnlen(MQTT_FRIDGE_DOOR, MAX_ID_LEN)))
  {
    MQTTConvertUInt32((uint8_t*)msg->payload, msg->payloadlen, &gFridgeDoor);
  }

  if (0 == strncmp(msg->topic, MQTT_FRIDGE_TEMP,
    strnlen(MQTT_FRIDGE_TEMP, MAX_ID_LEN)))
  {
    MQTTConvertDouble((uint8_t*)msg->payload, msg->payloadlen, &gFridgeTemp);
  }

  if (0 == strncmp(msg->topic, MQTT_LOCKOUT_TIME,
    strnlen(MQTT_LOCKOUT_TIME, MAX_ID_LEN)))
  {
    MQTTConvertInt32((uint8_t*)msg->payload, msg->payloadlen, &gLockoutTime);
  }

  if (0 == strncmp(msg->topic, MQTT_BORED_TIME,
    strnlen(MQTT_BORED_TIME, MAX_ID_LEN)))
  {
    MQTTConvertInt32((uint8_t*)msg->payload, msg->payloadlen, &gBoredTime);
  }

  if (0 == strncmp(msg->topic, MQTT_DOOR_TIME,
    strnlen(MQTT_DOOR_TIME, MAX_ID_LEN)))
  {
    MQTTConvertInt32((uint8_t*)msg->payload, msg->payloadlen, &gDoorTime);
  }

  if (0 == strncmp(msg->topic, MQTT_LAST_TWEET,
    strnlen(MQTT_LAST_TWEET, MAX_ID_LEN)))
  {
    MQTTConvertText((uint8_t*)msg->payload, msg->payloadlen,
      (uint8_t*)gLastTweet, MAX_TWEET_LEN);
  }

  if (0 == strncmp(msg->topic, MQTT_WEB_TWEET,
    strnlen(MQTT_WEB_TWEET, MAX_ID_LEN)))
  {
    MQTTConvertBool((uint8_t*)msg->payload, msg->payloadlen, &gWebTweet);
  }
}
