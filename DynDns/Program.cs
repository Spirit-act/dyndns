using Sentry;
using System;
using System.IO;
using System.Net;

namespace DynDns
{
    class Program
    {
        static void Main(string[] args)
        {
            SentryOptions options = new SentryOptions();
            options.ShutdownTimeout = TimeSpan.FromSeconds(60);
            options.Dsn = new Dsn("https://24114b8159194718bd00e8a88b1fcfdd@sentry.spirit-sys.net/3");
            using (SentrySdk.Init(options))
            {
                //string password = "!*2$Y@v7Y6E%gd6@%uKFn@w2y";
                //string username = "deltaone-community.de-dyndns";
                //string hostname = "dyndns.deltaone-community.de";

                Config config = new Config(Directory.GetCurrentDirectory() + @"\settings.xml");
                //Config config = new Config(@"D:\dev_root\c#\DynDns\DynDns\DynDns\settings.xml");

                //WebHelper wh = new WebHelper("https://checkip.amazonaws.com/", "https://www.ovh.com/nic/update");
                //WebHelper wh = new WebHelper("https://checkip.amazonaws.com/", config.api);
                //string newip = wh.getIp();
                //string currentip = Dns.GetHostAddresses(config.hostname).ToString();
                //if (currentip != newip)
                //{
                //    try
                //    {
                //        if (wh.updateIP(config.username, config.password, config.hostname, newip))
                //        {
                //            Console.WriteLine("Update Successful");
                //        }
                //        else
                //        {
                //            Console.WriteLine("Update Failed");
                //        }
                //    }
                //    catch (Exception e)
                //    {
                //        Console.WriteLine(e.Message);
                //    }
                //}
            }
        }
    }
}

