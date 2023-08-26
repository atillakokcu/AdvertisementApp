using AdvertisementApp.Business.Interfaces;
using AdvertisementApp.Dto;
using AdvertisementApp.UI.Extensions;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace AdvertisementApp.UI.Controllers
{
    [Authorize(Roles = "Admin")]
    public class ApplicationController : Controller
    {
        private readonly IAdvertisementService _advertisementService;

        public ApplicationController(IAdvertisementService advertisementService)
        {
            _advertisementService = advertisementService;
        }

        public async Task<IActionResult> List()
        {
            var response = await _advertisementService.GetAllAsync();
            return this.ResponseView(response);
        }

        public IActionResult Create()
        {
            return View(new AdvertisementCreateDto());
        }

        [HttpPost]
        public async Task<IActionResult> Create(AdvertisementCreateDto dto)
        {
            var response = await _advertisementService.CreateAsync(dto);
            return this.ResponseRedirecAction(response, "List");
        }

        public async Task< IActionResult> Update(int Id)
        {
            var response = await _advertisementService.GetByIdAsync<AdvertisementUpdateDto>(Id);

            return this.ResponseView(response);
        }

        [HttpPost]
        public async Task<IActionResult> Update(AdvertisementUpdateDto dto)
        {
           var response =await _advertisementService.UpdateAsync(dto);
            return this.ResponseRedirecAction(response, "List");
        }

        public async Task<IActionResult> Remove(int Id)
        {
            var response = await _advertisementService.RemoveAsync(Id);
            return this.ResponseRedirecAction(response, "List");
        }
    }
}
