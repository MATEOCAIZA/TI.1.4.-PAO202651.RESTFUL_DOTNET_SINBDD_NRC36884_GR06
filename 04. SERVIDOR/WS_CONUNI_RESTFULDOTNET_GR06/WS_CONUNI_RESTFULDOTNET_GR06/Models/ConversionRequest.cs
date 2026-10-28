using System.Runtime.Serialization;

namespace ec.edu.monster.modelo
{
    [DataContract]
    public class ConversionRequest
    {
        [DataMember]
        public string Categoria { get; set; } = string.Empty;

        [DataMember]
        public string Origen { get; set; } = string.Empty;

        [DataMember]
        public string Destino { get; set; } = string.Empty;

        [DataMember]
        public double Valor { get; set; }
    }
}