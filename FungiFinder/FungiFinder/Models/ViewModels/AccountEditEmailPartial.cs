using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FungiFinder.Models.ViewModels
{
    public class AccountEditEmailPartial
    {
        [Required(ErrorMessage = "Vänligen ange e-post")]
        [Display(Name = "E-post")]
        [DataType(DataType.EmailAddress)]
        [EmailAddress(ErrorMessage = "Se till att din e-postadress är rätt formaterad")]
        public string Email { get; set; }
    }
}
