using RestSharp;
using System;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Text;

namespace WebMotorsProject.App.Util
{
    public static class UtilRest
    {
        public class JsonRequest
        {
            private readonly RestClient _restClient;

            public JsonRequest(string url, IEnumerable<KeyValuePair<string, string>> defaultHeaders = null)
            {
                if (url == null || url.Trim() == "") return;

                _restClient = new RestClient(url);

                if (defaultHeaders != null)
                {
                    foreach (KeyValuePair<string, string> header in defaultHeaders)
                    {
                        _restClient.AddDefaultHeader(header.Key, header.Value);
                    }
                }

                _restClient.AddDefaultHeader("Accept", "application/json");
            }

            public T GET<T>(string resource, object body, IEnumerable<KeyValuePair<string, string>> specificHeaders = null) where T : new()
            {
                var request = new RestRequest(resource, Method.GET)
                {
                    RequestFormat = DataFormat.Json
                };

                request.AddBody(body);

                if (specificHeaders != null)
                    foreach (var header in specificHeaders)
                    {
                        request.AddHeader(header.Key, header.Value);
                    }

                var response = _restClient.Execute<T>(request);

                if (response.ErrorException != null)
                {
                    throw new ApplicationException("Error retrieving response. Check inner details for more info.", response.ErrorException);
                }

                return (new RestSharp.Deserializers.JsonDeserializer()).Deserialize<T>(response);
                //JsonConvert.DeserializeObject<T>(response.Content);
            }

            public List<T> PostList<T>(string resource, object body, IEnumerable<KeyValuePair<string, string>> specificHeaders = null) where T : new()
            {
                var request = new RestRequest(resource, Method.POST)
                {
                    RequestFormat = DataFormat.Json
                };

                request.AddBody(body);

                if (specificHeaders != null)
                    foreach (var header in specificHeaders)
                    {
                        request.AddHeader(header.Key, header.Value);
                    }

                var response = _restClient.Execute<List<T>>(request);

                if (response.ErrorException != null)
                {
                    throw new ApplicationException("Error retrieving response. Check inner details for more info.", response.ErrorException);
                }

                return (new RestSharp.Deserializers.JsonDeserializer()).Deserialize<List<T>>(response);
                //return JsonConvert.DeserializeObject<List<T>>(response.Content);
            }

            public string ObterToken(string userName, string password, string domumToken)
            {
                var webRequest = WebRequest.Create(domumToken);

                webRequest.ContentType = "application/x-www-form-urlencoded";
                webRequest.Method = "POST";

                var requestDetails = String.Format("username={0}&password={1}&grant_type=password", userName, password);
                var bytes = Encoding.ASCII.GetBytes(requestDetails);

                webRequest.ContentLength = bytes.Length;

                try
                {
                    using (var outputStream = webRequest.GetRequestStream())
                    {
                        outputStream.Write(bytes, 0, bytes.Length);
                    }

                    using (var webResponse = webRequest.GetResponse())
                    {
                        using (var reader = new StreamReader(webResponse.GetResponseStream()))
                        {
                            var content = reader.ReadToEnd();

                            if (!string.IsNullOrWhiteSpace(content))
                            {
                                var token = content.Substring(content.IndexOf(":", StringComparison.Ordinal) + 2, content.Length - content.IndexOf(":", StringComparison.Ordinal) - 2);
                                token = token.Substring(0, token.IndexOf(",", StringComparison.Ordinal) - 1);
                                return token;
                            }
                        }
                    }
                }
                catch (Exception ex)
                {
                    var m = ex.Message;
                    var n = ex.InnerException.InnerException;
                    return "";
                }

                return "";
            }
        }
    }
}