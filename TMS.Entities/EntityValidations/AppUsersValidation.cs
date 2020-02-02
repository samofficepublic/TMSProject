using System;
using System.Collections.Generic;
using System.Text;
using FluentValidation;
using TMS.Common.Enums;
using TMS.Common.Extensions;
using TMS.Entities.Entities;

namespace TMS.Entities.EntityValidations
{
    public class AppUsersValidation:AbstractValidator<AppUsers>
    {
        public AppUsersValidation()
        {
            RuleFor(x => x.FirstName).MaximumLength(100).WithMessage(EnumValidationMessage.MaximumLength.ToDisplay());
            RuleFor(x => x.LastName).MaximumLength(100).WithMessage(EnumValidationMessage.MaximumLength.ToDisplay());
            RuleFor(x => x.UserEmail).EmailAddress().WithMessage(EnumValidationMessage.EmailAddress.ToString());
            RuleFor(x => x.Password).NotNull().WithMessage(EnumValidationMessage.NotNull.ToDisplay());
            RuleFor(x => x.PersonalyCode).MaximumLength(10).WithMessage(EnumValidationMessage.MaximumLength.ToDisplay());
        }
    }
}
