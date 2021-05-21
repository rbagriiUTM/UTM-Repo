using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MolDavaBanking.Domain.UserViewModel
{
    public class UserViewModel
    {
        [Required(ErrorMessage = "Email is required")]
        [EmailAddress(ErrorMessage = "Invalid Email Address")]
        public string Email { get; set; }

        [Required(ErrorMessage = "Password is required")]
        [RegularExpression(@"^(?=.*[a-z])(?=.*[A-Z])(?=.*\d)(?=.*[@$!%*?&])[A-Za-z\d@$!%*?&]{8,}$", ErrorMessage = "Invalid Password. It should be 8 or more<br/>" +
            "characters long and contain at least:<br/> " +
            "<ul>" +
            "<li>one upper case letter</li>" +
            "<li>one lower case letter</li>" +
            "<li>one digit</li>" +
            "<li>one special character(@$!%*?&)</li>" +
            "</ul>")]
        public string Password { get; set; }
    }
}
