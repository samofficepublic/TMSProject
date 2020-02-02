using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using TMS.Entities.Entities;

namespace TMS.Entities.EntityConfigurations
{
    public class AppUserConfig : IEntityTypeConfiguration<AppUsers>
    {
        public void Configure(EntityTypeBuilder<AppUsers> builder)
        {
            builder.Property(x => x.FirstName).IsRequired(false);
            builder.Property(x => x.LastName).IsRequired(false);
            builder.Property(x => x.UserEmail).IsRequired();
            builder.Property(x => x.UserEmail).IsRequired();
            builder.Property(x => x.Password).IsRequired();
            builder.Property(x => x.PersonalyCode).IsRequired(false);
        }
    }
}
