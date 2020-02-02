
using System.Runtime.InteropServices.WindowsRuntime;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Diagnostics;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using TMS.Data;

namespace TMS.Framework.ServiceCollectionExtension
{
    public static class AddContext
    {
        public static void AddDbContext(this IServiceCollection service, IConfiguration configuration)
        {
            service.AddDbContext<ApplicationDbContext>(option =>
            {
                option.UseSqlServer(configuration.GetConnectionString("DefaultConnection"))
                    .ConfigureWarnings(war => war.Throw(RelationalEventId.QueryClientEvaluationWarning));
            });
        }
    }
}