using Newtonsoft.Json;
using System.IO;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Essentials;

namespace DreamProperties.Common.Network
{
    public interface INetworkService
    {
        Task<TResult> GetAsync<TResult>(string url);
        Task<TResult> PostAsync<TResult>(string url, string jsonData);
        Task<bool> PostAsync(string url, FileResult file);
    }

    public class NetworkService : INetworkService
    {
        private HttpClient _httpClient;

        public NetworkService()
        {
            _httpClient = new HttpClient();
        }

        public async Task<TResult> GetAsync<TResult>(string url)
        {
            //var token = await SecureStorage.GetAsync("token");

            //_httpClient.DefaultRequestHeaders.Authorization =
            //new AuthenticationHeaderValue("Bearer", token);

            HttpResponseMessage response = await _httpClient.GetAsync(url);

            string serialized = await response.Content.ReadAsStringAsync();
            TResult result = JsonConvert.DeserializeObject<TResult>(serialized);

            return result;
        }

        public async Task<TResult> PostAsync<TResult>(string url, string jsonData)
        {
            var content = new StringContent(jsonData, Encoding.UTF8, "application/json");
            HttpResponseMessage response = await _httpClient.PostAsync(url, content);

            string serialized = await response.Content.ReadAsStringAsync();
            TResult result = JsonConvert.DeserializeObject<TResult>(serialized);

            return result;
        }

        public async Task<bool> PostAsync(string url, FileResult file)
        {

            var content = new MultipartFormDataContent();
            content.Add(new StreamContent(await file.OpenReadAsync()), "file", file.FileName);

            HttpResponseMessage response = await _httpClient.PostAsync(url, content);
            return response.IsSuccessStatusCode;
        }
    }
}
