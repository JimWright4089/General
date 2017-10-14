//----------------------------------------------------------------------------
//
//  $Workfile: Config.cs$
//
//  $Revision: X$
//
//  Project:    Jims Fridge Iot Hub
//
//                            Copyright (c) 2017
//                               James A Wright
//                            All Rights Reserved
//
//  Modification History:
//  $Log:
//  $
//
//----------------------------------------------------------------------------
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Threading;
using System.Windows.Forms;
using Microsoft.ServiceBus.Messaging;
using Microsoft.Azure.Devices;

namespace JimsFridgeIoTHubTest
{
  //----------------------------------------------------------------------------
  //  Class Declarations
  //----------------------------------------------------------------------------
  //
  // Class Name: JimsFridgeIotHubTest
  // 
  // Purpose:
  //      Shows the status and allows people to tweet
  //
  //----------------------------------------------------------------------------
  public partial class JimsFridgeIotHubTest : Form
  {
    //----------------------------------------------------------------------------
    //  Class Attributes 
    //----------------------------------------------------------------------------
    static Config mConfig = new Config();
    static EventHubClient mEventHubClient1;
    static ServiceClient mServiceClient;

    static string mFridgeDoor = "";
    static string mFridgeTemp = "";
    static string mFreezerDoor = "";
    static string mFreezerTemp = "";
    static string mBoredTime = "";
    static string mLockoutTime = "";
    static string mDoorTime = "";
    static string mLastTweet = "";

    static bool mRunThread = true;
    static bool mTweetNow = false;

    //--------------------------------------------------------------------
    // Purpose:
    //     Constructor
    //
    // Notes:
    //     None.
    //--------------------------------------------------------------------
    public JimsFridgeIotHubTest()
    {
      InitializeComponent();

      mServiceClient = ServiceClient.CreateFromConnectionString(
        mConfig.mConnectionString);

      Thread.Sleep(300);

      mEventHubClient1 = EventHubClient.CreateFromConnectionString(
        mConfig.mConnectionString, mConfig.mIOTHubD2CEndpoint1);

      Thread.Sleep(300);

      // Start the receivers up
      var d2cPartitions = mEventHubClient1.GetRuntimeInformation().PartitionIds;
      var tasks = new List<Task>();
      foreach (string partition in d2cPartitions)
      {
        tasks.Add(ReceiveMessagesFromDeviceAsync(partition));
      }
      
      tDisplay.Enabled = true;
    }

    //--------------------------------------------------------------------
    // Purpose:
    //     Receive the Messages
    //
    // Notes:
    //     This is also the task that handles sending the tweet now message
    //--------------------------------------------------------------------
    private static async Task ReceiveMessagesFromDeviceAsync(string partition)
    {
      var eventHubReceiver = mEventHubClient1.GetDefaultConsumerGroup().CreateReceiver(partition, DateTime.UtcNow);
      while (true == mRunThread)
      {
        // If there tweet now flag is set, send the message down to tweet
        if(true == mTweetNow)
        {
          var commandMessage = new Microsoft.Azure.Devices.Message(Encoding.ASCII.GetBytes("TweetStatus"));
          commandMessage.Properties.Add(new KeyValuePair<string, string>("TweetNow", "True"));
          await mServiceClient.SendAsync(mConfig.mDeviceString, commandMessage);
          mTweetNow = false;
        }

        // Get Messages
        Thread.Sleep(10);
        EventData eventData = await eventHubReceiver.ReceiveAsync();
        if (eventData == null) continue;

        string data = Encoding.UTF8.GetString(eventData.GetBytes());
        Console.WriteLine("Message received. Partition: {0} Data: '{1}'", partition, data);

        // Parse all of the properties
        for (int i = 0; i < eventData.Properties.Count; i++)
        {
          KeyValuePair<string, object> theEvent = eventData.Properties.ElementAt(i);

          if ("FreezerDoor" == theEvent.Key)
          {
            mFreezerDoor = (string)theEvent.Value;
          }
          if ("FreezerTemp" == theEvent.Key)
          {
            mFreezerTemp = (string)theEvent.Value;
          }
          if ("FridgeDoor" == theEvent.Key)
          {
            mFridgeDoor = (string)theEvent.Value;
          }
          if ("FridgeTemp" == theEvent.Key)
          {
            mFridgeTemp = (string)theEvent.Value;
          }
          if ("LastTweet" == theEvent.Key)
          {
            mLastTweet = (string)theEvent.Value;
          }
          if ("LastTweet" == theEvent.Key)
          {
            mLastTweet = (string)theEvent.Value;
          }
          if ("DoorTime" == theEvent.Key)
          {
            mDoorTime = (string)theEvent.Value;
          }
          if ("LockoutTime" == theEvent.Key)
          {
            mLockoutTime = (string)theEvent.Value;
          }
          if ("BoredTime" == theEvent.Key)
          {
            mBoredTime = (string)theEvent.Value;
          }
          Console.WriteLine("  Prop {0} {1}", theEvent.Key, (string)theEvent.Value);
        }
      }
    }

