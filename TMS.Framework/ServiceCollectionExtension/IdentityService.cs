using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.Extensions.DependencyInjection;
using TMS.Common.Settings;
using TMS.Entities.Entities;
using TMS.Framework.Configuration;

namespace TMS.Framework.ServiceCollectionExtension
{
   public static class IdentityService
    {
        public static void AddCustomIdentity(this IServiceCollection service ,IdentitySetting identitySetting)
        {
            service.AddIdentity<AppUsers, AppRole>(option =>
            {
                option.Password.RequireDigit = identitySetting.PasswordRequireDigit;
                option.Password.RequiredLength = identitySetting.PasswordRequiredLength;
                option.Password.RequireNonAlphanumeric = identitySetting.PasswordRequireNonAlphanumic;
                option.Password.RequireUppercase = identitySetting.PasswordRequireUppercase;
                option.Password.RequireLowercase = identitySetting.PasswordRequireLowercase;
                option.User.RequireUniqueEmail = identitySetting.RequireUniqueEmail;
                option.Lockout.MaxFailedAccessAttempts = identitySetting.MaxFailedAccessAttempts;
                option.Lockout.DefaultLockoutTimeSpan = TimeSpan.FromMinutes(identitySetting.DefaultLockoutTimeSpan);
            });
        }
    }
}
