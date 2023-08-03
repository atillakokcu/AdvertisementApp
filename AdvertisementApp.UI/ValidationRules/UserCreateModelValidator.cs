using AdvertisementApp.UI.Models;
using FluentValidation;

namespace AdvertisementApp.UI.ValidationRules
{
    public class UserCreateModelValidator : AbstractValidator<UserCreateModel>
    {
        //[Obsolete]
        public UserCreateModelValidator()
        {
            //CascadeMode = CascadeMode.StopOnFirstFailure;
            RuleFor(x=>x.Password).NotEmpty();
            RuleFor(x=>x.Password).MinimumLength(3);
            RuleFor(x=>x.Password).Equal(x=>x.ConfirmPassword).WithMessage("Password not match");// password confirumpassword ile eşit olmalı diyoruz. WithMessage eşit olmaz ise bununla mesaj gönderebiliyoruz
            RuleFor(x => x.Firstname).NotEmpty();
            RuleFor(x => x.Surname).NotEmpty();
            RuleFor(x => x.Username).NotEmpty();
            RuleFor(x => x.Username).MinimumLength(3);
            RuleFor(x => new
            {
                x.Username,
                x.Firstname
            }).Must(x=> CanNotFirstname(x.Username,x.Firstname)).WithMessage("username contains firstname").When(x=>x.Username!=null && x.Firstname!=null); // username firstname içeremez dedik. when demek username ve firstname null olmadığı zaman bu ruleforu çalıştır demek istiyoruz

            RuleFor(x=>x.GenderId).NotEmpty();
            
            
           

        }

        private bool CanNotFirstname(string username, string firstname)
        {
           return !username.Contains(firstname);   
        }
    }
}
