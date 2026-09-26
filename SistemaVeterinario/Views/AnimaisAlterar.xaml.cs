using SistemaVeterinario.Models;
using SistemaVeterinario.Services;

namespace SistemaVeterinario.Views
{
    public partial class AnimaisAlterar : ContentPage
    {
        private readonly DatabaseService _databaseService;
        private readonly Animal _animal;
        private readonly List<Cliente> _clientes;
        private readonly List<Especie> _especies;

        public AnimaisAlterar(DatabaseService databaseService, Animal animal, List<Cliente> clientes, List<Especie> especies)
        {
            InitializeComponent();
            _databaseService = databaseService;
            _animal = animal;
            _clientes = clientes;
            _especies = especies;

            EntryNome.Text = animal.Nome;
            EntryApelido.Text = animal.Apelido;
            DatePickerNascimento.Date = animal.DataNascimento;
            EditorObservacoes.Text = animal.Observacoes;

            PickerEspecie.ItemsSource = _especies.Select(esp => esp.Nome).ToList();
            PickerCliente.ItemsSource = _clientes.Select(c => c.Nome).ToList();

            PickerEspecie.SelectedIndex = _especies.FindIndex(esp => esp.Id == animal.EspecieId);
            PickerCliente.SelectedIndex = _clientes.FindIndex(c => c.Id == animal.ClienteId);
        }

        private async void OnSalvarClicked(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(EntryNome.Text))
            {
                await DisplayAlert("Campo obrigatório", "Informe o nome do animal.", "OK");
                return;
            }

            if (PickerEspecie.SelectedIndex < 0)
            {
                await DisplayAlert("Campo obrigatório", "Selecione a espécie do animal.", "OK");
                return;
            }

            if (PickerCliente.SelectedIndex < 0)
            {
                await DisplayAlert("Campo obrigatório", "Selecione o dono (cliente) do animal.", "OK");
                return;
            }

            _animal.Nome = EntryNome.Text.Trim();
            _animal.Apelido = EntryApelido.Text?.Trim() ?? string.Empty;
            _animal.DataNascimento = DatePickerNascimento.Date;
            _animal.Observacoes = EditorObservacoes.Text?.Trim() ?? string.Empty;
            _animal.EspecieId = _especies[PickerEspecie.SelectedIndex].Id;
            _animal.ClienteId = _clientes[PickerCliente.SelectedIndex].Id;

            await _databaseService.SalvarAnimalAsync(_animal);
            await Navigation.PopAsync();
        }

        private async void OnVoltarClicked(object sender, EventArgs e)
        {
            await Navigation.PopAsync();
        }
    }
}
