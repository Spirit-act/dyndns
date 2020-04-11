using System;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Threading.Tasks;

namespace DynDns
{
    class Program
    {
        static void Main(string[] args)
        {
            string password = "!*2$Y@v7Y6E%gd6@%uKFn@w2y";
            string username = "deltaone-community.de-dyndns";
            string hostname = "dyndns.deltaone-community.de";

            WebHelper wh = new WebHelper("https://checkip.amazonaws.com/", "https://www.ovh.com/nic/update");
            string newip = wh.getIp();
            string currentip = Dns.GetHostAddresses(hostname).ToString();
            if (currentip != newip) {
                try
                {
                    if (wh.updateIP(username, password, hostname, newip))
                    {
                        Console.WriteLine("Update Successful");
                    } else
                    {
                        Console.WriteLine("Update Failed");
                    }
                } catch (Exception e)
                {
                    Console.WriteLine(e.Message);
                }
            }
        }
    }
}

