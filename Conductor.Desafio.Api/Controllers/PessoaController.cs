using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
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
    [Route("api/Pessoas")]
    public class PessoaController : Controller
    {
        private readonly IPessoasRepository _repository;

        public PessoaController(IPessoasRepository repository)
        {
            _repository = repository;
        }

        //GET: PEGAR TODAS AS PESSOAS
        //Enviar o metodo .ToString() da instancia de um objeto Pesssoa Query pelo path para filtrar resultado
        //path: api/pessoas?PorNome=&PorCpf=&NascimentoMinimo=&PorGenero=&PorEmail=
        [HttpGet]
        public IActionResult GetPessoas(PessoaQuery query)
        {
            query = query ?? new PessoaQuery();
            try
            {
                IEnumerable<PessoaModel> Pessoas = _repository.Get(query);
                if(Pessoas.Count() > 0)
                {
                    List<PessoaDTO> PessoaDTO = new List<PessoaDTO>();
                    foreach (var pessoa in Pessoas)
                    {
                        PessoaDTO.Add(new PessoaDTO() {
                            Cpf = pessoa.Cpf,
                            Email = pessoa.Email,
                            Genero = pessoa.Genero,
                            Nascimento = pessoa.Nascimento,
                            Id = pessoa.Id,
                            Nome = pessoa.Nome,
                            Sobrenome = pessoa.Sobrenome
                        });
                    }
                    return Ok(PessoaDTO);
                }
                else
                {
                    return NotFound("Nenhuma pessoa encontrada.");
                }
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        //GET: PEGAR PESSOA POR ID
        //Enviar pela rota o Id da pessoa
        //path: api/pessoas/1
        [HttpGet("{Id}")]
        public IActionResult GetPessoaById([FromRoute]int Id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest();
            }
            else
            {
                PessoaModel pessoa = _repository.GetById(Id);
                if(pessoa != null)
                {
                    PessoaDTO pessoaDTO = new PessoaDTO()
                    {
                        Cpf = pessoa.Cpf,
                        Email = pessoa.Email,
                        Genero = pessoa.Genero,
                        Nascimento = pessoa.Nascimento,
                        Id = pessoa.Id,
                        Nome = pessoa.Nome,
                        Sobrenome = pessoa.Sobrenome
                    };
                    return Ok(pessoaDTO);
                }
                else
                {
                    return NotFound("Pessoa não encontrada.");
                }
            }
        }

        //POST: ADICIONAR PESSOA
        //Enviar pelo body a instancia do objeto PessoaDTO
        //path: api/pessoas
        [HttpPost]
        public IActionResult PostPessoa([FromBody]PessoaDTO pessoa)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            else
            {
                PessoaModel pessoaModel = new PessoaModel()
                {
                    Cpf = pessoa.Cpf,
                    Email = pessoa.Email,
                    Genero = pessoa.Genero,
                    Nascimento = pessoa.Nascimento,
                    Nome = pessoa.Nome,
                    Senha = pessoa.Senha,
                    Sobrenome = pessoa.Sobrenome
                };
                try
                {
                    _repository.AddPessoa(pessoaModel);
                    string response = "Pessoa cadastrada com sucesso.";
                    return Created("GetPessoa", response);
                    //return CreatedAtAction("GetPessoa", new { id = pessoaModel.Id }, response);
                }
                catch (Exception ex)
                {

                    return BadRequest(ex.Message);
                }
            }

        }

        //POST: CHECAR CREDENCIAIS PESSOA
        //Enviar pelo body a instancia do objeto LoginDTO
        //path: api/pessoas/check
        [HttpPost("check")]
        public IActionResult CheckPessoa([FromBody] LoginDTO pessoa)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            else
            {
                try
                {
                    var pessoaQuery = _repository.CheckPessoa(pessoa.EmailLogin, pessoa.SenhaLogin);
                    PessoaDTO pessoaDTO = new PessoaDTO()
                    {
                        Id = pessoaQuery.Id,
                        Nome = pessoaQuery.Nome,
                        Sobrenome = pessoaQuery.Sobrenome,
                        Genero = pessoaQuery.Genero,
                        Email = pessoaQuery.Email,
                    };
                    return Accepted(pessoaDTO);
                }
                catch (Exception ex)
                {
                    return StatusCode(401,ex.Message);
                }
            }
        }

        //PUT: EDITAR PESSOA
        //Enviar pelo header a Id da Pessoa a ser editada e uma instancia da classe ContaDTO pelo body
        //path: api/pessoas/1
        [HttpPut("{Id}")]
        public IActionResult EditPessoa([FromRoute]int Id, [FromBody]PessoaDTO pessoa)
        {
            //SE OS IDS FOREM DIFERENTES RETORNAR NÃO ENCONTRADO
            if(Id != pessoa.Id)
            {
                return NotFound();
            }
            //SE AS INFORMAÇÕES NÃO FOREM VÁLIDAS RETORNAR BAD REQUEST
            if(pessoa.Senha != pessoa.ConfSenha)
            {
                ModelState.AddModelError("pessoa.Senha", "As senhas não conferem!");
            }
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            else
            {
                PessoaModel getPessoa = _repository.GetById(Id);
                getPessoa.Id = pessoa.Id.Value;
                getPessoa.Ativo = true;
                getPessoa.Cpf = pessoa.Cpf;
                getPessoa.Email = pessoa.Email;
                getPessoa.Genero = pessoa.Genero;
                getPessoa.Nascimento = pessoa.Nascimento;
                getPessoa.Nome = pessoa.Nome;
                getPessoa.Senha = pessoa.Senha == null ? getPessoa.Senha : pessoa.Senha;
                getPessoa.Sobrenome = pessoa.Sobrenome;
                try
                {
                    _repository.PutPessoa(getPessoa);
                    string response = "Pessoa editada com sucesso.";
                    return Ok(response);
                }
                catch (Exception ex)
                {

                    return BadRequest(ex.Message);
                }
            }
        }

        //DELETE: DELETAR PESSOA
        //Enviar pela rota a Id da Pessoa a ser deletada
        //path: api/pessoas/1
        [HttpDelete("{Id}")]
        public IActionResult DeletePessoa([FromRoute] int Id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest();
            }
            else
            {
                PessoaModel pessoa = _repository.GetById(Id);
                if(pessoa != null)
                {
                    try
                    {
                        _repository.DeletePessoa(pessoa);
                        string response = "Pessoa excluida com sucesso.";
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
    }
}