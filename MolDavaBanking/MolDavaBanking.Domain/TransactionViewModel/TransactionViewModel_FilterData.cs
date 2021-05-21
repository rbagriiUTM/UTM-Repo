using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MolDavaBanking.Domain.TransactionViewModel
{
     public class TransactionViewModel_FilterData
    {
        public List<int> Currencies { get; set; }

        public List<string> Statuses { get; set; }

        public double AmountSelected { get; set; }

        public DateTime DateFrom { get; set; }

        public DateTime DateTo { get; set; }

        public string TransactionNumber { get; set; }

    }
}
