using SistemaVeterinario.Models;

namespace SistemaVeterinario.Services
{
    public class EspecieRepositorio
    {
        private readonly DatabaseService _databaseService;

        public EspecieRepositorio(DatabaseService databaseService)
        {
            _databaseService = databaseService;
        }

        public async Task<List<Especie>> ListarAsync()
        {
            var conexao = await _databaseService.GetConnectionAsync();
            return await conexao.Table<Especie>().OrderBy(e => e.Nome).ToListAsync();
        }

        public async Task InserirAsync(Especie especie)
        {
            var conexao = await _databaseService.GetConnectionAsync();
            await conexao.InsertAsync(especie);
        }

        public async Task AlterarAsync(Especie especie)
        {
            var conexao = await _databaseService.GetConnectionAsync();
            await conexao.UpdateAsync(especie);
        }

        public async Task ExcluirAsync(Especie especie)
        {
            var conexao = await _databaseService.GetConnectionAsync();
            await conexao.DeleteAsync(especie);
        }
    }
}
