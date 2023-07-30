using AdvertisementApp.Business.Interfaces;
using AdvertisementApp.UI.Extensions;
using AdvertisementApp.UI.Models;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;

namespace AdvertisementApp.UI.Controllers
{
    public class HomeController : Controller
    {
        private readonly IProvidedServiceService _providedServiceService;

        public HomeController(IProvidedServiceService providedServiceService)
        {
            _providedServiceService = providedServiceService;
        }


        public async Task <IActionResult> Index()
        {
          var response =  await _providedServiceService.GetAllAsync();
            return this.ResponseView(response);

        }

        public IActionResult HumanResorce()
        {
            return View();

        }

    }
}