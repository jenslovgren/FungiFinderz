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
       

        [HttpGet]
        [Route("library")]
        public IActionResult Library()
        {
            return View();  
        }

        [HttpGet]
        [Route("librarySearch/{searchQuery}")]
        public IActionResult Library(string searchQuery)
        {
            FunctionLibraryResultPartialVM[] viewModels = service.GetMushroomsFromSearch(searchQuery);

            return PartialView("_LibraryResultPartial", viewModels);
        }

        [Route("Image/File")]
        [HttpPost]
        public async Task<IActionResult> FileUpload(IFormFile file)
        {
            if (!Utils.CheckFileSignature(file.OpenReadStream()))
                return BadRequest("error");

            if (file?.Length > 0)
            {
                var filePath = Path.Combine(hostEnvironment.WebRootPath, "Images/Uploads", file.FileName);
                using (var fileStream = new FileStream(filePath, FileMode.Create))
                {
                    await file.CopyToAsync(fileStream);
                }
            }
            return PartialView("_MainImagePartial", file.FileName);
        }

        [Route("Image/GetPartial/{shroomToFind}")]
        [HttpGet]
        public IActionResult GetResultPartial(string shroomToFind)
        {
            var result = service.PredictImage(shroomToFind);

            return PartialView("_MainResultPartial", result);
        }

        [Route("Map")]
        [HttpGet]
        public async Task<IActionResult> MapLocation()
        {
            var model = await service.GetUserLocations();
            return View(model);

        }

        [Route("Map/{locationName}/{lng}/{lat}")]
        [HttpPost]
        public async Task<IActionResult> AddMapLocation(string locationName, decimal lng, decimal lat)
        {
            await service.SaveLocation(locationName, lng, lat);

            return RedirectToAction(nameof(MapLocation));
        }

        //[Route("edit/locationname/{id}")]
        //[HttpPost]
        //public async Task<IActionResult> ChangeLocationName(int id)
        //{
        //    service.TryChangeLocationName(id);
        //}
    }
}