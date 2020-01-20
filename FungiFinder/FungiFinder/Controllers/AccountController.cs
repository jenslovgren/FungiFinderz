using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace FungiFinder.Controllers
{
    public class AccountController : Controller
    {
        [Route("")]
        [Route("/index")]
        public IActionResult Index()
        {
            return View();
        }
    }
}