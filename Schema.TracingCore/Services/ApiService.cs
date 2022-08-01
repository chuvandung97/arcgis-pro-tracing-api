using Microsoft.Extensions.Caching.Memory;
using Newtonsoft.Json.Linq;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;

namespace Schema.TracingCore.Services
{
    public class ApiService
    {
        /*        private const string GenerateTokenUrl = "https://inovaantagedev.com/portal/sharing/rest/generateToken";
                private const string BaseUrl = "https://inovaantagedev.com/server/rest/services/GEMS/GEMSDB_Network";
        */
        private const string GenerateTokenUrl = "https://sampleserver7.arcgisonline.com/portal/sharing/rest/generateToken";
        private const string BaseUrl = "https://sampleserver7.arcgisonline.com/server/rest/services/UtilityNetwork/NapervilleElectricV5";
        private readonly HttpClient _client;
        private string _accessToken;
        //private IMemoryCache _cache;

        public ApiService()
        {
            _client = new HttpClient();
            //_accessToken = GetToken();
            /*if (_cache.TryGetValue("access-token", out Task<string> AccessToken))
            {
                _accessToken = AccessToken;
            }
            else
            {
                _accessToken = GetToken();
            }*/
            //ConfigureHttpClientCore(_client);
        }

        public async Task<string> GetToken()
        {
            var dict = new Dictionary<string, string>();
            dict.Add("username", "viewer01");
            dict.Add("password", "I68VGU^nMurF");
            dict.Add("client", "referer");
            dict.Add("f", "json");
            dict.Add("referer", BaseUrl);
            dict.Add("expiration", Convert.ToString(60 * 24 * 7));
            HttpResponseMessage httpResponse = await _client.PostAsync(GenerateTokenUrl, new FormUrlEncodedContent(dict));
            if (!httpResponse.IsSuccessStatusCode)
            {
                throw new Exception("Can not get access token");
            }
            var content = JsonConvert.DeserializeObject<JObject>(await httpResponse.Content.ReadAsStringAsync());
            var token = (string)content["token"];
            //_cache.Set("access-token", token, new MemoryCacheEntryOptions().SetAbsoluteExpiration(TimeSpan.FromDays(6)));
            return token;
        }

        public async Task<JObject> QueryFeatureService(string layerDefs)
        {
            _accessToken = await GetToken();
            ConfigureHttpClientCore(_client);
            HttpResponseMessage httpResponse = await _client.GetAsync($"{BaseUrl}/FeatureServer/query?f=json&layerDefs={layerDefs}");
            if (!httpResponse.IsSuccessStatusCode)
            {
                throw new Exception("Can not query feature service");
            }
            return JsonConvert.DeserializeObject<JObject>(await httpResponse.Content.ReadAsStringAsync());
        }

        public async Task<JObject> Trace(Dictionary<string, string> dict)
        {
            _accessToken = await GetToken();
            ConfigureHttpClientCore(_client);
            HttpResponseMessage httpResponse = await _client.PostAsync($"{BaseUrl}/UtilityNetworkServer/trace", new FormUrlEncodedContent(dict));
            if (!httpResponse.IsSuccessStatusCode)
            {
                throw new Exception("Can not trace");
            }
            return JsonConvert.DeserializeObject<JObject>(await httpResponse.Content.ReadAsStringAsync());
        }

        private void ConfigureHttpClientCore(HttpClient client)
        {
            client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", _accessToken);
        }
    }
}
