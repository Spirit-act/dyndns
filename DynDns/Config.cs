using System;
using System.IO;
using System.Xml;
using Sentry;

namespace DynDns
{
    class Config
    {
        public string username;
        public string password;
        public string hostname;
        public string api;

        public Config(string path)
        {
            if (File.Exists(path))
            {
                try
                {
                    XmlDocument settings = new XmlDocument();
                    settings.Load(path);

                    this.username = settings.GetElementsByTagName("username").Item(0).InnerText;
                    this.password = settings.GetElementsByTagName("password").Item(0).InnerText;
                    this.hostname = settings.GetElementsByTagName("hostname").Item(0).InnerText;
                    this.api = settings.GetElementsByTagName("api").Item(0).InnerText;
                }
                catch (Exception e)
                {
                    SentrySdk.CaptureException(e);
                }
            }
            else
            {
                Console.WriteLine("File does not exit");
            }
        }
    }
}
