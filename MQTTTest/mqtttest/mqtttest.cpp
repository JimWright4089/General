//----------------------------------------------------------------------------
//
//  $Workfile: mqtttest.cpp$
//
//  $Revision: X$
//
//  Project:    MQTT Test
//
//                            Copyright (c) 2017
//                                Jim Wright
//                            All Rights Reserved
//
//  Modification History:
//  $Log:
//  $
//
//  Note:
//      This needs to have the mosquito example zip in the directory next to
//      the one that has this code in it.
//
//----------------------------------------------------------------------------
#include <stdio.h>
#include <math.h>
#include <mosquitto.h>
#include <memory.h>
#include "StopWatch.hpp"

//----------------------------------------------------------------------------
//  Local defines
//----------------------------------------------------------------------------
#define MESSAGE_SIZE 100

//----------------------------------------------------------------------------
//  Global Variables
//----------------------------------------------------------------------------
bool gRun = true;

//----------------------------------------------------------------------------
//  Local functions
//----------------------------------------------------------------------------
void connectCallback(struct mosquitto *mosq, void *obj, int rc);
void disconnectCallback(struct mosquitto *mosq, void *obj, int result);
void publishCallback(struct mosquitto *mosq, void *obj, int mid);

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
    double theCount = 0.0;
    struct mosquitto *mosq;
    char buffer[MESSAGE_SIZE];

    // Setup two stopwatches, one to count, the other to send sin/cos to mqtt server
    StopWatch* displayStopWatch = new StopWatch(500);
    StopWatch* countStopWatch = new StopWatch(20);
    displayStopWatch->Reset();
    countStopWatch->Reset();

    //Get the MQTT server running
    mosquitto_lib_init();
    mosq = mosquitto_new("mqtttest", true, NULL);
    mosquitto_connect_callback_set(mosq,    connectCallback);
    mosquitto_disconnect_callback_set(mosq, disconnectCallback);
    mosquitto_publish_callback_set(mosq,    publishCallback);

    // Connect to local
    mosquitto_connect(mosq, "127.0.0.1", 1883, 600);

    while (true == gRun)
    {
        // Find the sin/cos
        double theSin = sin(theCount);
        double theCos = cos(theCount);

        // Count
        if (true == countStopWatch->IsExpired())
        {
            theCount += 0.01;
            countStopWatch->Reset();
        }

        // Send the sin/cos to mqtt
        if (true == displayStopWatch->IsExpired())
        {
            sprintf(buffer, "%f", theSin);
            int rcsin = mosquitto_publish(mosq, NULL, "test/sinString", MESSAGE_SIZE, buffer, 0, false);
            sprintf(buffer, "%f", theCos);
            int rccos = mosquitto_publish(mosq, NULL, "test/cosString", MESSAGE_SIZE, buffer, 0, false);
            
            memcpy(buffer, &theSin, sizeof(double));
            rcsin = mosquitto_publish(mosq, NULL, "test/sin", sizeof(double), buffer, 0, false);
            memcpy(buffer, &theCos, sizeof(double));
            rccos = mosquitto_publish(mosq, NULL, "test/cos", sizeof(double), buffer, 0, false);
            printf("%d %d Count:%9.3f Sin:%9.3f  Cos:%9.3f\n",
                rcsin,rccos,
                theCount, theSin, theCos);
            displayStopWatch->Reset();
        }
    }

    // close the mqtt
    mosquitto_destroy(mosq);
    mosquitto_lib_cleanup();
    return 0;
}

//----------------------------------------------------------------------------
//  Purpose:
//      Connect to the server call back
//
//  Notes:
//      None
//
//----------------------------------------------------------------------------
void connectCallback(struct mosquitto *mosq, void *obj, int rc)
{
    printf("rc: %d\n", rc);
}

//----------------------------------------------------------------------------
//  Purpose:
//      Something caused a dissconnect
//
//  Notes:
//      None
//
//----------------------------------------------------------------------------
void disconnectCallback(struct mosquitto *mosq, void *obj, int result)
{
    gRun = false;
}

//----------------------------------------------------------------------------
//  Purpose:
//      Published has finished
//
//  Notes:
//      None
//
//----------------------------------------------------------------------------
void publishCallback(struct mosquitto *mosq, void *obj, int mid)
{
    // do nothing
}

