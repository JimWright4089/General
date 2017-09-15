#include <stdio.h>
#include <math.h>
#include <stdbool.h>
#include <stdint.h>
#include <stdlib.h>
#include <sys/time.h>
#include <mosquitto.h>

#include <msgsps_common.h>
#include "StopWatch.hpp"

bool gRun = true;

void my_connect_callback(struct mosquitto *mosq, void *obj, int rc)
{
    printf("rc: %d\n", rc);
}

void my_disconnect_callback(struct mosquitto *mosq, void *obj, int result)
{
    gRun = false;
}

void my_publish_callback(struct mosquitto *mosq, void *obj, int mid)
{
}

void my_message_callback(struct mosquitto *mosq, void *obj, const struct mosquitto_message *msg)
{
    printf("got message '%.*s' for topic '%s'\n", 
        (char*)message->payload, message->topic);
}


int main(void)
{
    struct mosquitto *mosq;
    char buf[MESSAGE_SIZE];
    int mid = 0;

    mosquitto_lib_init();

    mosq = mosquitto_new("perftest", true, NULL);
    mosquitto_connect_callback_set(mosq, my_connect_callback);
    mosquitto_disconnect_callback_set(mosq, my_disconnect_callback);
    mosquitto_publish_callback_set(mosq, my_publish_callback);
    mosquitto_message_callback_set(mosq, my_message_callback);

    mosquitto_connect(mosq, "127.0.0.1", 1884, 600);

    mosquitto_subscribe(mosq, &mid, "test/sin", 0);
    mosquitto_subscribe(mosq, &mid, "test/cos", 0);

    do {
        rc = mosquitto_loop(mosq, 1, 10);
    } while (rc == MOSQ_ERR_SUCCESS && run);
    printf("rc: %d\n", rc);


    while (true == gRun)
    {
        if (true == displayStopWatch->IsExpired())
        {
            sprintf(buf, "%f", theSin);
            mosquitto_publish(mosq, NULL, "test/sin", MESSAGE_SIZE, &buf[MESSAGE_SIZE], 0, false);
            sprintf(buf, "%f", theCos);
            mosquitto_publish(mosq, NULL, "test/cos", MESSAGE_SIZE, &buf[MESSAGE_SIZE], 0, false);
            printf("Count:%9.3f Sin:%9.3f  Cos:%9.3f\n", theCount, theSin, theCos);
            displayStopWatch->Reset();
        }
    }
    mosquitto_destroy(mosq);
    mosquitto_lib_cleanup();
    return 0;
}

#ifdef extra
struct mosquitto *mosq;
int i;
double dstart, dstop, diff;
FILE *fptr;
uint8_t *buf;

buf = malloc(MESSAGE_SIZE*MESSAGE_COUNT);
if (!buf) {
    printf("Error: Out of memory.\n");
    return 1;
}

start.tv_sec = 0;
start.tv_usec = 0;
stop.tv_sec = 0;
stop.tv_usec = 0;

if (create_data()) {
    printf("Error: Unable to create random input data.\n");
    return 1;
}
fptr = fopen("msgsps_pub.dat", "rb");
if (!fptr) {
    printf("Error: Unable to open random input data.\n");
    return 1;
}
fread(buf, sizeof(uint8_t), MESSAGE_SIZE*MESSAGE_COUNT, fptr);
fclose(fptr);

mosquitto_lib_init();

mosq = mosquitto_new("perftest", true, NULL);
mosquitto_connect_callback_set(mosq, my_connect_callback);
mosquitto_disconnect_callback_set(mosq, my_disconnect_callback);
mosquitto_publish_callback_set(mosq, my_publish_callback);

mosquitto_connect(mosq, "127.0.0.1", 1884, 600);

i = 0;
while (!mosquitto_loop(mosq, 1, 10) && run) {
    if (i<MESSAGE_COUNT) {
        mosquitto_publish(mosq, NULL, "perf/test", MESSAGE_SIZE, &buf[i*MESSAGE_SIZE], 0, false);
        i++;
    }
}
dstart = (double)start.tv_sec*1.0e6 + (double)start.tv_usec;
dstop = (double)stop.tv_sec*1.0e6 + (double)stop.tv_usec;
diff = (dstop - dstart) / 1.0e6;

printf("Start: %g\nStop: %g\nDiff: %g\nMessages/s: %g\n", dstart, dstop, diff, (double)MESSAGE_COUNT / diff);

mosquitto_destroy(mosq);
mosquitto_lib_cleanup();


#endif
