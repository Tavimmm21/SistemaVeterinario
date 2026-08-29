using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SistemaVeterinario.Models
{
    public class Especie
    {
        // Chave Primária (espid: int)
        public int Id { get; set; }

        // Nome da espécie (espnome: varchar 50)
        public string Nome { get; set; }
    }
}
