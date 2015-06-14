using System;
using System.IO;
using System.Net;

namespace ViaAnInstance
{
    public class UrlFetcher
    {
        public UrlFetcher(string url)
        {
            _url = url;
        }

        public void Fetch()
        {
            var request = WebRequest.Create(_url);
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

        private readonly string _url;
    }
}