namespace SistemaVeterinario
{
    public partial class MainPage : ContentPage
    {
        public MainPage()
        {
            InitializeComponent();
        }

        private async void OnClientesClicked(object sender, EventArgs e)
        {
            await Shell.Current.GoToAsync("//ClientesPage");
        }

        private async void OnAnimaisClicked(object sender, EventArgs e)
        {
            await Shell.Current.GoToAsync("//AnimaisPage");
        }

        private async void OnEspeciesClicked(object sender, EventArgs e)
        {
            await Shell.Current.GoToAsync("//EspeciesPage");
        }
    }
}
