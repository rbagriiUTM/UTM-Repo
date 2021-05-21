using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MolDavaBanking.Persistance
{
    public class User
    {
        public User()
        {
            this.Roles = new HashSet<Role>();
        }

        [Key]
        public int UserId { get; set; }

        [Required]
        public string FirstName { get; set; }

        [Required]
        public string LastName { get; set; }

        [Required]
        public string IDNP { get; set; }

        [Required]
        [DataType(DataType.Date)]
        public DateTime BirthDate { get; set; }

        [Required]
        public string Adress { get; set; }

        [Required]
        public string Email { get; set; }

        [Required]
        public string Password { get; set; }

        [Required]
        public byte IsLocked { get; set; }

        public virtual ICollection<Role> Roles { get; set; }
        public ICollection<Transaction> Transactions { get; set; }
        public ICollection<Card> Cards { get; set; }
        public virtual ICollection<SupportMessage> SupportMessages { get; set; }
    }
}