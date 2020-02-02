using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using TMS.Data.Contract;

namespace TMS.Data.Services
{
    public class UnitOfWork : IUnitOfWork
    {
        public ApplicationDbContext _applicationDbContext { get; private set; }

        public ApplicationDbContext ApplicationDbContext => throw new NotImplementedException();

        public UnitOfWork(ApplicationDbContext applicationDbContext)
        {
            _applicationDbContext = applicationDbContext;
        }

        public bool Save()
        {
            try
            {
                _applicationDbContext.SaveChanges();
                return true;
            }
            catch
            {
                return false;
            }
        }

        public async Task<bool> SaveAsync(CancellationToken cancellationToken)
        {
            try
            {
                var saveResult = await _applicationDbContext.SaveChangesAsync(cancellationToken);
                return await Task.FromResult(true);
            }
            catch (Exception e)
            {
                return await Task.FromResult(false);
            }

        }


        private bool disposed = false;

        public virtual void Dispose(bool disposing)
        {
            if (!this.disposed)
            {
                if (disposing)
                {
                    _applicationDbContext.Dispose();
                }
            }
            this.disposed = true;
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }
         

        public void DisposeAsync(bool disposing)
        {
            if (!this.disposed)
            {
                if (disposing)
                {
                    _applicationDbContext.Dispose();
                }
            }
            this.disposed = true;
        }
        public void DisposeSync()
        {
            DisposeAsync(true);
            GC.SuppressFinalize(this);
        }
    }
}
