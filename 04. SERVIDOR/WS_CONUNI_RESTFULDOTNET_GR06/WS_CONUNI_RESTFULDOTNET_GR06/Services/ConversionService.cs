using System;
using System.Collections.Generic;
using System.Globalization;
using ec.edu.monster.modelo;

namespace ec.edu.monster.servicio
{
    public class ConversionService
    {
        private static readonly Dictionary<string, double> MassFactors = new Dictionary<string, double>(StringComparer.OrdinalIgnoreCase)
        {
            ["kg"] = 1d,
            ["g"] = 0.001d,
            ["lb"] = 0.45359237d,
            ["oz"] = 0.028349523125d,
            ["t"] = 1000d
        };

        private static readonly Dictionary<string, double> LengthFactors = new Dictionary<string, double>(StringComparer.OrdinalIgnoreCase)
        {
            ["m"] = 1d,
            ["cm"] = 0.01d,
            ["km"] = 1000d,
            ["in"] = 0.0254d,
            ["gt"] = 0.3048d
        };

        public ConversionResponse Convert(ConversionRequest request)
        {
            if (request == null)
            {
                return Invalid("La solicitud de conversion no puede ser nula.");
            }

            var categoria = Normalize(request.Categoria);
            var origen = Normalize(request.Origen);
            var destino = Normalize(request.Destino);

            if (string.IsNullOrWhiteSpace(categoria) || string.IsNullOrWhiteSpace(origen) || string.IsNullOrWhiteSpace(destino))
            {
                return Invalid("Categoria, origen y destino son obligatorios.");
            }

            try
            {
                double resultado;

                switch (categoria)
                {
                    case "masa":
                    case "mass":
                        resultado = ConvertByFactors(request.Valor, origen, destino, MassFactors);
                        break;
                    case "longitud":
                    case "length":
                        resultado = ConvertByFactors(request.Valor, origen, destino, LengthFactors);
                        break;
                    case "temperatura":
                    case "temperature":
                        resultado = ConvertTemperature(request.Valor, origen, destino);
                        break;
                    default:
                        return Invalid("Categoria no valida. Use masa, longitud o temperatura.");
                }

                return new ConversionResponse
                {
                    Exitoso = true,
                    Resultado = resultado,
                    Mensaje = string.Format(CultureInfo.InvariantCulture, "{0} {1} = {2} {3}", request.Valor, origen, resultado, destino)
                };
            }
            catch (ArgumentException ex)
            {
                return Invalid(ex.Message);
            }
        }

        private static double ConvertByFactors(double value, string origin, string destination, IReadOnlyDictionary<string, double> factors)
        {
            if (!factors.TryGetValue(origin, out var originFactor))
            {
                throw new ArgumentException($"Unidad de origen no valida: {origin}");
            }

            if (!factors.TryGetValue(destination, out var destinationFactor))
            {
                throw new ArgumentException($"Unidad de destino no valida: {destination}");
            }

            return value * originFactor / destinationFactor;
        }

        private static double ConvertTemperature(double value, string origin, string destination)
        {
            double celsius;

            switch (origin)
            {
                case "c":
                case "c°":
                    celsius = value;
                    break;
                case "f":
                case "f°":
                    celsius = (value - 32d) * 5d / 9d;
                    break;
                case "k":
                    celsius = value - 273.15d;
                    break;
                case "r":
                case "r°":
                    celsius = (value - 491.67d) * 5d / 9d;
                    break;
                default:
                    throw new ArgumentException($"Unidad de origen no valida: {origin}");
            }

            switch (destination)
            {
                case "c":
                case "c°":
                    return celsius;
                case "f":
                case "f°":
                    return celsius * 9d / 5d + 32d;
                case "k":
                    return celsius + 273.15d;
                case "r":
                case "r°":
                    return (celsius + 273.15d) * 9d / 5d;
                default:
                    throw new ArgumentException($"Unidad de destino no valida: {destination}");
            }
        }

        private static ConversionResponse Invalid(string message)
        {
            return new ConversionResponse
            {
                Exitoso = false,
                Mensaje = message,
                Resultado = 0d
            };
        }

        private static string Normalize(string value)
        {
            return (value ?? string.Empty).Trim().ToLowerInvariant();
        }
    }
}