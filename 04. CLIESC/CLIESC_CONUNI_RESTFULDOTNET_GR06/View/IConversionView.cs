namespace ec.edu.monster.vista
{
    public interface IConversionView
    {
        void CargarCategorias(string[] categorias);
        void CargarUnidades(string[] unidades);
        void MostrarPantallaLogin();
        void MostrarPantallaPrincipal();
        void MostrarMensajeLogin(string mensaje, bool esError);
        void MostrarResultado(string mensaje, bool esExito);
        void LimpiarCamposConversion();
    }
}
