using System.Data.Entity;
using YTP.Domain.SportsStore.Entities;

namespace YTP.Domain.SportsStore.Concrete {
    public class EFDbContext : DbContext {
        public DbSet<Product> Products { get; set; }
    }
}
