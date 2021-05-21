using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MolDavaBanking.Persistance
{
    public class Bank
    {
        [Key]
        public int BankId { get; set; }

        [Required]
        public string Name { get; set; }

        [Required]
        public string Adress { get; set; }

        public ICollection<Transaction> Transactions { get; set; }
        public ICollection<Card> Cards { get; set; }
    }
}