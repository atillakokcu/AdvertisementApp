﻿using AdvertisementApp.Business.ValidationRules;
using AdvertisementApp.Dto;
using AdvertisementApp.Entities;
using AdvertisimentApp;
using AdvertisimentApp.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdvertisementApp.Business.Interfaces
{
    public interface IAdvertisementService : IService<AdvertisementCreateDto, AdvertisementUpdateDto, AdvertisementListDto, Advertisement>
    {

        Task<IResponse<List<AdvertisementListDto>>> GetActiveAsync();

    }
}
