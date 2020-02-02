using System;
using System.Collections.Generic;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using FluentValidation;
using TMS.Common.Enums;
using TMS.Common.Extensions;
using TMS.Entities.Entities;

namespace TMS.Entities.EntityValidations
{
    public class AppRoleValidation:AbstractValidator<AppRole>
    {
        public AppRoleValidation()
        {
            RuleFor(x => x.Name).MaximumLength(50).WithMessage(EnumValidationMessage.MaximumLength.ToDisplay());
        }
    }
}
