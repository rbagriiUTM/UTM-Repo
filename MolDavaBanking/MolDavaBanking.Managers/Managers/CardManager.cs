using AutoMapper;
using MolDavaBanking.Domain.CardViewModel;
using MolDavaBanking.Managers.Interfaces;
using MolDavaBanking.Persistance;
using MolDavaBanking.Repositories.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MolDavaBanking.Managers.Managers
{
    public class CardManager : ICardManager
    {
        private ICardRepository _iCardRepository;
        private IBankRepository _iBankRepository;
        private IUserRepository _iUserRepository;

        public CardManager(ICardRepository iCardRepository, IBankRepository iBankRepository, IUserRepository iUserRepository)
        {
            _iCardRepository = iCardRepository;
            _iBankRepository = iBankRepository;
            _iUserRepository = iUserRepository;
        }

        public bool CreateCard(CardViewModel_CreateCard cardDomain)
        {
            var cardPersistance = Mapper.Map<Card>(cardDomain);

            cardPersistance.Bank = _iBankRepository.GetBankByName(cardDomain.BankName);
            cardPersistance.BankId = cardPersistance.Bank.BankId;

            cardPersistance.User = _iUserRepository.GetUserByIDNP(cardDomain.UserIDNP);
            cardPersistance.UserId = cardPersistance.User.UserId;

            var isCreated = _iCardRepository.CreateCard(cardPersistance);

            return (isCreated ? true : false);
        }

        public bool DeleteCard(int cardId)
        {
            var cardToBeDeleted = _iCardRepository.GetCardById(cardId);

            var isDeleted = _iCardRepository.DeleteCard(cardToBeDeleted);

            return (isDeleted ? true : false);
        }

        public CardViewModel_Get GetCardById(int cardId)
        {
            var cardPersistance = _iCardRepository.GetCardById(cardId);

            var cardDomain = Mapper.Map<CardViewModel_Get>(cardPersistance);

            return cardDomain;
        }

        public List<CardViewModel_Get> GetCards()
        {
            var cardsPersistance = _iCardRepository.GetCards();

            List<CardViewModel_Get> cardsDomain = new List<CardViewModel_Get>();

            foreach (var cardPersistance in cardsPersistance)
            {
                var cardDomain = Mapper.Map<CardViewModel_Get>(cardPersistance);
                cardsDomain.Add(cardDomain);
            }

            return cardsDomain;
        }

        public List<CardViewModel_Get> GetCardsByEmail(string userEmail)
        {
            var cardsPersistance = _iCardRepository.GetCardsByUserEmail(userEmail);

            List<CardViewModel_Get> cardsDomain = new List<CardViewModel_Get>();

            foreach(var cardPersistance in cardsPersistance)
            {
                var cardDomain = Mapper.Map<CardViewModel_Get>(cardPersistance);
                cardsDomain.Add(cardDomain);
            }

            return cardsDomain;
        }
    }
}
