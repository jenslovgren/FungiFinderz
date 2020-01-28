using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FungiFinder.Models.ViewModels
{
    public class FunctionMainResultPartialVM
    {
        public float ProcentResult { get; set; }
        public bool Edible { get; set; }
        public string Info { get; set; }
        public string UrlMatchedMushroom { get; set; }

        public string Name { get; set; }
        public string LatinName { get; set; }
        public int Rating { get; set; }




    }
}
