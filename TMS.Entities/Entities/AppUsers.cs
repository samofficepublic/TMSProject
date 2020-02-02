﻿using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using TMS.Common.Enums;
using TMS.Entities.Common;
using Microsoft.AspNet.Identity.EntityFramework;

namespace TMS.Entities.Entities
{
    public class AppUsers:IdentityUser<Guid>
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string UserName { get; set; }
        public string Password { get; set; }
        public string UserEmail { get; set; }
        public DateTime? BirthDay { get; set; }
        public GenderType? Gender { get; set; }
        public string PersonalyCode { get; set; }
    }

   
}
