using Sentry;
using System;
using System.IO;
using System.Net;

namespace DynDns
{
    class WebHelper
    {
        private string url;
        private string api;

        public WebHelper(string url)
        {
            this.url = url;
        }

        public WebHelper(string url, string api)
        {
            this.url = url;
            this.api = api;
        }
        public string getIp()
        {
            HttpWebRequest req = (HttpWebRequest)WebRequest.Create(url);
            req.AutomaticDecompression = DecompressionMethods.GZip | DecompressionMethods.Deflate;

            try
            {

                using (HttpWebResponse response = (HttpWebResponse)req.GetResponse())
                using (Stream stream = response.GetResponseStream())
                using (StreamReader reader = new StreamReader(stream))
                {
                    return reader.ReadToEnd().Trim();
                }
            } catch (Exception e)
            {
                SentrySdk.CaptureException(e);
                return "127.0.0.1";
            }
        }

        public bool updateIP(String username, String password, String hostname, String ip)
        {
            if (api == null)
            {
                return false;
            }
            String encodedCredentials = System.Convert.ToBase64String(System.Text.Encoding.GetEncoding("ISO-8859-1").GetBytes(username + ":" + password));
            try
            {
                WebClient wc = new WebClient();
                wc.QueryString.Add("system", "dyndns");
                wc.QueryString.Add("hostname", hostname);
                wc.QueryString.Add("myip", ip);
                wc.Headers[HttpRequestHeader.Authorization] = string.Format("Basic {0}", encodedCredentials);
                String result = wc.DownloadString(api);
                if (result.StartsWith("nochg") || result.StartsWith("good"))
                {
                    return true;
                } else
                {
                    return false;
                }
            } catch (Exception e)
            {
                SentrySdk.CaptureException(e);
                return false;
            }
        }
    }
}
