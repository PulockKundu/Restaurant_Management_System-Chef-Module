using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Mvc;
using RestaurantManagement.Repos;
using System.Security.Claims;
using RestaurantManagement.Models;

namespace RestaurantManagement.Web.Controllers
{
    public class AuthController(UserInfoRepo repo) : Controller
    {
        public IActionResult Login()
        {
            return View(new LoginModel());
        }

        [HttpPost]
        public async Task<IActionResult> Login(LoginModel model)
        {
            if (ModelState.IsValid == false)
                return View(model);

            var result = repo.Authenticate(model.Email, model.Password);
            if (result.HasError || result.Data == null)
            {
                ViewBag.Error = result.Message;
                return View(model);
            }

            var claims = new List<Claim>()
            {
                new Claim(ClaimTypes.Name, result.Data.Name),
                new Claim(ClaimTypes.Role, result.Data.Role),
                new Claim("Email", result.Data.Email),
                new Claim("UserId", result.Data.ID.ToString())
            };
            var identity = new ClaimsIdentity(claims, "RmAuth");
            var principal = new ClaimsPrincipal(identity);
            await HttpContext.SignInAsync("RmAuth", principal);

            switch (result.Data.Role)
            {
                case "admin":
                case "chef":
                    return RedirectToAction("Index", "Home");
                case "customer":
                    return RedirectToAction("Denied");
                    //return RedirectToAction("Index", "CustomerHome");
                default:
                    return RedirectToAction("Denied");

            }

        }

        public async Task<IActionResult> Logout()
        {
            await HttpContext.SignOutAsync("RmAuth");
            return RedirectToAction("Login");
        }

        public IActionResult Denied()
        {
            return View();
        }
    }
}
