using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using FungiFinder.Models.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;

namespace FungiFinder.Controllers
{
    [Authorize]
    public class FunctionsController : Controller
    {
        private readonly IWebHostEnvironment hostEnvironment;

        public FunctionsController(IWebHostEnvironment hostEnvironment)
        {
            this.hostEnvironment = hostEnvironment;
        }
        [HttpGet]
        [Route("main")]
        public IActionResult Main()
        {
            return View();
        }
        [HttpPost]
        [Route("main")]
        public IActionResult Main(FunctionMainVM vm)
        {
            if(vm.ImgToSearch?.Length > 0)
            {
                var filePath = Path.Combine(hostEnvironment.WebRootPath, "Images/Searches", vm.ImgToSearch.FileName);

                using (var fileStream = new FileStream(filePath, FileMode.Create))
                {
                    vm.ImgToSearch.CopyTo(fileStream);
                } 
            }

            

            return View();
        }

        [Route("library")]
        public IActionResult Library()
        {
            return View();
        }

        [Route("profile")]
        public IActionResult Profile()
        {
            return View();
        }

        
    }
}