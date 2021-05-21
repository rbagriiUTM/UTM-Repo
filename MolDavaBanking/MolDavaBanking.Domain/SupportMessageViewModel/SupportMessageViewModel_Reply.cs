using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MolDavaBanking.Domain.SupportMessageViewModel
{
    public class SupportMessageViewModel_Reply
    {
        [Required]
        [MinLength(10)]
        public string Subject { get; set; }

        [Required]
        [MinLength(20)]
        public string Body { get; set; }

        [Required]
        [EmailAddress]
        public string EmailTo { get; set; }
    }
}
