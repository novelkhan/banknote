using banknote.Interfaces;
using banknote.Models;
using Microsoft.AspNetCore.Identity;

namespace banknote.Repositories
{
    public class AccountRepository : IAccount
    {
        private readonly UserManager<ApplicationUser> _userManager;
        public AccountRepository(UserManager<ApplicationUser> userManager)
        {
            _userManager = userManager;
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
    }
}
