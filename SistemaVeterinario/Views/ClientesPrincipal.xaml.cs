using SistemaVeterinario.Models;
using SistemaVeterinario.Services;

namespace SistemaVeterinario.Views
{
    public partial class ClientesPrincipal : ContentPage
    {
        private readonly DatabaseService _databaseService;
        private List<Cliente> _todosClientes = new();

        public ClientesPrincipal(DatabaseService databaseService)
        {
            InitializeComponent();
            _databaseService = databaseService;
        }

        protected override async void OnAppearing()
        {
            base.OnAppearing();
            await CarregarClientesAsync();
        }

        private async Task CarregarClientesAsync()
        {
            _todosClientes = await _databaseService.GetClientesAsync();
            ColecaoClientes.ItemsSource = _todosClientes;
            EntryPesquisar.Text = string.Empty;
        }

        private void OnPesquisarTextChanged(object sender, TextChangedEventArgs e)
        {
            var termo = e.NewTextValue?.Trim() ?? string.Empty;

            ColecaoClientes.ItemsSource = string.IsNullOrEmpty(termo)
                ? _todosClientes
                : _todosClientes
                    .Where(c => c.Nome.Contains(termo, StringComparison.OrdinalIgnoreCase))
                    .ToList();
        }

        private async void OnInserirClicked(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new ClientesInserir(_databaseService));
        }

        private async void OnAlterarClicked(object sender, EventArgs e)
        {
            if (sender is Button botao && botao.CommandParameter is Cliente cliente)
            {
                await Navigation.PushAsync(new ClientesAlterar(_databaseService, cliente));
            }
        }

        private async void OnExcluirClicked(object sender, EventArgs e)
        {
            if (sender is Button botao && botao.CommandParameter is Cliente cliente)
            {
                bool confirmar = await DisplayAlert(
                    "Excluir Cliente",
                    $"Confirma a exclusão do cliente \"{cliente.Nome}\"?",
                    "Excluir",
                    "Cancelar");

                if (!confirmar)
                    return;

                await _databaseService.ExcluirClienteAsync(cliente);
                await CarregarClientesAsync();
            }
        }
    }
}
