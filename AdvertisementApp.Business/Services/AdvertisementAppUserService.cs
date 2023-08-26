using AdvertisementApp.Business.Extensions;
using AdvertisementApp.Business.Interfaces;
using AdvertisementApp.DataAccess.UnitOfWork;
using AdvertisementApp.Dto;
using AdvertisementApp.Entities;
using AdvertisimentApp;
using AdvertisimentApp.Common;
using AdvertisimentApp.Common.Enums;
using AutoMapper;
using FluentValidation;
using Microsoft.EntityFrameworkCore;

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

        public async Task<List<AdvertisementAppUserListDto>> GetList(AdvertisementAppUserStatusType type)
        {
            var query = _uow.GetRepository<AdvertisementAppUser>().GetQuery();

            var list = await query.Include(x => x.Advertisement).Include(x => x.AdvertisementAppUserStatus).Include(x => x.MilitaryStatus).Include(x => x.AppUser).ThenInclude(x => x.Gender).Where(x => x.AdvertisementAppUserStatusId == (int)type).ToListAsync();

            return _mapper.Map<List<AdvertisementAppUserListDto>>(list);
        }

        public async Task SetStatusAsync(int advertismentAppUserId, AdvertisementAppUserStatusType type)
        {
            //var unchanged = await _uow.GetRepository<AdvertisementAppUser>().FindAsync(advertismentAppUserId);
            //var changed = await _uow.GetRepository<AdvertisementAppUser>().GetByFilterAsync(x => x.Id == advertismentAppUserId);
            //_uow.GetRepository<AdvertisementAppUser>().Update(changed, unchanged);

            //2.yontem
            var query = _uow.GetRepository<AdvertisementAppUser>().GetQuery();
            var entity = await query.SingleOrDefaultAsync(x => x.Id == advertismentAppUserId);
            entity.AdvertisementAppUserStatusId = (int)type;



            await _uow.SaveChangesAsync();
        }

    }
}
