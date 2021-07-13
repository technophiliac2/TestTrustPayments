using System;
using System.Collections.Generic;
using System.Text;
using System.IO;

namespace ConsoleApp2
{
    class TrustPaymentsSample
    {

        private string _username;

        private string _password;

        private Uri _postUrl = new Uri("https://webservices.securetrading.net:443/xml/");

        public Stream sendAndReceiveData(byte[] xmlRequest)
        {
            try
            {

                /* Use this block to bypass SSL certificate validation.  Do not use this on a production environment.
                System.Net.ServicePointManager.ServerCertificateValidationCallback =
                    ((sender, certificate, chain, sslPolicyErrors) => true);
                */

                var req = System.Net.HttpWebRequest.CreateHttp(_postUrl.ToString());

                req.Method = "POST";
                req.ContentType = "text/xml;charset=utf-8";
                req.ContentLength = xmlRequest.Length;

                string str2 = Convert.ToBase64String( // The value of the authentication header must be base-64 encoded.
                    Encoding.Convert( // SecureTrading expects the value of the authentication header to be UTF-8 encoded.
                        UnicodeEncoding.Unicode,
                        UTF8Encoding.UTF8,
                        UnicodeEncoding.Unicode.GetBytes(_username + ":" + _password)
                    )
                );

                req.Headers.Set("Authorization", "Basic " + str2);

                System.IO.Stream os = req.GetRequestStream();
                os.Write(xmlRequest, 0, xmlRequest.Length);
                os.Close();

                System.Net.WebResponse resp = req.GetResponse();

                if (resp == null)
                {
                    //throw new STPPException("The response was null.");
                    throw new Exception("The response was null.");
                }
                return resp.GetResponseStream();
            }
            catch (Exception e)
            {
                // Handle any exceptions here.
                return null;
            }
        }
    }
}


