using System.Web.Mvc;
using System.Linq;
using TAiMStore.Domain;
using TAiMStore.Model.Repository;
using System.Web;

namespace TAiMStore.WebUI.Controllers
{
    [Authorize]
    public class AdminController : Controller
    {
        IProductRepository _repository;


        public AdminController(IProductRepository productRepository)
        {
            _repository = productRepository;
        }

        public ViewResult Index()
        {
            return View(_repository.GetAll());
        }

        public ViewResult Edit(int Id)
        {
            Product product = _repository.GetById(Id);
            return View(product);
        }

        [HttpPost]
        public ActionResult Edit(Product product, HttpPostedFileBase image = null)
        {
            if (ModelState.IsValid)
            {
                if (image != null)
                {
                    product.ImageMimeType = image.ContentType;
                    product.ImageData = new byte[image.ContentLength];
                    image.InputStream.Read(product.ImageData, 0, image.ContentLength);
                }
                _repository.Update(product);
                TempData["message"] = string.Format("Изменения товара \"{0}\" сохранены", product.Name);
                return RedirectToAction("Index");
            }
            else
            {
                // Что-то не так со значениями данных
                return View(product);
            }
        }

        public ViewResult Create()
        {
            return View("Edit", new Product());
        }

        [HttpPost]
        public ActionResult Delete(int Id)
        {
            Product deletedProduct = _repository.GetById(Id);
            _repository.Delete(deletedProduct);
            if (deletedProduct != null)
            {
                TempData["message"] = string.Format("Товар \"{0}\" удален",
                    deletedProduct.Name);
            }
            return RedirectToAction("Index");
        }

    }
}