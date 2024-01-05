using Microsoft.EntityFrameworkCore;
using MVC_Rehearsals.Models;

namespace ImportExcelFile.Controllers {
    internal class EmployeeDbContext : DbContext {
        public EmployeeDbContext() {
        }
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder) {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder) {
        }
        //entities
        public DbSet<Employee> Students { get; set; }
    }
}