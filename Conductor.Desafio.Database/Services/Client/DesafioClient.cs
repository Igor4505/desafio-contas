using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace Conductor.Desafio.Database.Services.Client
{
    public class DesafioClient<T>
    {
        public string BaseUrl { get { return "http://localhost:60072/api/"; } }

        public async Task<HttpResponseMessage> Get(string uri)
        {
            using (HttpClient client = new HttpClient())
            {
                //REALIZAR A REQUISIÇÃO
                client.BaseAddress = new Uri(BaseUrl);
                var request = await client.GetAsync(uri);
                return request;
            }
        }

        public async Task<HttpResponseMessage> Post(T model, string uri)
        {
            using (HttpClient client = new HttpClient())
            {
                client.BaseAddress = new Uri(BaseUrl);
                var stringContent = new StringContent(JsonConvert.SerializeObject(model), Encoding.UTF8, "application/json");
                var request = await client.PostAsync(uri, stringContent);
                //string response = JsonConvert.DeserializeObject<string>(await request.Content.ReadAsStringAsync());
                return request;
            }
        }

        public async Task<HttpResponseMessage> GetSingle(string uri)
        {
            using (HttpClient client = new HttpClient())
            {
                client.BaseAddress = new Uri(BaseUrl);
                var request = await client.GetAsync(uri);
                //string dados = await response.Content.ReadAsStringAsync();
                //T dadosObject = JsonConvert.DeserializeObject<T>(dados);
                return request;
            }
        }

        public async Task<HttpResponseMessage> Put(T model, string uri)
        {
            using (HttpClient client = new HttpClient())
            {
                client.BaseAddress = new Uri(BaseUrl);
                var stringContent = new StringContent(JsonConvert.SerializeObject(model), Encoding.UTF8, "application/json");
                var request = await client.PutAsync(uri, stringContent);
                return request;
            }
        }

        public async Task<HttpResponseMessage> Delete(string uri)
        {
            using (HttpClient client = new HttpClient())
            {
                //REALIZAR A REQUISIÇÃO
                client.BaseAddress = new Uri(BaseUrl);
                var request = await client.DeleteAsync(uri);
                return request;
            }
        }
    }
}
