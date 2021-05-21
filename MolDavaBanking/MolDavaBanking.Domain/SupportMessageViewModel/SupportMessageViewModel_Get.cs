using MolDavaBanking.Persistance;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MolDavaBanking.Domain.SupportMessageViewModel
{
    public class SupportMessageViewModel_Get
    {
        public int SupportMessageId { get; set; }

        public string Subject { get; set; }

        [Required]
        [MinLength(20)]
        public string Body { get; set; }

        public DateTime CreationDate { get; set; }

        public User User { get; set; }
    }
}
