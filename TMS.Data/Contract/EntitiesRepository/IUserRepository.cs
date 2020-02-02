
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using TMS.Entities.Entities;

namespace TMS.Data.Contract.EntitiesRepository
{
    public interface IUserRepository:IGenericRepository<AppUsers>
    {
        Task<AppUsers> GetByUserAndPass(string username, string password, CancellationToken cancellationToken);
        Task AddAsync(AppUsers users, string password, CancellationToken cancellationToken);
        Task UpdateSecurityStampAsync(AppUsers users, CancellationToken cancellationToken);
        Task UpdateLastLoginDateAsync(AppUsers users, CancellationToken cancellationToken);
    }
}
