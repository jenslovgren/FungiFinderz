using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FungiFinder.Models.ViewModels
{
    public class AccountRegisterVM
    {
        [Required(ErrorMessage = "Vänligen ange användarnamn")]
        [Display(Name = "Användarnamn")]
        public string UserName { get; set; }
        [Required(ErrorMessage = "Vänligen ange e-mail")]
        [Display(Name = "E-post")]
        [DataType(DataType.EmailAddress)]
        public string Email { get; set; }
        [Required(ErrorMessage = "Vänligen ange lösenord")]
        [DataType(DataType.Password)]
        [Display(Name = "Lösenord")]
        public string Password { get; set; }
        [Required(ErrorMessage = "Vänligen ange lösenord")]
        [DataType(DataType.Password)]
        [Display(Name = "Lösenordsvalidering")]
        [Compare("Password", ErrorMessage = "Lösenorden matchar inte")]
        public string ConfirmPassword { get; set; }
        [Range(typeof(bool), "true", "true", ErrorMessage = "Vänligen godkänn villkoren")]
        public bool TermsAndConditions { get; set; }
    }
}
