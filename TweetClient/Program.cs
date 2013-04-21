using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using TweetClient.ServiceReference1;
using System.Net;
using System.IO;
using System.Xml.Serialization;

namespace TweetClient
{
   
    class Program
    {
        static string SendMessage = @"<s:Envelope xmlns:s=""http://schemas.xmlsoap.org/soap/envelope/"">
          <s:Body>
            <SendTweet xmlns=""http://tempuri.org/"">
              <tweet xmlns:a=""http://schemas.datacontract.org/2004/07/ConsoleApplication1.Contracts"" xmlns:i=""http://www.w3.org/2001/XMLSchema-instance"">
                <a:Created>2013-04-21T06:46:43.7989777+09:30</a:Created>
                <a:Id>1</a:Id>
                <a:Text>First Tweet</a:Text>
                <a:User>digory</a:User>
              </tweet>
            </SendTweet>
          </s:Body>
        </s:Envelope>";

        static string DeleteMessage = @"<s:Envelope xmlns:s=""http://schemas.xmlsoap.org/soap/envelope/"">
          <s:Body>
            <DeleteTweet xmlns=""http://tempuri.org/"">
              <tweet xmlns:a=""http://schemas.datacontract.org/2004/07/ConsoleApplication1.Contracts"" xmlns:i=""http://www.w3.org/2001/XMLSchema-instance"">
                <a:Created>2013-04-21T06:46:44.2470033+09:30</a:Created>
                <a:Id>1</a:Id>
                <a:Text>First Tweet</a:Text>
                <a:User>digory</a:User>
              </tweet>
            </DeleteTweet>
          </s:Body>
        </s:Envelope>";
        
        
        static void Main(string[] args)
        {
            RunWSClient();
            RunHttpClient();
        }

        private static void RunHttpClient()
        {
            var response = MakeRequest<TweetResponse>(SendMessage, "http://home-pc:8001/TweetService/a");
            response = MakeRequest<TweetResponse>(DeleteMessage, "http://home-pc:8001/TweetService/b");

            response = MakeRequest<TweetResponse>(SendMessage, "http://home-pc:8002/TweetService/a1");
            response = MakeRequest<TweetResponse>(DeleteMessage, "http://home-pc:8002/TweetService/b1");
        }

        private static void RunWSClient()
        {
            var clientA = new TweetServiceAClient("EndpointA");
            Console.WriteLine("Press any key to start.  Press Escape to exit.");

            var response = clientA.SendTweet(
                new SendTweetRequest
                {
                    Id = "1",
                    Text = "First Tweet",
                    User = "digory",
                    Created = DateTime.Now
                });

            Console.WriteLine(response.Message);

            var clientB = new TweetServiceBClient("EndpointB");
            response = clientB.DeleteTweet(
            new DeleteTweetRequest
            {
                Id = "1",
                Text = "First Tweet",
                User = "digory",
                Created = DateTime.Now
            });

            Console.WriteLine(response.Message);
        }


        static string MakeRequest<T>(string requestBody, string requestUrl)
        {
            var req = (HttpWebRequest)HttpWebRequest.Create(requestUrl);
            var bytes = System.Text.Encoding.ASCII.GetBytes(requestBody);
            req.Method = "POST";
            req.ContentLength = bytes.Length;
            req.ContentType = "text/xml; encoding='utf-8'";

            var stream = req.GetRequestStream();
            stream.Write(bytes, 0, bytes.Length);
            stream.Close();

            var resp = (HttpWebResponse)req.GetResponse();
            var strmReader = new StreamReader(resp.GetResponseStream());
            var responseData = strmReader.ReadToEnd().Trim();

            // TODO: Insert Deser code

            return responseData;
        }

       
    }
}
