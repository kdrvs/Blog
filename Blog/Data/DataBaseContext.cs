using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Pomelo.EntityFrameworkCore.MySql.Storage;

namespace Blog.Data
{
    public class DataBaseContext : DbContext
    {
        private string connectionString = Settings.ConnectionString;

        public DbSet<Post> Posts { get; set; }
        public DbSet<Comment> Comments { get; set; }

        

        public DataBaseContext()
        {
            //Database.EnsureDeleted();
            Database.EnsureCreated();
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if(!optionsBuilder.IsConfigured)
            {
                optionsBuilder.UseMySql(connectionString, ServerVersion.AutoDetect(connectionString))
                    .EnableDetailedErrors();
            }
           
        }

        



    }
}
