using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using WishList.Models;
using WishList.Models.AccountViewModels;

// For more information on enabling MVC for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace WishList.Controllers
{
	[Authorize]
    public class AccountController : Controller
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly SignInManager<ApplicationUser> _signInManager;

		public AccountController(UserManager<ApplicationUser> userManager, SignInManager<ApplicationUser> signInManager)
        {
            _userManager = userManager;
            _signInManager = signInManager;
        }

        // GET: /<controller>/
        public IActionResult Index()
        {
            return View();
        }

		[HttpGet]
		[AllowAnonymous]
		public IActionResult Register()
        {
            return View("Register");
        }

        [HttpPost]
        [AllowAnonymous]
        public IActionResult Register(RegisterViewModel user)
        {
            if (!ModelState.IsValid)
            {
                return View("Register", user);
            }

			var result = _userManager.CreateAsync(new ApplicationUser()
            {
				Email = user.Email,
				UserName = user.Email
            }, user.Password).Result;

            if (!result.Succeeded)
            {
				foreach(var error in result.Errors)
                {
                    ModelState.AddModelError("Password", error.Description);
                }
                return View("Register", user);
            }

            return RedirectToAction("Index", "Home");
        }
    }
}
