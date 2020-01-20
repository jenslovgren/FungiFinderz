using FungiFinder.Models.ViewModels;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FungiFinder.Models
{
    public class AccountService
    {
        private readonly UserManager<MyIdentityUser> userManager;
        private readonly SignInManager<MyIdentityUser> signInManager;

        public AccountService(UserManager<MyIdentityUser> userManager, SignInManager<MyIdentityUser> signInManager)
        {
            this.userManager = userManager;
            this.signInManager = signInManager;
        }

        internal async Task<SignInResult> TryLoginUser(LoginVM vm)
        {
            var result = await signInManager.PasswordSignInAsync(vm.Username, vm.Password, false, false);
            return result;
        }

        public async Task<IdentityResult> TryCreateUser(RegisterVM vm)
        {
            var result = await userManager.CreateAsync(new MyIdentityUser
            {
                UserName = vm.UserName,
                Email = vm.Email
            }, vm.Password);
            return result;
        }
    }
}
