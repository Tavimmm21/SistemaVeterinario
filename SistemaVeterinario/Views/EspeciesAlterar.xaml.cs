using SistemaVeterinario.Models;
using SistemaVeterinario.Services;

namespace SistemaVeterinario.Views
{
    public partial class EspeciesAlterar : ContentPage
    {
        private readonly DatabaseService _databaseService;
        private readonly Especie _especie;

        public EspeciesAlterar(DatabaseService databaseService, Especie especie)
        {
            InitializeComponent();
            _databaseService = databaseService;
            _especie = especie;

            EntryNome.Text = especie.Nome;
        }

        private async void OnSalvarClicked(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(EntryNome.Text))
            {
                await DisplayAlert("Campo obrigatório", "Informe o nome da espécie.", "OK");
                return;
            }

            _especie.Nome = EntryNome.Text.Trim();

            await _databaseService.SalvarEspecieAsync(_especie);
            await Navigation.PopAsync();
        }

        private async void OnVoltarClicked(object sender, EventArgs e)
        {
            await Navigation.PopAsync();
        }
    }
}
