using Microsoft.AspNetCore.Mvc;
using dz1.Services;

namespace dz1.Controllers
{
    public class CartController : Controller
    {
        private readonly AppDbContext _context;

        public CartController(AppDbContext context)
        {
            _context = context;
        }

        public IActionResult Index()
        {
            var cartItems = HttpContext.Session.CartItems();
            var products = _context.Products
                .Where(p => cartItems.Select(i => i.ProductId).Contains(p.Id))
                .ToList();
            return View(products);
        }

        public IActionResult RemoveFromCart(int productId)
        {
            var cartItems = HttpContext.Session.CartItems();
            var itemToRemove = cartItems.FirstOrDefault(i => i.ProductId == productId);
            if (itemToRemove != null)
            {
                cartItems.Remove(itemToRemove);
                HttpContext.Session.SetCartItems(cartItems);
            }
            return RedirectToAction("Index");
        }
    }
}
