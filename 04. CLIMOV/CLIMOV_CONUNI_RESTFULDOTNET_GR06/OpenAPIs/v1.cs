using System;
using System.Net.Http;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace CLIMOV_CONUNI_RESTFULDOTNET_GR06
{
    public partial class ConversionClient
    {
        private string _baseUrl = "http://localhost:8082";
        private readonly HttpClient _httpClient;

        public ConversionClient(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public string BaseUrl
        {
            get => _baseUrl;
            set => _baseUrl = value;
        }

        public virtual Task<object> PostAsync(ConversionRequest request)
        {
            return PostAsync(request, CancellationToken.None);
        }

        public virtual async Task<object> PostAsync(ConversionRequest request, CancellationToken cancellationToken)
        {
            if (request == null)
                throw new ArgumentNullException(nameof(request));

            var url = $"{BaseUrl?.TrimEnd('/')}/api/conversion";
            var json = JsonConvert.SerializeObject(request);

            using var content = new StringContent(json, Encoding.UTF8, "application/json");
            using var response = await _httpClient.PostAsync(url, content, cancellationToken).ConfigureAwait(false);
            var payload = await response.Content.ReadAsStringAsync().ConfigureAwait(false);

            if (!response.IsSuccessStatusCode)
                throw new ApiException($"The HTTP status code of the response was not expected ({(int)response.StatusCode}).", (int)response.StatusCode, payload);

            return JsonConvert.DeserializeObject<object>(payload) ?? new object();
        }
    }

    public class ConversionRequest
    {
        [JsonProperty("Categoria")]
        public string? Categoria { get; set; }

        [JsonProperty("Origen")]
        public string? Origen { get; set; }

        [JsonProperty("Destino")]
        public string? Destino { get; set; }

        [JsonProperty("Valor")]
        public double? Valor { get; set; }
    }

    public class ApiException : Exception
    {
        public int StatusCode { get; }
        public string Response { get; }

        public ApiException(string message, int statusCode, string response)
            : base(message)
        {
            StatusCode = statusCode;
            Response = response;
        }
    }
}
