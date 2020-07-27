using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Conductor.Desafio.Web.Models;
using Microsoft.AspNetCore.Http;

namespace Conductor.Desafio.Web.Controllers
{
    public class HomeController : Controller
    {
        public IActionResult Index()
        {
            if (!HttpContext.Session.Keys.Any())
            {
                return RedirectToAction("Index", "Login", new { area = "" });
            }
            else
            {
                string nomePessoa = HttpContext.Session.GetString("nome");
                string genero = HttpContext.Session.GetString("genero") == "masculino" ? "Sr." : "Sra.";
                ViewBag.Pessoa = genero + " " + nomePessoa;
                return View();
            }
        }

        //public IActionResult About()
        //{
        //    ViewData["Message"] = "Your application description page.";

        //    return View();
        //}

        //public IActionResult Contact()
        //{
        //    ViewData["Message"] = "Your contact page.";

        //    return View();
        //}

        //public IActionResult Error()
        //{
        //    return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        //}
    }
}
