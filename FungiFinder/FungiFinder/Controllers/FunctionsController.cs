using System;
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
    public class FunctionsController : Controller
    {
        private readonly IWebHostEnvironment hostEnvironment;
        private readonly FunctionsService service;

        public FunctionsController(IWebHostEnvironment hostEnvironment, FunctionsService service)
        {
            this.hostEnvironment = hostEnvironment;
            this.service = service;
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
                var filePath = Path.Combine(hostEnvironment.WebRootPath, "Images/Uploads", vm.ImgToSearch.FileName);

                using (var fileStream = new FileStream(filePath, FileMode.Create))
                {
                    vm.ImgToSearch.CopyTo(fileStream);
                } 
            }

            var result = service.PredictImage(vm.ImgToSearch.FileName);


            return PartialView("_MainResultPartial", result);
        }

        [HttpGet]
        [Route("library")]
        public IActionResult Library()
        {
            return View();
        }

        [HttpPost]
        [Route("library/{searchQuery}")]
        public IActionResult Library(string searchQuery)
        {
            FunctionLibraryResultPartialVM[] viewModels = service.GetMushroomsFromSearch(searchQuery);

            return PartialView("_LibraryResultPartial", viewModels);
        }

        [Route("Image/File")]
        [HttpPost]
        public async Task<IActionResult> FileUpload(IFormFile file)
        {
            if(file?.Length > 0)
            {
                var filePath = Path.Combine(hostEnvironment.WebRootPath, "Images/Uploads", file.FileName);

                using(var fileStream = new FileStream(filePath, FileMode.Create))
                {
                    await file.CopyToAsync(fileStream);
                }
            }

            return PartialView("_MainImagePartial", file.FileName);
        }

        //[Route("Image/GetPartial")]
        //[HttpGet]
        //public IActionResult GetPartial()
        //{

        //}

        
    }
}