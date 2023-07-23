using AdvertisementApp.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdvertisementApp.DataAccess.Configuraitons
{
    public class AdvertisementConfiguraiton : IEntityTypeConfiguration<Advertisement> // property lere özellik ve kısıtlamalar atıyoruz
    {
        public void Configure(EntityTypeBuilder<Advertisement> builder)
        {
            builder.Property(x=>x.Title).HasMaxLength(200).IsRequired();
            builder.Property(x=>x.Descrpition).HasColumnType("ntext").IsRequired();
            builder.Property(x => x.CreatedDate).HasDefaultValueSql("getdate()");
        }
    }
}
