using CPW219_eCommerceSite.Data;
using CPW219_eCommerceSite.Models;
using Microsoft.AspNetCore.Mvc;

namespace CPW219_eCommerceSite.Controllers
{
    public class CartController : Controller
    {
        private readonly ComputerPartContext _context;

        public CartController(ComputerPartContext context)
        {
            _context = context;
        }

        public IActionResult Add(int id)
        {
            Part? partToAdd = _context.Parts.Where(g => g.PartId == id).SingleOrDefault();

            if(partToAdd == null)
            {
                TempData["Message"] = "Sorry, that part no longer is available.";
                return RedirectToAction("Index", "Parts");
            }
            TempData["Message"] = "Item added to cart.";
            return RedirectToAction("Index", "Parts");
        }
    }
}
