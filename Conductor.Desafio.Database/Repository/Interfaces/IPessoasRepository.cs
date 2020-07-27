using Conductor.Desafio.Core.Models;
using Conductor.Desafio.Core.QueryObjects;
using System;
using System.Collections.Generic;
using System.Text;

namespace Conductor.Desafio.Database.Repository.Interfaces
{
    public interface IPessoasRepository
    {
        void AddPessoa(PessoaModel pessoa);
        PessoaModel GetById(int id);
        IEnumerable<PessoaModel> Get(PessoaQuery pessoaQuery);
        void PutPessoa(PessoaModel pessoa);
        void DeletePessoa(PessoaModel pessoa);
        PessoaModel CheckPessoa(string email, string senha);
    }
}
