using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using TMS.Entities.Entities;

namespace TMS.Entities.EntityConfigurations
{
    public class AppRoleConfig:IEntityTypeConfiguration<AppRole>
    {
        public void Configure(EntityTypeBuilder<AppRole> builder)
        {
            builder.Property(p => p.Name).IsRequired();
        }
    }
}
