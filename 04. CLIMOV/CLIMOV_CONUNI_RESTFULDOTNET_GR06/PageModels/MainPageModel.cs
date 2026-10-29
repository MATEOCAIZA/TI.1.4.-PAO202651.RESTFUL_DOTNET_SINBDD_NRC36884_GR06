using System.Collections.ObjectModel;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using ec.edu.monster.modelo;
using ec.edu.monster.servicio;

namespace ec.edu.monster.vista
{
    public partial class MainPageModel : ObservableObject
    {
        private const string ValidUser = "MONSTER";
        private const string ValidPassword = "MONSTER69";

        private readonly ConversionApiClient _conversionClient;

        public ObservableCollection<string> Categories { get; } = new ObservableCollection<string>
        {
            "masa",
            "longitud",
            "temperatura"
        };

        [ObservableProperty]
        private ObservableCollection<string> _originUnits = new ObservableCollection<string> { "kg", "g", "lb", "oz", "t" };

        [ObservableProperty]
        private ObservableCollection<string> _destinationUnits = new ObservableCollection<string> { "kg", "g", "lb", "oz", "t" };

        [ObservableProperty]
        private string _usuario = string.Empty;

        [ObservableProperty]
        private string _contrasena = string.Empty;

        [ObservableProperty]
        private string _loginError = string.Empty;

        [ObservableProperty]
        private bool _isLoginVisible = true;

        [ObservableProperty]
        private bool _isConverterVisible;

        [ObservableProperty]
        private string _selectedCategory = "masa";

        [ObservableProperty]
        private string _selectedOrigin = "kg";

        [ObservableProperty]
        private string _selectedDestination = "g";

        [ObservableProperty]
        private string _valueText = "0";

        [ObservableProperty]
        private string _result = "Resultado: espera una conversion";

        [ObservableProperty]
        private bool _isBusy;

        public MainPageModel(ConversionApiClient conversionApiClient)
        {
            _conversionClient = conversionApiClient;
            UpdateUnits();
        }

        partial void OnSelectedCategoryChanged(string value)
        {
            UpdateUnits();
        }

        private void UpdateUnits()
        {
            string[] units;
            if (SelectedCategory == "longitud")
            {
                units = new[] { "m", "cm", "km", "in", "gt" };
            }
            else if (SelectedCategory == "temperatura")
            {
                units = new[] { "C", "F", "K", "R" };
            }
            else
            {
                units = new[] { "kg", "g", "lb", "oz", "t" };
            }

            OriginUnits.Clear();
            DestinationUnits.Clear();
            foreach (var unit in units)
            {
                OriginUnits.Add(unit);
                DestinationUnits.Add(unit);
            }

            SelectedOrigin = units.FirstOrDefault() ?? string.Empty;
            SelectedDestination = units.Skip(1).FirstOrDefault() ?? units.FirstOrDefault() ?? string.Empty;
        }

        [RelayCommand]
        private void Login()
        {
            if (string.Equals(Usuario?.Trim(), ValidUser, System.StringComparison.OrdinalIgnoreCase) &&
                string.Equals(Contrasena ?? string.Empty, ValidPassword, System.StringComparison.Ordinal))
            {
                LoginError = string.Empty;
                IsLoginVisible = false;
                IsConverterVisible = true;
                SelectedCategory = "masa";
                ValueText = "0";
                Result = "Resultado: espera una conversion";
                return;
            }

            LoginError = "Credenciales incorrectas.";
            Contrasena = string.Empty;
        }

        [RelayCommand]
        private void Logout()
        {
            Usuario = string.Empty;
            Contrasena = string.Empty;
            LoginError = string.Empty;
            IsConverterVisible = false;
            IsLoginVisible = true;
            SelectedCategory = "masa";
            ValueText = "0";
            Result = "Resultado: espera una conversion";
        }

        [RelayCommand]
        private void Clear()
        {
            SelectedCategory = "masa";
            SelectedOrigin = "kg";
            SelectedDestination = "g";
            ValueText = "0";
            Result = "Resultado: espera una conversion";
        }

        [RelayCommand]
        private void Convert()
        {
            if (!IsConverterVisible)
            {
                return;
            }

            if (!double.TryParse(ValueText, out var value) && !double.TryParse(ValueText, System.Globalization.NumberStyles.Float, System.Globalization.CultureInfo.InvariantCulture, out value))
            {
                Result = "Resultado: valor invalido.";
                return;
            }

            IsBusy = true;

            try
            {
                var response = _conversionClient.Convert(new ConversionRequest
                {
                    Categoria = SelectedCategory,
                    Origen = SelectedOrigin,
                    Destino = SelectedDestination,
                    Valor = value
                });

                Result = response.Exitoso
                    ? $"Resultado: {response.Mensaje}"
                    : $"Resultado: {response.Mensaje}";
            }
            finally
            {
                IsBusy = false;
            }
        }
    }
}
