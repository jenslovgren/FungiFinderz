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
        public string Email { get; set; }
        public string MushroomLookAlike { get; set; }
        public string FavouriteMushroom { get; set; }
        public string PreviousSearches { get; set; }

        [Required(ErrorMessage = "Vänligen ange gammalt lösenord")]
        [DataType(DataType.Password)]
        [Display(Name = "Lösenord")]
        public string Password { get; set; }

        [Required(ErrorMessage = "Vänligen ange nytt lösenord")]
        [DataType(DataType.Password)]
        [Display(Name = "Ange nytt lösenord")]
        public string NewPassword { get; set; }

    }
}
