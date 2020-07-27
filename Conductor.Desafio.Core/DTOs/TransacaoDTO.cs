using Conductor.Desafio.Core.Enums;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Conductor.Desafio.Core.DTOs
{
    public class TransacaoDTO
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "Este campo é obrigatório.")]
        public TipoTransacaoEnum TipoTransacao { get; set; }

        [Required(ErrorMessage = "Este campo é obrigatório.")]
        [DataType(DataType.Currency)]
        [Range(0.1, Double.PositiveInfinity, ErrorMessage = "Este campo precisa ser maior que R$0.10")]
        public decimal Valor { get; set; }

        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "dd/mm/yyyy")]
        public DateTime DataTransacao { get; set; }

        [Required(ErrorMessage = "Este campo é obrigatório.")]
        public string Descricao { get; set; }

        [Required(ErrorMessage = "Este campo é obrigatório.")]
        public int ContaId { get; set; }

        public string ContaDescricao { get; set; }
    }
}
