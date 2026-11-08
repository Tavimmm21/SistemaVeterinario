using SistemaVeterinario.Models;
using SistemaVeterinario.Services;

namespace SistemaVeterinario.Views
{
    public partial class EspeciesPrincipal : ContentPage
    {
        private readonly EspecieRepositorio _especieRepositorio;
        private List<Especie> _todasEspecies = new();

        public EspeciesPrincipal(EspecieRepositorio especieRepositorio)
        {
            InitializeComponent();
            _especieRepositorio = especieRepositorio;
        }

        protected override async void OnAppearing()
        {
            base.OnAppearing();
            await CarregarEspeciesAsync();
        }

        private async Task CarregarEspeciesAsync()
        {
            _todasEspecies = await _especieRepositorio.ListarAsync();
            ColecaoEspecies.ItemsSource = _todasEspecies;
            EntryPesquisar.Text = string.Empty;
        }

        private void OnPesquisarTextChanged(object sender, TextChangedEventArgs e)
        {
            var termo = e.NewTextValue?.Trim() ?? string.Empty;

            ColecaoEspecies.ItemsSource = string.IsNullOrEmpty(termo)
                ? _todasEspecies
                : _todasEspecies
                    .Where(esp => esp.Nome.Contains(termo, StringComparison.OrdinalIgnoreCase))
                    .ToList();
        }

        private async void OnInserirClicked(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new EspeciesInserir(_especieRepositorio));
        }

        private async void OnAlterarClicked(object sender, EventArgs e)
        {
            if (sender is Button botao && botao.CommandParameter is Especie especie)
            {
                await Navigation.PushAsync(new EspeciesAlterar(_especieRepositorio, especie));
            }
        }

        private async void OnExcluirClicked(object sender, EventArgs e)
        {
            if (sender is Button botao && botao.CommandParameter is Especie especie)
            {
                bool confirmar = await DisplayAlert(
                    "Excluir Espécie",
                    $"Confirma a exclusão da espécie \"{especie.Nome}\"?",
                    "Excluir",
                    "Cancelar");

                if (!confirmar)
                    return;

                await _especieRepositorio.ExcluirAsync(especie);
                await CarregarEspeciesAsync();
            }
        }
    }
}
