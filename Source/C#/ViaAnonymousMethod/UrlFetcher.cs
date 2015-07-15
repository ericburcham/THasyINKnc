using System;
using System.IO;
using System.Net;

namespace ViaAnonymousMethod
{
    public class UrlFetcher
    {
        public void FetchUrl(string url, int count)
        {
            for (var i = 0; i < count; i++)
            {
                var request = WebRequest.Create(url);
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

                Console.WriteLine("Retrieved: {0}", url);
            }
        }
    }
}
