using MolDavaBanking.Persistance;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MolDavaBanking.Domain.TransactionViewModel
{
   public class TransactionViewModel_GetTransactionDetails
    {
        public Bank Bank { get; set; }

        public User User { get; set; }
    }
}
