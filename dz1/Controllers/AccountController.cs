using Microsoft.AspNetCore.Mvc;
using dz1.Models;
using dz1.ViewModels;

namespace dz1.Controllers
{
    public class AccountController : Controller
    {
        private readonly AppDbContext _context;

        public AccountController(AppDbContext context)
        {
            _context = context;
        }

        public IActionResult Register()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Register(RegisterViewModel model)
        {
            if (ModelState.IsValid)
            {
                // Check if username or email already exists
                if (_context.Users.Any(u => u.UserName == model.UserName || u.Email == model.Email))
                {
                    ModelState.AddModelError("", "Користувач з таким ім'ям або email вже існує");
                    return View(model);
                }

                var user = new User
                {
                    FirstName = model.FirstName,
                    LastName = model.LastName,
                    UserName = model.UserName,
                    Email = model.Email,
                    Password = model.Password, // In production, hash this password
                    CreatedAt = DateTime.Now
                };

                _context.Users.Add(user);
                _context.SaveChanges();

                TempData["Success"] = "Реєстрація успішна!";
                return RedirectToAction("Index", "Home");
            }

            return View(model);
        }
    }
}
