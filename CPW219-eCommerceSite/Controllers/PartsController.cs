﻿using CPW219_eCommerceSite.Data;
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

        public async Task<IActionResult> Index()
        {
            List<Part> parts = await (from part in _context.Parts select part).ToListAsync();
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

                return RedirectToAction("Index");
            }
            return View(partModel);
        }
    }
}
