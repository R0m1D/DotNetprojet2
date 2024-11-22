using System.Linq;
using Microsoft.AspNetCore.Mvc;
using P2FixAnAppDotNetCode.Models;
using P2FixAnAppDotNetCode.Models.Services;

namespace P2FixAnAppDotNetCode.Controllers
{
    public class CartController : Controller
    {
        private readonly ICart _cart;
        private readonly IProductService _productService;

        public CartController(ICart pCart, IProductService productService)
        {
            _cart = pCart;
            _productService = productService;
        }

        public ViewResult Index()
        {
            return View(_cart as Cart);
        }

        [HttpPost]
        public RedirectToActionResult AddToCart(int id)
        {
            Product product = _productService.GetProductById(id);
            int currentQuantity = _cart.GetQuantity(product);

            if (product != null)
            {
                if (currentQuantity >= product.Stock)
                {
                    TempData[$"ErrorNoStock_{id}"] = $"Il ne reste plus de stock pour {product.Name}.";
                    TempData["ErrorProductId"] = id;
                    return RedirectToAction("Index");

                }
                if (currentQuantity >= product.Stock - 1)
                {
                    TempData[$"ErrorNoStock_{id}"] = $"Il ne reste plus de stock pour {product.Name}.";
                    TempData["ErrorProductId"] = id;
                }
                _cart.AddItem(product, 1);
                return RedirectToAction("Index");
            }
            else
            {
                return RedirectToAction("Index", "Product");
            }
        }

        public RedirectToActionResult RemoveFromCart(int id)
        {
            Product product = _productService.GetAllProducts()
                .FirstOrDefault(p => p.Id == id);

            if (product != null)
            {
                _cart.RemoveLine(product);
            }
            return RedirectToAction("Index");
        }
    }
}
