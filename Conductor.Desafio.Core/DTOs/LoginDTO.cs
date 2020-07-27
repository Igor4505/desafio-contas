using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Conductor.Desafio.Core.DTOs
{
    public class LoginDTO
    {
        [Required(ErrorMessage = "Este campo é obrigatório.")]
        [DataType(DataType.EmailAddress, ErrorMessage = "E-mail inválido.")]
        public string EmailLogin { get; set; }

        [Required(ErrorMessage = "Este campo é obrigatório.")]
        [MinLength(6, ErrorMessage = "O campo deve possuir 6 caracteres ou mais")]
        [DataType(DataType.Password)]
        public string SenhaLogin { get; set; }
    }
}
