using System.Collections.Generic;
using YTP.Domain.SportsStore.Abstract;
using YTP.Domain.SportsStore.Entities;

namespace YTP.Domain.SportsStore.Concrete {
    public class EFProductRepository : IProductsRepository {
        private EFDbContext _dbContext = new EFDbContext();

        public IEnumerable<Product> Products {
            get { return _dbContext.Products; }
        }
    }
}
