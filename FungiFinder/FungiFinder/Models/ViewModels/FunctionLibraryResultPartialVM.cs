using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FungiFinder.Models.ViewModels
{
    public class FunctionLibraryResultPartialVM
    {
        public string LatinName { get; set; }
        public string Name { get; set; }
        public string Info { get; set; }
        // public IFormFile ImgToSearch { get; set; }
        public string ImgUrl { get; set; }

        public bool Edible { get; set; }
    }
}
