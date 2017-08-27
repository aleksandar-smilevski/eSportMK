using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CryptoHelper;
using eSportMK.Data;
using eSportMK.ViewModels.Account;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using Microsoft.EntityFrameworkCore;
using eSportMK.Database;
using eSportMk.Services;
using eSportMK.Dto;
using Microsoft.AspNetCore.Http;

namespace eSportMK.Controllers
{
    public class AccountController : Controller
    {
        private readonly IUserService _userService;

        public AccountController(IUserService userService)
        {
            _userService = userService;
        }

        

        [HttpGet]
        public IActionResult Login(string ReturnUrl="")
        {
            ViewData["ReturnUrl"] = ReturnUrl;
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Login(LoginViewModel model, string ReturnUrl="")
        {
            if (ModelState.IsValid)
            {
                var user = _userService.GetUserByUserName(model.UserName);
                if (user == null || !Crypto.VerifyHashedPassword(user.PasswordHash, model.Password))
                {
                    ModelState.AddModelError("","Invalid login attempt");
                    return View(model);
                }
                var claims = new List<Claim>
                {
                    new Claim(ClaimTypes.Name, model.UserName)
                };

                var userIdentity = new ClaimsIdentity(claims, "login");

                ClaimsPrincipal principal = new ClaimsPrincipal(userIdentity);
                await HttpContext.Authentication.SignInAsync("Cookies", principal);
                `
            }
            return View();
        }

        [HttpGet]
        public IActionResult Register()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Register(RegisterViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }

            var newUser = new CreateUserDto()
            {
                UserName = model.UserName,
                Email = model.Email,
                FirstName = model.FirstName,
                LastName = model.LastName,
                Password = Crypto.HashPassword(model.Password),
                DateOfBirth = model.DateOfBirth,
            };
            try
            {
                _userService.Create(newUser);
                return RedirectToAction("Index","Home");
            }
            catch (DbUpdateException)
            {
                ModelState.AddModelError("", "Unable to save changes! Try again");
            }
            return View(model);
        }

        [HttpGet]
        public async Task<IActionResult> LogoutAsync()
        {
            await HttpContext.Authentication.SignOutAsync("Cookies");
            return RedirectToAction("Login");
        }
    }
}