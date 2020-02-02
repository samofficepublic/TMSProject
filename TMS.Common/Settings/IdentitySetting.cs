namespace TMS.Common.Settings
{
    public class IdentitySetting
    {
        public bool PasswordRequireDigit { get; set; }
        public int PasswordRequiredLength { get; set; }
        public bool PasswordRequireNonAlphanumic { get; set; }
        public bool PasswordRequireUppercase { get; set; }
        public bool PasswordRequireLowercase { get; set; }
        public bool RequireUniqueEmail { get; set; }
        public int MaxFailedAccessAttempts { get; set; }
        public double DefaultLockoutTimeSpan { get; set; }
    }
}