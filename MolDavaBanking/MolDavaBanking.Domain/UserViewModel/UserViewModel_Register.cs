using MolDavaBanking.Persistance;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MolDavaBanking.Domain.UserViewModel
{
    public class UserViewModel_Register : UserViewModel
    {
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
    }
}