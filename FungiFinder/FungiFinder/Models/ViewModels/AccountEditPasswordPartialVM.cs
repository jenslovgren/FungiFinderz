﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FungiFinder.Models.ViewModels
{
    public class AccountEditPasswordPartialVM
    {

        [Required(ErrorMessage = "Vänligen ange gammalt lösenord")]
        [DataType(DataType.Password)]
        [Display(Name = "Ange gammalt lösenord")]
        public string Password { get; set; }

        [Required(ErrorMessage = "Vänligen ange nytt lösenord")]
        [DataType(DataType.Password)]
        [Display(Name = "Ange nytt lösenord")]
        public string NewPassword { get; set; }
        //[Compare("Password", ErrorMessage = "Confirm password doesn't match, Type again !")]
        //public string ConfirmPassword { get; set; }

    }
}
