using AdvertisementApp.Business.Extensions;
using AdvertisementApp.Business.Interfaces;
using AdvertisementApp.DataAccess.UnitOfWork;
using AdvertisementApp.Dto.Interfaces;
using AdvertisementApp.Dto.ProvidedServiceDtos;
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
    public class Service<CreateDto, UpdateDto, ListDto, T> : IService<CreateDto, ListDto, UpdateDto, T>
        where CreateDto : class, IDto, new()
        where UpdateDto : class, IDto, new()
        where ListDto : class, IDto, new()
        where T : BaseEntity
    {
        private readonly IMapper _mapper;
        private readonly IValidator<CreateDto> _createDtoValidator;
        private readonly IValidator<UpdateDto> _updateDtoValidator;
        private readonly IUow _uow;

        public Service(IMapper mapper, IValidator<CreateDto> createDtoValidator, IValidator<UpdateDto> updateDtoValidator, IUow uow)
        {
            _mapper = mapper;
            _createDtoValidator = createDtoValidator;
            _updateDtoValidator = updateDtoValidator;
            _uow = uow;
        }

        public async Task<IResponse<CreateDto>> CreateAsync(CreateDto dto)
        {
            var result =  _createDtoValidator.Validate(dto);
            if (result.IsValid)
            {
                var createdEntitiy = _mapper.Map<T>(dto);
               await _uow.GetRepository<T>().CreateAsync(createdEntitiy);
                return new Response<CreateDto>(ResponseType.Success,dto);
            }
            
            return new Response<CreateDto>(dto, result.ConvertToCustomValidationError());

        }

        public Task<IResponse<List<UpdateDto>>> GetAllAsync()
        {
            throw new NotImplementedException();
        }

        public Task<IResponse<IDto>> GetByIdAsync<IDto>(int id)
        {
            throw new NotImplementedException();
        }

        public Task<IResponse> RemoveAsync(int id)
        {
            throw new NotImplementedException();
        }

        public Task<IResponse<ListDto>> UpdateAsync(ListDto dto)
        {
            throw new NotImplementedException();
        }
    }
}
