﻿using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using FungiFinder.Models;
using FungiFinder.Models.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace FungiFinder.Controllers
{
    [Authorize]


    public class AccountController : Controller
    {
        private readonly AccountService service;
        private readonly IWebHostEnvironment hostEnvironment;

        public AccountController(AccountService service, IWebHostEnvironment hostEnvironment)
        {
            this.service = service;
            this.hostEnvironment = hostEnvironment;
        }
        [HttpGet]
        [AllowAnonymous]
        [Route("")]
        [Route("/index")]
        public IActionResult Index()
        {
            return View();
        }

        [AllowAnonymous]
        [HttpGet]
        [Route("/login")]
        public IActionResult Login()
        {
            return View();
        }

        [AllowAnonymous]
        [HttpPost]
        [Route("/login")]
        public async Task<IActionResult> Login(AccountLoginVM vm)
        {
            if (!ModelState.IsValid)
                return View(vm);


            var result = await service.TryLoginUser(vm);
            if (!result.Succeeded)
            {
                ModelState.AddModelError(string.Empty, "Användarnamn och/eller lösenord är felaktigt");
                return View(vm);
            }
            return RedirectToAction("Main", "Functions");
        }

        [AllowAnonymous]
        [HttpGet]
        [Route("/Register")]
        public IActionResult Register()
        {
            return View();
        }


        [AllowAnonymous]
        [HttpPost]
        [Route("/Register")]
        public async Task<IActionResult> Register(AccountRegisterVM vm)
        {
            if (!ModelState.IsValid)
                return View(vm);


            var result = await service.TryCreateUser(vm);
            if (!result.Succeeded)
            {
                ModelState.AddModelError(string.Empty, result.Errors.First().Description);
                return View(vm);
            }

            return RedirectToAction(nameof(Login));
        }

        [HttpGet]
        [Route("logout")]
        public async Task<IActionResult> LogOut()
        {
            await service.TryLogOutUserAsync();

            return RedirectToAction(nameof(Index));
        }

        [Route("/profile")]
        [HttpGet]
        public async Task<IActionResult> Profile()
        {
            var model = await service.GetProfileData();
            return View(model);
        }


        [Route("profile/edit/email")]
        [HttpGet]
        public IActionResult EditEmail()
        {
            return PartialView("_EditProfilEmail");
        }

        [Route("profile/edit/email")]
        [HttpPost]
        public async Task<IActionResult> EditEmail([FromBody] AccountEditEmailPartial VM)
        {

            if (!ModelState.IsValid)
                return BadRequest(ModelState.First().Value.Errors.First().ErrorMessage);

            var result = await service.EditEmail(VM);
            if (!result.Succeeded)
            {
                return BadRequest(ModelState.First().Value.Errors.First().ErrorMessage);
            }

            return Ok();
        }




        [Route("profile/edit/password")]
        [HttpGet]
        public IActionResult EditPassword()
        {

            return PartialView("_EditProfilePassword");
        }

        [Route("profile/edit/password")]
        [HttpPost]
        public async Task<IActionResult> EditPassword([FromBody] AccountEditPasswordPartialVM VM)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState.First().Value.Errors.First().ErrorMessage);

            var result = await service.ChangePassword(VM);
            if (!result.Succeeded)
            {
                return BadRequest(result.Errors.First().Description);

            }


            return Ok();
        }

        [RequestSizeLimit(2097152)]
        [Route("profile/changepicture")]
        [HttpPost]
        public async Task<IActionResult> ChangeProfilePicture(IFormFile profilePic)
        {

            


            if (profilePic?.Length > 0)
            {
                var filePath = Path.Combine(hostEnvironment.WebRootPath, "Images/UserPics", profilePic.FileName);

                using (var fileStream = new FileStream(filePath, FileMode.Create))
                {
                    await profilePic.CopyToAsync(fileStream);
                }
            }
            try
            {
                await service.TryChangeProfilePic(profilePic.FileName);
            }
            catch
            {
                return BadRequest("error");
            }

            return Ok(profilePic.FileName);


        }

        [Route("profile/edit/favoritemushroom")]
        [HttpGet]
        public IActionResult EditFavoriteMushroom()
        {
            return PartialView("_EditFavoriteMushroom");
        }

        [Route("profile/edit/favoritemushroom")]
        [HttpPost]
        public async Task<IActionResult> EditFavoriteMushroom([FromBody] AccountEditFavoriteMushroomVM vm)
        {
            var result = await service.ChangeFavoriteMushroom(vm);
            if (!result.Succeeded)
            {
                return BadRequest();
            }
            return Ok();
        }

        [Route("profile/edit/locationname/{locationName}/{id}")]
        [HttpPost]
        public IActionResult EditLocationName(string locationName, int id)
        {
            service.TryEditLocationName(locationName, id);

            return Ok();
        }
        [Route("/profile/deletelocation/{id}")]
        [HttpPost]
        public IActionResult DeleteLocation(int id)
        {
            service.TryDeleteLocation(id);

            return Ok();
        }
    }
}