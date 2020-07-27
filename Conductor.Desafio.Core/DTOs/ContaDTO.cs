using Conductor.Desafio.Core.Enums;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Conductor.Desafio.Core.DTOs
{
    public class ContaDTO
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "Este campo é obrigatório.")]
        [DataType(DataType.Currency)]
        public decimal Saldo { get; set; }

        [Required(ErrorMessage = "Este campo é obrigatório.")]
        [DataType(DataType.Currency)]
        [Range(0.1, Double.PositiveInfinity, ErrorMessage ="Este campo precisa ser maior que R$0.10")]
        public decimal LimiteSaqueDiario { get; set; }

        [Required(ErrorMessage = "Este campo é obrigatório.")]
        public bool FlagAtivo { get; set; }

        [Required(ErrorMessage = "Este campo é obrigatório.")]
        public TipoContaEnum Tipo { get; set; }

        [Required(ErrorMessage = "Este campo é obrigatório.")]
        public string Descricao { get; set; }

        [Required(ErrorMessage = "Este campo é obrigatório.")]
        public int IdPessoa { get; set; }
    }
}
