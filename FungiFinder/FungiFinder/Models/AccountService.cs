using FungiFinder.Models.ViewModels;
using Microsoft.AspNetCore.Http;
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
        private readonly IHttpContextAccessor accessor;

        public AccountService(UserManager<MyIdentityUser> userManager, SignInManager<MyIdentityUser> signInManager, IHttpContextAccessor accessor)
        {
            this.userManager = userManager;
            this.signInManager = signInManager;
            this.accessor = accessor;
        }

        internal async Task<SignInResult> TryLoginUser(AccountLoginVM vm)
        {
            var result = await signInManager.PasswordSignInAsync(vm.Username, vm.Password, false, false);
            return result;
        }

        public async Task<IdentityResult> TryCreateUser(AccountRegisterVM vm)
        {
            var result = await userManager.CreateAsync(new MyIdentityUser
            {
                UserName = vm.UserName,
                Email = vm.Email,
                
                
            }, vm.Password);
            return result;
        }

        internal async Task TryLogOutUserAsync()
        {
            await signInManager.SignOutAsync();
        }

        internal async Task/*<IdentityResult>*/ TryEditProfile(AccountProfileVM vm)
        {

            //var user = await userManager.GetUserAsync(accessor.HttpContext.User);
            //var newPass = await userManager.ChangePasswordAsync(user, vm.Password, vm.NewPassword);
            //user.Email = vm.Email;

            //user.FavoriteMushroom = vm.FavouriteMushroom;
            //await userManager.UpdateAsync(user);

            //return newPass;

            var user = await userManager.GetUserAsync(accessor.HttpContext.User);
            vm = new AccountProfileVM
            {
                UrlProfilePicture = user.ProfileImageUrl,
                Username = user.UserName,
                Email = user.Email,
                Password = user.PasswordHash,

             };



          



        }

        internal async Task<AccountProfileVM> GetProfileData()
        {
            var user = await userManager.GetUserAsync(accessor.HttpContext.User);
            AccountProfileVM vm = new AccountProfileVM
            {
                UrlProfilePicture = user.ProfileImageUrl,
                Username = user.UserName,
                Email = user.Email,
                Password = user.PasswordHash,

            };

            return vm;
        }

       
    }
}
