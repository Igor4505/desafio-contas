using Conductor.Desafio.Core.Enums;
using Conductor.Desafio.Core.Models.Base;
using System;
using System.Collections.Generic;
using System.Text;

namespace Conductor.Desafio.Core.Models
{
    public class PessoaModel : ModelBase
    {
        public string Nome { get; set; }
        public string Sobrenome { get; set; }
        public string Cpf { get; set; }
        public DateTime Nascimento { get; set; }
        public GeneroEnum Genero { get; set; }
        public string Email { get; set; }
        public string Senha { get; set; }
        public bool? Ativo { get; set; }

        public List<ContaModel> Contas { get; set; }
    }
}
