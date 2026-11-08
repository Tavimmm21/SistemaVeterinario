using SistemaVeterinario.Models;
using SistemaVeterinario.Services;

namespace SistemaVeterinario.Views
{
    public partial class EspeciesAlterar : ContentPage
    {
        private readonly EspecieRepositorio _especieRepositorio;
        private readonly Especie _especie;

        public EspeciesAlterar(EspecieRepositorio especieRepositorio, Especie especie)
        {
            InitializeComponent();
            _especieRepositorio = especieRepositorio;
            _especie = especie;

            EntryId.Text = especie.Id.ToString();
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

            await _especieRepositorio.AlterarAsync(_especie);
            await Navigation.PopAsync();
        }

        private async void OnVoltarClicked(object sender, EventArgs e)
        {
            await Navigation.PopAsync();
        }
    }
}
