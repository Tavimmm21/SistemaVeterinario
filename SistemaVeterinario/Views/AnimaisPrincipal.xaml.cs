using SistemaVeterinario.Models;
using SistemaVeterinario.Services;

namespace SistemaVeterinario.Views
{
    public partial class AnimaisPrincipal : ContentPage
    {
        private readonly DatabaseService _databaseService;
        private List<Animal> _todosAnimais = new();

        public AnimaisPrincipal(DatabaseService databaseService)
        {
            InitializeComponent();
            _databaseService = databaseService;
        }

        protected override async void OnAppearing()
        {
            base.OnAppearing();
            await CarregarAnimaisAsync();
        }

        private async Task CarregarAnimaisAsync()
        {
            var animais = await _databaseService.GetAnimaisAsync();
            var clientes = await _databaseService.GetClientesAsync();
            var especies = await _databaseService.GetEspeciesAsync();

            foreach (var animal in animais)
            {
                animal.ClienteNome = clientes.FirstOrDefault(c => c.Id == animal.ClienteId)?.Nome ?? "(sem dono)";
                animal.EspecieNome = especies.FirstOrDefault(esp => esp.Id == animal.EspecieId)?.Nome ?? "(sem espécie)";
            }

            _todosAnimais = animais;
            ColecaoAnimais.ItemsSource = _todosAnimais;
            EntryPesquisar.Text = string.Empty;
        }

        private void OnPesquisarTextChanged(object sender, TextChangedEventArgs e)
        {
            var termo = e.NewTextValue?.Trim() ?? string.Empty;

            ColecaoAnimais.ItemsSource = string.IsNullOrEmpty(termo)
                ? _todosAnimais
                : _todosAnimais
                    .Where(a => a.Nome.Contains(termo, StringComparison.OrdinalIgnoreCase))
                    .ToList();
        }

        private async void OnInserirClicked(object sender, EventArgs e)
        {
            var clientes = await _databaseService.GetClientesAsync();
            var especies = await _databaseService.GetEspeciesAsync();

            if (clientes.Count == 0 || especies.Count == 0)
            {
                await DisplayAlert(
                    "Cadastro incompleto",
                    "Antes de cadastrar um animal, é necessário ter pelo menos um Cliente e uma Espécie cadastrados.",
                    "OK");
                return;
            }

            await Navigation.PushAsync(new AnimaisInserir(_databaseService, clientes, especies));
        }

        private async void OnAlterarClicked(object sender, EventArgs e)
        {
            if (sender is Button botao && botao.CommandParameter is Animal animal)
            {
                var clientes = await _databaseService.GetClientesAsync();
                var especies = await _databaseService.GetEspeciesAsync();
                await Navigation.PushAsync(new AnimaisAlterar(_databaseService, animal, clientes, especies));
            }
        }

        private async void OnExcluirClicked(object sender, EventArgs e)
        {
            if (sender is Button botao && botao.CommandParameter is Animal animal)
            {
                bool confirmar = await DisplayAlert(
                    "Excluir Animal",
                    $"Confirma a exclusão do animal \"{animal.Nome}\"?",
                    "Excluir",
                    "Cancelar");

                if (!confirmar)
                    return;

                await _databaseService.ExcluirAnimalAsync(animal);
                await CarregarAnimaisAsync();
            }
        }
    }
}
