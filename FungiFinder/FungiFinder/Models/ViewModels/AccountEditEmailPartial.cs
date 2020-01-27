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
        [Required(ErrorMessage = "Vänligen ange e-mail")]
        [Display(Name = "E-Mail")]
        [DataType(DataType.EmailAddress)]
        [EmailAddress(ErrorMessage = "Se till att din epost adress är rätt formaterad")]
        public string Email { get; set; }
    }
}
