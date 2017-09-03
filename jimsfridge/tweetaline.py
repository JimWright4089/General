#!/usr/bin/python

import sys
from twython import Twython

execfile("/home/pi/keys.py")

if 2 == len(sys.argv):
	myTweet = Twython(C_key,C_secret,A_token,A_secret)
	myTweet.update_status(status=sys.argv[1])
else:
	print "Need a string to tweet"


