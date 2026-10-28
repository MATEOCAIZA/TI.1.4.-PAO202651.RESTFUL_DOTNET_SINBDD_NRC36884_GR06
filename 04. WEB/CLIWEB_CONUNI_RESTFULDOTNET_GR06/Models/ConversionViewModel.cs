using System.Web.Mvc;

namespace ec.edu.monster.modelo
{
    public class ConversionViewModel : ConversionRequest
    {
        public string Usuario { get; set; }
        public string Contrasena { get; set; }
        public string LoginError { get; set; }
        public bool IsAuthenticated { get; set; }

        public double? Resultado { get; set; }

        public string Mensaje { get; set; }

        public SelectList CategoriaOptions { get; set; }
        public SelectList OrigenOptions { get; set; }
        public SelectList DestinoOptions { get; set; }

        public ConversionViewModel()
        {
            Usuario = string.Empty;
            Contrasena = string.Empty;
            LoginError = string.Empty;
            Mensaje = string.Empty;
            InitializeOptions();
        }

        private void InitializeOptions()
        {
            CategoriaOptions = new SelectList(new[] { "masa", "longitud", "temperatura" }, Categoria ?? "masa");
            UpdateUnitOptions();
        }

        public void UpdateUnitOptions()
        {
            var units = GetUnitsForCategoria(Categoria ?? "masa");
            CategoriaOptions = new SelectList(new[] { "masa", "longitud", "temperatura" }, Categoria ?? "masa");
            OrigenOptions = new SelectList(units, Origen ?? string.Empty);
            DestinoOptions = new SelectList(units, Destino ?? string.Empty);
        }

        private string[] GetUnitsForCategoria(string categoria)
        {
            var cat = categoria == null ? "masa" : categoria.ToLower();
            if (cat == "masa") return new[] { "kg", "g", "lb", "oz", "t" };
            if (cat == "longitud") return new[] { "m", "cm", "km", "in", "gt" };
            if (cat == "temperatura") return new[] { "C", "F", "K", "R" };
            return new[] { "kg", "g", "lb", "oz", "t" };
        }

        public ConversionRequest ToRequest()
        {
            return new ConversionRequest
            {
                Categoria = Categoria,
                Origen = Origen,
                Destino = Destino,
                Valor = Valor
            };
        }
    }
}