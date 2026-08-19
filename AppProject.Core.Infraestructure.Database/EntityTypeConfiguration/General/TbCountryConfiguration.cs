using System;
using AppProject.Core.Infraestructure.Database.Entities.General;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace AppProject.Core.Infraestructure.Database.EntityTypeConfiguration.General;

public class TbCountryConfiguration : IEntityTypeConfiguration<TbCountry>
{
    public void Configure(EntityTypeBuilder<TbCountry> builder)
    {
        builder.HasIndex(x => x.Name).IsUnique();
    }
}
