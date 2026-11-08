using SistemaVeterinario.Models;
using SistemaVeterinario.Services;

namespace SistemaVeterinario.Views
{
    public partial class EspeciesInserir : ContentPage
    {
        private readonly EspecieRepositorio _especieRepositorio;

        public EspeciesInserir(EspecieRepositorio especieRepositorio)
        {
            InitializeComponent();
            _especieRepositorio = especieRepositorio;
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

            await _especieRepositorio.InserirAsync(especie);
            await Navigation.PopAsync();
        }

        private async void OnVoltarClicked(object sender, EventArgs e)
        {
            await Navigation.PopAsync();
        }
    }
}
