using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Text;

namespace TMS.Data.Services
{
    public class AccessToken
    {
        public string Access_Token { get; set; }
        public string Refresh_Token { get; set; }
        public string Token_Type { get; set; }
        public int Expires_In { get; set; }

        public AccessToken(JwtSecurityToken securityToken)
        {
                Access_Token=new JwtSecurityTokenHandler().WriteToken(securityToken);
                Token_Type = "Bearer";
                Expires_In = (int) (securityToken.ValidTo - DateTime.UtcNow).TotalSeconds;
        }
    }
}
