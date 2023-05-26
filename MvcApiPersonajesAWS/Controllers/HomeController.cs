using Microsoft.AspNetCore.Mvc;
using MvcApiPersonajesAWS.Models;
using MvcApiPersonajesAWS.Services;
using System.Diagnostics;

namespace MvcApiPersonajesAWS.Controllers
{
    public class HomeController : Controller
    {
        private ServiceApiPersonajes service;

        public HomeController(ServiceApiPersonajes service)
        {
            this.service = service;
        }

        public IActionResult Index()
        {
            return View();
        }

        public async Task<IActionResult> Test()
        {
            ViewData["TEST"] = await this.service.TestApiAsync();
            return View();
        }

        public async Task<IActionResult> ApiPersonajes()
        {
            List<Personaje> personajes =
                await this.service.GetPersonajesAsync();
            return View(personajes);
        }
    }
}