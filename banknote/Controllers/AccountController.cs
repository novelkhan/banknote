using banknote.Interfaces;
using banknote.Models;
using Microsoft.AspNetCore.Mvc;

namespace banknote.Controllers
{
    public class AccountController : Controller
    {
        private readonly IAccount _accountRepository;
        public AccountController(IAccount accountRepository)
        {
            _accountRepository = accountRepository;
        }

        [Route("signup")]
        public IActionResult SignUp()
        {
            return View();
        }


        [Route("signup")]
        [HttpPost]
        public async Task<IActionResult> SignUp(SignUpUserModel userModel)
        {
            if(ModelState.IsValid)
            {
                var result = await _accountRepository.CreateUserAsync(userModel);
                if (!result.Succeeded)
                {
                    foreach (var errorMessage in result.Errors)
                    {
                        ModelState.AddModelError("", errorMessage.Description);
                    }

                    return View(userModel);
                }


                ModelState.Clear();
                TempData["AlertMsg"] = "Record has been succesfully saved";
                return View();
            }
            return View(userModel);
        }


        //[Route("login")]
        //public IActionResult LogIn()
        //{
        //    return View();
        //}

        //[Route("login")]
        //[HttpPost]
        //public async Task<IActionResult> LogIn(SignInModel signInModel)
        //{
        //    if (ModelState.IsValid)
        //    {
        //        var result = await _accountRepository.PasswordSignInAsync(signInModel);
        //        if (result.Succeeded)
        //        {
        //            return RedirectToAction("Index", "Home");
        //        }

        //        ModelState.AddModelError("", "Invalid credentials");
        //    }

        //    return View(signInModel);
        //}




        [Route("login")]
        public IActionResult LogIn()
        {
            return View();
        }

        [Route("login")]
        [HttpPost]
        public async Task<IActionResult> LogIn(SignInModel signInModel, string returnurl)
        {
            if (ModelState.IsValid)
            {
                var result = await _accountRepository.PasswordSignInAsync(signInModel);
                if (result.Succeeded)
                {
                    if (!(string.IsNullOrEmpty(returnurl)))
                    {
                        return LocalRedirect(returnurl);
                    }
                    return RedirectToAction("Index", "Home");
                }

                ModelState.AddModelError("", "Invalid credentials");
            }

            return View(signInModel);
        }




        [Route("logout")]
        public async Task<IActionResult> LogOut()
        {
            await _accountRepository.SignOutAsync();
            return RedirectToAction("Index", "Home");
        }
    }
}
