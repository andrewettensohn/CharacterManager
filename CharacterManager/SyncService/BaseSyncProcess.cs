using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace CharacterManager.SyncService
{
    public class BaseSyncProcess
    {
        private readonly HttpClient _http;
        public BaseSyncProcess(HttpClient http)
        {
            _http = http;
        }

        protected async Task<List<T>> GetRequestForListAsync<T>(string baseRoute, string controller, string endpoint)
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
                Console.WriteLine(ex);
                return null;
            }
        }

        protected async Task<T> GetRequestForItemAsync<T>(string baseRoute, string controller, string endpoint)
        {
            try
            {
                HttpResponseMessage response = await _http.GetAsync($"{baseRoute}{controller}/{endpoint}");

                string responseString = await response.Content.ReadAsStringAsync();
                JObject responseJson = JObject.Parse(responseString);
                T content = responseJson.ToObject<T>();

                return content;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
                return default;
            }
        }

        public async Task<HttpResponseMessage> PostRequestForResponse(object content, string baseRoute, string controller, string endpoint)
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
                Console.WriteLine(ex);
                return new HttpResponseMessage { StatusCode = System.Net.HttpStatusCode.BadRequest };
            }
        }
    }
}
