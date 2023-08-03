using AdvertisementApp.Business.Interfaces;
using AdvertisementApp.UI.Models;
using FluentValidation;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace AdvertisementApp.UI.Controllers
{
    public class AccountController : Controller
    {
        private readonly IGenderService _genderService;
        private readonly IValidator<UserCreateModel> _userCreateValidator;

        public AccountController(IGenderService genderService, IValidator<UserCreateModel> userCreateValidator)
        {
            _genderService = genderService;
            _userCreateValidator = userCreateValidator;
        }

        public async Task< IActionResult> SignUp()
        {
            var response = await _genderService.GetAllAsync();

            var model = new UserCreateModel{ Genders = new SelectList(response.Data,"Id","Defination")

            };
           
            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> SignUp(UserCreateModel model)
        {
           var result =  _userCreateValidator.Validate(model);

            if (result.IsValid)
            {
                return View(model);
            }
            foreach (var error in result.Errors)
            {
                ModelState.AddModelError(error.PropertyName, error.ErrorMessage);
                
            }
            var response = await _genderService.GetAllAsync();
            model.Genders = new SelectList(response.Data, "Id", "Defination",model.GenderId);
            return View(model);
            

            
        }



    }
}
