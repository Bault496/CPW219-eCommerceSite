using CPW219_eCommerceSite.Data;
using CPW219_eCommerceSite.Models;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace CPW219_eCommerceSite.Controllers
{
    public class CartController : Controller
    {
        private readonly ComputerPartContext _context;
        private const string Cart = "ShoppingCart";

        public CartController(ComputerPartContext context)
        {
            _context = context;
        }

        public IActionResult Add(int id)
        {
            Part? partToAdd = _context.Parts.Where(g => g.PartId == id).SingleOrDefault();

            if (partToAdd == null)
            {
                TempData["Message"] = "Sorry, that part no longer is available.";
                return RedirectToAction("Index", "Parts");
            }

            CartPartViewModel cartPart = new()
            {
                PartId = partToAdd.PartId,
                Name = partToAdd.Name,
                Price = partToAdd.Price
            };

            List<CartPartViewModel> cartParts = GetExistingCartData();
            cartParts.Add(cartPart);
            WriteShoppingCartCookie(cartParts);
            TempData["Message"] = "Item added to cart.";
            return RedirectToAction("Index", "Parts");
        }

        private void WriteShoppingCartCookie(List<CartPartViewModel> cartParts)
        {
            string cookieData = JsonConvert.SerializeObject(cartParts);

            HttpContext.Response.Cookies.Append(Cart, cookieData, new CookieOptions()
            {
                Expires = DateTimeOffset.Now.AddYears(1)
            });
        }

        /// <summary>
        /// return the current list of parts in the user's shopping cart
        /// If there is no cookie, an empty list
        /// </summary>
        /// <returns></returns>
        /// <exception cref="NotImplementedException"></exception>
        private List<CartPartViewModel> GetExistingCartData()
        {
            string? cookie = HttpContext.Request.Cookies[Cart];
            if (string.IsNullOrWhiteSpace(cookie))
            {
                return new List<CartPartViewModel>();
            }

            return JsonConvert.DeserializeObject<List<CartPartViewModel>>(cookie);
        }
        public IActionResult Summary()
        {
            List<CartPartViewModel> cartParts = GetExistingCartData();
            return View(cartParts);
        }

        public IActionResult Remove(int id)
        {
            List<CartPartViewModel> cartParts = GetExistingCartData();
            CartPartViewModel? targetPart = cartParts.Where(p => p.PartId == id).FirstOrDefault();

            cartParts.Remove(targetPart);
            WriteShoppingCartCookie(cartParts);

            return RedirectToAction("Summary");
        }
    }
}
