using System;
using System.IO;
using System.Net;

namespace PassingParametersToThreads
{
    public class UrlFetcher
    {
        public UrlFetcher(string url)
        {
            _url = url;
        }

        public void Fetch()
        {
            FetchUrl(_url);
        }

        public void FetchUrl(string url)
        {
            var request = WebRequest.Create(url);
            request.Credentials = CredentialCache.DefaultCredentials;

            using (var response = (HttpWebResponse)request.GetResponse())
            {
                using (var dataStream = response.GetResponseStream())
                {
                    using (var reader = new StreamReader(dataStream))
                    {
                        var responseFromServer = reader.ReadToEnd();
                        Console.WriteLine(responseFromServer);
                    } 
                }
            }
        }


        public void FetchUrl(object url)
        {
            FetchUrl((string)url);
        }

        private readonly string _url;
    }
}