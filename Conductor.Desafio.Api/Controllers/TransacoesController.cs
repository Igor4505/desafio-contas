using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Conductor.Desafio.Core.DTOs;
using Conductor.Desafio.Core.Models;
using Conductor.Desafio.Core.QueryObjects;
using Conductor.Desafio.Database.Repository.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Conductor.Desafio.Api.Controllers
{
    [Produces("application/json")]
    [Route("api/Transacoes")]
    public class TransacoesController : Controller
    {
        private readonly ITransacoesRepository _repository;
        public TransacoesController(ITransacoesRepository repository)
        {
            _repository = repository;
        }

        //GET: PEGAR TODAS AS TRANSAÇÕES
        //Enviar pela rota o método .ToString() da instancia de um objeto TransacoesQuery para filtragem de resultado
        //path: api/transacoes?PorConta=&PorTipo=&DataMinima=&DataMaxima=";
        [HttpGet]
        public IActionResult GetTransacoes(TransacoesQuery query)
        {
            query = query ?? new TransacoesQuery();
            try
            {
                IEnumerable<TransacaoModel> Transacoes = _repository.Get(query);
                if (Transacoes.Count() > 0)
                {
                    List<TransacaoDTO> TransacoesDTO = new List<TransacaoDTO>();
                    foreach (var transacao in Transacoes)
                    {
                        TransacoesDTO.Add(new TransacaoDTO()
                        {
                            Id = transacao.Id,
                           ContaId = transacao.ContaId,
                           DataTransacao = transacao.DataTransacao,
                           Descricao = transacao.Descricao,
                           TipoTransacao = transacao.TipoTransacao,
                           Valor = transacao.Valor,
                           ContaDescricao = transacao.Conta.Descricao
                        });
                    }
                    return Ok(TransacoesDTO);
                }
                else
                {
                    return NotFound("Nenhuma transacao encontrada.");
                }
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        //GET: PEGAR TRANSACAO POR ID DA CONTA
        //Enviar pela rota o Id da conta
        //path: api/transacoes/conta/1
        [HttpGet("conta/{Id}")]
        public IActionResult GetTransacaoByContaId([FromRoute]int Id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest();
            }
            else
            {
                TransacoesQuery transacoesQuery = new TransacoesQuery()
                {
                    PorConta = Id.ToString()
                };
                try
                {
                    IEnumerable<TransacaoModel> Transacoes = _repository.Get(transacoesQuery);
                    if (Transacoes.Count() > 0)
                    {
                        List<TransacaoDTO> TransacoesDTO = new List<TransacaoDTO>();
                        foreach (var transacao in Transacoes)
                        {
                            TransacoesDTO.Add(new TransacaoDTO()
                            {
                                Id = transacao.Id,
                                ContaId = transacao.ContaId,
                                DataTransacao = transacao.DataTransacao,
                                Descricao = transacao.Descricao,
                                TipoTransacao = transacao.TipoTransacao,
                                Valor = transacao.Valor,
                                ContaDescricao = transacao.Conta.Descricao
                            });
                        }
                        return Ok(TransacoesDTO);
                    }
                    else
                    {
                        return NotFound("Nenhuma transacao encontrada.");
                    }
                }
                catch (Exception ex)
                {
                    return BadRequest(ex.Message);
                }
            }
        }

        //GET: PEGAR TRANSACAO POR ID
        //Enviar pela rota o Id da transação
        //path: api/transacoes/1
        [HttpGet("{Id}")]
        public IActionResult GetTransacaoById([FromRoute]int Id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest();
            }
            else
            {
                TransacaoModel transacao = _repository.GetById(Id);
                if (transacao != null)
                {
                    TransacaoDTO transacaoDTO = new TransacaoDTO()
                    {
                        Id = transacao.Id,
                       DataTransacao = transacao.DataTransacao,
                       ContaId = transacao.ContaId,
                       Valor = transacao.Valor,
                       TipoTransacao = transacao.TipoTransacao,
                       Descricao = transacao.Descricao,
                       ContaDescricao = transacao.Conta.Descricao
                       
                    };
                    return Ok(transacaoDTO);
                }
                else
                {
                    return NotFound("Transação não encontrada.");
                }
            }
        }

        //POST: ADICIONAR TRANSAÇÃO
        //Enviar pelo body uma instancia da classe TransacaoDTO
        //path: api/transacoes/1
        [HttpPost]
        public IActionResult PostTransacao([FromBody]TransacaoDTO transacao)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            else
            {
                TransacaoModel transacaoModel = new TransacaoModel()
                {
                    ContaId = transacao.ContaId,
                    Descricao = transacao.Descricao,
                    TipoTransacao = transacao.TipoTransacao,
                    Valor = transacao.Valor
                };
               
                try
                {
                    _repository.AddTransacao(transacaoModel);
                    string response = "Transação realizada com sucesso.";
                    return Created("GetTransacao", response);
                }
                catch (Exception ex)
                {
                    return BadRequest(ex.Message);
                }
            }

        }

        //DELETE: DELETAR TRANSAÇÃO
        //Enviar pela route o id da transacao a ser excluida
        //path: api/transacoes/1
        [HttpDelete("{Id}")]
        public IActionResult DeletePessoa([FromRoute] int Id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest();
            }
            else
            {
                TransacaoModel transacao = _repository.GetById(Id);
                if (transacao != null)
                {
                    try
                    {
                        _repository.DeleteTransacao(transacao);
                        string response = "Transação excluida com sucesso.";
                        return Ok(response);
                    }
                    catch (Exception ex)
                    {
                        return BadRequest(ex.Message);
                    }
                }
                else
                {
                    return NotFound("Transação não encontrada");
                }
            }
        }
    }
}