using System;
using System.Collections.Generic;
using System.Reflection;
using Core.Entities;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Data;

public partial class DBFirstContext : DbContext
{
    public DBFirstContext()
    {
    }

    public DBFirstContext(DbContextOptions<DBFirstContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Driver> Drivers { get; set; }

    public virtual DbSet<Team> Teams { get; set; }

    
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder
            .UseCollation("utf8mb4_0900_ai_ci")
            .HasCharSet("utf8mb4");

            modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());

       

    

    }

}
