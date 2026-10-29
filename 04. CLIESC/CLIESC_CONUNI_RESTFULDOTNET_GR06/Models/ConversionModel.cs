using System;
using System.Collections.Generic;

namespace ec.edu.monster.modelo
{
    public class ConversionModel
    {
        public static readonly string[] Categorias = { "masa", "longitud", "temperatura" };

        public static readonly Dictionary<string, string[]> UnidadesPorCategoria = new Dictionary<string, string[]>(StringComparer.OrdinalIgnoreCase)
        {
            { "masa", new[] { "kg", "g", "lb", "oz", "t" } },
            { "longitud", new[] { "m", "cm", "km", "in", "ft" } },
            { "temperatura", new[] { "C", "F", "K", "R" } }
        };

        public static string[] ObtenerUnidades(string categoria)
        {
            if (string.IsNullOrWhiteSpace(categoria))
            {
                return new string[0];
            }

            return UnidadesPorCategoria.TryGetValue(categoria, out var unidades) ? unidades : new string[0];
        }
    }
}
