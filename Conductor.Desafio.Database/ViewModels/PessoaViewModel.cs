using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;
using System.Threading.Tasks;
using Conductor.Desafio.Core.DTOs;
using Conductor.Desafio.Core.QueryObjects;
using Conductor.Desafio.Database.Services;

namespace Conductor.Desafio.Database.ViewModels
{
    public class PessoaViewModel
    {
        public PessoaDTO Pessoa { get; set; }
        public ObservableCollection<PessoaDTO> Pessoas { get; set; }

        public LoginDTO LoginDTO { get; set; }

        public PessoaViewModel()
        {
            Pessoa = new PessoaDTO();
            LoginDTO = new LoginDTO();
            Pessoas = new ObservableCollection<PessoaDTO>();
        }

        public async Task GetPessoaById(int id)
        {
            try
            {
                PessoaService service = new PessoaService();
                Pessoa = await service.GetSingle(id);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task GetPessoas()
        {
            try
            {
                PessoaService service = new PessoaService();
                var pessoas = await service.Get(new PessoaQuery());
                foreach(var pessoa in pessoas)
                {
                    Pessoas.Add(pessoa);
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
