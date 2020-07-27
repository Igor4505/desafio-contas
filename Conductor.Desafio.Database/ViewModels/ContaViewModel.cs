using Conductor.Desafio.Core.DTOs;
using Conductor.Desafio.Core.QueryObjects;
using Conductor.Desafio.Database.Services;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Conductor.Desafio.Database.ViewModels
{
    public class ContaViewModel
    {
        public ContaDTO Conta { get; set; }
        public ContaDTO ContaEdit { get; set; }
        public ContaQuery Filtros { get; set; }
        public List<ContaDTO> Contas { get; set; }

        public ContaViewModel()
        {
            Filtros = new ContaQuery();
            Conta = new ContaDTO();
        }

        public async Task GetContas()
        {
            try
            {
                ContaService service = new ContaService();
                Contas = await service.Get(Filtros);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
