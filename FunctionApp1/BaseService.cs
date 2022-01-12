using System;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using Newtonsoft.Json;

namespace FunctionApp1
{
    public class BaseService
    {
        protected internal virtual HttpClient CreateHttpClient(string baseAddressUrl, HttpClientHandler clientHandler)
        {
            HttpClient client = null;

            if (clientHandler != null)
                client = new HttpClient(clientHandler);
            else
                client = new HttpClient();

            if (!string.IsNullOrWhiteSpace(baseAddressUrl))
                client.BaseAddress = new Uri(baseAddressUrl);

            return client;
        }

        public static string GetResponseText(string address)
        {
            var content = string.Empty;
            var request = (HttpWebRequest)WebRequest.Create(address);
            try
            {
                var response = (HttpWebResponse) request.GetResponse();

                //using (var response = (HttpWebResponse)request.GetResponse())
                //{
                if (response != null &&
                    IsRestServiceCallSuccessful(response))
                {
                    var encoding = System.Text.Encoding.GetEncoding(response.CharacterSet);

                    using (var responseStream = response.GetResponseStream())
                    using (var reader = new StreamReader(responseStream, encoding))
                        content = reader.ReadToEnd();
                }

                //}
            }
            catch (Exception ex)
            {
                content = ex.Message;
            }


            return content;
        }

        public string GetStringContent(HttpResponseMessage response)
        {
            var content = string.Empty;

            if (response != null &&
                response.Content != null)
            {
                var taskResponseContent = response.Content.ReadAsStringAsync();

                // Get the result from the read.
                if (taskResponseContent.Result != null)
                    content = taskResponseContent.Result;
            }

            return content;
        }

        public string GetResponseContent(HttpResponseMessage response)
        {
            var responseContent = GetStringContent(response);
            return responseContent;
        }
        public static bool IsRestServiceCallSuccessful(HttpWebResponse response)
        {
            var isSuccessful = response != null &&
                               response.StatusCode == HttpStatusCode.OK;

            return isSuccessful;
        }

        protected string SerializeObject(object data)
        {
            var serializedData = string.Empty;

            // Check to see whether to serialize or not.  Need to check because if the data object
            // is null, Newtonsoft will return a serialized string literal value of "null".  This does not
            // mean it is a null string object.  It means a string object with a literal value of "null".
            if (data != null)
                serializedData = JsonConvert.SerializeObject(data);

            return serializedData;
        }


        protected T DeserializeObject<T>(string data) where T : class
        {
            // Create default object of type T.
            var deserializedObj = default(T);

            // Check to see whether to deserialize or not.
            if (!string.IsNullOrWhiteSpace(data))
                deserializedObj = JsonConvert.DeserializeObject<T>(data);

            return deserializedObj;
        }
    }
}
