using AdvertisementApp.Dto.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdvertisementApp.Dto
{
    public class GenderUpdateDto : IUpdateDto
    {
        public int Id { get; set; }
        public string Defination { get; set; }
    }
}
