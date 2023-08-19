using AdvertisementApp.Dto.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdvertisementApp.Dto
{
    public class AdvertisementAppUserCreateDto : IDto
    {
        public int AdvertisementId { get; set; }
        public int AppUserId { get; set; }
        public int AdvertisementAppUserStatusId { get; set; } 
        public int MilitarySatusId { get; set; }
        public DateTime? EndDate { get; set; }
        public int WorkExperience { get; set; }
        public string CvPath { get; set; }
    }
}
