#----------------------------------------------------------------------------
#
#  $Workfile: StopWatch.py$
#
#  $Revision: 126$
#
#  Project:    JimsFridge
#
#                            Copyright (c) 2017
#                               Jim Wright
#                            All Rights Reserved
#
#  Modification History:
#  $Log:
#  $
#
#----------------------------------------------------------------------------

#----------------------------------------------------------------------------
#  Imports
#----------------------------------------------------------------------------
import time

#----------------------------------------------------------------------------
#  Class Declarations
#----------------------------------------------------------------------------
#
# Class Name: IconBuilder
# 
# Purpose:
#      Handles the main form
#
#----------------------------------------------------------------------------
class StopWatch:

  #--------------------------------------------------------------------
  # Purpose:
  #     Constructor
  #
  # Notes:
  #     None.
  #--------------------------------------------------------------------
  def __init__(self, waitTime=1000, isSeconds=False):
    self.mLastTime = 0
    self.mWaitTime = waitTime
    self.mIsSeconds = isSeconds

  #--------------------------------------------------------------------
  # Purpose:
  #     Change the wait time
  #
  # Notes:
  #     None.
  #--------------------------------------------------------------------
  def setTime(self, waitTime):
    self.mWaitTime = waitTime

  #--------------------------------------------------------------------
  # Purpose:
  #     Return time in ms
  #
  # Notes:
  #     None.
  #--------------------------------------------------------------------
  @staticmethod
  def now():
    return int(round(time.time() * 1000))

  #--------------------------------------------------------------------
  # Purpose:
  #     Return time in seconds
  #
  # Notes:
  #     None.
  #--------------------------------------------------------------------
  @staticmethod
  def nowInSeconds():
    return int(round(time.time()))

  #--------------------------------------------------------------------
  # Purpose:
  #     Has the stopwatch expired
  #
  # Notes:
  #     None.
  #--------------------------------------------------------------------
  def isExpired(self):
    if (False == self.mIsSeconds):
      if ((StopWatch.now() - self.mLastTime) > self.mWaitTime):
        return True
    else:
      if ((StopWatch.nowInSeconds() - self.mLastTime) > self.mWaitTime):
        return True
    return False

  #--------------------------------------------------------------------
  # Purpose:
  #     Reset stopwatch
  #
  # Notes:
  #     None.
  #--------------------------------------------------------------------
  def reset(self):
    if (False == self.mIsSeconds):
      self.mLastTime = StopWatch.now()
    else:
      self.mLastTime = StopWatch.nowInSeconds()

  #--------------------------------------------------------------------
  # Purpose:
  #     Return time left
  #
  # Notes:
  #     None.
  #--------------------------------------------------------------------
  def getTimeLeft(self):
    if (False == self.mIsSeconds):
      return self.mWaitTime - (StopWatch.now() - self.mLastTime)
    else:
      return self.mWaitTime - (StopWatch.nowInSeconds() - self.mLastTime)
