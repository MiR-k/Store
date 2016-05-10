using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using TAiMStore.Domain;
using TAiMStore.Model;
using TAiMStore.Model.Abstract;
using TAiMStore.Model.ViewModels;

namespace TAiMStore.Controllers
{
    public class ProductController : Controller
    {
        // GET: Product
        private IProductRepository _repository;
        public int pageSize = 4;

        public ProductController(IProductRepository productRepository)
        {
            _repository = productRepository;
        }

        public ViewResult List(string category, int page = 1)
        {
            ProductsViewModel model = new ProductsViewModel
            {
                Products = _repository.Products
                .Where(p => category == null || p.Category == category)
                .OrderBy(p => p.Id)
                .Skip((page - 1) * pageSize)
                .Take(pageSize),
                PagingInfo = new PagingInfo
                {
                    CurrentPage = page,
                    ItemsPerPage = pageSize,
                    TotalItems = category == null ?
                    _repository.Products.Count():
                    _repository.Products.Where(p => category == null || p.Category == category).Count()
                },
                CurrentCategory = category
            };
            return View(model);
        }

        public ViewResult Detail(int Id)
        {

            Product product = _repository.Products
                .FirstOrDefault(p => p.Id == Id);
            return View(product);
        }

        public FileContentResult GetImage(int Id)
        {
            Product product = _repository.Products
                .FirstOrDefault(p => p.Id == Id);

            if (product != null)
            {
                return File(product.ImageData, product.ImageMimeType);
            }
            else
            {
                return null;
            }
        }
    }
}