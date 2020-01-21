using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FungiFinder.Models;
using FungiFinder.Models.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace FungiFinder.Controllers
{
    [Authorize]
   

    public class AccountController : Controller
    {
        private readonly AccountService service;

        public AccountController(AccountService service)
        {
            this.service = service;
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

        [Route("profile")]
        [HttpGet]
        public IActionResult Profile()
        {
            return View();
        }


        [Route("profile")]
        [HttpPost]
        public async Task<IActionResult> Profile(AccountProfileVM profileVM)
        {
         

            await service.EditProfile(profileVM);
        
            

            return RedirectToAction(nameof(Index));
        }



    }
}