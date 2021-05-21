using MolDavaBanking.Persistance;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MolDavaBanking.Domain.TransactionViewModel
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

    public class TransactionViewModel_GetTransactions
    {
        public string TransactionNumber { get; set; }

        public DateTimeOffset CreatedDate { get; set; }

        public Statuses Status { get; set; }

        public double Amount { get; set; }

        public Currencies Currency { get; set; }
    }
}