using MolDavaBanking.Persistance;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MolDavaBanking.Repositories.Interfaces
{
    public interface ICardRepository
    {
        List<Card> GetCards();
        Card GetCardById(int cardId);
        bool CreateCard(Card card);
        bool DeleteCard(Card card);
        List<Card> GetCardsByUserEmail(string userEmail);
    }
}
