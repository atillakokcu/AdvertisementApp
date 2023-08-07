using AdvertisementApp.Business.Interfaces;
using AdvertisementApp.Dto;
using AdvertisementApp.UI.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace AdvertisementApp.UI.Controllers
{
    public class AdvertisementController : Controller
    {
        private readonly IAppUserService _userService;

        public AdvertisementController(IAppUserService userService)
        {
            _userService = userService;
        }

        public IActionResult Index()
        {
            return View();
        }

        [Authorize(Roles ="Member")]
        public async Task<IActionResult> Send(int AdvertisementId)
        {
           
            var userId = int.Parse((User.Claims.FirstOrDefault(x => x.Type == ClaimTypes.NameIdentifier)).Value);
            var userInfo = await _userService.GetByIdAsync<AppUserListDto>(userId);
            
            ViewBag.Gender = userInfo.Data.GenderId;
            return View( new AdvertisementAppUserCreateModel
            {
                AdvertismentId = AdvertisementId,
                AppUserId = userId,

            });
        }

    }
}
