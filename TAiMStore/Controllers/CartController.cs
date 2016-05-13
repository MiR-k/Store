using System.Linq;
using System.Web.Mvc;
using TAiMStore.Domain;
using TAiMStore.Model.Repository;
using TAiMStore.Model.ViewModels;

namespace TAiMStore.WebUI.Controllers
{
    public class CartController : Controller
    {
        private IProductRepository _repository;
        //private IOrderProcessor _orderProcessor;

        public CartController(IProductRepository productRepository)
        {
            _repository = productRepository;
        }

        public ViewResult Index(Cart cart,string returnUrl)
        {
            return View(new CartIndexViewModel
            {
                Cart = GetCart(),
                ReturnUrl = returnUrl
            });
        }

        public RedirectToRouteResult AddToCart(int Id, string returnUrl)
        {
            Product product = _repository.GetById(Id);

            if (product != null)
            {
                GetCart().AddItem(product, 1);
            }
            return RedirectToAction("Index", new { returnUrl });
        }

        public RedirectToRouteResult RemoveFromCart(int Id, string returnUrl)
        {
            Product product = _repository.GetById(Id);

            if (product != null)
            {
                GetCart().RemoveLine(product);
            }
            return RedirectToAction("Index", new { returnUrl });
        }

        public PartialViewResult Summary(Cart cart)
        {
            return PartialView(GetCart());
        }

        public ViewResult Checkout()
        {
            return View(new ShippingDetails());
        }

        //[HttpPost]
        //public ViewResult Checkout(Cart cart, ShippingDetails shippingDetails)
        //{
        //    if (cart.Lines.Count() == 0)
        //    {
        //        ModelState.AddModelError("", "Извините, ваша корзина пуста!");
        //    }

        //    if (ModelState.IsValid)
        //    {
        //        _orderProcessor.ProcessOrder(cart, shippingDetails);
        //        cart.Clear();
        //        return View("Completed");
        //    }
        //    else
        //    {
        //        return View(shippingDetails);
        //    }
        //}

        public Cart GetCart()
        {
            Cart cart = (Cart)Session["Cart"];
            if (cart == null)
            {
                cart = new Cart();
                Session["Cart"] = cart;
            }
            return cart;
        }
	}
}