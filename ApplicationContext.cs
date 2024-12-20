using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using пробник.Model;

namespace пробник
{
     public class ApplicationContext : DbContext
    {
        public DbSet<Note> Notes { get; set; } = null!;
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlite("Data Source=Calendar.db");
        }
    }
}
