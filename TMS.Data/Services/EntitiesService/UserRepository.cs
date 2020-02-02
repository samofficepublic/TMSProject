using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using TMS.Common.DependencyMarkers;
using TMS.Common.Exceptions;
using TMS.Common.Helpers;
using TMS.Data.Contract.EntitiesRepository;
using TMS.Entities.Entities;

namespace TMS.Data.Services.EntitiesService
{
    public class UserRepository:GenericRepository<AppUsers>,IUserRepository,IScopedDependency
    {
        public UserRepository(ApplicationDbContext dbContext):base(dbContext)
        {
            
        }

        public Task<AppUsers> GetByUserAndPass(string username, string password, CancellationToken cancellationToken)
        {
            var passwordHash = SecurityHelper.GetSha256Hash(password);
            return Table.Where(p => p.UserName == username && p.PasswordHash == passwordHash).SingleOrDefaultAsync(cancellationToken);
        }

        public Task UpdateSecurityStampAsync(AppUsers users, CancellationToken cancellationToken)
        {
            return UpdateAsync(users, cancellationToken);
        }

        public Task UpdateLastLoginDateAsync(AppUsers users, CancellationToken cancellationToken)
        {
            users.LastLoginDate = DateTimeOffset.Now;
            return UpdateAsync(users, cancellationToken);
        }

        public async Task AddAsync(AppUsers users, string password, CancellationToken cancellationToken)
        {
            var exists = await TableNoTracking.AnyAsync(p => p.UserName == users.UserName);
            if (exists)
                throw new BadRequestException("نام کاربری تکراری است");

            var passwordHash = SecurityHelper.GetSha256Hash(password);
            users.PasswordHash = passwordHash;
            await base.AddAsync(users, cancellationToken);
        }
    }
}
