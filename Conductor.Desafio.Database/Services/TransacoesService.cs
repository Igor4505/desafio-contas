using Conductor.Desafio.Core.DTOs;
using Conductor.Desafio.Core.QueryObjects;
using Conductor.Desafio.Database.Services.Client;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace Conductor.Desafio.Database.Services
{
    public class TransacoesService : IServices<TransacaoDTO, TransacoesQuery>
    {
        public async Task<string> Delete(int id)
        {
            DesafioClient<TransacaoDTO> client = new DesafioClient<TransacaoDTO>();
            //RESPOSTA DA REQUISIÇÃO
            HttpResponseMessage response = await client.Delete("Transacoes/" + id);
            string dados = await response.Content.ReadAsStringAsync();
            string responseString = JsonConvert.DeserializeObject<string>(dados);
            //SE FOR SUCCESS STATUS CODE
            if (response.IsSuccessStatusCode)
            {
                return responseString;
            }
            //SE NÃO LANÇAR EXCEÇÃO COM ERRO E MENSAGEM
            else
            {
                throw new Exception($"Erro {response.StatusCode} - {responseString}");
            }
        }

        //PEGAR TODAS AS TRANSACOES
        public async Task<List<TransacaoDTO>> Get(TransacoesQuery queryObject)
        {
            queryObject = queryObject ?? new TransacoesQuery();
            //INSTANCIAR A LISTA DE PESSOAS
            List<TransacaoDTO> Transacoes = new List<TransacaoDTO>();

            //INSTANCIAR O CLIENT
            DesafioClient<TransacaoDTO> client = new DesafioClient<TransacaoDTO>();
            string uri = $"Transacoes{queryObject.ToString()}";

            //RESPOSTA
            var response = await client.Get(uri);
            string dados = await response.Content.ReadAsStringAsync();

            //SUCCESS STATUS CODE
            if (response.IsSuccessStatusCode)
            {
                IEnumerable<TransacaoDTO> listaDados = JsonConvert.DeserializeObject<IEnumerable<TransacaoDTO>>(dados);
                foreach (var transacao in listaDados)
                {
                    Transacoes.Add(transacao);
                }
                return Transacoes;
            }

            //ERROR STATUS CODE
            else
            {
                //LANÇAR EXCEÇÃO COM CODIGO DO ERRO E MENSAGEM
                string responseString = JsonConvert.DeserializeObject<string>(dados);
                throw new Exception($"Erro {response.StatusCode} - {responseString}");
            }
        }

        //PEGAR TRANSACAO POR ID
        public async Task<TransacaoDTO> GetSingle(int idTransacao)
        {
            //INSTANCIAR A PESSOA
            TransacaoDTO Transacao = new TransacaoDTO();

            //INSTANCIAR O CLIENT
            DesafioClient<TransacaoDTO> client = new DesafioClient<TransacaoDTO>();

            //RESPOSTA
            var response = await client.Get("Transacoes/" + idTransacao);
            string dados = await response.Content.ReadAsStringAsync();

            //SUCCESS STATUS CODE
            if (response.IsSuccessStatusCode)
            {
                Transacao = JsonConvert.DeserializeObject<TransacaoDTO>(dados);
                return Transacao;
            }
            //ERROR STATUS CODE
            else
            {
                //LANÇAR EXCEÇÃO COM CODIGO DO ERRO E MENSAGEM
                string responseString = JsonConvert.DeserializeObject<string>(dados);
                throw new Exception($"Erro {response.StatusCode} - {responseString}");
            }
        }

        //POST: INSERIR TRANSAÇÃO
        public async Task<string> Post(TransacaoDTO model)
        {
            //INSTANCIAR O CLIENT
            DesafioClient<TransacaoDTO> client = new DesafioClient<TransacaoDTO>();

            //RESPOSTA DA REQUISIÇÃO
            HttpResponseMessage response = await client.Post(model, "Transacoes");
            string dados = await response.Content.ReadAsStringAsync();
            string responseString = JsonConvert.DeserializeObject<string>(dados);
            //SE FOR SUCCESS STATUS CODE
            if (response.IsSuccessStatusCode)
            {
                return responseString;
            }
            //SE NÃO LANÇAR EXCEÇÃO COM ERRO E MENSAGEM
            else
            {
                throw new Exception($"Erro {response.StatusCode} - {responseString}");
            }
        }

        public Task<string> Put(TransacaoDTO model, int id)
        {
            throw new NotImplementedException();
        }
    }
}
