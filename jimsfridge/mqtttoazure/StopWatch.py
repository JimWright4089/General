import time

class StopWatch:
  def __init__(self, waitTime=1000, isSeconds=False):
    self.mLastTime = 0
    self.mWaitTime = waitTime
    self.mIsSeconds = isSeconds

  def setTime(self, waitTime):
    self.mWaitTime = waitTime

  @staticmethod
  def now():
    return int(round(time.time() * 1000))

  @staticmethod
  def nowInSeconds():
    return int(round(time.time()))

  def isExpired(self):
    if (False == self.mIsSeconds):
      if ((StopWatch.now() - self.mLastTime) > self.mWaitTime):
        return True
    else:
      if ((StopWatch.nowInSeconds() - self.mLastTime) > self.mWaitTime):
        return True
    return False

  def reset(self):
    if (False == self.mIsSeconds):
      self.mLastTime = StopWatch.now()
    else:
      self.mLastTime = StopWatch.nowInSeconds()

  def getTimeLeft(self):
    if (False == self.mIsSeconds):
      return self.mWaitTime - (StopWatch.now() - self.mLastTime)
    else:
      return self.mWaitTime - (StopWatch.nowInSeconds() - self.mLastTime)
