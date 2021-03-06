using Microsoft.EntityFrameworkCore.Storage;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NLayerCore.UnitOfWork
{
    public interface IUnitOfWork
    {
        Task Commit();
        IDbContextTransaction BeginTransaction();
    }
}
