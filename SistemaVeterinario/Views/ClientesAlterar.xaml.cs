using SistemaVeterinario.Models;
using SistemaVeterinario.Services;

namespace SistemaVeterinario.Views
{
    public partial class ClientesAlterar : ContentPage
    {
        private readonly DatabaseService _databaseService;
        private readonly Cliente _cliente;

        public ClientesAlterar(DatabaseService databaseService, Cliente cliente)
        {
            InitializeComponent();
            _databaseService = databaseService;
            _cliente = cliente;

            EntryNome.Text = cliente.Nome;
            EntryCpf.Text = cliente.Cpf.ToString();
            EntryEmail.Text = cliente.Email;
            DatePickerCadastro.Date = cliente.DataCadastro;
        }

        private async void OnSalvarClicked(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(EntryNome.Text))
            {
                await DisplayAlert("Campo obrigatório", "Informe o nome do cliente.", "OK");
                return;
            }

            if (string.IsNullOrWhiteSpace(EntryCpf.Text) || !decimal.TryParse(EntryCpf.Text, out var cpf))
            {
                await DisplayAlert("Campo obrigatório", "Informe um CPF válido (somente números).", "OK");
                return;
            }

            _cliente.Nome = EntryNome.Text.Trim();
            _cliente.Cpf = cpf;
            _cliente.Email = EntryEmail.Text?.Trim() ?? string.Empty;
            _cliente.DataCadastro = DatePickerCadastro.Date;

            await _databaseService.SalvarClienteAsync(_cliente);
            await Navigation.PopAsync();
        }

        private async void OnVoltarClicked(object sender, EventArgs e)
        {
            await Navigation.PopAsync();
        }
    }
}
