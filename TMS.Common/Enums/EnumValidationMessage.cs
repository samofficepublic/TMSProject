using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace TMS.Common.Enums
{
    public enum EnumValidationMessage
    {
        [Display(Name = "تعداد کاراکتر غیر مجاز")]
        MaximumLength,
        [Display(Name = "مقدار الزامی است")]
        NotNull,
        [Display(Name = "آدرس ایمیل نامعتبر است")]
        EmailAddress
    }
}
