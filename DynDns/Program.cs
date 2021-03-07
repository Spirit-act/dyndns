using System;
using System.IO;
using System.Net;

namespace DynDns
{
    class Program
    {
        static void Main(string[] args)
        {
            Config config = new Config(Directory.GetCurrentDirectory() + @"\settings.xml");
            WebHelper wh = new WebHelper("https://checkip.amazonaws.com/", config.api);
            string newip = wh.getIp();
            string currentip = Dns.GetHostAddresses(config.hostname).ToString();
            if (currentip != newip && newip != "127.0.0.1")
            {
                try
                {
                    if (wh.updateIP(config.username, config.password, config.hostname, newip))
                    {
                        Console.WriteLine("Update Successful");
                    }
                    else
                    {
                        Console.WriteLine("Update Failed");
                    }
                }
                catch (Exception e)
                {
                    Console.WriteLine(e.Message);
                }
            }
        }
    }
}

