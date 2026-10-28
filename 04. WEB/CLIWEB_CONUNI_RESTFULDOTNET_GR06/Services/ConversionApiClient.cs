using System;
using System.Configuration;
using System.Net.Http;
using System.Text;
using ec.edu.monster.modelo;
using Newtonsoft.Json;

namespace ec.edu.monster.servicio
{
    public class ConversionApiClient
    {
        private static readonly HttpClient HttpClient = new HttpClient();

        public ConversionResponse Convert(ConversionRequest request)
        {
            try
            {
                var baseUrl = ConfigurationManager.AppSettings["ApiBaseUrl"];
                var url = $"{baseUrl?.TrimEnd('/')}/api/conversion";
                var json = JsonConvert.SerializeObject(request);
                using (var content = new StringContent(json, Encoding.UTF8, "application/json"))
                using (var response = HttpClient.PostAsync(url, content).GetAwaiter().GetResult())
                {
                    var payload = response.Content.ReadAsStringAsync().GetAwaiter().GetResult();
                    return JsonConvert.DeserializeObject<ConversionResponse>(payload)
                        ?? new ConversionResponse { Exitoso = false, Mensaje = "Respuesta vacía del servidor." };
                }
            }
            catch (Exception ex)
            {
                return new ConversionResponse
                {
                    Exitoso = false,
                    Resultado = 0d,
                    Mensaje = ex.Message
                };
            }
        }
    }
}
