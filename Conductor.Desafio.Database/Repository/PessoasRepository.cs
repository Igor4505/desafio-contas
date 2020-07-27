using Conductor.Desafio.Core.Models;
using Conductor.Desafio.Core.QueryObjects;
using Conductor.Desafio.Database.DataContext;
using Conductor.Desafio.Database.Repository.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.EntityFrameworkCore;

namespace Conductor.Desafio.Database.Repository
{
    public class PessoasRepository : IPessoasRepository
    {
        private readonly DesafioDbContext _context;
        public PessoasRepository(DesafioDbContext context)
        {
            _context = context;
        }

        //GET: PEGAR TODAS AS PESSOAS
        public IEnumerable<PessoaModel> Get(PessoaQuery pessoaQuery)
        {
            string where = pessoaQuery.Filtrar();
            IQueryable<PessoaModel> Pessoas = _context.Pessoas.FromSql(where);
            return Pessoas.ToList();
        }

        //GET: PEGAR PESSOA POR ID
        public PessoaModel GetById(int id)
        {
            PessoaModel pessoa = _context.Pessoas.SingleOrDefault(p => p.Id == id);
            return pessoa;
        }

        //POST: ADICIONAR UMA PESSOA
        public void AddPessoa(PessoaModel pessoa)
        {
            if (!pessoa.Senha.StartsWith("$2b$10$"))
            {
                string passToHash = "c0dVc10R" + pessoa.Senha + "^~++45";
                string salt = BCrypt.Net.BCrypt.GenerateSalt();
                string hash = BCrypt.Net.BCrypt.HashPassword(passToHash, salt);
                pessoa.Senha = hash;
            }

            using (var transaction = _context.Database.BeginTransaction())
            {
                try
                {
                    var checagemCpf = _context.Pessoas.SingleOrDefault(p => p.Cpf == pessoa.Cpf);
                    var checagemEmail = _context.Pessoas.SingleOrDefault(p => p.Email == pessoa.Email);
                    if (checagemCpf == null && checagemEmail == null)
                    {
                        _context.Pessoas.Add(pessoa);
                        _context.SaveChanges();
                        transaction.Commit();
                    }
                    else
                    {
                        string propriedade = checagemCpf != null ? "CPF" : "E-mail";
                        throw new Exception($"Funcionário com mesmo {propriedade} já cadastrado.");
                    }

                }
                catch (Exception ex)
                {
                    transaction.Rollback();
                    throw ex;
                }
            }
        }

        //DELETE: DELETAR UMA PESSOA
        public void DeletePessoa(PessoaModel pessoa)
        {
            try
            {
                var contas = _context.Contas.Where(p => p.PessoaId == pessoa.Id);
                if (contas.Count() == 0)
                {
                    _context.Pessoas.Remove(pessoa);
                    _context.SaveChanges();
                }
                else
                {
                    throw new Exception("A pessoa possui contas ativas, primeiro encerre as contas para excluir o perfil!");
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        //PUT: EDITAR PESSOA
        public void PutPessoa(PessoaModel pessoa)
        {
            using (var transaction = _context.Database.BeginTransaction())
            {
                try
                {
                    _context.Pessoas.Update(pessoa);
                    _context.SaveChanges();
                    transaction.Commit();
                }
                catch (Exception ex)
                {
                    transaction.Rollback();
                    throw ex;
                }
            }
        }

        //GET: CHEGAR CREDENCIAIS
        public PessoaModel CheckPessoa(string email, string senha)
        {
            var pessoaCheck = _context.Pessoas.Where(e => e.Email == email && e.Ativo == true).SingleOrDefault();
            if (pessoaCheck == null)
            {
                throw new Exception("O e-mail ou a senha estão incorretas.");
            }
            else
            {
                string senhaSalt = "c0dVc10R" + senha + "^~++45";
                string hashUsuario = pessoaCheck.Senha;

                bool resultado = BCrypt.Net.BCrypt.Verify(senhaSalt, hashUsuario);
                if (!resultado)
                {
                    throw new Exception("O e-mail ou a senha estão incorretas.");
                }
                else
                {
                    return pessoaCheck;
                }
            }
        }
    }
}
