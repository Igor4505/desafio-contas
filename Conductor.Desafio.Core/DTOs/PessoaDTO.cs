using Conductor.Desafio.Core.Enums;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Conductor.Desafio.Core.DTOs
{
    public class PessoaDTO
    {
        public int? Id { get; set; }

        [Required(ErrorMessage = "Este campo é obrigatório.")]
        [RegularExpression(@"^\S*$", ErrorMessage ="O campo não pode conter espaços")]
        public string Nome { get; set; }

        [Required(ErrorMessage = "Este campo é obrigatório.")]
        [RegularExpression(@"^\S*$", ErrorMessage = "O campo não pode conter espaços")]
        public string Sobrenome { get; set; }

        public string NomeCompleto {
            get { return Nome + " " + Sobrenome; }
        }

        [Required(ErrorMessage = "Este campo é obrigatório.")]
        [RegularExpression(@"[0-9]{3}\.?[0-9]{3}\.?[0-9]{3}\-?[0-9]{2}|[0-9]{2}\.?[0-9]{3}\.?[0-9]{3}\/?[0-9]{4}\-?[0-9]{2}", ErrorMessage ="CPF inválido.")]
        public string Cpf { get; set; }

        [Required(ErrorMessage = "Este campo é obrigatório.")]
        [DataType(DataType.EmailAddress, ErrorMessage = "E-mail inválido.")]
        public string Email { get; set; }

        [MinLength(6, ErrorMessage = "O campo deve possuir 6 caracteres ou mais")]
        [DataType(DataType.Password)]
        public string Senha { get; set; }

        [MinLength(6, ErrorMessage = "O campo deve possuir 6 caracteres ou mais")]
        [DataType(DataType.Password)]
        [Compare("Senha", ErrorMessage = "As senhas não conferem")]
        public string ConfSenha { get; set; }

        [Required(ErrorMessage = "Este campo é obrigatório.")]
        [DisplayFormat(DataFormatString ="dd/mm/yyyy")]
        [DataType(DataType.Date)]
        public DateTime Nascimento { get; set; }

        public int Idade {
            get { return DateTime.Now.Year - Nascimento.Year; }
        }

        [Required]
        public GeneroEnum Genero { get; set; }
    }

 
}
