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
        [Required(ErrorMessage = "Vänligen namnge ditt svampställe")]
        [Display(Name = "Svampställe:")]
        public string LocationName { get; set; }
        public string Latitude { get; set; }
        public string Longitude { get; set; }
        public int Id { get; set; }

    }
}
