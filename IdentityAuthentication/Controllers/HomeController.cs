using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Security.Claims;

namespace IdentityAuthentication.Controllers
{
    public class HomeController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }

        [Authorize]
        public IActionResult Secret()
        {
            return View();
        }

        public IActionResult Login()
        {
            return View();
        }

        public IActionResult Login(string userName, string password)
        {
            // login functionality
            return RedirectToAction("Index");
        }

        public IActionResult Register()
        {
            return View();
        }

        public IActionResult Register(string userName, string password)
        {
            // register functionality
            return RedirectToAction("Index");
        }
    }
}
