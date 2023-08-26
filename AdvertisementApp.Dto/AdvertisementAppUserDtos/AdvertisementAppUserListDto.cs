using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdvertisementApp.Dto
{
    public class AdvertisementAppUserListDto
    {
        public int Id { get; set; }
        public int AdvertisementId { get; set; }
        public AdvertisementListDto Advertisement { get; set; }

        public int AppUserId { get; set; }
        public AppUserListDto AppUser { get; set; }

        public int AdvertisementAppUserStatusId { get; set; }
        public AdvertisementAppUserStatusListDto AdvertisementAppUserStatus { get; set; }

        public int MilitarySatusId { get; set; }
        public MilitarySatusListDto MilitaryStatus { get; set; }
        public DateTime? EndDate { get; set; }

        public int WorkExperienceId { get; set; }

        public string CvPath { get; set; }

    }
}
