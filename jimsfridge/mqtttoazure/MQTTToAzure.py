#!/usr/bin/env python

# Copyright (c) Microsoft. All rights reserved.
# Licensed under the MIT license. See LICENSE file in the project root for
# full license information.

import random
import time
import sys
from iothub_client import IoTHubClient, IoTHubClientError, IoTHubTransportProvider, IoTHubClientResult
from iothub_client import IoTHubMessage, IoTHubMessageDispositionResult, IoTHubError, DeviceMethodReturnValue
import re
import paho.mqtt.client as paho
import struct
from StopWatch import StopWatch
import urllib

MQTT_FREEZER_DOOR = "freezer/door"
MQTT_FREEZER_TEMP = "freezer/temp"
MQTT_FRIDGE_DOOR  = "fridge/door"
MQTT_FRIDGE_TEMP  = "fridge/temp"
MQTT_BORED_TIME   = "time/bored"
MQTT_LOCKOUT_TIME = "time/lockout"
MQTT_DOOR_TIME    = "time/door"
MQTT_LAST_TWEET   = "tweet/last"
MQTT_WEB_TWEET    = "tweet/web"
MQTT_FREEZER_ALL  = "freezer/#"
MQTT_FRIDGE_ALL   = "fridge/#"
MQTT_TIME_ALL     = "time/#"
MQTT_TWEET_ALL    = "tweet/#"

TIMEOUT = 241000
MINIMUM_POLLING_TIME = 9
MESSAGE_TIMEOUT = 10000
RECEIVE_CONTEXT = 0
TWIN_CONTEXT = 0
SEND_REPORTED_STATE_CONTEXT = 0
METHOD_CONTEXT = 0
PROTOCOL = IoTHubTransportProvider.MQTT
AZURE_REPORT_TIME = 1500
CONNECTION_STRING = sys.argv[1]

gFreezerDoor = 0;
gFridgeDoor  = 0;
gFreezerTemp = 0.0;
gFridgeTemp  = 0.0;
gBoredTime   = 0;
gDoorTime    = 0;
gLockoutTime = 0;
gWebTweet    = "";
gLastTweet   = "";

gSendMessage = True
gReceiveCallBackCount = 0
gMQTTClient = paho.Client()

if len(sys.argv) < 2:
    print ( "You need to provide the device connection string as command line arguments." )
    sys.exit(0)

def is_correct_connection_string():
    foundHost = re.search("HostName=.*;DeviceId=.*;", CONNECTION_STRING)
    if foundHost:
        return True
    else:
        return False

if not is_correct_connection_string():
    print ( "Device connection string is not correct." )
    sys.exit(0)

def receive_message_callback(message, counter):
    global gReceiveCallBackCount
    message_buffer = message.get_bytearray()
    size = len(message_buffer)
    print ( "Received Message [%d]:" % counter )
    print ( "    Data: <<<%s>>> & Size=%d" % (message_buffer[:size].decode("utf-8"), size) )
    map_properties = message.properties()
    key_value_pair = map_properties.get_internals()
    print ( "    Properties: %s" % key_value_pair )
    counter += 1
    gReceiveCallBackCount += 1
    print ( "    Total calls received: %d" % gReceiveCallBackCount )
    return IoTHubMessageDispositionResult.ACCEPTED


def send_confirmation_callback(message, result, user_context):
    print ( "Confirmation[%d] received for message with result = %s" % (user_context, result) )
    map_properties = message.properties()
    print ( "    message_id: %s" % message.message_id )
    print ( "    correlation_id: %s" % message.correlation_id )
    key_value_pair = map_properties.get_internals()
    print ( "    Properties: %s" % key_value_pair )

def device_twin_callback(update_state, payload, user_context):
    pass

def send_reported_state_callback(status_code, user_context):
    pass

def device_method_callback(method_name, payload, user_context):
    global gSendMessage
    print ( "\nMethod callback called with:\nmethodName = %s\npayload = %s\ncontext = %s" % (method_name, payload, user_context) )
    device_method_return_value = DeviceMethodReturnValue()
    device_method_return_value.response = "{ \"Response\": \"This is the response from the device\" }"
    device_method_return_value.status = 200
    if method_name == "start":
        gSendMessage = True
        print ( "Start sending message\n" )
        device_method_return_value.response = "{ \"Response\": \"Successfully started\" }"
        return device_method_return_value
    if method_name == "stop":
        gSendMessage = False
        print ( "Stop sending message\n" )
        device_method_return_value.response = "{ \"Response\": \"Successfully stopped\" }"
        return device_method_return_value
    return device_method_return_value

def iothub_client_init():
    # prepare iothub client
    client = IoTHubClient(CONNECTION_STRING, PROTOCOL)
    if client.protocol == IoTHubTransportProvider.HTTP:
        client.set_option("timeout", TIMEOUT)
        client.set_option("MinimumPollingTime", MINIMUM_POLLING_TIME)
    # set the time until a message times out
    client.set_option("messageTimeout", MESSAGE_TIMEOUT)
    # to enable MQTT logging set to 1
    if client.protocol == IoTHubTransportProvider.MQTT:
        client.set_option("logtrace", 0)
    client.set_message_callback(
        receive_message_callback, RECEIVE_CONTEXT)
    if client.protocol == IoTHubTransportProvider.MQTT or client.protocol == IoTHubTransportProvider.MQTT_WS:
        client.set_device_twin_callback(
            device_twin_callback, TWIN_CONTEXT)
        client.set_device_method_callback(
            device_method_callback, METHOD_CONTEXT)
    return client

