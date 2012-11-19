using System;
using System.IO;
using System.Xml;
using System.Xml.Serialization;

namespace ServerScan
{
    [Serializable]
    public class Config
    {
        public String           ScannerID           = "";
        public String           ScannerName         = "";
        public Int32            ButtonVID           = 0;
        public Int32            ButtonPID           = 0;
        public Int32            ButtonReadSize      = 0;
        public Boolean          AutoConnect         = false;
        public String           SavePath            = "";
        public int              ScanDpi             = 100;
        public int              ScanColor           = 1;
        public bool             ScanADF             = false;
        public bool             ScanTryFlatbed      = false;
        public bool             StartMinimized      = false;
        public bool             ShowErrors          = false;

        const String file = "config.xml";

        public void Serialize()
        {
            System.Xml.Serialization.XmlSerializer xs
               = new System.Xml.Serialization.XmlSerializer(this.GetType());
            StreamWriter writer = File.CreateText(file);
            xs.Serialize(writer, this);
            writer.Flush();
            writer.Close();

            Logger.Log("Configuration saved");
        }

        public static Config Deserialize()
        {
            if (!File.Exists(file))
                return new Config();
            
            System.Xml.Serialization.XmlSerializer xs
               = new System.Xml.Serialization.XmlSerializer(
                  typeof(Config));
            StreamReader reader = File.OpenText(file);
            Config c = (Config)xs.Deserialize(reader);
            reader.Close();

            Logger.Log("Configuration loaded");
            return c;
        }
    }
}
