﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FungiFinder.Models.ViewModels
{
    public class AccountEditFavoriteMushroomVM
    {
        [Display(Name = "Favoritsvamp")]
        public string FavoriteMushroom { get; set; }

    }
}
