using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FungiFinder.Models
{
    public class MyIdentityUser : IdentityUser
    {
        public string FavoriteMushroom { get; set; }
        public string ProfileImageUrl { get; set; }
        public int MushroomId { get; set; }


    }
}
