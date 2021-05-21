using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MolDavaBanking.Persistance
{
    public class Card
    {
        public enum CardCurrencies
        {
            EUR,
            USD,
            MDL,
            RUB
        }

        public enum CardTypes
        {
            AMEX,
            Visa,
            VisaElectron,
            MasterCard,
            Maestro
        }

        [Key]
        public int CardId { get; set; }

        [Required]
        public string CardNumber { get; set; }

        [Required]
        public CardCurrencies Currency { get; set; }

        [Required]
        public double Amount { get; set; }

        [Required]
        public DateTime ExpirationDate { get; set; }

        [Required]
        public CardTypes CardType { get; set; }

        public int UserId { get; set; }
        public virtual User User { get; set; }

        public int BankId { get; set; }
        public virtual Bank Bank { get; set; }
    }
}
