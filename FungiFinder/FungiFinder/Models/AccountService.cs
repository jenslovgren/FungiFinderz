using FungiFinder.Models.Entities;
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
        private readonly FungiFinderContext context;

        public AccountService(UserManager<MyIdentityUser> userManager, SignInManager<MyIdentityUser> signInManager, IHttpContextAccessor accessor, FungiFinderContext context)
        {
            this.userManager = userManager;
            this.signInManager = signInManager;
            this.accessor = accessor;
            this.context = context;
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


        internal async Task<AccountProfileVM> GetProfileData()
        {
            var user = await userManager.GetUserAsync(accessor.HttpContext.User);
            AccountProfileVM vm = new AccountProfileVM
            {
                UrlProfilePicture = user.ProfileImageUrl,
                Username = user.UserName,
                Email = user.Email,
                FavouriteMushroom = user.FavoriteMushroom,
               

            };

            
            

            vm.LatestSearches = context.LatestSearches
                .Where(o => o.UserId == userManager
                .GetUserId(accessor.HttpContext.User))
                .OrderByDescending(o => o.SearchDate)
                .Take(5)
                .Select(o => new LatestSearchesDetailsVM { Mushroom = o.Mushroom, SearchDate = o.SearchDate , ImageUrl = o.ImageUrl})
                .ToArray();

            vm.Locations = context.MapLocation
              .Where(o => o.UserId == userManager
              .GetUserId(accessor.HttpContext.User))
              .OrderBy(o => o.LocationName)
              .Take(5)
              .Select(o => new FunctionMapVM { LocationName = o.LocationName, Latitude = o.Latitude.ToString(), Longitude = o.Longitude.ToString(), Id = o.Id })
              .ToArray();


            return vm;
        }

        internal string FindMushroomUrl(string mushroom)
        {
            var result = context.Mushrooms.SingleOrDefault(m => m.Name == mushroom);
            return result.ImageUrl;
        }

        internal async Task<IdentityResult> EditEmail(AccountEditEmailPartial VM)
        {
            var user = await userManager.GetUserAsync(accessor.HttpContext.User);
            user.Email = VM.Email;

            return await userManager.UpdateAsync(user);

        }

        internal async Task<IdentityResult> ChangePassword(AccountEditPasswordPartialVM vm)
        {

            var user = await userManager.GetUserAsync(accessor.HttpContext.User);
            var newPass = await userManager.ChangePasswordAsync(user, vm.Password, vm.NewPassword);
            await userManager.UpdateAsync(user);

            return newPass;

        }

        internal async Task TryChangeProfilePic(string fileName)
        {
            string userId = userManager.GetUserId(accessor.HttpContext.User);
            var user = await userManager.FindByIdAsync(userId);
            user.ProfileImageUrl = fileName;
            await userManager.UpdateAsync(user);
        }

        internal async Task<IdentityResult> ChangeFavoriteMushroom(AccountEditFavoriteMushroomVM vm)
        {
            var user = await userManager.GetUserAsync(accessor.HttpContext.User);

            user.FavoriteMushroom = vm.FavoriteMushroom;
            var result = await userManager.UpdateAsync(user);

            return result;
        }

        internal void TryEditLocationName(string locationName, int id)
        {
            var location = context.MapLocation.SingleOrDefault(o => o.Id == id);

            location.LocationName = locationName;
            context.MapLocation.Update(location);
            context.SaveChanges();


        }

        internal void TryDeleteLocation(int id)
        {
            var location = context.MapLocation.SingleOrDefault(o => o.Id == id);

            context.Remove(location);
            context.SaveChanges();
        }
    }
}
