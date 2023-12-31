using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Core.Interfaces
{
    public interface IUnitOfWork
    {
        Task<int> SaveAsync();
        ITeam Teams { get; }
        IDriver Drivers { get; }
    }
}