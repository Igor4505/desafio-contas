using Conductor.Desafio.Core.Models;
using Conductor.Desafio.Core.QueryObjects;
using Conductor.Desafio.Database.Repository.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.EntityFrameworkCore;
using Conductor.Desafio.Database.DataContext;
using System.Linq;
using Conductor.Desafio.Core.Enums;

namespace Conductor.Desafio.Database.Repository
{
   
    public class TransacoesRepository : ITransacoesRepository
    {
        private readonly DesafioDbContext _context;
        public TransacoesRepository(DesafioDbContext context)
        {
            _context = context;
        }

        //POST: ADICIONAR TRANSAÇÃO
        public void AddTransacao(TransacaoModel transacao)
        {
            //COMEÇAR A TRANSAÇÃO
            using(var transaction = _context.Database.BeginTransaction())
            {
                try
                {
                    //PEGAR A CONTA DE DESTINO DA TRANSAÇÃO
                    var conta = _context.Contas.Find(transacao.ContaId);
                    //SE A CONTA EXISTIR
                    if(conta != null)
                    {
                        //SE FOR UMA TRANSAÇÃO DE DÉBITO
                        if(transacao.TipoTransacao == TipoTransacaoEnum.Saque)
                        {
                            //CHECAR SE EXISTE SALDO SUFICIENTE NA CONTA
                            if(conta.Saldo > transacao.Valor)
                            {
                                //PEGAR TODAS AS TRANSAÇÕES REALIZADAS NO DIA
                                var transacoesDoDia = _context.Transacoes.Where(t => t.ContaId == transacao.ContaId)
                                                                     .Where(t => t.TipoTransacao == TipoTransacaoEnum.Saque)
                                                                     .Where(t => t.DataTransacao.Date == DateTime.Now.Date);
                                //SOMAR OS VALORES SACADOS NO DIA
                                decimal debitos = transacao.Valor;
                                foreach (var transacaoDoDia in transacoesDoDia)
                                {
                                    debitos += transacaoDoDia.Valor;
                                }
                                //SE OS DEBITOS FOREM MAIORES QUE O LIMITE DE SAQUE DIÁRIO
                                if (debitos > conta.LimiteSaqueDiario)
                                {
                                    throw new Exception("Limite de saque diário ultrapassado");
                                }
                                //SE EXISTIR SALDO E POSSUIR LIMITE
                                else
                                {
                                    //DIMINUIR O VALOR DO SALDO
                                    conta.Saldo -= transacao.Valor;
                                    _context.Update(conta);
                                    _context.SaveChanges();
                                    //ADICIONAR A TRANSAÇÃO AO BANCO
                                    _context.Transacoes.Add(transacao);
                                    _context.SaveChanges();
                                    transaction.Commit();
                                }
                            }
                            //SE NÃO ESTIVER SALDO SUFICIENTE
                            else
                            {
                                throw new Exception("Saldo insuficiente");
                            }
                           
                        }
                        //SE FOR UMA TRANSAÇÃO DE CREDITO
                        else
                        {
                            //ADICIONAR VALOR AO SALDO DA CONTA
                            conta.Saldo += transacao.Valor;
                            _context.Update(conta);
                            _context.SaveChanges();
                            //ADICIONAR TRANSACAO AO BANCO
                            _context.Transacoes.Add(transacao);
                            _context.SaveChanges();
                            transaction.Commit();
                        }
                    }
                    else
                    {
                        throw new Exception("Conta não encontrada.");
                    }
                }
                catch (Exception ex)
                {
                    transaction.Rollback();
                    throw ex;

                }
            }
        }

        public void DeleteTransacao(TransacaoModel transacao)
        {
            using (var transaction = _context.Database.BeginTransaction())
            {
                try
                {
                    //PEGAR CONTA DESTINO
                    var contaDestino = _context.Contas.Find(transacao.ContaId);

                    //SE A OPERAÇÃO FOI DE CREDITO RETIRAR VALOR DA CONTA
                    if(transacao.TipoTransacao == TipoTransacaoEnum.Deposito)
                    {
                        if(contaDestino.Saldo - transacao.Valor < 0)
                        {
                            throw new Exception("Não é possível apagar a transação, saldo insuficiente na conta destino");
                        }
                        else
                        {
                            //RETIRAR VALOR DA CONTA DESTINO
                            contaDestino.Saldo -= transacao.Valor;
                            _context.Update(contaDestino);
                            _context.SaveChanges();

                            //APAGAR TRANSAÇÃO
                            _context.Remove(transacao);
                            _context.SaveChanges();
                            transaction.Commit();

                        }
                    }
                    //SE A OPERAÇÃO FOI DE DEBITO, CREDITAR VALOR NA CONTA
                    else
                    {
                        contaDestino.Saldo += transacao.Valor;
                        _context.Update(contaDestino);
                        _context.SaveChanges();

                        //APAGAR TRANSAÇÃO
                        _context.Remove(transacao);
                        _context.SaveChanges();
                        transaction.Commit();
                    }
                }
                catch (Exception ex)
                {
                    throw ex;
                }
            }
        }

        //GET: PEGAR TODAS AS TRANSAÇÕES
        public IEnumerable<TransacaoModel> Get(TransacoesQuery filtros)
        {
            string where = filtros.Filtrar();
            IQueryable<TransacaoModel> transacoes = _context.Transacoes.FromSql(where)
                                                                       .Include(c => c.Conta)
                                                                       .OrderBy(t => t.ContaId)
                                                                       .ThenByDescending(t => t.DataTransacao);
            if (filtros.PorPessoaId != 0)
            {
                transacoes = transacoes.Where(p => p.Conta.PessoaId == filtros.PorPessoaId);
                return transacoes.ToList();
            }
            else
            {
                return transacoes.ToList();
            }
        }

        //GET: PEGAR TRANSACAO POR ID
        public TransacaoModel GetById(int Id)
        {
            TransacaoModel transacao = _context.Transacoes.Find(Id);
            return transacao;
        }
       
    }
}
