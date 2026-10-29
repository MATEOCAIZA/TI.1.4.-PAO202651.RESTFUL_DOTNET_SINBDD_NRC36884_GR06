using System;
using System.Configuration;
using System.Net.Http;
using ec.edu.monster.modelo;
using Newtonsoft.Json.Linq;

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
                var generatedClient = new CLICON_CONUNI_RESTFULDOTNET_GR06.ConversionClient(HttpClient)
                {
                    BaseUrl = baseUrl
                };

                var generatedRequest = new CLICON_CONUNI_RESTFULDOTNET_GR06.ConversionRequest
                {
                    Categoria = request.Categoria,
                    Origen = request.Origen,
                    Destino = request.Destino,
                    Valor = request.Valor
                };

                var result = generatedClient.PostAsync(generatedRequest).GetAwaiter().GetResult();

                if (result is JObject jObject)
                    return jObject.ToObject<ConversionResponse>();

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
