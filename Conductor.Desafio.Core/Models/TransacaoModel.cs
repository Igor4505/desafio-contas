using Conductor.Desafio.Core.Enums;
using Conductor.Desafio.Core.Models.Base;
using System;
using System.Collections.Generic;
using System.Text;

namespace Conductor.Desafio.Core.Models
{
    public class TransacaoModel : ModelBase
    {
        public TipoTransacaoEnum TipoTransacao { get; set; }
        public decimal Valor { get; set; }
        public DateTime DataTransacao { get; set; }
        public string Descricao { get; set; }

        public ContaModel Conta { get; set; }
        public int ContaId { get; set; }
    }
}
