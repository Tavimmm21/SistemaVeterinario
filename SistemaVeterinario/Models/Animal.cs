using SQLite;

namespace SistemaVeterinario.Models
{
    [Table("tblanimais")]
    public class Animal
    {
        [PrimaryKey, AutoIncrement, Column("anid")]
        public int Id { get; set; }

        [Column("aninome"), MaxLength(50), NotNull]
        public string Nome { get; set; } = string.Empty;

        [Column("aniapelido"), MaxLength(25)]
        public string Apelido { get; set; } = string.Empty;

        [Column("anidatanasc")]
        public DateTime DataNascimento { get; set; }

        [Column("anobservacoes"), MaxLength(500)]
        public string Observacoes { get; set; } = string.Empty;

        // FK -> tblespecies (espid)
        [Column("espid")]
        public int EspecieId { get; set; }

        // FK -> tblclientes (cliid)
        [Column("cliid")]
        public int ClienteId { get; set; }
    }
}
