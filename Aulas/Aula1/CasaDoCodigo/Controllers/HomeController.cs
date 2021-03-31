using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using CasaDoCodigo.Models;

namespace CasaDoCodigo.Controllers
{
    public class HomeController : Controller
    {
        public IActionResult Index() // A funcao tem que chamar o nome da view Index.cshtml
        {
            return View();
        }

        public IActionResult About() // A funcao tem que chamar o nome da view About.cshtml
        {
            ViewData["Message"] = "Your application description page.";

            return View();
        }

        public IActionResult Contact() // A funcao tem que chamar o nome da view Contact.cshtml
        {
            ViewData["Message"] = "Your contact page.";

            return View();
        }

        public IActionResult Error() // A funcao tem que chamar o nome da view Error.cshtml
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
