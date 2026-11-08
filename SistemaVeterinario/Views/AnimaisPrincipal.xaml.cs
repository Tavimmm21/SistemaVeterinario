using SistemaVeterinario.Models;
using SistemaVeterinario.Services;

namespace SistemaVeterinario.Views
{
    public partial class AnimaisPrincipal : ContentPage
    {
        private readonly AnimalRepositorio _animalRepositorio;
        private readonly ClienteRepositorio _clienteRepositorio;
        private readonly EspecieRepositorio _especieRepositorio;
        private List<Animal> _todosAnimais = new();

        public AnimaisPrincipal(AnimalRepositorio animalRepositorio, ClienteRepositorio clienteRepositorio, EspecieRepositorio especieRepositorio)
        {
            InitializeComponent();
            _animalRepositorio = animalRepositorio;
            _clienteRepositorio = clienteRepositorio;
            _especieRepositorio = especieRepositorio;
        }

        protected override async void OnAppearing()
        {
            base.OnAppearing();
            await CarregarAnimaisAsync();
        }

        private async Task CarregarAnimaisAsync()
        {
            var animais = await _animalRepositorio.ListarAsync();
            var clientes = await _clienteRepositorio.ListarAsync();
            var especies = await _especieRepositorio.ListarAsync();

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
            var clientes = await _clienteRepositorio.ListarAsync();
            var especies = await _especieRepositorio.ListarAsync();

            if (clientes.Count == 0 || especies.Count == 0)
            {
                await DisplayAlert(
                    "Cadastro incompleto",
                    "Antes de cadastrar um animal, é necessário ter pelo menos um Cliente e uma Espécie cadastrados.",
                    "OK");
                return;
            }

            await Navigation.PushAsync(new AnimaisInserir(_animalRepositorio, clientes, especies));
        }

        private async void OnAlterarClicked(object sender, EventArgs e)
        {
            if (sender is Button botao && botao.CommandParameter is Animal animal)
            {
                var clientes = await _clienteRepositorio.ListarAsync();
                var especies = await _especieRepositorio.ListarAsync();
                await Navigation.PushAsync(new AnimaisAlterar(_animalRepositorio, animal, clientes, especies));
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

                await _animalRepositorio.ExcluirAsync(animal);
                await CarregarAnimaisAsync();
            }
        }
    }
}