def convertDouble( stringArray ):
   if(8 == len(stringArray)):
     byteArray = bytearray()
     byteArray.extend(stringArray)
     return struct.unpack('d', byteArray)[0]
   return 0.0

def convertInt32( stringArray ):
   if(4 == len(stringArray)):
     byteArray = bytearray()
     byteArray.extend(stringArray)
     return struct.unpack('l', byteArray)[0]
   return 0

def on_message(mosq, obj, msg):
    global gFreezerDoor
    global gFridgeDoor
    global gFreezerTemp
    global gFridgeTemp
    global gBoredTime
    global gDoorTime
    global gLockoutTime
    global gWebTweet
    global gLastTweet

    if MQTT_FREEZER_TEMP == msg.topic:
      gFreezerTemp = convertDouble(msg.payload)
    else:
      if MQTT_FRIDGE_TEMP == msg.topic:
        gFridgeTemp = convertDouble(msg.payload)
      else:
        if MQTT_FREEZER_DOOR == msg.topic:
          gFreezerDoor = convertInt32(msg.payload)
        else:
          if MQTT_FRIDGE_DOOR == msg.topic:
            gFridgeDoor = convertInt32(msg.payload)
          else:
            if MQTT_BORED_TIME == msg.topic:
              gBoredTime = convertInt32(msg.payload)
            else:
              if MQTT_LOCKOUT_TIME == msg.topic:
                gLockoutTime = convertInt32(msg.payload)
              else:
                if MQTT_DOOR_TIME == msg.topic:
                  gDoorTime = convertInt32(msg.payload)
                else:
                  if MQTT_LAST_TWEET == msg.topic:
                    gLastTweet = msg.payload
                  else:
                    if MQTT_WEB_TWEET == msg.topic:
                      gWebTweet = msg.payload
                    else:
                      print "We did not expect this%-20s %d" % (msg.topic, len(msg.payload))

def on_publish(mosq, obj, mid):
    pass

def iothub_client_sample_run():
    global gMQTTClient
    try:
        iotHubStopWatch = StopWatch(AZURE_REPORT_TIME)
        iotHubStopWatch.reset()
        client = iothub_client_init()

        if client.protocol == IoTHubTransportProvider.MQTT:
            print ( "IoTHubClient is reporting state" )
            reported_state = "{\"newState\":\"standBy\"}"
            client.send_reported_state(reported_state, len(reported_state), send_reported_state_callback, SEND_REPORTED_STATE_CONTEXT)

        count = 1
        while True:
            if (True == iotHubStopWatch.isExpired()):
              global gSendMessage
              if gSendMessage:
                message = IoTHubMessage("JimsFridgeStatus")
              
                message.message_id = "message_%d" % count
                message.correlation_id = "correlation_%d" % count
              
                prop_map = message.properties()
                prop_map.add("FreezerDoor", str(gFreezerDoor))
                prop_map.add("FreezerTemp", str(gFreezerTemp))
                prop_map.add("FridgeDoor",  str(gFridgeDoor))
                prop_map.add("FridgeTemp",  str(gFridgeTemp))
                prop_map.add("BoredTime",   str(gBoredTime))
                prop_map.add("LockoutTime", str(gLockoutTime))
                prop_map.add("DoorTime",    str(gDoorTime))
                prop_map.add("LastTweet",   urllib.quote(gLastTweet))
                prop_map.add("WebTweet",    gWebTweet)

                client.send_event_async(message, send_confirmation_callback, count)
                count = count + 1

                status = client.get_send_status()
                iotHubStopWatch.reset()
            gMQTTClient.loop()
            time.sleep(0.01)
            pass

    except IoTHubError as iothub_error:
        print ( "Unexpected error %s from IoTHub" % iothub_error )
        return
    except KeyboardInterrupt:
        print ( "IoTHubClient sample stopped" )

    print_last_message_time(client)

def parse_iot_hub_name():
    m = re.search("HostName=(.*?)\.", CONNECTION_STRING)
    return m.group(1)

if __name__ == "__main__":
    time.sleep(30)
    gMQTTClient.on_message = on_message
    gMQTTClient.on_publish = on_publish

    #client.tls_set('root.ca', certfile='c1.crt', keyfile='c1.key')
    gMQTTClient.connect("127.0.0.1", 1883, 60)

    gMQTTClient.subscribe(MQTT_FREEZER_ALL, 0)
    gMQTTClient.subscribe(MQTT_FRIDGE_ALL, 0)
    gMQTTClient.subscribe(MQTT_TIME_ALL, 0)
    gMQTTClient.subscribe(MQTT_TWEET_ALL, 0)

    iothub_client_sample_run()
