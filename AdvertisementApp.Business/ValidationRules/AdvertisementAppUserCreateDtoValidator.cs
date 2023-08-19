using AdvertisementApp.Dto;
using AdvertisimentApp.Common.Enums;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdvertisementApp.Business.ValidationRules
{
    public class AdvertisementAppUserCreateDtoValidator : AbstractValidator<AdvertisementAppUserCreateDto>
    {
        public AdvertisementAppUserCreateDtoValidator()
        {
            this.RuleFor(x=>x.AdvertisementAppUserStatusId).NotEmpty();
            this.RuleFor(x=>x.AdvertisementId).NotEmpty();
            this.RuleFor(x=>x.AppUserId).NotEmpty();
            this.RuleFor(x=>x.CvPath).NotEmpty().WithMessage("Bir cv dosyası seçini");
            this.RuleFor(x=>x.EndDate).NotEmpty().When(x=>x.MilitarySatusId == (int)MilitaryStatusType.Tecilli).WithMessage("tecil tarhi boş olamaz");
            

        }
    }
}
