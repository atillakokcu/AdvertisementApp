using AdvertisementApp.Business.Interfaces;
using AdvertisementApp.Dto;
using AdvertisementApp.UI.Extensions;
using AdvertisementApp.UI.Models;
using AdvertisimentApp;
using AdvertisimentApp.Common.Enums;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.Security.Claims;

namespace AdvertisementApp.UI.Controllers
{
    public class AdvertisementController : Controller
    {
        private readonly IAppUserService _userService;
        private readonly IAdvertisementAppUserService _advertisementAppUserService;

        public AdvertisementController(IAppUserService userService, IAdvertisementAppUserService advertisementAppUserService)
        {
            _userService = userService;
            _advertisementAppUserService = advertisementAppUserService;
        }

        public IActionResult Index()
        {
            return View();
        }

        [Authorize(Roles = "Member")]
        public async Task<IActionResult> Send(int AdvertisementId)
        {

            var userId = int.Parse((User.Claims.FirstOrDefault(x => x.Type == ClaimTypes.NameIdentifier)).Value);
            var userResponse = await _userService.GetByIdAsync<AppUserListDto>(userId);

            ViewBag.Gender = userResponse.Data.GenderId;

            var items = Enum.GetValues(typeof(MilitaryStatusType)); // oluşturduğumuz enumları burada tanımladık
            var list = new List<MilitarySatusListDto>(); // enumları liste içine atmak için liste tanımladık

            foreach (int item in items)
            {
                list.Add(new MilitarySatusListDto
                {
                    Id = item,
                    Defination = Enum.GetName(typeof(MilitaryStatusType), item),
                });

            }

            ViewBag.MilitaryStatus = new SelectList(list, "Id", "Defination");


            return View(new AdvertisementAppUserCreateModel
            {
                AdvertisementId = AdvertisementId,
                AppUserId = userId,

            });
        }


        [Authorize(Roles = "Member")]
        [HttpPost]
        public async Task<IActionResult> Send(AdvertisementAppUserCreateModel model)
        {
            AdvertisementAppUserCreateDto dto = new();

            if (model.CvFile != null)
            {
                var filename = Guid.NewGuid().ToString();
                var extName = Path.GetExtension(model.CvFile.FileName).ToLower();
                string path = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "cvFiles", filename + extName);
                var stream = new FileStream(path, FileMode.Create);
                await model.CvFile.CopyToAsync(stream);
                dto.CvPath = path;
            }

            dto.AdvertisementAppUserStatusId = model.AdvertisementAppUserStatusId;
            dto.AdvertisementId = model.AdvertisementId;
            dto.AppUserId = model.AppUserId;
            dto.EndDate = model.EndDate;
            dto.MilitarySatusId = model.MilitarySatusId;
            dto.WorkExperience = model.WorkExperience;

            var response = await _advertisementAppUserService.CreateAsync(dto);

            if (response.ResponseType == ResponseType.ValidationError)
            {
                foreach (var error in response.ValidationErrors)
                {
                    ModelState.AddModelError(error.PropertyName, error.ErrorMessage);
                    var userId = int.Parse((User.Claims.FirstOrDefault(x => x.Type == ClaimTypes.NameIdentifier)).Value);
                    var userResponse = await _userService.GetByIdAsync<AppUserListDto>(userId);

                    ViewBag.Gender = userResponse.Data.GenderId;
                    var items = Enum.GetValues(typeof(MilitaryStatusType)); // oluşturduğumuz enumları burada tanımladık
                    var list = new List<MilitarySatusListDto>(); // enumları liste içine atmak için liste tanımladık

                    foreach (int item in items)
                    {
                        list.Add(new MilitarySatusListDto
                        {
                            Id = item,
                            Defination = Enum.GetName(typeof(MilitaryStatusType), item),
                        });

                    }

                    ViewBag.MilitaryStatus = new SelectList(list, "Id", "Defination");
                }
                return View(model);
            }
            else
            {
                return RedirectToAction("HumanResorce", "Home");
            }
        }

        [Authorize(Roles ="Admin")] // rolun içerisinde admin olan kişi buraya girebilsin
        public async Task< IActionResult> List()
        {

          var list= await _advertisementAppUserService.GetList(AdvertisementAppUserStatusType.Başvurdu);

            return View(list);
        }

        [Authorize(Roles = "Admin")]
        public async Task< IActionResult> SetStatus(int advertismentAppUserId, AdvertisementAppUserStatusType type)
        {
           await _advertisementAppUserService.SetStatusAsync(advertismentAppUserId, type);
            return RedirectToAction("List");
        }

        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> ApprovedList()
        {
            var list = await _advertisementAppUserService.GetList(AdvertisementAppUserStatusType.Mülakat);

            return View(list);

        }[Authorize(Roles = "Admin")]
        public async Task<IActionResult> RejectedList()
        {
            var list = await _advertisementAppUserService.GetList(AdvertisementAppUserStatusType.Olumsuz);

            return View(list);

        }



    }
}
