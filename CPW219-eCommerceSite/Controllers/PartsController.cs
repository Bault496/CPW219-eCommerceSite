using CPW219_eCommerceSite.Data;
using CPW219_eCommerceSite.Models;
using Microsoft.AspNetCore.Mvc;

namespace CPW219_eCommerceSite.Controllers
{
    public class PartsController : Controller
    {
        private readonly ComputerPartContext _context;
        public PartsController(ComputerPartContext context)
        {
            _context = context;
        }

        [HttpGet]
        public IActionResult Create()
        {
            return View(); 
        }
        [HttpPost]
        public IActionResult Create(Part part)
        {
            if (ModelState.IsValid)
            {
                _context.Parts.Add(part); // prepares Insert
                _context.SaveChanges(); // executes pending insert
                //Show Success Message
                ViewData["message"] = $"{part.Name} was added sucessfully!";
                return View();
            }

            return View(part);
        }
    }
}
