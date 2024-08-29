
using DemoMvcAgain.PL.Models;
using Hospital.DAL.Entites;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using System.Threading.Tasks;

namespace DemoMvcAgain.PL.Controllers.Account
{
    public class AccountController : Controller
    {
		private readonly UserManager<ApplicationUser> userManager;
		private readonly SignInManager<ApplicationUser> signInManager;

		public AccountController(UserManager<ApplicationUser> userManager, SignInManager<ApplicationUser> signInManager)
		{
			this.userManager = userManager;
			this.signInManager = signInManager;
		}
		// Register
		public IActionResult Register()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Register(RegisterViewModel model) 
        {
            if (!model.IsAgree) {
                ModelState.AddModelError(string.Empty, "You Must Agree The Terms And Conditions");
            return View(model);
            }
            if(ModelState.IsValid)
            {
                var User = new ApplicationUser
                {
                    UserName = model.Email.Split("@")[0],
                    Email = model.Email,
                    FName = model.FName,
                    LName = model.LName,
                    //IsAgree = model.IsAgree,
                };
                var Result =await userManager.CreateAsync(User,model.Password);
                if (Result.Succeeded)
                {
                    return RedirectToAction("Login");
                }
                else
                {
                    foreach (var error in Result.Errors)
                    {
                        ModelState.AddModelError(string.Empty, error.Description);
                    }
                }
            }
            return View(model);

        }

        
        // Login

        public IActionResult Login()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Login(LoginViewModel model)
        {
            if (ModelState.IsValid)
            {
                var User = await userManager.FindByEmailAsync(model.Email);
                if(User is  not null)
                {
                    var Flag = await userManager.CheckPasswordAsync(User, model.Password);
                    if (Flag)
                    {
                        var Result = await signInManager.PasswordSignInAsync(User, model.Password, model.RememberMe, false);
                        if (Result.Succeeded)
                        {
                            return RedirectToAction("Index", "Home");
                        }
                    }
                    else
                    {
                        ModelState.AddModelError(string.Empty, "Incorrect Password");
                    }
                }
                else
                {
                    ModelState.AddModelError(string.Empty, "Email is not Exist");
                }
            }
            return View(model);
        }

        // Sign Out
        public new async Task<IActionResult> SignOut()
        {
            await signInManager.SignOutAsync();
            return RedirectToAction(nameof(Login));
        }

        // ForgetPassword
        public IActionResult ForgetPassword()
        {
            return View();
        }
        [HttpPost]
    //    public async Task<IActionResult> SendEmail(ForgetPasswordViewModel model)
    //    {
    //        if (ModelState.IsValid)
    //        {
    //            var User = await userManager.FindByEmailAsync(model.Email);
    //            if(User is not null)
    //            {
    //                // Valid For Only One time For This User
    //                var token = await userManager.GeneratePasswordResetTokenAsync(User);
    //                // https://localhost:44317/Account/ResetPassword?email=ali@gamil.com?Token=kahjdkjasgdiabfhsljhdsln
    //                var ResetPasswordLink = Url.Action("ResetPassword", "Account", new { email = User.Email , Token = token },Request.Scheme);
    //                var email = new Email
    //                {
    //                    Subject = "Reset Password",
    //                    To = model.Email,
    //                    Body = "ResetPasswordLink"
    //                };
    //                EmailSettings.SendEmail(email);
    //                return RedirectToAction(nameof(CheckYourInbox));

				//}
    //            else
    //            {
    //                ModelState.AddModelError(string.Empty, "Email does not Exist");
    //            }
    //        }
    //        return View(nameof(ForgetPassword),model);
    //    }

        public IActionResult CheckYourInbox()
        {
            return View();
        }
		// Reset Password
	}
}
