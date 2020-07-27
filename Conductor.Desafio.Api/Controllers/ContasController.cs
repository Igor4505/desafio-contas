using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Conductor.Desafio.Core.DTOs;
using Conductor.Desafio.Core.Models;
using Conductor.Desafio.Database.Repository.Interfaces;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Conductor.Desafio.Api.Controllers
{
    [Produces("application/json")]
    [Route("api/Contas")]
    public class ContasController : Controller
    {
        private readonly IContasRepository _repository;
        public ContasController(IContasRepository repository)
        {
            _repository = repository;
        }

        //GET: PEGAR TODAS AS CONTAS
        //Enviar através da rota "true" para usuários ativos e "false" para usuário inativos
        //path: api/contas?ativo=true
        [HttpGet]
        public IActionResult GetContas(bool ativo = true)
        {
            try
            {
                IEnumerable<ContaModel> Contas = _repository.Get(ativo);
                if (Contas.Count() > 0)
                {
                    List<ContaDTO> ContaDTO = new List<ContaDTO>();
                    foreach (var conta in Contas)
                    {
                        ContaDTO.Add(new ContaDTO()
                        {
                            Id = conta.Id,
                            Descricao = conta.Descricao,
                            FlagAtivo = conta.FlagAtivo,
                            LimiteSaqueDiario = conta.LimiteSaqueDiario,
                            Saldo = conta.Saldo,
                            Tipo = conta.Tipo
                        });
                    }
                    return Ok(ContaDTO);
                }
                else
                {
                    return NotFound("Nenhuma conta encontrada.");
                }
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        //GET: GET CONTA POR ID
        //Enviar através da rota o Id da conta;
        //path: api/contas/2
        [HttpGet("{Id}")]
        public IActionResult GetContaById([FromRoute]int Id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest();
            }
            else
            {
                ContaModel conta = _repository.GetById(Id);
                if (conta != null)
                {
                    ContaDTO contaDTO = new ContaDTO()
                    {
                        Id = conta.Id,
                        Tipo = conta.Tipo,
                        Saldo = conta.Saldo,
                        LimiteSaqueDiario = conta.LimiteSaqueDiario,
                        Descricao = conta.Descricao,
                        FlagAtivo = conta.FlagAtivo,
                        IdPessoa = conta.PessoaId
                    };
                    return Ok(contaDTO);
                }
                else
                {
                    return NotFound("Conta não encontrada.");
                }
            }
        }

        //GET: PEGAR TODAS AS CONTAS PELO ID DA PESSOA
        //Enviar através da rota o Id da pessoa;
        //path: api/contas/pessoa/1
        [HttpGet("pessoa/{Id}")]
        public IActionResult GetContaByPessoaId([FromRoute]int Id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest();
            }
            else
            {
                IEnumerable<ContaModel> Contas = _repository.GetByPessoaId(Id);
                if (Contas.Count() > 0)
                {
                    List<ContaDTO> ContaDTO = new List<ContaDTO>();
                    foreach (var conta in Contas)
                    {
                        ContaDTO.Add(new ContaDTO()
                        {
                            Id = conta.Id,
                            Descricao = conta.Descricao,
                            FlagAtivo = conta.FlagAtivo,
                            LimiteSaqueDiario = conta.LimiteSaqueDiario,
                            Saldo = conta.Saldo,
                            Tipo = conta.Tipo
                        });
                    }
                    return Ok(ContaDTO);
                }
                else
                {
                    return NotFound("Nenhuma conta encontrada.");
                }
            }
        }

        //POST: CRIAR CONTA
        //Enviar pelo body uma instancia da classe ContaDTO
        //path: api/contas
        [HttpPost]
        public IActionResult PostConta([FromBody]ContaDTO conta)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            else
            {
                ContaModel contaModel = new ContaModel()
                {
                    Descricao = conta.Descricao,
                    FlagAtivo = true,
                    LimiteSaqueDiario = conta.LimiteSaqueDiario,
                    PessoaId = conta.IdPessoa,
                    Saldo = 0,
                    Tipo = conta.Tipo
                };
                try
                {
                    _repository.AddConta(contaModel);
                    string response = "Conta cadastrada com sucesso.";
                    return Created("GetConta", response);
                }
                catch (Exception ex)
                {
                    return BadRequest(ex.Message);
                }
            }

        }

        //PUT: EDITAR CONTA
        //Enviar pelo body uma instancia da classe ContaDTO
        //path: api/contas/1
        [HttpPut("{Id}")]
        public IActionResult EditConta([FromRoute]int Id, [FromBody]ContaDTO conta)
        {
            //SE OS IDS FOREM DIFERENTES RETORNAR NÃO ENCONTRADO
            if (Id != conta.Id)
            {
                return NotFound();
            }
            //SE AS INFORMAÇÕES NÃO FOREM VÁLIDAS RETORNAR BAD REQUEST
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            else
            {
                ContaModel getConta = _repository.GetById(Id);
                getConta.Descricao = conta.Descricao;
                getConta.LimiteSaqueDiario = conta.LimiteSaqueDiario;
                getConta.Tipo = conta.Tipo;
                try
                {
                    _repository.PutConta(getConta);
                    string response = "Conta editada com sucesso.";
                    return Ok(response);
                }
                catch (Exception ex)
                {

                    return BadRequest(ex.Message);
                }
            }
        }

        //DELETE: EXCLUIR CONTA
        //Enviar pela route o id da conta a ser excluida
        //path: api/contas/1
        [HttpDelete("{Id}")]
        public IActionResult DeleteConta([FromRoute] int Id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest();
            }
            else
            {
                ContaModel conta = _repository.GetById(Id);
                if (conta != null)
                {
                    try
                    {
                        _repository.DeleteConta(conta);
                        string response = "Conta excluida com sucesso.";
                        return Ok(response);
                    }
                    catch (Exception ex)
                    {
                        return BadRequest(ex.Message);
                    }
                }
                else
                {
                    return NotFound("Pessoa não encontrada");
                }
            }
        }

        /*====PATHS DESAFIO CONDUCTOR===*/

        //GET: VISUALIZAR SALDO DE CONTA
        //Enviar header id da conta a ser visualizada
        //path: api/contas/saldo/1
        [HttpGet("saldo/{Id}")]
        public IActionResult ConsultarSaldo([FromRoute] int Id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest();
            }
            else
            {
                ContaModel conta = _repository.GetById(Id);
                if (conta != null)
                {
                    
                    return Ok("Saldo atual da conta: "+ conta.Saldo);
                }
                else
                {
                    return NotFound("Conta não encontrada.");
                }
            }
        }

        //PUT: DESATIVAR CONTA
        //Enviar header id da conta a ser desativada
        //path: api/contas/desativar/1
        [HttpPut("desativar/{Id}")]
        public IActionResult DesativarConta([FromRoute] int Id)
        {
            //SE AS INFORMAÇÕES NÃO FOREM VÁLIDAS RETORNAR BAD REQUEST
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            else
            {
                ContaModel getConta = _repository.GetById(Id);
                getConta.FlagAtivo = false;
                try
                {
                    _repository.PutConta(getConta);
                    string response = "Conta desativada com sucesso.";
                    return Ok(response);
                }
                catch (Exception ex)
                {

                    return BadRequest(ex.Message);
                }
            }
        }

    }
}