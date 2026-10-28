using System.Runtime.Serialization;

namespace ec.edu.monster.modelo
{
    [DataContract]
    public class ConversionRequest
    {
        [DataMember]
        public string Categoria { get; set; }

        [DataMember]
        public string Origen { get; set; }

        [DataMember]
        public string Destino { get; set; }

        [DataMember]
        public double Valor { get; set; }

        public ConversionRequest()
        {
            Categoria = string.Empty;
            Origen = string.Empty;
            Destino = string.Empty;
        }
    }
}