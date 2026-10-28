using System;
using System.Globalization;
using ec.edu.monster.modelo;
using ec.edu.monster.servicio;
using ec.edu.monster.vista;

namespace ec.edu.monster.controlador
{
    public class ConversionController
    {
        private readonly IConversionView _vista;
        private readonly ConversionApiClient _apiClient;
        private readonly AuthService _authService;
        private bool _isAuthenticated;

        public bool IsAuthenticated => _isAuthenticated;

        public ConversionController(IConversionView vista)
            : this(vista, new ConversionApiClient(), new AuthService())
        {
        }

        public ConversionController(IConversionView vista, ConversionApiClient apiClient, AuthService authService)
        {
            _vista = vista ?? throw new ArgumentNullException(nameof(vista));
            _apiClient = apiClient ?? throw new ArgumentNullException(nameof(apiClient));
            _authService = authService ?? throw new ArgumentNullException(nameof(authService));
        }

        public void Iniciar()
        {
            _isAuthenticated = false;
            _vista.CargarCategorias(ConversionModel.Categorias);
            var categoriaInicial = ConversionModel.Categorias.Length > 0 ? ConversionModel.Categorias[0] : string.Empty;
            ActualizarUnidades(categoriaInicial);
            _vista.MostrarPantallaLogin();
        }

        public void IniciarSesion(string usuario, string contrasena)
        {
            if (_authService.ValidarCredenciales(usuario, contrasena))
            {
                _isAuthenticated = true;
                _vista.MostrarMensajeLogin(string.Empty, false);
                _vista.MostrarPantallaPrincipal();
            }
            else
            {
                _isAuthenticated = false;
                _vista.MostrarMensajeLogin("Credenciales incorrectas.", true);
            }
        }

        public void CerrarSesion()
        {
            _isAuthenticated = false;
            _vista.MostrarMensajeLogin(string.Empty, false);
            _vista.MostrarPantallaLogin();
        }

        public void ActualizarUnidades(string categoria)
        {
            var unidades = ConversionModel.ObtenerUnidades(categoria);
            _vista.CargarUnidades(unidades);
        }

        public void Convertir(string categoria, string origen, string destino, string valorTexto)
        {
            if (!_isAuthenticated)
            {
                _vista.MostrarPantallaLogin();
                return;
            }

            if (!TryParseValue(valorTexto, out var valor))
            {
                _vista.MostrarResultado("Resultado: valor invalido.", false);
                return;
            }

            var request = new ConversionRequest
            {
                Categoria = categoria ?? string.Empty,
                Origen = origen ?? string.Empty,
                Destino = destino ?? string.Empty,
                Valor = valor
            };

            var response = _apiClient.Convert(request);
            if (response != null && response.Exitoso)
            {
                _vista.MostrarResultado("Resultado: " + response.Mensaje, true);
            }
            else
            {
                var errorMsg = response?.Mensaje ?? "Error al procesar la conversión.";
                _vista.MostrarResultado("Resultado: " + errorMsg, false);
            }
        }

        public void Limpiar()
        {
            if (!_isAuthenticated) return;
            _vista.LimpiarCamposConversion();
            _vista.MostrarResultado("Resultado:", true);
        }

        private static bool TryParseValue(string text, out double value)
        {
            return double.TryParse(text, NumberStyles.Float, CultureInfo.CurrentCulture, out value)
                || double.TryParse(text, NumberStyles.Float, CultureInfo.InvariantCulture, out value);
        }
    }
}