    //--------------------------------------------------------------------
    // Purpose:
    //     Display the values to the screen
    //
    // Notes:
    //     none
    //--------------------------------------------------------------------
    private void tDisplay_Tick(object sender, EventArgs e)
    {
      Int32 tempInt;
      double tempDouble;

      if("0" == mFridgeDoor)
      {
        lFridgeDoor.Text = "Closed";
      }
      else
      {
        lFridgeDoor.Text = "Open";
      }

      if ("0" == mFreezerDoor)
      {
        lFreezerDoor.Text = "Closed";
      }
      else
      {
        lFreezerDoor.Text = "Open";
      }

      double.TryParse(mFridgeTemp, out tempDouble);
      lFridgeTemp.Text = GetTemp(tempDouble);
      double.TryParse(mFreezerTemp, out tempDouble);
      lFreezerTemp.Text = GetTemp(tempDouble);

      Int32.TryParse(mDoorTime, out tempInt);
      lDoorTime.Text = GetTime(tempInt);
      Int32.TryParse(mBoredTime, out tempInt);
      lBoredTime.Text = GetTime(tempInt);
      Int32.TryParse(mLockoutTime, out tempInt);
      lLockoutTime.Text = GetTime(tempInt);

      lLastTweet.Text = mLastTweet;

      Int32 lockout;
      Int32.TryParse(mLockoutTime, out lockout);

      if(lockout <= 0)
      {
        bTweet.Enabled = true;
      }
      else
      {
        bTweet.Enabled = false;
      }
    }

    //--------------------------------------------------------------------
    // Purpose:
    //     Close all of the treads on exit
    //
    // Notes:
    //     none
    //--------------------------------------------------------------------
    private void JimsFridgeIotHubTest_FormClosing(object sender, FormClosingEventArgs e)
    {
      mRunThread = false;
    }

    //--------------------------------------------------------------------
    // Purpose:
    //     Tweet Button
    //
    // Notes:
    //     none
    //--------------------------------------------------------------------
    private void bTweet_Click(object sender, EventArgs e)
    {
      mTweetNow = true;
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
    //     Convert temps
    //
    //  Notes:
    //      None
    //
    //----------------------------------------------------------------------------
    string GetTemp(double temp)
    {
      string tempString = CelsiusToFahrenheit(temp).ToString("F2") +
            "F (" + temp.ToString("F2") + "C)";
      return tempString;
    }

    //----------------------------------------------------------------------------
    //  Purpose:
    //     Convert temps
    //
    //  Notes:
    //      None
    //
    //----------------------------------------------------------------------------
    string GetTime(Int32 time)
    {
      TimeSpan theTime = new TimeSpan(0, 0, time);
      string timeString = "";

      int mTotalDays = (int)theTime.TotalDays;
      int mTotalHours = (int)theTime.TotalHours;
      int mTotalMinutes = (int)theTime.TotalMinutes;
      int mTotalSeconds = (int)theTime.TotalSeconds;

      if (mTotalDays > 0)
      {
        timeString = mTotalDays.ToString() + " Days " +
          theTime.Hours.ToString("D2") + ":" +
          theTime.Minutes.ToString("D2") + ":" +
          theTime.Seconds.ToString("D2");
      }
      else
      {
        if(mTotalHours > 0)
        {
          timeString = mTotalHours.ToString("D") + ":" +
            theTime.Minutes.ToString("D2") + ":" +
            theTime.Seconds.ToString("D2");
        }
        else
        {
          if (mTotalMinutes > 0)
          {
            timeString = mTotalMinutes.ToString("D") + ":" +
              theTime.Seconds.ToString("D2");
          }
          else
          {
            timeString = mTotalSeconds.ToString();
          }
        }
      }

      return timeString;
    }
  }
}
