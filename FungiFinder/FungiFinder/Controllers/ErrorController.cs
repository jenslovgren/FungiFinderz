using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace FungiFinder.Controllers
{
    public class ErrorController : Controller
    {
        [HttpGet]
        [Route("error/exception")]
        public IActionResult Error()
        {
            return View();
        }


        [HttpGet]
        [Route("error/http/{id}")]
        public IActionResult HttpError(int id)
        {
            return View(id);
        }
    }
}