using banknote.Interfaces;
using banknote.Models;
using banknote.Services;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Data;
using SignInResult = Microsoft.AspNetCore.Identity.SignInResult;

namespace banknote.Repositories
{
    public class AccountRepository : IAccount
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly SignInManager<ApplicationUser> _signInManager;
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly IUserService _userService;
        public AccountRepository(UserManager<ApplicationUser> userManager, SignInManager<ApplicationUser> signInManager, RoleManager<IdentityRole> roleManager,IUserService userService)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _roleManager = roleManager;
            _userService = userService;
        }

        public async Task<IdentityResult> CreateUserAsync(SignUpUserModel userModel)
        {
            //var user = new IdentityUser()
            //{
            //    Email = userModel.Email,
            //    UserName = userModel.Email
            //};

            var user = new ApplicationUser()
            {
                FirstName= userModel.FirstName,
                LastName= userModel.LastName,
                Email = userModel.Email,
                UserName = userModel.Email
            };

            var result = await _userManager.CreateAsync(user, userModel.Password);
            return result;
        }



        public async Task<SignInResult> PasswordSignInAsync(SignInModel signInModel)
        {
            var result = await _signInManager.PasswordSignInAsync(signInModel.Email, signInModel.Password, signInModel.RememberMe, false);
            return result;
        }

        public async Task SignOutAsync()
        {
            await _signInManager.SignOutAsync();
        }


        public async Task<IdentityResult> ChangePasswordAsync(ChangePasswordModel model)
        {
            var userId = _userService.GetUserId();
            var user = await _userManager.FindByIdAsync(userId);

            return await _userManager.ChangePasswordAsync(user, model.OldPassword, model.NewPassword);
        }

        //public async void DefaultRoles()
        //{
        //    var user = await _userManager.FindByEmailAsync("nkhan3717@gmail.com");
        //    var result = await _userManager.AddToRoleAsync(user, "Admin");
        //    if (result.Succeeded) {}
        //}
    }
}
