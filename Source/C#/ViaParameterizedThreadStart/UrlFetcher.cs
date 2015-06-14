using System;
using System.IO;
using System.Net;

namespace ViaParameterizedThreadStart
{
    public class UrlFetcher
    {
        public void FetchUrl(object o)
        {
            var url = (string)o;
            var request = WebRequest.Create(url);
            request.Credentials = CredentialCache.DefaultCredentials;

            using (var response = (HttpWebResponse)request.GetResponse())
            {
                using (var dataStream = response.GetResponseStream())
                {
                    using (var reader = new StreamReader(dataStream))
                    {
                        reader.ReadToEnd();
                    }
                }
            }

            Console.WriteLine("Retrieved: {0}", url);
        }
    }
}