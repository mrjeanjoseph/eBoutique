using System.Linq;
using System.Web.Mvc;
using YTP.Domain.SportsStore.Abstract;
using YTP.Domain.SportsStore.Entities;
using YTP.Main.Areas.SportsStore.Models;

namespace YTP.Main.Areas.SportsStore.Controllers {

    public class ProductController : Controller {
        private readonly IProductsRepository _productRepo;
        public int PageSize = 5;

        public ProductController(IProductsRepository productRepo) {
            _productRepo = productRepo;
        }

        // GET: SportsStore/Product
        public ViewResult ListProducts(string category, int page = 1) {

            ProductsList_VM viewModel = new ProductsList_VM {
                Products = _productRepo.Products
                .Where(p => category == null || p.Category == category)
                .OrderBy(p => p.ProductID)
                .Skip((page - 1) * PageSize)
                .Take(PageSize),

                PagingInfo = new PagingInfo {
                    CurrentPage = page,
                    ItemsPerPage = PageSize,
                    //TotalItems = _productRepo.Products.Count()
                    TotalItems = category == null ?
                    _productRepo.Products.Count() :
                    _productRepo.Products.Where(c => c.Category == category).Count() //
                },

                CurrentCategory = category,
            };

            return View(viewModel);

            //return View(_productRepo.Products
            //        .OrderBy(p => p.ProductID)
            //        .Skip((page - 1) * PageSize)
            //        .Take(PageSize));

            //return View(_productRepo.Products);
        }

        public FileContentResult GetImage(int productId) {
            Product prodImage = _productRepo.Products.FirstOrDefault(p => p.ProductID == productId);

            //if (prodImage != null)
            //    return File(prodImage.ImageData, prodImage.ImageMimeType);
            //else return null;

            //One liner
            return (prodImage != null) ? File(prodImage.ImageData, prodImage.ImageMimeType) : null;
        }
    }
}