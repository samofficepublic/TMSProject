using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.AspNetCore.Mvc;
using TMS.Framework.Filters;

namespace TMS.Framework.Api
{
    [ApiController]
    [ApiResultFilter]
    //[Route("api/v{version:apiVersion}/[controller]")]
    [Route("api/[controller]")]
    public class CustomBaseController:ControllerBase
   {
       public bool isAuthenticated => HttpContext.User.Identity.IsAuthenticated;
   }
}
