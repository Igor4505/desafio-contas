using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Conductor.Desafio.Core.DTOs;
using Conductor.Desafio.Database.Services;
using Conductor.Desafio.Database.ViewModels;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Conductor.Desafio.Web.Controllers
{
    public class PerfilController : Controller
    {
        public async Task<IActionResult> Index()
        {
            if (!HttpContext.Session.Keys.Any())
            {
                return RedirectToAction("Index", "Login", new { area = "" });
            }
            else
            {
                PessoaViewModel vm = new PessoaViewModel();
                await vm.GetPessoaById(HttpContext.Session.GetInt32("Id").Value);
                string nomePessoa = HttpContext.Session.GetString("nome");
                string genero = HttpContext.Session.GetString("genero") == "masculino" ? "Sr." : "Sra.";
                ViewBag.Pessoa = genero + " " + nomePessoa;
                ViewBag.Id = HttpContext.Session.GetInt32("Id").Value;
                return View(vm);
            }
        }

        public async Task<IActionResult> EditarPerfil([FromRoute]int Id, PessoaDTO pessoa)
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
                    PessoaService service = new PessoaService();
                    string response = await service.Put(pessoa, Id);
                    TempData["Mensagem"] = response;
                    TempData["Alerta"] = true;
                    TempData["Classe"] = "sucesso";
                    HttpContext.Session.SetString("nome", pessoa.NomeCompleto);
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

        public async Task<IActionResult> ExcluirPerfil([FromRoute]int Id)
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
                    PessoaService service = new PessoaService();
                    string response = await service.Delete(Id);
                    TempData["Mensagem"] = response;
                    TempData["Alerta"] = true;
                    TempData["Classe"] = "sucesso";
                    HttpContext.Session.Clear();
                    return RedirectToAction("Index","Login",new {area ="" });
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