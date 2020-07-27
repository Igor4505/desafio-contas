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
    public class ContaService : IServices<ContaDTO, ContaQuery>
    {
        //DELETE: EXCLUIR CONTA
        public async Task<string> Delete(int Id)
        {
            DesafioClient<PessoaDTO> client = new DesafioClient<PessoaDTO>();
            //RESPOSTA DA REQUISIÇÃO
            HttpResponseMessage response = await client.Delete("Contas/" + Id);
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

        //GET: PEGAR TODAS AS CONTAS
        public async Task<List<ContaDTO>> Get(ContaQuery queryObject)
        {
            //INSTANCIAR A LISTA DE PESSOAS
            List<ContaDTO> Contas = new List<ContaDTO>();

            //INSTANCIAR O CLIENT
            DesafioClient<PessoaDTO> client = new DesafioClient<PessoaDTO>();
            string uri = queryObject.IdPessoa != 0 ? $"contas/pessoa/{queryObject.IdPessoa}" : "contas";

            //RESPOSTA
            var response = await client.Get(uri);
            string dados = await response.Content.ReadAsStringAsync();

            //SUCCESS STATUS CODE
            if (response.IsSuccessStatusCode)
            {
                IEnumerable<ContaDTO> listaContas = JsonConvert.DeserializeObject<IEnumerable<ContaDTO>>(dados);
                foreach (var pessoa in listaContas)
                {
                    Contas.Add(pessoa);
                }
                return Contas;
            }

            //ERROR STATUS CODE
            else
            {
                //LANÇAR EXCEÇÃO COM CODIGO DO ERRO E MENSAGEM
                string responseString = JsonConvert.DeserializeObject<string>(dados);
                throw new Exception($"Erro {response.StatusCode} - {responseString}");
            }
        }

        //GET: PEGAR CONTA POR ID DA CONTA
        public async Task<ContaDTO> GetSingle(int idConta)
        {
            //INSTANCIAR A PESSOA
            ContaDTO Conta = new ContaDTO();

            //INSTANCIAR O CLIENT
            DesafioClient<ContaDTO> client = new DesafioClient<ContaDTO>();

            //RESPOSTA
            var response = await client.Get("Contas/" + idConta);
            string dados = await response.Content.ReadAsStringAsync();

            //SUCCESS STATUS CODE
            if (response.IsSuccessStatusCode)
            {
                Conta = JsonConvert.DeserializeObject<ContaDTO>(dados);
                return Conta;
            }
            //ERROR STATUS CODE
            else
            {
                //LANÇAR EXCEÇÃO COM CODIGO DO ERRO E MENSAGEM
                string responseString = JsonConvert.DeserializeObject<string>(dados);
                throw new Exception($"Erro {response.StatusCode} - {responseString}");
            }
        }

        //POST: CADASTRAR CONTA
        public async Task<string> Post(ContaDTO model)
        {
            //INSTANCIAR O CLIENT
            DesafioClient<ContaDTO> client = new DesafioClient<ContaDTO>();

            //RESPOSTA DA REQUISIÇÃO
            HttpResponseMessage response = await client.Post(model, "Contas");
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

        //PUT: EDITAR CONTA
        public async Task<string> Put(ContaDTO model, int id)
        {
            DesafioClient<ContaDTO> client = new DesafioClient<ContaDTO>();
            //RESPOSTA DA REQUISIÇÃO
            HttpResponseMessage response = await client.Put(model, "Contas/" + id);
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

        //POST: DESATIVAR CONTA
        public async Task<string> Desativar(int id)
        {
            DesafioClient<ContaDTO> client = new DesafioClient<ContaDTO>();
            //RESPOSTA DA REQUISIÇÃO
            HttpResponseMessage response = await client.Put(new ContaDTO(), "Contas/desativar/" + id);
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
    }
}
