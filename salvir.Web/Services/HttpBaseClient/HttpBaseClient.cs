using System.Net;
using System.Net.Http;
using Newtonsoft.Json;
using System.Text;
using System;
using System.IO;
using System.Threading.Tasks;
using deprosa.Web.Model;

namespace deprosa.WebsiteService
{
    public class HttpBaseClient
    {
        private readonly string _baseuri;
        public HttpBaseClient(string baseuri)
        {
            _baseuri = baseuri;
        }
        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="T">T is sending content</typeparam>
        /// <typeparam name="R">R is receiving content</typeparam>
        /// <param name="uri"></param>
        /// <param name="method"></param>
        /// <param name="content"></param>
        /// <returns></returns>
        public async Task<R> GetResponseObject<S,R>(string uri, eHttpMethodType method, S content)
        {
            R responseObject = default(R);

            var request = CreateWebRequest(string.Format("{0}{1}",_baseuri,uri), method, content);
            await HandleResponse(request, responseAsString =>
             {
                 responseObject = JsonConvert.DeserializeObject<R>(responseAsString);
             });

            return responseObject;
        }
        private HttpResponseMessage CreateWebRequest<T>(string uri, eHttpMethodType method, T content)
        {
            HttpResponseMessage response = null;
            using (var client = new HttpClient())
            {
                string jsonstring = JsonConvert.SerializeObject(content);
                var request = new HttpRequestMessage()
                {
                    RequestUri = new Uri(uri),
                    Content = new StringContent(jsonstring, Encoding.UTF8, "application/json"),
                };
                request.Headers.Add("Authorization", "Basic " + Convert.ToBase64String(Encoding.ASCII.GetBytes($"{CurrentUser.ID}:{CurrentUser.Email}")));

                switch (method)
                {
                    case eHttpMethodType.GET:
                        request.Method = HttpMethod.Get;
                        request.Content = null;
                        response = client.SendAsync(request).Result;
                        break;
                    case eHttpMethodType.POST:
                        if (jsonstring != null)
                        {
                            request.Method = HttpMethod.Post;
                            response = client.SendAsync(request).Result;
                            //response = client.PostAsync(uri, new StringContent(jsonstring, Encoding.UTF8, "application/json")).Result;
                        }
                        break;
                    case eHttpMethodType.PUT:
                        if (jsonstring != null)
                        {
                            request.Method = HttpMethod.Put;
                            response = client.SendAsync(request).Result;
                            //response = client.PutAsync(uri, new StringContent(jsonstring, Encoding.UTF8, "application/json")).Result;
                        }
                        break;
                    case eHttpMethodType.DELETE:
                        if (jsonstring != null)
                        {
                            request.Method = HttpMethod.Delete;
                            response = client.SendAsync(request).Result;
                            //response = client.DeleteAsync(uri).Result;
                        }
                        break;
                }

                return response;
            }

          }

        async Task HandleResponse(HttpResponseMessage request,  Action<string> handleResponseAsString)
        {
            try
            {
                if (request.IsSuccessStatusCode)
                {
                    using (var response = await request.Content.ReadAsStreamAsync())
                    {
                        if (response != null && request.IsSuccessStatusCode)
                        {
                            using (var reader = new StreamReader(response, Encoding.UTF8))
                            {
                                string responseFromServer = reader.ReadToEnd();
                                handleResponseAsString(responseFromServer);
                            }

                        }
                    }
                }
            }

            catch (WebException exception)
            {
                // todo: Log exceptions!
            }
        
        }
    }
}
