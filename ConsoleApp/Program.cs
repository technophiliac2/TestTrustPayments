using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net.Http;
using System.IO;
using System.Threading;
using System.Threading.Tasks;
using System.Net;

using Core;

namespace ConsoleApp1
{
    class Program
    {
        internal class requestWrapper
        {
            internal string alias { get; set; }
            internal string version { get; set; }
            internal request request { get; set; }
        }

        internal class request
        {
            internal string currencyiso3a { get; set; }
            internal string requesttypedescriptions { get; set; }
            internal string sitereference { get; set; }
            internal string baseamount { get; set; }
            internal string orderreference { get; set; }
            internal string accounttypedescription { get; set; }
            internal string pan { get; set; }
            internal string expirydate { get; set; }
            internal string securitycode { get; set; }
        }

        static void Main(string[] args)
        {
            MakeRequest();
            Console.ReadKey();
        }

        private static void MakeRequest()
        {
            var request = new request
            {
                currencyiso3a = "GBP",
                requesttypedescriptions = "[\"AUTH\"]",
                sitereference = "test_site12345",
                baseamount = "1050",
                orderreference = "My_Order_123",
                accounttypedescription = "ECOM",
                pan = "4111111111111111",
                expirydate = "12/2022",
                securitycode = "123"
            };

            var requestWrapper = new requestWrapper
            {
                alias = "webservices@example.com",
                version = "1.00",
                request = request
            };

            SendRequest2(requestWrapper);
        }

        private async static void SendRequest(requestWrapper requestWrapper)
        {
            var client = new HttpClient();

            // Create the HttpContent for the form to be posted.
            var requestContent = new FormUrlEncodedContent(new[] {
                new KeyValuePair<string, string>("text", "This is a block of text"),
            });

            // Get the response.
            HttpResponseMessage response = await client.PostAsync(
                "http://api.repustate.com/v2/demokey/score.json",
                requestContent);

            // Get the response content.
            HttpContent responseContent = response.Content;

            // Get the stream of the content.
            using (var reader = new StreamReader(await responseContent.ReadAsStreamAsync()))
            {
                // Write the output.
                Console.WriteLine(await reader.ReadToEndAsync());
            }
        }

        private static void SendRequest2(requestWrapper requestWrapper)
        {
            var data = Core.JsonSerializer.SerializeToJson(requestWrapper);

            using (var client = new WebClient())
            {
                client.Headers[HttpRequestHeader.ContentType] = "application/json";
                client.Encoding = Encoding.UTF8;

                string result = client.UploadString("https://payments.securetrading.net/json/", "POST", data);
            }
        }
    }
}
