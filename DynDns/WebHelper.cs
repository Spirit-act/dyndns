using System;
using System.Collections.Generic;
using System.Text;
using System.IO;
using System.Net;

namespace DynDns
{
    class WebHelper
    {
        private string url;

        public WebHelper(string url)
        {
            this.url = url;
        }
        public string getIp()
        {
            HttpWebRequest req = (HttpWebRequest)WebRequest.Create(url);
            req.AutomaticDecompression = DecompressionMethods.GZip | DecompressionMethods.Deflate;

            using (HttpWebResponse response = (HttpWebResponse)req.GetResponse())
            using (Stream stream = response.GetResponseStream())
            using (StreamReader reader = new StreamReader(stream))
            {
                return reader.ReadToEnd().Trim();
            }
        }
    }
}
