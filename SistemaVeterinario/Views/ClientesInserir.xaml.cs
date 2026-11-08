using SistemaVeterinario.Models;
using SistemaVeterinario.Services;

namespace SistemaVeterinario.Views
{
    public partial class ClientesInserir : ContentPage
    {
        private readonly ClienteRepositorio _clienteRepositorio;

        public ClientesInserir(ClienteRepositorio clienteRepositorio)
        {
            InitializeComponent();
            _clienteRepositorio = clienteRepositorio;
            DatePickerCadastro.Date = DateTime.Today;
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

            var cliente = new Cliente
            {
                Nome = EntryNome.Text.Trim(),
                Cpf = cpf,
                Email = EntryEmail.Text?.Trim() ?? string.Empty,
                DataCadastro = DatePickerCadastro.Date
            };

            await _clienteRepositorio.InserirAsync(cliente);
            await Navigation.PopAsync();
        }

        private async void OnVoltarClicked(object sender, EventArgs e)
        {
            await Navigation.PopAsync();
        }
    }
}
