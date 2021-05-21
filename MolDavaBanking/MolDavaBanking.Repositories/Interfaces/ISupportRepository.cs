using MolDavaBanking.Persistance;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MolDavaBanking.Repositories.Interfaces
{
    public interface ISupportRepository
    {
        bool AddSupportMessage(SupportMessage supportMessage);
        List<SupportMessage> GetSupportMessages();
        SupportMessage GetSupportMessageById(int supportMessageId);
        int DeleteSupportMessageById(int supportMessageId);
    }
}
