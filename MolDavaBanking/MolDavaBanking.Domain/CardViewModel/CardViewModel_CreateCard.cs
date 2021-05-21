using MolDavaBanking.Persistance;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MolDavaBanking.Domain.CardViewModel
{
    public class CardViewModel_CreateCard
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

        [Required(ErrorMessage = "Card Number is required")]
        [RegularExpression(@"^\d{16}$", ErrorMessage = "Invalid Card Number. It should contain 16 digits.")]
        public string CardNumber { get; set; }

        [Required(ErrorMessage = "Card Currency is required")]
        public CardCurrencies Currency { get; set; }

        [Required(ErrorMessage = "Card Expiration Date is required")]
        [DataType(DataType.Date)]
        public DateTime ExpirationDate { get; set; }

        [Required(ErrorMessage = "Card Type is required")]
        public CardTypes CardType { get; set; }

        [Required(ErrorMessage = "Card should be assigned to a bank")]
        public string BankName { get; set; }

        [Required(ErrorMessage = "Card should be assigned to a user")]
        [RegularExpression(@"^\d{13}$", ErrorMessage = "Invalid User IDNP. It should contain 13 digits.")]
        public string UserIDNP { get; set; }
    }
}
