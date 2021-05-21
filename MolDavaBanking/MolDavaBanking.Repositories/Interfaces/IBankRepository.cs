using MolDavaBanking.Persistance;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MolDavaBanking.Repositories.Interfaces
{
    public interface IBankRepository
    {
        List<Bank> GetBanks();
        Bank GetBankByName(string name);
    }
}