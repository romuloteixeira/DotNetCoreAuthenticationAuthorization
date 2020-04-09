using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace Basics.Controllers
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

        public IActionResult Authenticate()
        {
            HttpContextSignIn();

            return RedirectToAction("Index");
        }

        private void HttpContextSignIn()
        {
            var claimsPrincipal = CreateClaims();
            HttpContext.SignInAsync(claimsPrincipal);
        }

        private static ClaimsPrincipal CreateClaims()
        {
            var companyClaimsIdentity = CreateCompanyClaimsIdentity();
            var licenseClaimsIdentity = CreateGovernmetLicenseClaimsIdentity();
            var userClaimsPrincipal = new ClaimsPrincipal(new[] { companyClaimsIdentity, licenseClaimsIdentity });

            return userClaimsPrincipal;
        }

        private static ClaimsIdentity CreateCompanyClaimsIdentity()
        {
            var mainCompanyClaims = CreateMainCompanyClaims();
            var companyClaimsIdentity = new ClaimsIdentity(mainCompanyClaims, "Main Company Identity");
            return companyClaimsIdentity;
        }

        private static ClaimsIdentity CreateGovernmetLicenseClaimsIdentity()
        {
            var licenseClaims = CreateLicenseClaims();
            var licenseClaimsIdentity = new ClaimsIdentity(licenseClaims, "Government");
            return licenseClaimsIdentity;
        }

        private static List<Claim> CreateMainCompanyClaims()
        {
            var claimName = new Claim(ClaimTypes.Name, "Romulo");
            var claimEmail = new Claim(ClaimTypes.Email, "romulo@mycompany.com");
            var claimSay = new Claim("MainCompany.Says", "CEO");
            var claimsMainCompany = new List<Claim>
            {
                claimName,
                claimEmail,
                claimSay,
            };
            return claimsMainCompany;
        }

        private static List<Claim> CreateLicenseClaims()
        {
            var claimName = new Claim(ClaimTypes.Name, "Romulo Teixeira");
            var claimLicenseCategory = new Claim("DrivingLicenseCategory", "B;C;");
            var claimsMainCompany = new List<Claim>
            {
                claimName,
                claimLicenseCategory,
            };
            return claimsMainCompany;
        }
    }
}
