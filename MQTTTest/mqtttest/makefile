
CC=g++
CFLAGS=-I../mosquitto/src -I../mosquitto/lib -I. -I.. -Wall -ggdb -DDEBUG -DWITH_CLIENT
RM=/bin/rm -f

#for Windows, use RM=del
#calling 'make all' will ensure that every feature of the program is compiled
all: mqtttest mqttdisplay

#create the object file for frog.cc
StopWatch.o: StopWatch.hpp StopWatch.cpp
	${CC} ${CFLAGS} -c StopWatch.cpp

#create the object file for the main file
mqtttest.o: StopWatch.hpp mqtttest.cpp
	${CC} ${CFLAGS} -c mqtttest.cpp

#create the object file for the main file
mqttdisplay.o: mqttdisplay.cpp
	${CC} ${CFLAGS} -c mqttdisplay.cpp

#create the executable
mqtttest: mqtttest.o StopWatch.o
	${CC} ${CFLAGS} -o mqtttest mqtttest.o StopWatch.o ../mosquitto/lib/libmosquitto.so.1

#create the executable
mqttdisplay: mqttdisplay.o
	${CC} ${CFLAGS} -o mqttdisplay mqttdisplay.o ../mosquitto/lib/libmosquitto.so.1

# rule for cleaning files generated during compilations. Call 'make clean' to
#use it
clean:
	${RM} *.o mqtttest mqttdisplay
