using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace TMS.Data.Contract
{
    public interface IUnitOfWork
    {
        ApplicationDbContext ApplicationDbContext { get; }
        bool Save();
        Task<bool> SaveAsync(CancellationToken cancellationToken);
        void Dispose(bool disposing);
        void DisposeAsync(bool disposing);
    }
}
