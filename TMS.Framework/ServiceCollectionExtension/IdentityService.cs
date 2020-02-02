using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNet.Identity;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;
using TMS.Common.Enums;
using TMS.Common.Exceptions;
using TMS.Common.Extensions;
using TMS.Common.Settings;
using TMS.Data.Contract.EntitiesRepository;
using TMS.Entities.Entities;
using TMS.Framework.Api;
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

        public static void AddCustomAuthentication(this IServiceCollection service, JwtSetting jwtSetting)
        {
            service.AddAuthentication(option =>
                {
                    option.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                    option.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
                    option.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
                })
                .AddJwtBearer(option =>
                {
                    var secretKey = Encoding.UTF8.GetBytes(jwtSetting.SecretKey);
                    var encryptionKey = Encoding.UTF8.GetBytes(jwtSetting.Encryptkey);
                    var validationParameters = new TokenValidationParameters
                    {
                        ClockSkew = TimeSpan.Zero,
                        RequireSignedTokens = true,
                        ValidateIssuerSigningKey = true,
                        IssuerSigningKey = new SymmetricSecurityKey(secretKey),
                        RequireExpirationTime = true,
                        ValidateLifetime = true,
                        ValidateAudience = true,
                        ValidAudience = jwtSetting.Audience,
                        ValidateIssuer = true,
                        ValidIssuer = jwtSetting.Issuer,
                        TokenDecryptionKey = new SymmetricSecurityKey(encryptionKey)
                    };
                    option.RequireHttpsMetadata = false;
                    option.SaveToken = true;
                    option.TokenValidationParameters = validationParameters;
                    option.Events = new JwtBearerEvents
                    {
                        OnAuthenticationFailed = context =>
                        {
                            if (context.Exception != null)
                            {
                                throw new AppException(ApiResultStatusCode.UnAuthorized, "Authentication Failed.",
                                    HttpStatusCode.Unauthorized, context.Exception, null);
                            }

                            return Task.CompletedTask;
                        },
                        OnTokenValidated = async context =>
                        {
                            var signInManager = context.HttpContext.RequestServices
                                .GetRequiredService<SignInManager<AppUsers>>();
                            var userRepository =
                                context.HttpContext.RequestServices.GetRequiredService<IUserRepository>();

                            var claimsIdentity = context.Principal.Identity as ClaimsIdentity;
                            if (claimsIdentity.Claims?.Any() != true)
                                context.Fail("This token has no claims.");

                            var securityStamp =
                                claimsIdentity.FindFirstValue(new ClaimsIdentityOptions().SecurityStampClaimType);
                            if (!securityStamp.HasValue())
                                context.Fail("This token has no secuirty stamp");

                            //Find user and token from database and perform your custom validation
                            var userId = claimsIdentity.GetUserId<int>();
                            var user = await userRepository.GetByIdAsync(context.HttpContext.RequestAborted, userId);

                            //if (user.SecurityStamp != Guid.Parse(securityStamp))
                            //    context.Fail("Token secuirty stamp is not valid.");

                            var validatedUser = await signInManager.ValidateSecurityStampAsync(context.Principal);
                            if (validatedUser == null)
                                context.Fail("Token secuirty stamp is not valid.");

                            if (!user.IsActive)
                                context.Fail("User is not active.");

                            await userRepository.UpdateLastLoginDateAsync(user, context.HttpContext.RequestAborted);
                        },
                        OnChallenge = context =>
                        {
                            //var logger = context.HttpContext.RequestServices.GetRequiredService<ILoggerFactory>().CreateLogger(nameof(JwtBearerEvents));
                            //logger.LogError("OnChallenge error", context.Error, context.ErrorDescription);

                            if (context.AuthenticateFailure != null)
                                throw new AppException(ApiResultStatusCode.UnAuthorized, "Authenticate failure.",
                                    HttpStatusCode.Unauthorized, context.AuthenticateFailure, null);
                            throw new AppException(ApiResultStatusCode.UnAuthorized,
                                "You are unauthorized to access this resource.", HttpStatusCode.Unauthorized);

                            //return Task.CompletedTask;
                        }
                    };
                });
        }
    }
}
