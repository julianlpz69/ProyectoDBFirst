using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Core.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Data.Configuration
{
    public class TeamDriverConfiguration : IEntityTypeConfiguration<TeamDriver>
    {
        public void Configure(EntityTypeBuilder<TeamDriver> builder){
    
            builder.ToTable("team_drivers");
    
            
    
        }
    }
}