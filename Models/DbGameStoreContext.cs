using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace LessonNetCore.Models
{
    public class DbGameStoreContext : DbContext
    {
        public DbSet<Game> Games { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer("Server=(localdb)\\MSSQLLocalDB;Database=GameStore;Trusted_Connection=True;");
        }
    }
}
