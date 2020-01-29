using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace FungiFinder.Models.ViewModels
{
    public class AccountProfileVM
    {
        public string UrlProfilePicture { get; set; }
        public string Username { get; set; }
        [DataType(DataType.EmailAddress)]
        [Display(Name = "E-post")]
        public string Email { get; set; }
        public string MushroomLookAlike { get; set; }
        [Display(Name = "Favoritsvamp")]
        public string FavouriteMushroom { get; set; }
        public LatestSearchesDetailsVM[] LatestSearches { get; set; }



    }
}
