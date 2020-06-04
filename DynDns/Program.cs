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
                        SentrySdk.CaptureException(e);
                        Console.WriteLine(e.Message);
                    }
                }
            }
        }
    }
}

