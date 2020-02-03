using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using Microsoft.VisualBasic.CompilerServices;
using TMS.Common.DependencyMarkers;
using TMS.Common.Settings;
using TMS.Data.Contract;
using TMS.Entities.Entities;

namespace TMS.Data.Services
{
    public class JwtService : IJwtService, IScopedDependency
    {
        private readonly SiteSetting _siteSetting;
        private readonly SignInManager<AppUsers> _signInManager;

        public JwtService(IOptionsSnapshot<SiteSetting> siteSetting, SignInManager<AppUsers> signInManager)
        {
            _siteSetting = siteSetting.Value;
            _signInManager = signInManager;
        }

        public async Task<AccessToken> GenerateAsync(AppUsers users)
        {
            var secretKey = Encoding.UTF8.GetBytes(_siteSetting.JwtSetting.SecretKey);

            var signingCredentials = new SigningCredentials(new SymmetricSecurityKey(secretKey), SecurityAlgorithms.HmacSha256Signature);

            var encryptionKey = Encoding.UTF8.GetBytes(_siteSetting.JwtSetting.SecretKey);

            var encryptingCredentials = new EncryptingCredentials(new SymmetricSecurityKey(encryptionKey), SecurityAlgorithms.Aes128KW, SecurityAlgorithms.Aes128CbcHmacSha256);

            var claims = await _getClaimsAsync(users);

            var descriptor = new SecurityTokenDescriptor
            {
                Issuer = _siteSetting.JwtSetting.Issuer,
                Audience = _siteSetting.JwtSetting.Audience,
                IssuedAt = DateTime.Now,
                NotBefore = DateTime.Now.AddMinutes(_siteSetting.JwtSetting.NotBeforeMinutes),
                Expires = DateTime.Now.AddMinutes(_siteSetting.JwtSetting.ExpirationMinutes),
                SigningCredentials = signingCredentials,
                EncryptingCredentials = encryptingCredentials,
                Subject = new ClaimsIdentity(claims)
            };

            var tokenHandler = new JwtSecurityTokenHandler();

            var securityToken = tokenHandler.CreateJwtSecurityToken(descriptor);

            return new AccessToken(securityToken);
        }

        private async Task<IEnumerable<Claim>> _getClaimsAsync(AppUsers user)
        {
            var result = await _signInManager.ClaimsFactory.CreateAsync(user);
            var securityStampClaimType = new ClaimsIdentityOptions().SecurityStampClaimType;

            var list = new List<Claim>()
            {
                new Claim(ClaimTypes.Name, user.UserName),
                new Claim(ClaimTypes.NameIdentifier, user.Id.ToString()),
                new Claim(ClaimTypes.MobilePhone, user.MobilePhone),
                new Claim(securityStampClaimType, user.SecurityStamp.ToString())
            };

            var roles = new AppRole[] { new AppRole { Name = "Admin" } };

            foreach (var role in roles)
            {
                list.Add(new Claim(ClaimTypes.Role, role.Name));
            }

            return list;
        }
    }
}