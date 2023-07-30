using AdvertisementApp.Business.Interfaces;
using AdvertisementApp.Business.Mappings.AutoMapper;
using AdvertisementApp.Business.Services;
using AdvertisementApp.Business.ValidationRules;
using AdvertisementApp.DataAccess.Contexts;
using AdvertisementApp.DataAccess.UnitOfWork;
using AdvertisementApp.Dto;
using AutoMapper;
using FluentValidation;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdvertisementApp.Business.DependencyResolvers.Microsoft
{
    public static class DependencyExtension
    {
        public static void AddDependencies(this IServiceCollection services, IConfiguration configuration)
        {

            services.AddDbContext<AdvetisementContext>(opt =>
            {
                //opt.UseSqlServer(configuration.GetConnectionString("Local"));
                opt.UseSqlServer("Server = GODSWHIP\\SQLEXPRESS; Database = AdvertisementDb; Trusted_Connection = True; TrustServerCertificate=True");
            });


            var mapperConfiguration = new MapperConfiguration(opt =>
            {
                opt.AddProfile(new ProvidedServiceProfile());
            });

            var mapper = mapperConfiguration.CreateMapper();

            services.AddSingleton(mapper);
            services.AddScoped<IUow, Uow>();

            services.AddTransient<IValidator<ProvidedServiceCreateDto>, ProvidedServiceCreateDtoValidator>();
            services.AddTransient<IValidator<ProvidedServiceUpdateDto>, ProvidedServiceUpdateDtoValidator>();

            services.AddScoped<IProvidedServiceService, ProvidedServiceService>();

            services.AddDbContext<AdvetisementContext>(options =>
options.UseSqlServer(configuration.GetConnectionString("Server = GODSWHIP\\SQLEXPRESS; Database = AdvertisementDb; Trusted_Connection = True; TrustServerCertificate=True")));
        }


    }
}
