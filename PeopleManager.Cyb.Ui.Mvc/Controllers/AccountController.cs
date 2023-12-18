using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using PeopleManager.Cyb.Ui.Mvc.Models;

namespace PeopleManager.Cyb.Ui.Mvc.Controllers
{
    public class AccountController(
        SignInManager<IdentityUser> signInManager,
        UserManager<IdentityUser> userManager) : Controller
    {
        [HttpGet]
        public async Task<IActionResult> SignIn(string returnUrl)
        {
            await signInManager.SignOutAsync();
            
            ViewBag.ReturnUrl = returnUrl;
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> SignIn(SignInModel model, string returnUrl)
        {

            if (string.IsNullOrWhiteSpace(returnUrl))
            {
                returnUrl = "/";
            }

            if (!ModelState.IsValid)
            {
                ViewBag.ReturnUrl = returnUrl;
                return View();
            }

            var result =
                await signInManager.PasswordSignInAsync(model.Username, model.Password, model.RememberMe, false);

            if (!result.Succeeded)
            {
                ModelState.AddModelError("", "User/Password combination is wrong.");

                ViewBag.ReturnUrl = returnUrl;
                return View();
            }

            return LocalRedirect(returnUrl);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> LogOut(string returnUrl)
        {
            if (string.IsNullOrWhiteSpace(returnUrl))
            {
                returnUrl = "/";
            }

            await signInManager.SignOutAsync();

            return LocalRedirect(returnUrl);
        }

        [HttpGet]
        public IActionResult Register(string returnUrl)
        {
            ViewBag.ReturnUrl = returnUrl;
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Register(RegisterModel model, string returnUrl)
        {
            if (string.IsNullOrWhiteSpace(returnUrl))
            {
                returnUrl = "/";
            }

            if (!ModelState.IsValid)
            {
                ViewBag.ReturnUrl = returnUrl;
                return View(model);
            }

            var user = new IdentityUser(model.Username);
            var result = await userManager.CreateAsync(user, model.Password);

            if (!result.Succeeded)
            {
                foreach (var error in result.Errors)
                {
                    ModelState.AddModelError("", error.Description);
                }
                ViewBag.ReturnUrl = returnUrl;
                return View(model);
            }

            await signInManager.SignInAsync(user, false);

            return LocalRedirect(returnUrl);
        }
    }
}
