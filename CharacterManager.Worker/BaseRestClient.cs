using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace CharacterManager.Worker
{
    public class BaseRestClient
    {
        private readonly HttpClient _http;

        public BaseRestClient(HttpClient http)
        {
            _http = http;
        }

        public async Task<HttpResponseMessage> PostContent(object content, string baseRoute, string controller, string endpoint)
        {
            try
            {
                string json = JsonConvert.SerializeObject(content,
                        new JsonSerializerSettings()
                        {
                            ReferenceLoopHandling = ReferenceLoopHandling.Ignore
                        });

                StringContent payload = new StringContent(json, Encoding.UTF8, "application/json");
                payload.Headers.ContentType.CharSet = string.Empty;

                return await _http.PostAsync($"{baseRoute}{controller}/{endpoint}", payload);
            }
            catch (Exception ex)
            {
                return new HttpResponseMessage { StatusCode = System.Net.HttpStatusCode.BadRequest };
            }
        }

        public async Task<HttpResponseMessage> GetContent(string baseRoute, string controller, string endpoint)
        {
            try
            {
                return await _http.GetAsync($"{baseRoute}{controller}/{endpoint}");
            }
            catch (Exception ex)
            {
                return new HttpResponseMessage { StatusCode = System.Net.HttpStatusCode.BadRequest };
            }
        }

        public async Task<List<T>> GetRequestForListAsync<T>(string baseRoute, string controller, string endpoint)
        {
            try
            {
                HttpResponseMessage response = await _http.GetAsync($"{baseRoute}{controller}/{endpoint}");

                string responseString = await response.Content.ReadAsStringAsync();
                JArray responseJson = JArray.Parse(responseString);
                List<T> contentList = responseJson.ToObject<List<T>>();

                return contentList;
            }
            catch (Exception ex)
            {
                return null;
            }
        }
    }
}
