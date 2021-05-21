using AutoMapper;
using MolDavaBanking.Domain.BankViewModel;
using MolDavaBanking.Managers.Interfaces;
using MolDavaBanking.Repositories.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MolDavaBanking.Managers.Managers
{
    public class BankManager : IBankManager
    {
        private IBankRepository _iBankRepository;

        public BankManager(IBankRepository iBankRepository)
        {
            _iBankRepository = iBankRepository;
        }

        public List<BankViewModel> GetBanks()
        {
            var banksPersistance = _iBankRepository.GetBanks();

            List<BankViewModel> banksDomain = new List<BankViewModel>();

            foreach (var bankPersistance in banksPersistance)
            {
                var bankDomain = Mapper.Map<BankViewModel>(bankPersistance);
                banksDomain.Add(bankDomain);
            }

            return banksDomain;
        }
    }
}
