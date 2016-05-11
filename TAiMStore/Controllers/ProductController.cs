using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using TAiMStore.Domain;
using TAiMStore.Model;
using TAiMStore.Model.Repository;
using TAiMStore.Model.ViewModels;

namespace TAiMStore.Controllers
{
    public class ProductController : Controller
    {
        // GET: Product
        protected readonly IProductRepository _repository;
        protected readonly ICategoryRepository _categoryRepository;
        public int pageSize = 4;

        public ProductController(IProductRepository productRepository, ICategoryRepository categoryRepository)
        {
            _repository = productRepository;
            _categoryRepository = categoryRepository;
            _categoryRepository.GetAll();
        }

        public ViewResult List(string category, int page = 1)
        {
            var products = _repository.GetMany(p => category == null || p.Category.Name == category)
                .OrderBy(p => p.Id)
                .Skip((page - 1) * pageSize)
                .Take(pageSize);
            var productsViewModel = new List<ProductViewModel>();

            foreach (var product in products)
            {
                var productViewModel = new ProductViewModel();
                productViewModel.EntityToProductViewModel(product);
                productsViewModel.Add(productViewModel);
            }

            ProductsViewModel model = new ProductsViewModel
            {
                Products = productsViewModel,
                PagingInfo = new PagingInfo
                {
                    CurrentPage = page,
                    ItemsPerPage = pageSize,
                    TotalItems = category == null ?
                    _repository.GetCount():
                    _repository.GetMany(p => p.Category.Name == category).Count()
                },
                CurrentCategory = category
            };
            return View(model);
        }

        public ViewResult Detail(int Id)
        {

            Product product = _repository.GetById(Id);
            return View(product);
        }

        public FileContentResult GetImage(int Id)
        {
            Product product = _repository.GetById(Id);

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