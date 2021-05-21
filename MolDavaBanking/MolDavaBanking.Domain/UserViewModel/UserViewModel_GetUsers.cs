using MolDavaBanking.Persistance;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MolDavaBanking.Domain.UserViewModel
{
    public class UserViewModel_GetUsers
    {
        public int UserId { get; set; }

        [Required(ErrorMessage = "First name is required")]
        [RegularExpression(@"^(?=.*[a-z])(?=.*[A-Z])[A-Za-z]+$", ErrorMessage = "Invalid First Name")]
        public string FirstName { get; set; }
            
        [Required(ErrorMessage = "Last name is required")]
        [RegularExpression(@"^(?=.*[a-z])(?=.*[A-Z])[A-Za-z]+$", ErrorMessage = "Invalid First Name")]
        public string LastName { get; set; }

        [Required(ErrorMessage = "IDNP is required")]
        [RegularExpression(@"^\d{13}$", ErrorMessage = "Invalid INDP")]
        public string IDNP { get; set; }

        [Required(ErrorMessage = "Date of birth is required")]
        [DataType(DataType.Date)]
        public DateTime BirthDate { get; set; }

        [Required(ErrorMessage = "Adress is required")]
        public string Adress { get; set; }

        [Required(ErrorMessage = "Email is required")]
        [EmailAddress(ErrorMessage = "Invalid Email Address")]
        public string Email { get; set; }

        //[Required(ErrorMessage = "Password is required")]
        [RegularExpression(@"^(?=.*[a-z])(?=.*[A-Z])(?=.*\d)(?=.*[@$!%*?&])[A-Za-z\d@$!%*?&]{8,}$", ErrorMessage = "Invalid Password")]
        public string Password { get; set; }

        [Required]
        public byte IsLocked { get; set; }

        [Required]
        public bool IsAdmin { get; set; }

        [Required]
        public bool IsAccountant { get; set; }

        [Required]
        public bool IsClient { get; set; }

        public List<Role> Roles { get; set; }
    }
}
