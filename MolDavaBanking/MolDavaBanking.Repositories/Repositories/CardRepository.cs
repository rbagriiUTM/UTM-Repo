using MolDavaBanking.Persistance;
using MolDavaBanking.Repositories.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MolDavaBanking.Repositories.Repositories
{
    public class CardRepository : ICardRepository
    {
        MolDavaBankingContext dbContext = MolDavaBankingContext.GetInstance();

        public bool CreateCard(Card card)
        {            
            dbContext.Cards.Add(card);
            dbContext.SaveChanges();
            return true;
        }

        public bool DeleteCard(Card card)
        {
            dbContext.Cards.Remove(card);
            dbContext.SaveChanges();
            return true;
        }

        public Card GetCardById(int cardId)
        {
            var card = dbContext.Cards.Single(c => c.CardId == cardId);

            return card;
        }

        public List<Card> GetCards()
        {
            var cards = dbContext.Cards.ToList();

            return cards;
        }

        public List<Card> GetCardsByUserEmail(string userEmail)
        {
            var cards = dbContext.Cards.Where(c => c.User.Email == userEmail).ToList();

            return cards;
        }
    }
}
