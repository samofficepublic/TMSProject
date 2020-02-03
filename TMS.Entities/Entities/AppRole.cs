using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.AspNet.Identity.EntityFramework;
using Microsoft.AspNetCore.Identity;
using TMS.Entities.Common;

namespace TMS.Entities.Entities
{
    public class AppRole:IdentityRole<Guid>,IEntity
    {
        public string Description { get; set; }
    }
}
