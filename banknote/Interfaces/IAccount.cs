using banknote.Models;
using Microsoft.AspNetCore.Identity;

namespace banknote.Interfaces
{
    public interface IAccount
    {
        public Task<IdentityResult> CreateUserAsync(SignUpUserModel userModel);
        public Task<SignInResult> PasswordSignInAsync(SignInModel signInModel);
        public Task SignOutAsync();
    }
}