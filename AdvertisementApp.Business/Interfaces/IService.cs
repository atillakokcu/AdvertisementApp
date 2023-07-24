using AdvertisementApp.Dto.Interfaces;
using AdvertisementApp.Dto.ProvidedServiceDtos;
using AdvertisimentApp;
using AdvertisimentApp.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdvertisementApp.Business.Interfaces
{
    public interface IService<CreateDto, UpdateDto, ListDto>
        where CreateDto : class,IDto,new()
        where UpdateDto : class,IDto,new()
        where ListDto : class,new()
    {
        Task<IResponse<CreateDto>> CreateAsync (ProvidedServiceCreateDto dto);
        Task<IResponse<UpdateDto>> UpdateAsync (ProvidedServiceUpdateDto dto);
        Task<IResponse<IDto>> GetByIdAsync<IDto>(int  id);
        Task<IResponse> RemoveAsync(int id);
        Task<IResponse<List<ListDto>>> GetAllAsync();


    }
}
