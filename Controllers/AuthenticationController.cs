using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using Microsoft.AspNetCore.Authentication;

namespace Blog.Controllers
{
    public class AuthenticationController: Controller
    {

        [HttpGet]
        public IActionResult LogIn()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> LogIn(string password)
        {
            
            if (await Settings.checkPassword(password))
            {

                var claims = new List<Claim>
                {
                    new Claim(ClaimTypes.Role, "Admin"),

                };
                var userIdentity = new ClaimsIdentity(claims, "login");
                var principal = new ClaimsPrincipal(userIdentity);
                await HttpContext.SignInAsync(principal);
                return RedirectToAction("All", "Console");
            }

            return View();
        }

        public async Task<IActionResult> LogOut()
        {
            await HttpContext.SignOutAsync();
            return RedirectToAction("Index", "Blog");
        }

    }
}
