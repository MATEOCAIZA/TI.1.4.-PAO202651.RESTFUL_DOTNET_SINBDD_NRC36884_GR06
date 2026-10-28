using System.Web.Http;
using ec.edu.monster.modelo;
using ec.edu.monster.servicio;

namespace ec.edu.monster.vista
{
    [RoutePrefix("api/conversion")]
    public class ConversionController : ApiController
    {
        private readonly ConversionService _conversionService = new ConversionService();

        [HttpPost]
        [Route("")]
        public IHttpActionResult Post([FromBody] ConversionRequest request)
        {
            return Ok(_conversionService.Convert(request));
        }
    }
}
