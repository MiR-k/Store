using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using TAiMStore.Model;
using TAiMStore.Model.Abstract;
using TAiMStore.Model.ViewModels;

namespace TAiMStore.Controllers
{
    public class ProductController : Controller
    {
        // GET: Product
        private IProductRepository repository;
        public int pageSize = 4;
        public ProductController(IProductRepository repo)
        {
            repository = repo;
        }

        public ViewResult List(int page = 1)
        {
            ProductsViewModel model = new ProductsViewModel
            {
                Products = repository.Products
                .OrderBy(prod => prod.Id)
                .Skip((page-1)*pageSize)
                .Take(pageSize),
                PagingInfo = new PagingInfo
                {
                    CurrentPage = page,
                    ItemsPerPage = pageSize,
                    TotalItems = repository.Products.Count()
                }                
            };  
            return View(model);
        }
    }
}