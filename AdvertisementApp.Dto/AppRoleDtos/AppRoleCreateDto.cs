using AdvertisementApp.Dto.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdvertisementApp.Dto.AppRoleDtos
{
    public class AppRoleCreateDto:IDto
    {
        public string Definition { get; set; }
    }
}
