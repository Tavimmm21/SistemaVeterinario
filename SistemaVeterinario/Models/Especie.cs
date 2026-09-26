using SQLite;

namespace SistemaVeterinario.Models
{
    [Table("tblespecies")]
    public class Especie
    {
        [PrimaryKey, AutoIncrement, Column("espid")]
        public int Id { get; set; }

        [Column("espnome"), MaxLength(50), NotNull]
        public string Nome { get; set; } = string.Empty;
    }
}
