using MolDavaBanking.Persistance;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MolDavaBanking.Domain.CardViewModel
{
    public class CardViewModel_Get
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

        public int CardId { get; set; }

        public string CardNumber { get; set; }

        public CardCurrencies Currency { get; set; }

        public double Amount { get; set; }

        [DataType(DataType.Date)]
        public DateTime ExpirationDate { get; set; }

        public CardTypes CardType { get; set; }

        public User User { get; set; }

        public Bank Bank { get; set; }
    }
}