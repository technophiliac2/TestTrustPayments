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

namespace ConsoleApp2
{
    class Program
    {
        internal class requestWrapper
        {
            public string alias { get; set; }
            public string version { get; set; }
            public request request { get; set; }
        }

        internal class request
        {
            public string currencyiso3a { get; set; }
            public string[] requesttypedescriptions { get; set; }
            public string sitereference { get; set; }
            public string baseamount { get; set; }
            public string orderreference { get; set; }
            public string accounttypedescription { get; set; }
            public string pan { get; set; }
            public string expirydate { get; set; }
            public string securitycode { get; set; }
        }

        //        stconfig = securetrading.Config()
        //stconfig.username = "ws@icmarkets.com"
        //stconfig.password = "J,4b(j!t"
        //st = securetrading.Api(stconfig)

        //request = {
        //            "sitereference": "test_icmarketseultd89904",
        //    "requesttypedescriptions": ["AUTH"],
        //    "accounttypedescription": "ECOM",
        //    "currencyiso3a": "USD",
        //    "baseamount": "2000",
        //    "orderreference": "12345678",
        //    "billingfirstname": "Fname",
        //    "billinglastname": "Lname",
        //    "pan": "4111111111111111",
        //    "expirydate": "12/2022",
        //    "securitycode": "123"
        //}

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
                requesttypedescriptions = new[] { "AUTH" },
                sitereference = "test_icmarketseultd89904",
                baseamount = "1050",
                orderreference = "My_Order_123",
                accounttypedescription = "ECOM",
                pan = "4111111111111111",
                expirydate = "12/2022",
                securitycode = "123"
            };

            var requestWrapper = new requestWrapper
            {
                alias = "ws@icmarkets.com",
                version = "1.00",
                request = request
            };

            SendRequest2(requestWrapper);
        }

        private async static void SendRequest(request request)
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
                client.Proxy = null;
                client.Credentials = new NetworkCredential("ws@icmarkets.com", "J,4b(j!t");

                //{ "alias":"webservices@example.com","version":"1.00","request":[{ "currencyiso3a":"GBP","requesttypedescriptions":["AUTH"],"sitereference":"test_site12345","baseamount":"1050","orderreference":"My_Order_123","accounttypedescription":"ECOM","pan":"4111111111111111","expirydate":"12\/2022","securitycode":"123"}]}

                string result = client.UploadString("https://webservices.securetrading.net/json/", "POST", data);
            }
        }
    }
}
