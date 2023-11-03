using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Core.Interfaces;
using Infrastructure.Data;
using Infrastructure.Repository;

namespace Infrastructure.UnitOfWork
{
    public class UnitOfWork : IUnitOfWork
    {

        private readonly DBFirstContext _context; 
        private IDriver _Driver;
        private ITeam _Team;
        public UnitOfWork(DBFirstContext context){
            _context = context;
        }



        public ITeam Teams
        {
            get{
                if (_Team == null)
                {
                    _Team = new TeamRepository(_context);
                }
                return _Team;
            }
        }


        public IDriver Drivers
        {
            get{
                if (_Driver == null)
                {
                    _Driver = new DriverRepository(_context);
                }
                return _Driver;
            }
        }


        public void Dispose()
        {
            _context.Dispose();
        }


        public async Task<int> SaveAsync()
        {
            return await _context.SaveChangesAsync();
        }
    }
}