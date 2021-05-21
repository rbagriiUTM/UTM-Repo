using MolDavaBanking.Persistance;
using MolDavaBanking.Repositories.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MolDavaBanking.Repositories.Repositories
{
    public class SupportRepository : ISupportRepository
    {
        MolDavaBankingContext dbContext = MolDavaBankingContext.GetInstance();

        public bool AddSupportMessage(SupportMessage supportMessage)
        {
            try
            {
                dbContext.SupportMessages.Add(supportMessage);
                dbContext.SaveChanges();
                return true;
            }
            catch (Exception exception)
            {
                Console.WriteLine(exception.Message);
                return false;
            }
        }

        public int DeleteSupportMessageById(int supportMessageId)
        {
            var supportMessageToBeDeleted = dbContext.SupportMessages.Single(sm => sm.SupportMessageId == supportMessageId);

            dbContext.SupportMessages.Remove(supportMessageToBeDeleted);

            var isDeleted = dbContext.SaveChanges();

            return isDeleted;
            
        }

        public SupportMessage GetSupportMessageById(int supportMessageId)
        {
            var supportMessage = dbContext.SupportMessages.Single(sm => sm.SupportMessageId == supportMessageId);
            return supportMessage;
        }

        public List<SupportMessage> GetSupportMessages()
        {
            var supportMessages = dbContext.SupportMessages.ToList();
            return supportMessages;
        }
    }
}
