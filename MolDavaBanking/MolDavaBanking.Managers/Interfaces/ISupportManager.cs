using MolDavaBanking.Domain.SupportMessageViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MolDavaBanking.Managers.Interfaces
{
    public interface ISupportManager
    {
        bool AddSupportMessage(SupportMessageViewModel supportMessageViewModel);
        List<SupportMessageViewModel_Get> GetSupportMessages();
        SupportMessageViewModel_Get GetSupportMessageById(int supportMessageId);
        void SendReplySupportMessageEmail(string toEmail, string subject, string body);
        bool DeleteSupportMessageById(int supportMessageId);
    }
}
