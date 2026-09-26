using SistemaVeterinario.Models;
using SQLite;

namespace SistemaVeterinario.Services
{
    public class DatabaseService
    {
        private SQLiteAsyncConnection? _connection;

        private async Task InitAsync()
        {
            if (_connection is not null)
                return;

            var dbPath = Path.Combine(FileSystem.AppDataDirectory, "sistemaveterinario.db3");
            _connection = new SQLiteAsyncConnection(dbPath);

            await _connection.CreateTableAsync<Cliente>();
            await _connection.CreateTableAsync<Especie>();
            await _connection.CreateTableAsync<Animal>();
        }

        // --- CLIENTES ---

        public async Task<List<Cliente>> GetClientesAsync()
        {
            await InitAsync();
            return await _connection!.Table<Cliente>().OrderBy(c => c.Nome).ToListAsync();
        }

        public async Task<int> SalvarClienteAsync(Cliente cliente)
        {
            await InitAsync();
            return cliente.Id == 0
                ? await _connection!.InsertAsync(cliente)
                : await _connection!.UpdateAsync(cliente);
        }

        public async Task<int> ExcluirClienteAsync(Cliente cliente)
        {
            await InitAsync();
            return await _connection!.DeleteAsync(cliente);
        }

        // --- ESPÉCIES ---

        public async Task<List<Especie>> GetEspeciesAsync()
        {
            await InitAsync();
            return await _connection!.Table<Especie>().OrderBy(e => e.Nome).ToListAsync();
        }

        public async Task<int> SalvarEspecieAsync(Especie especie)
        {
            await InitAsync();
            return especie.Id == 0
                ? await _connection!.InsertAsync(especie)
                : await _connection!.UpdateAsync(especie);
        }

        public async Task<int> ExcluirEspecieAsync(Especie especie)
        {
            await InitAsync();
            return await _connection!.DeleteAsync(especie);
        }

        // --- ANIMAIS ---

        public async Task<List<Animal>> GetAnimaisAsync()
        {
            await InitAsync();
            return await _connection!.Table<Animal>().OrderBy(a => a.Nome).ToListAsync();
        }

        public async Task<int> SalvarAnimalAsync(Animal animal)
        {
            await InitAsync();
            return animal.Id == 0
                ? await _connection!.InsertAsync(animal)
                : await _connection!.UpdateAsync(animal);
        }

        public async Task<int> ExcluirAnimalAsync(Animal animal)
        {
            await InitAsync();
            return await _connection!.DeleteAsync(animal);
        }
    }
}
