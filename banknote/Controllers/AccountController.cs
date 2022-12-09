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
            }
            return View(userModel);
        }
    }
}
