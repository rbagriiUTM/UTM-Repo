using AutoMapper;
using MolDavaBanking.Domain.SupportMessageViewModel;
using MolDavaBanking.Managers.Interfaces;
using MolDavaBanking.Persistance;
using MolDavaBanking.Repositories.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mail;
using System.Text;
using System.Threading.Tasks;

namespace MolDavaBanking.Managers.Managers
{
    public class SupportManager : ISupportManager
    {
        private ISupportRepository _iSupportRepository;
        private IUserRepository _iUserRepository;

        public SupportManager(ISupportRepository iSupportRepository, IUserRepository iUserRepository)
        {
            _iSupportRepository = iSupportRepository;
            _iUserRepository = iUserRepository;
        }

        public bool AddSupportMessage(SupportMessageViewModel supportMessageViewModel)
        {
            var supportMessagePersistance = Mapper.Map<SupportMessage>(supportMessageViewModel);

            supportMessagePersistance.User = _iUserRepository.GetUserByEmail(supportMessageViewModel.UserEmail);
            supportMessagePersistance.UserId = supportMessagePersistance.User.UserId;

            var isAdded = _iSupportRepository.AddSupportMessage(supportMessagePersistance);

            return isAdded;
        }

        public bool DeleteSupportMessageById(int supportMessageId)
        {
            var isDeleted = _iSupportRepository.DeleteSupportMessageById(supportMessageId);

            return (isDeleted != 0 ? true : false);
        }

        public SupportMessageViewModel_Get GetSupportMessageById(int supportMessageId)
        {
            var supportMessagePersistance = _iSupportRepository.GetSupportMessageById(supportMessageId);

            var supportMessageDomain = Mapper.Map<SupportMessageViewModel_Get>(supportMessagePersistance);

            return supportMessageDomain;
        }

        public List<SupportMessageViewModel_Get> GetSupportMessages()
        {
            var supportMessagesPersistance = _iSupportRepository.GetSupportMessages();

            var supportMessagesDomain = Mapper.Map<List<SupportMessageViewModel_Get>>(supportMessagesPersistance);

            return supportMessagesDomain;
        }

        public void SendReplySupportMessageEmail(string toEmail, string subject, string body)
        {
            MailMessage mailMessage = new MailMessage("moldavabanking@gmail.com", toEmail);

            StringBuilder emailBody = new StringBuilder();

            emailBody.Append(body);

            mailMessage.IsBodyHtml = true;

            mailMessage.Body = emailBody.ToString();
            mailMessage.Subject = "Reply " + subject;

            SmtpClient smtpClient = new SmtpClient("smtp.gmail.com", 587);

            smtpClient.Credentials = new System.Net.NetworkCredential()
            {
                UserName = "moldavabanking@gmail.com",
                Password = "molDAVA7!"
            };

            smtpClient.EnableSsl = true;

            smtpClient.Send(mailMessage);
        }
    }
}
