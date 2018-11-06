using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;

namespace Onion.API.Client
{
    public partial class ApiClient
    {
        private readonly HttpClient _httpClient;
        public Uri BaseEndpoint { get; private set; }

        /// <summary>
        /// Property for dates proper formatting.
        /// </summary>
        private static JsonSerializerSettings MicrosoftDateFormatSettings
        {
            get { return new JsonSerializerSettings { DateFormatHandling = DateFormatHandling.MicrosoftDateFormat }; }
        }

        public ApiClient(Uri baseEndpoint)
        {
            BaseEndpoint = baseEndpoint ?? throw new ArgumentNullException("baseEndpoint");
            _httpClient = new HttpClient();
        }

        /// <summary>
        /// Take a string url and query string and return a Uri type which will be passed in get and post common methods.
        /// </summary>
        /// <param name="relativePath"></param>
        /// <param name="queryString"></param>
        /// <returns></returns>
        public Uri CreateRequestUri(string relativePath, string queryString = "")
        {
            var endpoint = new Uri(BaseEndpoint, relativePath);
            var uriBuilder = new UriBuilder(endpoint);
            uriBuilder.Query = queryString;
            return uriBuilder.Uri;
        }

        /// <summary>
        /// Common method to make GET calls.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="requestUrl"></param>
        /// <returns></returns>
        public async Task<T> GetAsync<T>(Uri requestUrl)
        {
            var response = await _httpClient.GetAsync(requestUrl, HttpCompletionOption.ResponseHeadersRead);
            response.EnsureSuccessStatusCode();
            var data = await response.Content.ReadAsStringAsync();
            return JsonConvert.DeserializeObject<T>(data);
        }

        public async Task<IEnumerable<T>> GetAllAsync<T>(Uri requestUrl)
        {
            var response = await _httpClient.GetAsync(requestUrl, HttpCompletionOption.ResponseHeadersRead);
            response.EnsureSuccessStatusCode();
            var data = await response.Content.ReadAsStringAsync();
            return JsonConvert.DeserializeObject<IEnumerable<T>>(data);
        }

        /// <summary>
        /// Common method to make POST calls. Takes input model and returns the same type. 
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="requestUrl"></param>
        /// <param name="content"></param>
        /// <returns></returns>
        public async Task<T> PostAsync<T>(Uri requestUrl, T content)
        {
            var response = await _httpClient.PostAsync(requestUrl.ToString(), CreateHttpContent<T>(content));
            response.EnsureSuccessStatusCode();
            var data = await response.Content.ReadAsStringAsync();
            return JsonConvert.DeserializeObject<T>(data);
        }

        /// <summary>
        /// Common method to make POST calls. Takes different input and returns different Output model. 
        /// </summary>
        /// <typeparam name="T1"></typeparam>
        /// <typeparam name="T2"></typeparam>
        /// <param name="requestUrl"></param>
        /// <param name="content"></param>
        /// <returns></returns>
        public async Task<T1> PostAsync<T1, T2>(Uri requestUrl, T2 content)
        {
            var response = await _httpClient.PostAsync(requestUrl.ToString(), CreateHttpContent<T2>(content));
            response.EnsureSuccessStatusCode();
            var data = await response.Content.ReadAsStringAsync();
            return JsonConvert.DeserializeObject<T1>(data);
        }

        /// <summary>
        /// Create content for post request from the models.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="content"></param>
        /// <returns></returns>
        private HttpContent CreateHttpContent<T>(T content)
        {
            var json = JsonConvert.SerializeObject(content, MicrosoftDateFormatSettings);

            var buffer = System.Text.Encoding.UTF8.GetBytes(json);
            var byteContent = new ByteArrayContent(buffer);
            byteContent.Headers.ContentType = new MediaTypeHeaderValue("application/json");

            return byteContent;
        }
    }
}
