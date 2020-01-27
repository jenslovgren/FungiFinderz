using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FungiFinder.Models.ViewModels
{
    public class FunctionMapVM
    {
        //public int ID { get; set; }
        //[Display(Name = "Ange ett namn för ditt svampställe:")]
        public string LocationName { get; set; }
        public long Latitude { get; set; }
        public long Longitude { get; set; }


    }
}
