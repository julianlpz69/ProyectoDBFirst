using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Core.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Data.Configuration
{
    public class TeamConfiguration : IEntityTypeConfiguration<Team>
    {
        public void Configure(EntityTypeBuilder<Team> builder){
    
            

          
            builder.HasKey(e => e.Id).HasName("PRIMARY");

            builder.ToTable("teams");

            builder.Property(e => e.Id).HasColumnName("id");
            builder.Property(e => e.Name)
                .HasMaxLength(255)
                .HasColumnName("name");


                

            builder.HasMany(e => e.Drivers)
            .WithMany(p => p.Teams)
            .UsingEntity<TeamDriver>(
                j =>{
                 j
                    .HasOne(ep => ep.Driver)
                    .WithMany(e => e.TeamDrivers)
                    .HasForeignKey(ep => ep.driver_id);
                 j
                    .HasOne(ep => ep.Team)
                    .WithMany(p => p.TeamDrivers)
                    .HasForeignKey(ep => ep.team_id);

                    j.HasKey(ep => new { ep.driver_id , ep.team_id});
                }
            );
    
    
        }
    }
}