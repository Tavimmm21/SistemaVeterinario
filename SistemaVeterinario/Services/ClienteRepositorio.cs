using SistemaVeterinario.Models;

namespace SistemaVeterinario.Services
{
    public class ClienteRepositorio
    {
        private readonly DatabaseService _databaseService;

        public ClienteRepositorio(DatabaseService databaseService)
        {
            _databaseService = databaseService;
        }

        public async Task<List<Cliente>> ListarAsync()
        {
            var conexao = await _databaseService.GetConnectionAsync();
            return await conexao.Table<Cliente>().OrderBy(c => c.Nome).ToListAsync();
        }

        public async Task InserirAsync(Cliente cliente)
        {
            var conexao = await _databaseService.GetConnectionAsync();
            await conexao.InsertAsync(cliente);
        }

        public async Task AlterarAsync(Cliente cliente)
        {
            var conexao = await _databaseService.GetConnectionAsync();
            await conexao.UpdateAsync(cliente);
        }

        public async Task ExcluirAsync(Cliente cliente)
        {
            var conexao = await _databaseService.GetConnectionAsync();
            await conexao.DeleteAsync(cliente);
        }
    }
}
