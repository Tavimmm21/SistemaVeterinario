using SistemaVeterinario.Models;

namespace SistemaVeterinario.Services
{
    public class AnimalRepositorio
    {
        private readonly DatabaseService _databaseService;

        public AnimalRepositorio(DatabaseService databaseService)
        {
            _databaseService = databaseService;
        }

        public async Task<List<Animal>> ListarAsync()
        {
            var conexao = await _databaseService.GetConnectionAsync();
            return await conexao.Table<Animal>().OrderBy(a => a.Nome).ToListAsync();
        }

        public async Task InserirAsync(Animal animal)
        {
            var conexao = await _databaseService.GetConnectionAsync();
            await conexao.InsertAsync(animal);
        }

        public async Task AlterarAsync(Animal animal)
        {
            var conexao = await _databaseService.GetConnectionAsync();
            await conexao.UpdateAsync(animal);
        }

        public async Task ExcluirAsync(Animal animal)
        {
            var conexao = await _databaseService.GetConnectionAsync();
            await conexao.DeleteAsync(animal);
        }
    }
}
