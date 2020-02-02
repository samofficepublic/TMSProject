using System;
using System.Collections.Generic;
using System.Text;
using FluentValidation;
using TMS.Entities.Entities;

namespace TMS.Entities.EntityValidations
{
    public class AppUsersValidation:AbstractValidator<AppUsers>
    {
        public AppUsersValidation()
        {
            RuleFor(x => x.FirstName).MaximumLength(100).WithMessage("نام طولانی است");
            RuleFor(x => x.LastName).MaximumLength(100).WithMessage("نام خانوادگی طولانی است");
            RuleFor(x => x.UserEmail).EmailAddress().WithMessage("ایمیل نامعتبر است");
            RuleFor(x => x.Password).NotNull().WithMessage("رمز عبور وارد نشده است");
            RuleFor(x => x.PersonalyCode).MaximumLength(10).WithMessage("کد پرسنلی نامعتبر است");
        }
    }
}
