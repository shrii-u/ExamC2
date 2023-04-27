using ExamC2Web.Data;
using ExamC2Web.Models;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;

namespace ExamC2Web.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly ApplicationDbContext _db;

        public HomeController(ILogger<HomeController> logger,ApplicationDbContext db)
        {
            _logger = logger;
            _db = db;
        }

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
        public IActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Login(Admin model)
        {

            var User =  _db.Admins.Where(s => s.AdminEmail.Contains(model.AdminEmail));
            if (User.Count() != 0)
            {
                if (User.First().AdminPasswords == model.AdminPasswords)
                {
                    TempData["success"] = "Admin login successfully";
                    return RedirectToAction("Index","student");
                }
            }
            
            return RedirectToAction("Login");
        }
    }
}