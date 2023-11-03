using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Core.Entities;
using Core.Interfaces;
using Infrastructure.Data;

namespace Infrastructure.Repository
{
    public class DriverRepository : GenericRepository<Driver> , IDriver
    {
        private readonly DBFirstContext _context;

        public DriverRepository(DBFirstContext context):base(context)
        {
            _context = context;
        }
    }
}