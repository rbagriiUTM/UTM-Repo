using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MolDavaBanking.Persistance
{
    public class SupportMessage
    {
        [Key]
        public int SupportMessageId { get; set; }

        [Required]
        public string Subject { get; set; }

        [Required]
        public string Body { get; set; }

        [Required]
        public DateTime CreationDate { get; set; }

        public int UserId { get; set; }
        public virtual User User { get; set; }
    }
}
