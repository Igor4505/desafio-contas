using Conductor.Desafio.Core.Enums;
using Conductor.Desafio.Core.Models.Base;
using System;
using System.Collections.Generic;
using System.Text;

namespace Conductor.Desafio.Core.Models
{
    public class ContaModel : ModelBase
    {
        public decimal Saldo { get; set; }
        public decimal LimiteSaqueDiario { get; set; }
        public bool FlagAtivo { get; set; }
        public TipoContaEnum Tipo { get; set; }
        public string Descricao { get; set; }

        public int PessoaId { get; set; }
        public PessoaModel Pessoa { get; set; }

        public List<TransacaoModel> Transacoes { get; set; }

    }
}
