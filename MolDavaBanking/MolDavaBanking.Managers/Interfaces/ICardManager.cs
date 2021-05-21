using MolDavaBanking.Domain.CardViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MolDavaBanking.Managers.Interfaces
{
    public interface ICardManager
    {
        List<CardViewModel_Get> GetCards();
        List<CardViewModel_Get> GetCardsByEmail(string userEmail);
        bool CreateCard(CardViewModel_CreateCard cardDomain);
        bool DeleteCard(int cardId);
        CardViewModel_Get GetCardById(int cardId);
    }
}
