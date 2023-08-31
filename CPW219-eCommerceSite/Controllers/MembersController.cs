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
                LogUserIn(newMember.Email);
                return RedirectToAction("Index", "Home");
            }
            return View(regModel);
        }
        [HttpGet]
        public IActionResult Login()
        {
            return View();
        }
        [HttpPost]
        public IActionResult Login(LoginViewModel logModel)
        {
            if (ModelState.IsValid)
            {
                Member? m = (from member in _conxtext.Members
                             where member.Email == logModel.Email &&
                             member.Password == logModel.Password
                             select member).SingleOrDefault();
                if (m != null)
                {
                    LogUserIn(logModel.Email);
                    return RedirectToAction("Index", "Home");
                }

                ModelState.AddModelError(string.Empty, "Credentials not found.");
            }
            return View(logModel);
        }

        private void LogUserIn(string email)
        {
            HttpContext.Session.SetString("Email", email);
        }

        public IActionResult Logout()
        {
            HttpContext.Session.Clear();
            return RedirectToAction("Index", "Home");
        }
    }
}
