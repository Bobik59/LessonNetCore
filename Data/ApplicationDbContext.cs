using LessonNetCore.Models;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;

namespace LessonNetCore.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        { }

        public DbSet<Game> Games { get; set; }
    }
}
