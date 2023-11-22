using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using YTP.Domain.SportsStore.Entities;

namespace YTP.Domain.SportsStore.Abstract {
    public interface IProductsRepository {
        IEnumerable <Product> Products { get; }

        void SaveProduct(Product product);

        Product DeleteProduct(int productId);
    }
}
