namespace ec.edu.monster.vista.Pages
{
    public partial class MainPage : ContentPage
    {
        public MainPage(ec.edu.monster.vista.MainPageModel model)
        {
            InitializeComponent();
            BindingContext = model;
        }
    }
}