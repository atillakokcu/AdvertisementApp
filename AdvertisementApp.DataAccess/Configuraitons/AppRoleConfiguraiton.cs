﻿using AdvertisementApp.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdvertisementApp.DataAccess.Configuraitons
{
    internal class AppRoleConfiguraiton : IEntityTypeConfiguration<AppRole>
    {
        public void Configure(EntityTypeBuilder<AppRole> builder)
        {
            builder.Property(x=>x.Defination).HasMaxLength(300);
            builder.HasData(new AppRole[]
            {
                new AppRole()
                {
                    Defination="Admin",
                    Id=1
                },
                new AppRole()
                {
                    Defination="Member",
                    Id=2
                }
            });
        }
    }
}
