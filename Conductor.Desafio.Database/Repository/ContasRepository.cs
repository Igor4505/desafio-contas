using Conductor.Desafio.Core.Models;
using Conductor.Desafio.Database.DataContext;
using Conductor.Desafio.Database.Repository.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Conductor.Desafio.Database.Repository
{
    public class ContasRepository : IContasRepository
    {
        private readonly DesafioDbContext _context;
        public ContasRepository(DesafioDbContext context)
        {
            _context = context;
        }

        //POST: ADICIONAR CONTA
        public void AddConta(ContaModel Conta)
        {
            using (var transaction = _context.Database.BeginTransaction())
            {
                try
                {
                    _context.Contas.Add(Conta);
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

        //DELETE: DELETAR CONTA
        public void DeleteConta(ContaModel Conta)
        {
            try
            {
                _context.Contas.Remove(Conta);
                _context.SaveChanges();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        //GET: PEGAR TODAS AS CONTAS
        public IEnumerable<ContaModel> Get(bool ativo = true)
        {
            List<ContaModel> Contas = _context.Contas.Where(c => c.FlagAtivo == ativo).ToList();
            return Contas;
        }

        //GET: PEGAR CONTA PELO ID DO AUTOR
        public IEnumerable<ContaModel> GetByPessoaId(int id)
        {
            List<ContaModel> Contas = _context.Contas.Where(c => c.PessoaId == id).ToList();
            return Contas;
        }

        //GET: PEGAR CONTA PELO ID DA CONTA
        public ContaModel GetById(int id)
        {
            ContaModel conta = _context.Contas.Find(id);
            return conta;
        }

        //PUT: EDITAR CONTA
        public void PutConta(ContaModel Conta)
        {
            using (var transaction = _context.Database.BeginTransaction())
            {
                try
                {
                    _context.Contas.Update(Conta);
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
    }
}
