using AdvertisementApp.DataAccess.Configuraitons;
using AdvertisementApp.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdvertisementApp.DataAccess.Contexts
{
    public  class AdvetisementContext : DbContext
    {
        public AdvetisementContext(DbContextOptions<AdvetisementContext> options) : base(options)
        {

        }
        public class DesignTimeDbContextFactory : IDesignTimeDbContextFactory<AdvetisementContext>
        {
            public AdvetisementContext CreateDbContext(string[] args)
            {
                var builder = new DbContextOptionsBuilder<AdvetisementContext>();
                var connectionString = "Server = GODSWHIP\\SQLEXPRESS; Database = AdvertisementDb; Trusted_Connection = True; TrustServerCertificate=True";
                builder.UseSqlServer(connectionString);
                return new AdvetisementContext(builder.Options);
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder) // oluşturduğumuz configuraitonları burada tanıtmamız gerekiyor bu yüzden onmodelcreatng metodunu overide ettik
        {
            modelBuilder.ApplyConfiguration(new AdvertisementAppUserConfiguraiton());
            modelBuilder.ApplyConfiguration(new AdvertisementAppUserStatusConfiguraiton());
            modelBuilder.ApplyConfiguration(new AdvertisementConfiguraiton());
            modelBuilder.ApplyConfiguration(new AppRoleConfiguraiton());
            modelBuilder.ApplyConfiguration(new AppUserConfiguration());
            modelBuilder.ApplyConfiguration(new AppUserRoleConfiguraiton());
            modelBuilder.ApplyConfiguration(new GenderConfiguraiton());
            modelBuilder.ApplyConfiguration(new MilitaryStatusConfiguraiton());
            modelBuilder.ApplyConfiguration(new ProvidedServiceConfiguraiton());
            
        }

        public DbSet<Advertisement> Advertisements { get; set; }
        public DbSet<AdvertisementAppUser> AdvertisementAppUsers { get; set; }
        public DbSet<AdvertisementAppUserStatus> AdvertisementAppUserStatuses { get; set; }
        public DbSet<AppUser> AppUsers { get; set; }
        public DbSet<AppRole> AppRoles { get; set; }
        public DbSet<AppUserRole> AppUserRoles { get; set; }
        public DbSet<Gender> Genders { get; set; }
        public DbSet<MilitaryStatus> MilitaryStatuses { get; set; }
        public DbSet<ProvidedService> ProvidedServices { get; set; }
        

    }
}
