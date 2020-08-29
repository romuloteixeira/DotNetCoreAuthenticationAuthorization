using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Security.Claims;
using System.Threading.Tasks;

namespace IdentityAuthentication.Controllers
{
    public class HomeController : Controller
    {
        private readonly UserManager<IdentityUser> userManager;
        private readonly SignInManager<IdentityUser> signInManager;

        public HomeController(
            UserManager<IdentityUser> userManager,
            SignInManager<IdentityUser> signInManager
            )
        {
            this.userManager = userManager;
            this.signInManager = signInManager;
        }

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

        public async Task<IActionResult> Login(string userName, string password)
        {
            var user = await userManager.FindByNameAsync(userName);

            bool hasUser = user != null;
            if (!hasUser)
            {
                return RedirectToAction("Index");
            }

            var signInResult = signInManager.PasswordSignInAsync(user, password, false, false);
            if (!signInResult.IsCompletedSuccessfully)
            {
                return RedirectToAction("Index"); // https://www.youtube.com/watch?v=IjbtWPXVJGw /// 25:00
            }

            return RedirectToAction("Index");
        }

        public IActionResult Register()
        {
            return View();
        }

        public async Task<IActionResult> Register(string userName, string password)
        {
            var user = new IdentityUser
            {
                UserName = userName,
                Email = string.Empty,
            };

            var identityResult = userManager.CreateAsync(user, password);

            if (identityResult.IsCompletedSuccessfully)
            {
                // sign user
            }

            return RedirectToAction("Index");
        }
    }
}
