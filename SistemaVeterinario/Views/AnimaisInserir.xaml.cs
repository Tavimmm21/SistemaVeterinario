using SistemaVeterinario.Models;
using SistemaVeterinario.Services;

namespace SistemaVeterinario.Views
{
    public partial class AnimaisInserir : ContentPage
    {
        private readonly DatabaseService _databaseService;
        private readonly List<Cliente> _clientes;
        private readonly List<Especie> _especies;

        public AnimaisInserir(DatabaseService databaseService, List<Cliente> clientes, List<Especie> especies)
        {
            InitializeComponent();
            _databaseService = databaseService;
            _clientes = clientes;
            _especies = especies;

            DatePickerNascimento.Date = DateTime.Today;
            PickerEspecie.ItemsSource = _especies.Select(esp => esp.Nome).ToList();
            PickerCliente.ItemsSource = _clientes.Select(c => c.Nome).ToList();
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

            var animal = new Animal
            {
                Nome = EntryNome.Text.Trim(),
                Apelido = EntryApelido.Text?.Trim() ?? string.Empty,
                DataNascimento = DatePickerNascimento.Date,
                Observacoes = EditorObservacoes.Text?.Trim() ?? string.Empty,
                EspecieId = _especies[PickerEspecie.SelectedIndex].Id,
                ClienteId = _clientes[PickerCliente.SelectedIndex].Id
            };

            await _databaseService.SalvarAnimalAsync(animal);
            await Navigation.PopAsync();
        }

        private async void OnVoltarClicked(object sender, EventArgs e)
        {
            await Navigation.PopAsync();
        }
    }
}
