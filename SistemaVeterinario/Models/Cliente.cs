using SQLite;

namespace SistemaVeterinario.Models
{
    [Table("tblclientes")]
    public class Cliente
    {
        [PrimaryKey, AutoIncrement, Column("cliid")]
        public int Id { get; set; }

        [Column("clinome"), MaxLength(50), NotNull]
        public string Nome { get; set; } = string.Empty;

        [Column("clicpf")]
        public decimal Cpf { get; set; }

        [Column("cliemail"), MaxLength(100)]
        public string Email { get; set; } = string.Empty;

        [Column("clidatacadastro")]
        public DateTime DataCadastro { get; set; }
    }
}
