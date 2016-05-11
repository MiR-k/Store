using System.Linq;
using System.Web.Mvc;
using TAiMStore.Model.Interfaces;
using TAiMStore.Model.Repository;
using TAiMStore.Model.ViewModels;

namespace TAiMStore.WebUI.Controllers
{
    public class CartController : Controller
    {
        //
        // GET: /Cart/
        private IProductRepository _productRepository;
        private IOrderRepository _orderRepository;

        public CartController(IProductRepository repo, IOrderRepository orderRepository)
        {
            _productRepository = repo;
            _orderRepository = orderRepository;
        }

        public ViewResult Index(Cart cart, string returnUrl)
        {
            return View(new CartIndexViewModel
            {
                Cart = cart,
                ReturnUrl = returnUrl
            });
        }

        public RedirectToRouteResult AddToCart(Cart cart, int Id, string returnUrl)
        {
            var product = _productRepository.GetById(Id);

            if (product != null)
            {
                cart.AddItem(product, 1);
            }
            return RedirectToAction("Index", new { returnUrl });
        }

        public RedirectToRouteResult RemoveFromCart(Cart cart, int Id, string returnUrl)
        {
            var product = _productRepository.GetById(Id);

            if (product != null) cart.RemoveLine(product);

            return RedirectToAction("Index", new { returnUrl });
        }

        public PartialViewResult Summary(Cart cart)
        {
            return PartialView(cart);
        }

        public ViewResult CheckOut()
        {
            return View(new ShippingDetails());
        }

        [HttpPost]
        public ViewResult Checkout(Cart cart, ShippingDetails shippingDetails)
        {
            if (cart.Lines.Count() == 0)
            {
                ModelState.AddModelError("", "Sorry, your cart is empty!");
            }
            if (ModelState.IsValid)
            {
                _orderRepository.ProcessOrder(cart, shippingDetails);
                cart.Clear();
                return View("Completed");
            }
            else
            {
                return View(shippingDetails);
            }
        }
	}
}