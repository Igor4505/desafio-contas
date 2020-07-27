using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace Conductor.Desafio.Database.Services
{
    public interface IServices<TModel, TQuery>
    {
        Task<List<TModel>> Get(TQuery queryObject);

        Task<TModel> GetSingle(int idPessoa);

        Task<string> Post(TModel model);

        Task<string> Put(TModel model, int id);

        Task<string> Delete(int id);
    }
}
