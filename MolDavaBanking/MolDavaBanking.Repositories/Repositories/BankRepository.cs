using MolDavaBanking.Persistance;
using MolDavaBanking.Repositories.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MolDavaBanking.Repositories.Repositories
{
    public class BankRepository : IBankRepository
    {
        MolDavaBankingContext dbContext = MolDavaBankingContext.GetInstance();

        public Bank GetBankByName(string name)
        {
            var bank = dbContext.Banks.Single(b => b.Name == name);

            return bank;
        }

        public List<Bank> GetBanks()
        {
            var banks = dbContext.Banks.ToList();

            return banks;
        }
    }
}
