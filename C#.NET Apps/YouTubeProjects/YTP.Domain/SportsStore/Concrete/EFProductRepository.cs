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
                    dbproductentry.ImageData = product.ImageData;
                    dbproductentry.ImageMimeType = product.ImageMimeType;
                }
            }
            _dbContext.SaveChanges();
        }

        public Product DeleteProduct(int productId) {
            Product dbproductentry = _dbContext.Products.Find(productId);
            if(dbproductentry != null) {
                _dbContext.Products.Remove(dbproductentry);
                _dbContext.SaveChanges();
            }
            return dbproductentry;
        }
    }
}
