using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SistemaVeterinario.Models
{
    public class Cliente
    {
        // Chave Primária (cliid: int)
        public int Id { get; set; }

        // Nome do cliente (clinome: varchar 50)
        public string Nome { get; set; }

        // CPF do cliente (clicpf: decimal 11)
        public decimal Cpf { get; set; }

        // Email (cliemail: varchar 100)
        public string Email { get; set; }

        // Data de Cadastro (clidatacadastro: date)
        public DateTime DataCadastro { get; set; }
    }
}
