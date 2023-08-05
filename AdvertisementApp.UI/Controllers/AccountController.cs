using AdvertisementApp.Business.Interfaces;
using AdvertisementApp.Dto;
using AdvertisementApp.UI.Extensions;
using AdvertisementApp.UI.Mappings.AutoMapper;
using AdvertisementApp.UI.Models;
using AdvertisimentApp.Common.Enums;
using AutoMapper;
using FluentValidation;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace AdvertisementApp.UI.Controllers
{
    public class AccountController : Controller
    {
        private readonly IGenderService _genderService;
        private readonly IValidator<UserCreateModel> _userCreateValidator;
        private readonly IValidator<AppUserLoginDto> _appUserLoginValidator;
        private readonly IAppUserService _appUserService;
        private readonly IMapper _mapper;

        public AccountController(IGenderService genderService, IValidator<UserCreateModel> userCreateValidator, IAppUserService appUserService, IMapper mapper, IValidator<AppUserLoginDto> appUserLoginValidator)
        {
            _genderService = genderService;
            _userCreateValidator = userCreateValidator;
            _appUserService = appUserService;
            _mapper = mapper;
            _appUserLoginValidator = appUserLoginValidator;
        }

        public async Task<IActionResult> SignUp()
        {
            var response = await _genderService.GetAllAsync();

            var model = new UserCreateModel
            {
                Genders = new SelectList(response.Data, "Id", "Defination")

            };

            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> SignUp(UserCreateModel model)
        {
            var result = _userCreateValidator.Validate(model);

            if (result.IsValid)
            {
                var dto = _mapper.Map<AppUserCreateDto>(model);


                var createResponse = await _appUserService.CreateWithRoleAsync(dto,(int)RoleType.Member);
                return this.ResponseRedirecAction(createResponse, "SignIn");
                
            }
            foreach (var error in result.Errors)
            {
                ModelState.AddModelError(error.PropertyName, error.ErrorMessage);
            }
            var response = await _genderService.GetAllAsync();
            model.Genders = new SelectList(response.Data, "Id", "Defination", model.GenderId);
            return View(model);

        }

        public IActionResult SignIn()
        {
            return View();
        }



        [HttpPost]
        public IActionResult SignIn(AppUserLoginDto dto)
        {
            if (ModelState.IsValid)
            {
            }

            return View(dto);
        }

    }
}
