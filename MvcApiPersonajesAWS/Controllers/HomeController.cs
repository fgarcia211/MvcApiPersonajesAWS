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

        public IActionResult Create()
        {
            return View();
        }

        public async Task<IActionResult> Create(Personaje personaje)
        {
            await this.service.InsertPersonajeAsync(personaje);
            return RedirectToAction("ApiPersonajes");
        }

        public IActionResult Edit()
        {
            return View();
        }

        public async Task<IActionResult> Edit(Personaje personaje)
        {
            await this.service.PutPersonajeAsync(personaje);
            return RedirectToAction("ApiPersonajes");
        }

        public async Task<IActionResult> Delete(int id)
        {
            await this.service.DeletePersonajeAsync(id);
            return RedirectToAction("ApiPersonajes");
        }

        public async Task<IActionResult> ApiPersonajes()
        {
            List<Personaje> personajes =
                await this.service.GetPersonajesAsync();
            return View(personajes);
        }
    }
}