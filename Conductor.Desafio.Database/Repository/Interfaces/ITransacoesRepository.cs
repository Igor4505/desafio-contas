using Conductor.Desafio.Core.Models;
using Conductor.Desafio.Core.QueryObjects;
using System;
using System.Collections.Generic;
using System.Text;

namespace Conductor.Desafio.Database.Repository.Interfaces
{
    public interface ITransacoesRepository 
    {
        void AddTransacao(TransacaoModel transacao);
        TransacaoModel GetById(int Id);
        IEnumerable<TransacaoModel> Get(TransacoesQuery filtros);
        void DeleteTransacao(TransacaoModel transacao);
    }
}
