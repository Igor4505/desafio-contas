using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Conductor.Desafio.Core.DTOs;
using Conductor.Desafio.Core.QueryObjects;
using Conductor.Desafio.Database.Services;
using Conductor.Desafio.Database.ViewModels;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Conductor.Desafio.Web.Controllers
{
    public class TransacoesController : Controller
    {
        public async Task<IActionResult> Index(TransacoesQuery filtros)
        {
            if (!HttpContext.Session.Keys.Any())
            {
                return RedirectToAction("Index", "Login", new { area = "" });
            }
            else
            {
                ViewBag.Id = HttpContext.Session.GetInt32("Id").Value;
                filtros = filtros ?? new TransacoesQuery();
                filtros.PorPessoaId = ViewBag.Id;

                TransacoesViewModel vm = new TransacoesViewModel
                {
                    Filtros = filtros
                };
                try
                {
                    await vm.GetTransacoes();
                    
                }
                catch (Exception ex)
                {

                    ViewBag.Error = ex;
                }
                try
                {
                    await vm.GetContas(HttpContext.Session.GetInt32("Id").Value);
                }
                catch (Exception ex)
                {
                    ViewBag.ContaError = ex;
                }

                string nomePessoa = HttpContext.Session.GetString("nome");
                string genero = HttpContext.Session.GetString("genero") == "masculino" ? "Sr." : "Sra.";
                ViewBag.Pessoa = genero + " " + nomePessoa;
               
                return View(vm);
            }
        }

        public async Task<IActionResult> Extrato([FromRoute]int Id, TransacoesQuery filtros)
        {
            if (!HttpContext.Session.Keys.Any())
            {
                return RedirectToAction("Index", "Login", new { area = "" });
            }
            else
            {
                if (!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }
                else
                {
                    filtros = filtros ?? new TransacoesQuery();
                    filtros.PorConta = Id.ToString();
                    TransacoesViewModel vm = new TransacoesViewModel
                    {
                        Filtros = filtros
                    };
                    await vm.GetConta(Id);
                    try
                    {
                        await vm.GetTransacoes();
                    }
                    catch (Exception ex)
                    {

                        ViewBag.Error = ex;
                    }
                    

                    string nomePessoa = HttpContext.Session.GetString("nome");
                    string genero = HttpContext.Session.GetString("genero") == "masculino" ? "Sr." : "Sra.";
                    ViewBag.Pessoa = genero + " " + nomePessoa;

                    return View(vm);
                }
            }
        }

        public async Task<IActionResult> AddTransacao(TransacaoDTO transacao)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            else
            {
                try
                {
                    TransacoesService service = new TransacoesService();
                    string response = await service.Post(transacao);
                    TempData["Mensagem"] = response;
                    TempData["Alerta"] = true;
                    TempData["Classe"] = "sucesso";
                    return RedirectToAction("Index");
                }
                catch (Exception ex)
                {
                    TempData["Mensagem"] = ex.Message;
                    TempData["Alerta"] = true;
                    TempData["Classe"] = "atencao";
                    return RedirectToAction("Index");
                }
            }
        }

        public async Task<IActionResult> ExcluirTransacao([FromRoute]int Id)
        {
            if (!ModelState.IsValid)
            {
                TempData["Mensagem"] = ModelState;
                TempData["Alerta"] = true;
                TempData["Classe"] = "atencao";
                return RedirectToAction("Index");
            }
            else
            {
                try
                {
                    TransacoesService service = new TransacoesService();
                    string response = await service.Delete(Id);
                    TempData["Mensagem"] = response;
                    TempData["Alerta"] = true;
                    TempData["Classe"] = "sucesso";
                    return RedirectToAction("Index");
                }
                catch (Exception ex)
                {
                    TempData["Mensagem"] = ex.Message;
                    TempData["Alerta"] = true;
                    TempData["Classe"] = "atencao";
                    return RedirectToAction("Index");
                }
            }
        }
    }
}