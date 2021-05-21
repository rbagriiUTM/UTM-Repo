using MolDavaBanking.Persistance;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MolDavaBanking.Domain.BankViewModel
{
    public class BankViewModel
    {
        public int BankId { get; set; }

        public string Name { get; set; }

        public string Adress { get; set; }

        public List<Transaction> Transactions { get; set; }
        public List<Card> Cards { get; set; }
    }
}
