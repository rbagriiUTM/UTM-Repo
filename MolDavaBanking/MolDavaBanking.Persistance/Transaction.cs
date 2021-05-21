using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MolDavaBanking.Persistance
{
    public enum Statuses
    {
        Pending,
        Completed,
        Processing,
        Canceled
    }

    public enum Currencies
    {
        EUR,
        USD,
        MDL,
        RUB
    } 

    public class Transaction
    {
        [Key]
        public int TransactionId { get; set; }

        [Required]
        public string TransactionNumber { get; set; }

        [Required]
        public DateTime CreatedDate { get; set; }

        [Required]
        public Statuses Status { get; set; }

        [Required]
        public double Amount { get; set; }

        [Required]
        public Currencies Currency { get; set; }

        public int BankId { get; set; }
        public virtual Bank Bank { get; set; }

        public int UserId { get; set; }
        public virtual User User { get; set; }
    }
}