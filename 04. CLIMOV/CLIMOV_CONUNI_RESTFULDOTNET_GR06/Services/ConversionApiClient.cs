using System;
using System.Net.Http;
using ec.edu.monster.modelo;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json.Linq;

namespace ec.edu.monster.servicio
{
    public class ConversionApiClient
    {
        private static readonly HttpClient HttpClient = new HttpClient();
        private readonly string _baseUrl;

        public ConversionApiClient(IConfiguration configuration)
        {
            _baseUrl = configuration["ApiBaseUrl"] ?? "http://172.20.10.2:8082";
        }

        public ConversionResponse Convert(ConversionRequest request)
        {
            try
            {
                var generatedClient = new CLIMOV_CONUNI_RESTFULDOTNET_GR06.ConversionClient(HttpClient)
                {
                    BaseUrl = _baseUrl
                };

                var generatedRequest = new CLIMOV_CONUNI_RESTFULDOTNET_GR06.ConversionRequest
                {
                    Categoria = request.Categoria,
                    Origen = request.Origen,
                    Destino = request.Destino,
                    Valor = request.Valor
                };

                var result = generatedClient.PostAsync(generatedRequest).GetAwaiter().GetResult();

                if (result is JObject jObject)
                    return jObject.ToObject<ConversionResponse>() ?? new ConversionResponse { Exitoso = false, Mensaje = "Respuesta inesperada del servidor." };

                return new ConversionResponse { Exitoso = false, Mensaje = "Respuesta inesperada del servidor." };
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
