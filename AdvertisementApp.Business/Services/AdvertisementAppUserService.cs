using AdvertisementApp.Business.Extensions;
using AdvertisementApp.Business.Interfaces;
using AdvertisementApp.DataAccess.UnitOfWork;
using AdvertisementApp.Dto;
using AdvertisementApp.Entities;
using AdvertisimentApp;
using AdvertisimentApp.Common;
using AutoMapper;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdvertisementApp.Business.Services
{
    public class AdvertisementAppUserService : IAdvertisementAppUserService
    {
        private readonly IUow _uow;
        private readonly IMapper _mapper;
        private readonly IValidator<AdvertisementAppUserCreateDto> _createDtoValidator;

        public AdvertisementAppUserService(IUow uow, IMapper mapper, IValidator<AdvertisementAppUserCreateDto> createDtoValidator)
        {
            _uow = uow;
            _mapper = mapper;
            _createDtoValidator = createDtoValidator;
        }

        public async Task<IResponse<AdvertisementAppUserCreateDto>> CreateAsync(AdvertisementAppUserCreateDto dto)
        {
            var result = _createDtoValidator.Validate(dto);

            var createAdvertisementAppUser = _mapper.Map<AdvertisementAppUser>(dto);

            if (result.IsValid)
            {
                var control = await _uow.GetRepository<AdvertisementAppUser>().GetByFilterAsync(x => x.AppUserId == dto.AppUserId && x.AdvertisementId == dto.AdvertisementId);
                if (control == null)
                {
                    await _uow.GetRepository<AdvertisementAppUser>().CreateAsync(createAdvertisementAppUser);
                    await _uow.SaveChangesAsync();
                    return new Response<AdvertisementAppUserCreateDto>(ResponseType.Success, dto);
                }

                List<CustomValidationError> errors = new List<CustomValidationError>
                {
                    new CustomValidationError{ErrorMessage="daha önce bu ilana başvuru yaptınız"}
                };

                return new Response<AdvertisementAppUserCreateDto>(dto, errors);

            }
            return new Response<AdvertisementAppUserCreateDto>(dto, result.ConvertToCustomValidationError());


        }


    }
}
