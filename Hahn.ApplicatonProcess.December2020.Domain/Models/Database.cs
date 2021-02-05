using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;


namespace Hahn.ApplicatonProcess.December2020.Domain
{
    public class Database : DbContext
    {
        public DbSet<ModelDB> DBCON { get; set; }
     
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer("Data Source =.; Initial Catalog = ClientDb; Integrated Security = True");
        }
    }
}
