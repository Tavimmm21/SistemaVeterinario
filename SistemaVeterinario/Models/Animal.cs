using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SistemaVeterinario.Models
{
    public class Animal
    {
        // Chave Primária (anid: int)
        public int Id { get; set; }

        // Nome do animal (aninome: varchar 50)
        public string Nome { get; set; }

        // Apelido (aniapelido: varchar 25)
        public string Apelido { get; set; }

        // Data de nascimento (anidatanasc: date)
        public DateTime DataNascimento { get; set; }

        // Observações (anobservacoes: varchar 500)
        public string Observacoes { get; set; }

        // --- CHAVES ESTRANGEIRAS ---

        // FK da Espécie (espid: int)
        public int EspecieId { get; set; }

        // FK do Cliente/Dono (cliid: int)
        public int ClienteId { get; set; }
    }
}
