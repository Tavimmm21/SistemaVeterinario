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
            await Shell.Current.GoToAsync("//ClientesPrincipal");
        }

        private async void OnAnimaisClicked(object sender, EventArgs e)
        {
            await Shell.Current.GoToAsync("//AnimaisPrincipal");
        }

        private async void OnEspeciesClicked(object sender, EventArgs e)
        {
            await Shell.Current.GoToAsync("//EspeciesPrincipal");
        }
    }
}
