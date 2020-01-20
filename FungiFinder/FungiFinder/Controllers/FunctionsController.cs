using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace FungiFinder.Controllers
{
    [Authorize]
    public class FunctionsController : Controller
    {
        [Route("main")]
        public IActionResult Main()
        {
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