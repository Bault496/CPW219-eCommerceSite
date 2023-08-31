using CPW219_eCommerceSite.Data;
using CPW219_eCommerceSite.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace CPW219_eCommerceSite.Controllers
{
    public class PartsController : Controller
    {
        private readonly ComputerPartContext _context;
        public PartsController(ComputerPartContext context)
        {
            _context = context;
        }

        public async Task<IActionResult> Index(int? id)
        {
            const int NumPartsToDisplayPerPage = 3;
            const int PageOffset = 1;
            
            int currPage = id ?? 1; // set currPage to id unless it's null then it's 1.
            List<Part> parts = await (from part in _context.Parts 
                                      select part)
                                      .Skip(NumPartsToDisplayPerPage * (currPage - PageOffset))
                                      .Take(NumPartsToDisplayPerPage)
                                      .ToListAsync();
            return View(parts);
        }

        [HttpGet]
        public IActionResult Create()
        {
            return View(); 
        }
        [HttpPost]
        public async Task<IActionResult> Create(Part part)
        {
            if (ModelState.IsValid)
            {
                _context.Parts.Add(part); // prepares Insert
                await _context.SaveChangesAsync(); // executes pending insert
                //Show Success Message
                ViewData["message"] = $"{part.Name} was added sucessfully!";
                return View();
            }

            return View(part);
        }
        [HttpGet]
        public async Task<IActionResult> Edit(int id)
        {
            Part partToEdit = await _context.Parts.FindAsync(id);
            if (partToEdit == null)
            {
                return NotFound();
            }
            return View(partToEdit);
        }
        [HttpPost]
        public async Task<IActionResult> Edit(Part partModel)
        {
            if (ModelState.IsValid)
            {
                _context.Parts.Update(partModel);
                await _context.SaveChangesAsync();

                TempData["Message"] = $"{partModel.Name} was updated successfully!";
                return RedirectToAction("Index");
            }
            return View(partModel);
        }

        [HttpGet]
        public async Task<IActionResult> Delete(int id)
        {
            Part partToDelete = await _context.Parts.FindAsync(id);
            if (partToDelete == null)
            {
                return NotFound();
            }
            return View(partToDelete);
        }
        [HttpPost, ActionName("Delete")]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            Part partToDelete = await _context.Parts.FindAsync(id);

            if (partToDelete != null)
            {
                _context.Parts.Remove(partToDelete);
                await _context.SaveChangesAsync();
                TempData["Message"] = partToDelete.Name + " was deleted successfully!";
                return RedirectToAction("Index");
            }

            TempData["Message"] = "This part was already deleted.";
            return RedirectToAction("Index");
        }
        [HttpGet]
        public async Task<IActionResult> Details(int id)
        {
            Part partDetails = await _context.Parts.FindAsync(id);
            if (partDetails == null)
            {
                return NotFound();
            }
            return View(partDetails);
        }
    }
}
