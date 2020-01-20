using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace FungiFinder.Models.ViewModels
{
    public class MainVM
    {
        [Display(Name = "Fungi to search")]
        public string Search { get; set; }
    }
}
