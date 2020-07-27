using Conductor.Desafio.Core.DTOs;
using Conductor.Desafio.Core.QueryObjects;
using Conductor.Desafio.Database.Services;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Conductor.Desafio.Database.ViewModels
{
    public class TransacoesViewModel
    {
        public TransacaoDTO Transacao { get; set; }
        public List<TransacaoDTO> Transacoes { get; set; }
        public TransacoesQuery Filtros { get; set; }
        public List<ContaDTO> Contas { get; set; }
        public ContaDTO Conta { get; set; }

        public TransacoesViewModel()
        {
            Conta = new ContaDTO();
            Transacao = new TransacaoDTO();
            Contas = new List<ContaDTO>();
            Transacoes = new List<TransacaoDTO>();
        }

        public async Task GetContas(int idPessoa)
        {
            try
            {
                ContaService contaServide = new ContaService();
                Contas = await contaServide.Get(new ContaQuery() { IdPessoa = idPessoa });

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task GetTransacoes()
        {
            try
            {
                TransacoesService service = new TransacoesService();
                Transacoes = await service.Get(Filtros);
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }

        public async Task GetConta(int idConta)
        {
            try
            {
                ContaService service = new ContaService();
                Conta = await service.GetSingle(idConta);
            }
            catch (Exception)
            {

                throw;
            }
        }
    }
}
