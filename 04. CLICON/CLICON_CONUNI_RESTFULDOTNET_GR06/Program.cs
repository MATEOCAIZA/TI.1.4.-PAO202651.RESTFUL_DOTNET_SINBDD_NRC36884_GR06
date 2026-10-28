using System;
using System.Globalization;
using ec.edu.monster.modelo;
using ec.edu.monster.servicio;

namespace ec.edu.monster.vista
{
    internal class Program
    {
        private const string ValidUser = "MONSTER";
        private const string ValidPassword = "MONSTER69";
        private static readonly ConversionApiClient ConversionClient = new ConversionApiClient();

        static void Main(string[] args)
        {
            Console.WriteLine("╔════════════════════════════════════════════════════════════════════╗");
            Console.WriteLine("║                           Iniciar sesion                           ║");
            Console.WriteLine("╚════════════════════════════════════════════════════════════════════╝");
            Console.WriteLine();

            while (true)
            {
                Console.Write("Usuario: ");
                var usuario = (Console.ReadLine() ?? string.Empty).Trim();

                Console.Write("Contraseña: ");
                var contrasena = ReadPassword();
                Console.WriteLine();

                if (!string.Equals(usuario, ValidUser, StringComparison.OrdinalIgnoreCase) ||
                    !string.Equals(contrasena, ValidPassword, StringComparison.Ordinal))
                {
                    Console.WriteLine();
                    Console.WriteLine("Credenciales incorrectas.");
                    Console.WriteLine("Presiona cualquier tecla para volver a intentarlo.");
                    Console.ReadKey(true);
                    Console.Clear();
                    Console.WriteLine("╔════════════════════════════════════════════════════════════════════╗");
                    Console.WriteLine("║                           Iniciar sesion                           ║");
                    Console.WriteLine("╚════════════════════════════════════════════════════════════════════╝");
                    Console.WriteLine();
                    continue;
                }

                Console.WriteLine("Acceso concedido.");
                Console.WriteLine();
                break;
            }

            while (true)
            {
                Console.WriteLine("┌──────────────────────────────────────────────────────────────────┐");
                Console.WriteLine("│ SELECCIONA UNA CATEGORIA                                         │");
                Console.WriteLine("├──────────────────────────────────────────────────────────────────┤");
                Console.WriteLine("│ 1. Masa                                                          │");
                Console.WriteLine("│ 2. Longitud                                                      │");
                Console.WriteLine("│ 3. Temperatura                                                   │");
                Console.WriteLine("│ 0. Salir                                                         │");
                Console.WriteLine("└──────────────────────────────────────────────────────────────────┘");
                Console.Write("Tu opcion: ");

                var categoryChoice = Console.ReadLine() ?? string.Empty;
                string categoria;
                if (categoryChoice == "1") categoria = "masa";
                else if (categoryChoice == "2") categoria = "longitud";
                else if (categoryChoice == "3") categoria = "temperatura";
                else if (categoryChoice == "0") categoria = null;
                else categoria = string.Empty;

                if (categoria == null)
                {
                    Console.WriteLine("Gracias por usar el servicio.");
                    break;
                }

                if (categoria == string.Empty)
                {
                    Console.WriteLine("Opcion invalida. Intenta de nuevo.");
                    Console.WriteLine();
                    continue;
                }

                string[] unidades;
                if (categoria == "longitud")
                {
                    unidades = new[] { "m", "cm", "km", "in", "gt" };
                }
                else if (categoria == "temperatura")
                {
                    unidades = new[] { "C", "F", "K", "R" };
                }
                else
                {
                    unidades = new[] { "kg", "g", "lb", "oz", "t" };
                }

                Console.WriteLine();
                Console.WriteLine("┌──────────────────────────────────────────────────────────────────┐");
                Console.WriteLine("│ SELECCIONA UNIDAD ORIGEN                                         │");
                Console.WriteLine("├──────────────────────────────────────────────────────────────────┤");
                for (int i = 0; i < unidades.Length; i++)
                {
                    Console.WriteLine("│ " + (i + 1).ToString() + ". " + unidades[i].PadRight(62) + "│");
                }
                Console.WriteLine("└──────────────────────────────────────────────────────────────────┘");
                Console.Write("Tu opcion: ");

                var origenChoice = Console.ReadLine() ?? string.Empty;
                int origenIdx;
                if (!int.TryParse(origenChoice, out origenIdx) || origenIdx < 1 || origenIdx > unidades.Length)
                {
                    Console.WriteLine("Opcion invalida.");
                    Console.WriteLine();
                    continue;
                }

                var origen = unidades[origenIdx - 1];

                Console.WriteLine();
                Console.WriteLine("┌──────────────────────────────────────────────────────────────────┐");
                Console.WriteLine("│ SELECCIONA UNIDAD DESTINO                                        │");
                Console.WriteLine("├──────────────────────────────────────────────────────────────────┤");
                for (int i = 0; i < unidades.Length; i++)
                {
                    Console.WriteLine("│ " + (i + 1).ToString() + ". " + unidades[i].PadRight(62) + "│");
                }
                Console.WriteLine("└──────────────────────────────────────────────────────────────────┘");
                Console.Write("Tu opcion: ");

                var destinoChoice = Console.ReadLine() ?? string.Empty;
                int destinoIdx;
                if (!int.TryParse(destinoChoice, out destinoIdx) || destinoIdx < 1 || destinoIdx > unidades.Length)
                {
                    Console.WriteLine("Opcion invalida.");
                    Console.WriteLine();
                    continue;
                }

                var destino = unidades[destinoIdx - 1];

                Console.WriteLine();
                Console.Write("Ingresa el valor: ");
                var valorTexto = Console.ReadLine() ?? string.Empty;

                double valor;
                if (!double.TryParse(valorTexto, NumberStyles.Float, CultureInfo.CurrentCulture, out valor) &&
                    !double.TryParse(valorTexto, NumberStyles.Float, CultureInfo.InvariantCulture, out valor))
                {
                    Console.WriteLine("Valor invalido.");
                    Console.WriteLine();
                    continue;
                }

                var response = ConversionClient.Convert(new ConversionRequest
                {
                    Categoria = categoria,
                    Origen = origen,
                    Destino = destino,
                    Valor = valor
                });

                Console.WriteLine();
                Console.WriteLine("┌──────────────────────────────────────────────────────────────────┐");
                Console.WriteLine("│ RESULTADO                                                        │");
                Console.WriteLine("├──────────────────────────────────────────────────────────────────┤");
                if (response.Exitoso)
                {
                    Console.WriteLine("│ ✓ " + response.Mensaje.PadRight(64) + "│");
                }
                else
                {
                    Console.WriteLine("│ ✗ " + response.Mensaje.PadRight(64) + "│");
                }
                Console.WriteLine("└──────────────────────────────────────────────────────────────────┘");
                Console.WriteLine();
            }
        }

        private static string ReadPassword()
        {
            var password = string.Empty;

            while (true)
            {
                var key = Console.ReadKey(true);

                if (key.Key == ConsoleKey.Enter)
                {
                    break;
                }

                if (key.Key == ConsoleKey.Backspace)
                {
                    if (password.Length > 0)
                    {
                        password = password.Substring(0, password.Length - 1);
                        if (Console.CursorLeft > 0)
                        {
                            Console.Write("\b \b");
                        }
                    }

                    continue;
                }

                if (!char.IsControl(key.KeyChar))
                {
                    password += key.KeyChar;
                    Console.Write("*");
                }
            }

            return password;
        }
    }
}
