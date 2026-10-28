using System.Runtime.Serialization;

namespace ec.edu.monster.modelo
{
    [DataContract]
    public class ConversionResponse
    {
        [DataMember]
        public bool Exitoso { get; set; }

        [DataMember]
        public string Mensaje { get; set; }

        [DataMember]
        public double Resultado { get; set; }

        public ConversionResponse()
        {
            Mensaje = string.Empty;
        }
    }
}