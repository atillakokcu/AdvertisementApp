using AdvertisementApp.DataAccess.Contexts;
using AdvertisementApp.DataAccess.UnitOfWork;
using AutoMapper;
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
            //services.AddDbContext<AdvetisementContext>(opt =>
            //{
            //    opt.UseSqlServer(configuration.GetConnectionString("Local"));
            //});

            var mapperConfiguration= new MapperConfiguration(opt =>
            {

            });

            var mapper = mapperConfiguration.CreateMapper();

            services.AddSingleton(mapper);
            services.AddScoped<IUow, Uow>();


        }


    }
}
