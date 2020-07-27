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
    public class PessoaService : IServices<PessoaDTO, PessoaQuery>
    {
        //GET: PEGAR TODAS AS PESSOAS
        public async Task<List<PessoaDTO>> Get(PessoaQuery queryObject)
        {
            //INSTANCIAR A LISTA DE PESSOAS
            List<PessoaDTO> Pessoas = new List<PessoaDTO>();

            //INSTANCIAR O CLIENT
            DesafioClient<PessoaDTO> client = new DesafioClient<PessoaDTO>();
            string uri = $"Pessoas{queryObject.ToString()}";

            //RESPOSTA
            var response = await client.Get(uri);
            string dados = await response.Content.ReadAsStringAsync();
            
            //SUCCESS STATUS CODE
            if (response.IsSuccessStatusCode)
            {
                IEnumerable<PessoaDTO> listaDados = JsonConvert.DeserializeObject<IEnumerable<PessoaDTO>>(dados);
                foreach (var pessoa in listaDados)
                {
                    Pessoas.Add(pessoa);
                }
                return Pessoas;
            }

            //ERROR STATUS CODE
            else
            {
                //LANÇAR EXCEÇÃO COM CODIGO DO ERRO E MENSAGEM
                string responseString = JsonConvert.DeserializeObject<string>(dados);
                throw new Exception($"Erro {response.StatusCode} - {responseString}");
            }
        }

        //GET: PEGAR PESSOA POR ID
        public async Task<PessoaDTO> GetSingle(int idPessoa)
        {
            //INSTANCIAR A PESSOA
            PessoaDTO Pessoa = new PessoaDTO();

            //INSTANCIAR O CLIENT
            DesafioClient<PessoaDTO> client = new DesafioClient<PessoaDTO>();

            //RESPOSTA
            var response = await client.Get("Pessoas/"+idPessoa);
            string dados = await response.Content.ReadAsStringAsync();

            //SUCCESS STATUS CODE
            if (response.IsSuccessStatusCode)
            {
                Pessoa = JsonConvert.DeserializeObject<PessoaDTO>(dados);
                return Pessoa;
            }
            //ERROR STATUS CODE
            else
            {
                //LANÇAR EXCEÇÃO COM CODIGO DO ERRO E MENSAGEM
                string responseString = JsonConvert.DeserializeObject<string>(dados);
                throw new Exception($"Erro {response.StatusCode} - {responseString}");
            }
        }

        //POST: INSERIR USUÁRIO
        public async Task<string> Post(PessoaDTO model)
        {
            //CRIPTOGRAFAR A SENHA
            string passToHash = "c0dVc10R" + model.Senha + "^~++45";
            string salt = BCrypt.Net.BCrypt.GenerateSalt();
            string hash = BCrypt.Net.BCrypt.HashPassword(passToHash, salt);
            model.Senha = hash;
            model.ConfSenha = hash;

            //INSTANCIAR O CLIENT
            DesafioClient<PessoaDTO> client = new DesafioClient<PessoaDTO>();

            //RESPOSTA DA REQUISIÇÃO
            HttpResponseMessage response = await client.Post(model, "Pessoas");
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

        //POST: CHECAR IDENTIDADE DE USUÁRIO
        public async Task<PessoaDTO> CheckPessoa(LoginDTO pessoaDTOLogin)
        {
            //INSTANCIAR O PESSOA DTO
            PessoaDTO Pessoa = new PessoaDTO();

            //INSTANCIAR O CLIENT
            DesafioClient<LoginDTO> client = new DesafioClient<LoginDTO>();

            //RESPOSTA
            var response = await client.Post(pessoaDTOLogin, "Pessoas/check");
            string dados = await response.Content.ReadAsStringAsync();

            //SUCCESS STATUS CODE
            if (response.IsSuccessStatusCode)
            {
                Pessoa = JsonConvert.DeserializeObject<PessoaDTO>(dados);
                return Pessoa;
            }
            //ERROR STATUS CODE
            else
            {
                string responseString = JsonConvert.DeserializeObject<string>(dados);
                //LANÇAR EXCEÇÃO COM CODIGO DO ERRO E MENSAGEM
                throw new Exception($"Erro {response.StatusCode} - {responseString}");
            }
        }

        //PUT: EDITAR USUÁRIO
        public async Task<string> Put(PessoaDTO model, int Id)
        {
            //CRIPTOGRAFAR A SENHA
            string passToHash = "c0dVc10R" + model.Senha + "^~++45";
            string salt = BCrypt.Net.BCrypt.GenerateSalt();
            string hash = BCrypt.Net.BCrypt.HashPassword(passToHash, salt);
            model.Senha = hash;
            model.ConfSenha = hash;

            DesafioClient<PessoaDTO> client = new DesafioClient<PessoaDTO>();
            //RESPOSTA DA REQUISIÇÃO
            HttpResponseMessage response = await client.Put(model, "Pessoas/"+Id);
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

        //DELETE: DELETAR USUÁRIO
        public async Task<string> Delete(int Id)
        {
            DesafioClient<PessoaDTO> client = new DesafioClient<PessoaDTO>();
            //RESPOSTA DA REQUISIÇÃO
            HttpResponseMessage response = await client.Delete("Pessoas/" + Id);
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
