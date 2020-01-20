using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FungiFinder.Models.ViewModels
{
    public class FunctionProfileVM
    {
        public IFormFile ProfilePicture { get; set; }
        public string Username { get; set; }
        public string MushroomLookAlike { get; set; }
        public string FavouriteMushroom { get; set; }
        public string PreviousSearches { get; set; }


    }
}
