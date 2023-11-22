using System.Collections.Generic;
using YTP.Domain.SportsStore.Abstract;
using YTP.Domain.SportsStore.Entities;

namespace YTP.Domain.SportsStore.Concrete {
    public class EFProductRepository : IProductsRepository {
        private readonly EFDbContext _dbContext = new EFDbContext();

        public IEnumerable<Product> Products {
            get { return _dbContext.Products; }
        }

        public void SaveProduct(Product product) {
            if(product.ProductID == 0)
                _dbContext.Products.Add(product);
            else {
                Product dbproductentry = _dbContext.Products.Find(product.ProductID);
                if(dbproductentry != null ) {
                    dbproductentry.ProductName = product.ProductName;
                    dbproductentry.Description = product.Description;
                    dbproductentry.ProductPrice = product.ProductPrice;
                    dbproductentry.Category = product.Category;
                }
            }
            _dbContext.SaveChanges();
        }
    }
}
