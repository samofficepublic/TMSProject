using System;
using TMS.Common.Enums;

namespace TMS.Api.Dtos
{
    public class AppUserDto
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string UserName { get; set; }
        public string Password { get; set; }
        public string MobilePhone { get; set; }
        public string UserEmail { get; set; }
        public DateTime? BirthDay { get; set; }
        public GenderType? Gender { get; set; }
        public string PersonalyCode { get; set; }
        public DateTimeOffset? LastLoginDate { get; set; }
        public bool IsActive { get; set; }
    }
}