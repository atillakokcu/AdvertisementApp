using AdvertisementApp.Dto.Interfaces;

namespace AdvertisementApp.Dto
{
    public class AppUserLoginDto : IDto
    {
        public string Username { get; set; }
        public string Password { get; set; }
        public bool RememberMe { get; set; }

    }
}
