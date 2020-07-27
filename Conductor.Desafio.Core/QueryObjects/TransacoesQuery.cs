using Conductor.Desafio.Core.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Conductor.Desafio.Core.QueryObjects
{
    public class TransacoesQuery
    {
        public int PorPessoaId { get; set; }
        public string PorConta { get; set; }
        public string PorTipo { get; set; }
        public string DataMinima { get; set; }
        public string DataMaxima { get; set; }

        public string Filtrar()
        {
            //FILTROS
            List<string> Filtros = new List<string>();
            string sqlWhere = "SELECT * FROM Transacoes";

            if(PorPessoaId != 0) { }

            //POR CONTA
            if (!String.IsNullOrEmpty(PorConta)) { Filtros.Add($"ContaId = '{PorConta}'"); }
           
            //POR TIPO
            if (!String.IsNullOrEmpty(PorTipo))
            {
                switch (PorTipo)
                {
                    case "Deposito":
                        Filtros.Add($"TipoTransacao = {TipoTransacaoEnum.Deposito.ToString("D")}");
                        break;
                    case "Saque":
                        Filtros.Add($"TipoTransacao = {TipoTransacaoEnum.Saque.ToString("D")}");
                        break;
                    default:
                        break;
                }
            }

            //DATA MINIMA
            if (!String.IsNullOrEmpty(DataMinima)) { Filtros.Add($"cast ([DataTransacao] as date) >= '{DataMinima}'"); }

            //DATA MÁXIMA
            if (!String.IsNullOrEmpty(DataMaxima)) { Filtros.Add($"cast ([DataTransacao] as date) <= '{DataMaxima}'"); }

            //ADICIONAR WHERE
            if (Filtros.Any()) { sqlWhere += $" WHERE {String.Join(" AND ", Filtros)}"; }

            //RETORNAR O WHERE
            return sqlWhere;
        }

        public override string ToString()
        {
            return $"?PorPessoaId={PorPessoaId}&PorConta={PorConta}&PorTipo={PorTipo}&DataMinima={DataMinima}&DataMaxima={DataMaxima}";
        }
    }
}
