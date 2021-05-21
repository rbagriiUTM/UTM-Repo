using MolDavaBanking.Domain.BankViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MolDavaBanking.Managers.Interfaces
{
    public interface IBankManager
    {
        List<BankViewModel> GetBanks();
    }
}
