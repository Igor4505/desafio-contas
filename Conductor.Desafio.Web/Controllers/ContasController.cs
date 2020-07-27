using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Conductor.Desafio.Core.DTOs;
using Conductor.Desafio.Core.Enums;
using Conductor.Desafio.Core.QueryObjects;
using Conductor.Desafio.Database.Services;
using Conductor.Desafio.Database.ViewModels;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Conductor.Desafio.Web.Controllers
{
    public class ContasController : Controller
    {
        public async Task<IActionResult> Index()
        {
            if (!HttpContext.Session.Keys.Any())
            {
                return RedirectToAction("Index", "Login", new { area = "" });
            }
            else
            {
                ContaViewModel vm = new ContaViewModel
                {
                    Filtros = new ContaQuery(){ IdPessoa = HttpContext.Session.GetInt32("Id").Value}
                };
                string nomePessoa = HttpContext.Session.GetString("nome");
                string genero = HttpContext.Session.GetString("genero") == "masculino" ? "Sr." : "Sra.";
                ViewBag.Pessoa = genero + " " + nomePessoa;
                ViewBag.Id = HttpContext.Session.GetInt32("Id").Value;
                ViewBag.Url = $"{this.Request.Scheme}://{Request.Host}{Request.PathBase}";
                try
                {
                    await vm.GetContas();
                }
                catch (Exception ex)
                {
                    ViewBag.ErrorMessage = ex.Message;
                }
                return View(vm);
            }
        }

        public async Task<IActionResult> AddConta(ContaDTO conta)
        {
            if (!ModelState.IsValid)
            {
                ContaViewModel vm = new ContaViewModel();
                vm.Conta= conta;
                return RedirectToAction("Index", vm);
            }
            else
            {
                try
                {
                    ContaService service = new ContaService();
                    string response = await service.Post(conta);
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

        public async Task<IActionResult> ExcluirConta([FromRoute]int Id)
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
                    ContaService service = new ContaService();
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

        public async Task<IActionResult> EditarConta([FromRoute]int Id, string descricao, TipoContaEnum tipo, decimal limite)
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
                ContaDTO conta = new ContaDTO()
                {
                    Id = Id,
                    Descricao = descricao,
                    Tipo = tipo,
                    LimiteSaqueDiario = limite,
                };
                try
                {
                    ContaService service = new ContaService();
                    string response = await service.Put(conta, Id);
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

        public async Task<IActionResult> DesativarConta(int id)
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
                    ContaService service = new ContaService();
                    string response = await service.Desativar(id);
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