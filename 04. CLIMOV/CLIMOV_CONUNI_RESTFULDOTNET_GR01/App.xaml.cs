using Microsoft.Extensions.DependencyInjection;

namespace CLIMOV_CONUNI_RESTFULDOTNET_GR01
{
    public partial class App : Application
    {
        public App()
        {
            InitializeComponent();
        }

        protected override Window CreateWindow(IActivationState? activationState)
        {
            return new Window(new AppShell());
        }
    }
}