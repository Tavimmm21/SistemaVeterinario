using SistemaVeterinario.Models;
using SistemaVeterinario.Services;

namespace SistemaVeterinario.Views
{
    public partial class EspeciesInserir : ContentPage
    {
        private readonly DatabaseService _databaseService;

        public EspeciesInserir(DatabaseService databaseService)
        {
            InitializeComponent();
            _databaseService = databaseService;
        }

        private async void OnSalvarClicked(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(EntryNome.Text))
            {
                await DisplayAlert("Campo obrigatório", "Informe o nome da espécie.", "OK");
                return;
            }

            var especie = new Especie
            {
                Nome = EntryNome.Text.Trim()
            };

            await _databaseService.SalvarEspecieAsync(especie);
            await Navigation.PopAsync();
        }

        private async void OnVoltarClicked(object sender, EventArgs e)
        {
            await Navigation.PopAsync();
        }
    }
}
