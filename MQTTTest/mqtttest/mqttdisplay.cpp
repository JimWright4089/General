//----------------------------------------------------------------------------
//
//  $Workfile: mqttdisplay.cpp$
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
#include <mosquitto.h>
#include "StopWatch.hpp"

//----------------------------------------------------------------------------
//  Global Variables
//----------------------------------------------------------------------------
bool gRun = true;

//----------------------------------------------------------------------------
//  Local functions
//----------------------------------------------------------------------------
void connectCallback(struct mosquitto *mosq, void *obj, int rc);
void disconnectCallback(struct mosquitto *mosq, void *obj, int result);
void messageCallback(struct mosquitto *mosq, void *obj,
    const struct mosquitto_message *message);

int main(void)
{
    struct mosquitto *mosq;
    int mid = 0;
    int rc;

    //Get the MQTT server running
    mosquitto_lib_init();
    mosq = mosquitto_new("mqttdisplay", true, NULL);
    mosquitto_connect_callback_set(mosq,    connectCallback);
    mosquitto_disconnect_callback_set(mosq, disconnectCallback);
    mosquitto_message_callback_set(mosq,    messageCallback);

    // Connect to the MQTT server
    mosquitto_connect(mosq, "127.0.0.1", 1883, 600);

    // What thing to subscribe to
    mosquitto_subscribe(mosq, &mid, "test/#", 0);
    //mosquitto_subscribe(mosq, &mid, "$SYS/#", 0);
    //mosquitto_subscribe(mosq, &mid, "test/cos", 0);

    // Wait loop
    do {
        rc = mosquitto_loop(mosq, 10000, 10);
    } while (rc == MOSQ_ERR_SUCCESS && gRun);
    printf("the rc: %d\n", rc);

    // close the connection
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
    printf("connect rc: %d\n", rc);
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
    printf("disconnect rc: %d\n", result);
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
}

//----------------------------------------------------------------------------
//  Purpose:
//      Called when a message arrives
//
//  Notes:
//      None
//
//----------------------------------------------------------------------------
void messageCallback(struct mosquitto *mosq, void *obj,
    const struct mosquitto_message *message)
{
    printf("got message '%s' for topic '%s'\n",
        (char*)message->payload, (char*)message->topic);
}

