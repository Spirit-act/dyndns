using Ovh.Api;
using System;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;

namespace DynDns
{
    class Program
    {
        static async Task Main(string[] args)
        {
            string ap = "BUI0VWT7UWp2mQ0g";
            string secret = "KSXHZ4vG4eJckcXyk1eE262IundHNpDG";
            string ck = "prJhH0hKFL31Zit5ZuDx6b7KBu2yPWqp";
            string zone = "deltaone-community.de";
            string id = "5062989470";
            string subdomain = "srv";

            WebHelper wh = new WebHelper("https://checkip.amazonaws.com/");
            Client ovhclient = new Client("ovh-eu", ap, secret, ck);
            string newip = wh.getIp();
            string currentip = getSavedIp();
            if (currentip == null || currentip != newip) {
                Dictionary<string, object> payload = new Dictionary<string, object>();
                payload.Add("subDomain", subdomain);
                payload.Add("target", newip);
                try
                {
                    await ovhclient.PutAsync($"/domain/zone/{zone}/record/{id}", payload);
                } catch (Exception e)
                {
                    Console.WriteLine(e.Message);
                }
                writeFile(newip);
            }
        }

        static void writeFile(string content)
        {
            try
            {
                string dir = Directory.GetCurrentDirectory();
                string file = $"{dir}/dyndns.doc";
                File.WriteAllText(file, content);
            } catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }
        }

        static string getSavedIp()
        {
            string dir = Directory.GetCurrentDirectory();
            string file = $"{dir}/dyndns.doc";
            if (File.Exists(file))
            {
                return File.ReadAllText(file);
            } else
            {
                return null;
            }
        }
    }
}

