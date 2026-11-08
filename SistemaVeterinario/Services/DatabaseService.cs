using SistemaVeterinario.Models;
using SQLite;

namespace SistemaVeterinario.Services
{
    // Responsável apenas por abrir/manter a conexão com o banco e criar as tabelas.
    // As operações de cada entidade ficam nos repositórios específicos
    // (ClienteRepositorio, EspecieRepositorio, AnimalRepositorio).
    public class DatabaseService
    {
        private SQLiteAsyncConnection? _connection;

        public async Task<SQLiteAsyncConnection> GetConnectionAsync()
        {
            if (_connection is not null)
                return _connection;

            var dbPath = Path.Combine(FileSystem.AppDataDirectory, "sistemaveterinario.db3");
            _connection = new SQLiteAsyncConnection(dbPath);

            await _connection.CreateTableAsync<Cliente>();
            await _connection.CreateTableAsync<Especie>();
            await _connection.CreateTableAsync<Animal>();

            return _connection;
        }
    }
}
