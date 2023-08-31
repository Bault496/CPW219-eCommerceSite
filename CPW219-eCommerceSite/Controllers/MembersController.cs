using CPW219_eCommerceSite.Data;
using CPW219_eCommerceSite.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore.Query.Internal;
using System.ComponentModel.DataAnnotations;

namespace CPW219_eCommerceSite.Controllers
{
    public class MembersController : Controller
    {
        private readonly ComputerPartContext _conxtext;
        public MembersController(ComputerPartContext conxtext)
        {
            _conxtext = conxtext;
        }
        [HttpGet]
        public IActionResult Register()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Register(RegisterViewModel regModel)
        {
            if (ModelState.IsValid)
            {
                Member newMember = new()
                {
                    Email = regModel.Email,
                    Password = regModel.Password
                };
                _conxtext.Members.Add(newMember);
                await _conxtext.SaveChangesAsync();
                return RedirectToAction("Index", "Home");
            }
            return View(regModel);
        }
    }
}
