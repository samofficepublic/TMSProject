using System.Threading.Tasks;
using TMS.Data.Services;
using TMS.Entities.Entities;

namespace TMS.Data.Contract
{
    public interface IJwtService
    {
        Task<AccessToken> GenerateAsync(AppUsers users);
    }
}