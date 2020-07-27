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
    public class LoginController : Controller
    {
        public IActionResult Index()
        {
            if (HttpContext.Session.Keys.Any())
            {
                return RedirectToAction("index", "home", new { area = "" });
            }
            else
            {
                return View();
            }
        }

        public async Task<IActionResult> RealizarLogin(LoginDTO loginDTO)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            else
            {
                try
                {
                    PessoaService service = new PessoaService();
                    var response = await service.CheckPessoa(loginDTO);
                    HttpContext.Session.SetInt32("Id", response.Id.Value);
                    HttpContext.Session.SetString("nome", response.Nome + " " + response.Sobrenome);
                    HttpContext.Session.SetString("email", response.Email);
                    HttpContext.Session.SetString("genero", response.Genero.ToString());
                    return RedirectToAction("Index", "Home", new { area = "" });
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

        public async Task<IActionResult> AddUsuario(PessoaDTO pessoa)
        {
            if (!ModelState.IsValid)
            {
                PessoaViewModel vm = new PessoaViewModel();
                vm.Pessoa = pessoa;
                return View("Usuario", vm);
            }
            else
            {
                try
                {
                    PessoaService service = new PessoaService();
                    string response = await service.Post(pessoa);
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