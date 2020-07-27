using Conductor.Desafio.Core.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace Conductor.Desafio.Database.Repository.Interfaces
{
    public interface IContasRepository
    {
        void AddConta(ContaModel Conta);
        ContaModel GetById(int id);
        IEnumerable<ContaModel> GetByPessoaId(int id);
        IEnumerable<ContaModel> Get(bool ativo);
        void PutConta(ContaModel Conta);
        void DeleteConta(ContaModel Conta);
    }
}
