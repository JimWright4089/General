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
using System.Xml;

namespace JimsFridgeIoTHubTest
{
  //----------------------------------------------------------------------------
  //  Class Declarations
  //----------------------------------------------------------------------------
  //
  // Class Name: Config
  // 
  // Purpose:
  //      This stores and retrives configuration data from the xml file
  //
  //----------------------------------------------------------------------------
  public class Config
  {
    //----------------------------------------------------------------------------
    //  Class Consts
    //----------------------------------------------------------------------------
    const string CONFIG_FILE_NAME = "C:/keys/Azure.xml"; // this file must be in the same directory as the .dll

    const string CONNECTION_STRING_TAG = "ConnectionString";  // The XML file tags
    const string END_POINT_TAG = "EndPoint";
    const string IOT_HUB_URL_TAG = "IOTHubURL";
    const string DEVICE_KEY_TAG = "DeviceKey";
    const string DEVICE_STRING_TAG = "DeviceString";
    const string CONFIG_TAG = "Config";

    //----------------------------------------------------------------------------
    //  Class Attributes 
    //----------------------------------------------------------------------------
    public string mConnectionString = "XX";
    public string mIOTHubD2CEndpoint1 = "XX";
    public string mIOTHubURL = "XX";
    public string mDeviceKey = "XX";
    public string mDeviceString = "XX";

    //--------------------------------------------------------------------
    // Purpose:
    //     Constructor, load in the file.  If the file is not found the 
    //     defaults from above are used.
    //
    // Notes:
    //     None.
    //--------------------------------------------------------------------
    public Config()
    {
      XmlDocument doc = new XmlDocument();
      doc.Load(CONFIG_FILE_NAME);
      XmlNode node = doc.FirstChild;
      XmlNode childNode = node.FirstChild;
      while (null != childNode)
      {
        if (CONNECTION_STRING_TAG == childNode.Name)
        {
          mConnectionString = childNode.InnerText;
        }

        if (END_POINT_TAG == childNode.Name)
        {
          mIOTHubD2CEndpoint1 = childNode.InnerText;
        }

        if (IOT_HUB_URL_TAG == childNode.Name)
        {
          mIOTHubURL = childNode.InnerText;
        }

        if (DEVICE_KEY_TAG == childNode.Name)
        {
          mDeviceKey = childNode.InnerText;
        }

        if (DEVICE_STRING_TAG == childNode.Name)
        {
          mDeviceString = childNode.InnerText;
        }
        childNode = childNode.NextSibling;
      }
    }

    //--------------------------------------------------------------------
    // Purpose:
    //     Saves the file to the new config file.
    //
    // Notes:
    //     This must be called from the thing that is using the config file.
    //--------------------------------------------------------------------
    public void save()
    {
      XmlDocument doc = new XmlDocument();

      XmlNode nodeCONFIG_TAG = doc.CreateNode("element", CONFIG_TAG, "");
      doc.AppendChild(nodeCONFIG_TAG);

      XmlNode nodeCONNECTION_STRING_TAG = doc.CreateNode("element", CONNECTION_STRING_TAG, "");
      nodeCONNECTION_STRING_TAG.InnerText = mConnectionString;
      nodeCONFIG_TAG.AppendChild(nodeCONNECTION_STRING_TAG);

      XmlNode nodeEND_POINT_TAG = doc.CreateNode("element", END_POINT_TAG, "");
      nodeEND_POINT_TAG.InnerText = mIOTHubD2CEndpoint1;
      nodeCONFIG_TAG.AppendChild(nodeEND_POINT_TAG);

      XmlNode nodeIOT_HUB_URL_TAG = doc.CreateNode("element", IOT_HUB_URL_TAG, "");
      nodeIOT_HUB_URL_TAG.InnerText = mIOTHubURL;
      nodeCONFIG_TAG.AppendChild(nodeCONNECTION_STRING_TAG);

      XmlNode nodeDEVICE_KEY_TAG = doc.CreateNode("element", DEVICE_KEY_TAG, "");
      nodeDEVICE_KEY_TAG.InnerText = mDeviceKey;
      nodeCONFIG_TAG.AppendChild(nodeDEVICE_KEY_TAG);

      XmlNode nodeDEVICE_STRING_TAG = doc.CreateNode("element", DEVICE_STRING_TAG, "");
      nodeDEVICE_STRING_TAG.InnerText = mDeviceString;
      nodeCONFIG_TAG.AppendChild(nodeCONNECTION_STRING_TAG);

      doc.Save(CONFIG_FILE_NAME);
    }
  }
}
