using Conductor.Desafio.Core.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Conductor.Desafio.Core.QueryObjects
{
    public class PessoaQuery
    {
        public string PorNome { get; set; }
        public string PorCpf { get; set; }
        public string APartirDe { get; set; }
        public string NascimentoMinimo { get; set; }
        public string PorGenero { get; set; }
        public string PorEmail { get; set; }
        public string Ativo { get; set; } = "true";

        public string Filtrar()
        {
            //FILTROS
            List<string> Filtros = new List<string>();
            string sqlWhere = "SELECT * FROM Pessoas";

            //NOME
            if (!String.IsNullOrEmpty(PorNome))
            {
                List<string> Nomes = new List<string>();
                foreach (string nome in PorNome.Trim().Split())
                {
                    if (!String.IsNullOrEmpty(nome)) { Nomes.Add($"Nome + ' ' + Sobrenome LIKE '%{nome}%'"); }
                }
                Filtros.Add(String.Join(" OR ", Nomes));
            }
            //CPF
            if (!String.IsNullOrEmpty(PorCpf)) { Filtros.Add($"Cpf = '{PorCpf}'"); }
            //IDADE MINIMA
            if (!String.IsNullOrEmpty(NascimentoMinimo)) { Filtros.Add($"Nascimento >= '{NascimentoMinimo}'"); }
            //POR GÊNERO
            if (String.IsNullOrEmpty(PorGenero))
            {
                switch (PorGenero)
                {
                    case "Masc":
                        Filtros.Add($"Genero = {GeneroEnum.masculino.ToString("D")}");
                        break;
                    case "Fem":
                        Filtros.Add($"Genero = {GeneroEnum.feminino.ToString("D")}");
                        break;
                    default:
                        break;
                }
            }
            //POR EMAIL
            if (!String.IsNullOrEmpty(PorEmail)) { Filtros.Add($"Email = '{PorEmail}'"); }
            //ATIVO
            if (!String.IsNullOrEmpty(Ativo)) { Filtros.Add("Ativo = 1"); }

            //ADICIONAR WHERE
            if (Filtros.Any()) { sqlWhere += $" WHERE {String.Join(" AND ", Filtros)}"; }

            //RETORNAR O WHERE
            return sqlWhere;
        }

        public override string ToString()
        {
            return $"?PorNome={PorNome}&PorCpf={PorCpf}&NascimentoMinimo={NascimentoMinimo}&PorGenero={PorGenero}&PorEmail={PorEmail}";
        }
    }
}
