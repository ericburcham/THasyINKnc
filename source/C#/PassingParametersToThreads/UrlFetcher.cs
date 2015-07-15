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
                using (var responseStream = response.GetResponseStream())
                {
                    if (responseStream != null)
                    {
                        using (var reader = new StreamReader(responseStream))
                        {
                            reader.ReadToEnd();
                        }
                    }
                }
            }

            Console.WriteLine("Retrieved: {0}", _url);
        }

        private readonly string _url;
    }
}