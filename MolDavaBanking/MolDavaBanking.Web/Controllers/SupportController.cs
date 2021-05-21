using MolDavaBanking.Domain.SupportMessageViewModel;
using MolDavaBanking.Managers.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MolDavaBanking.Web.Controllers
{
    [Authorize(Roles = "Client")]
    public class SupportController : Controller
    {
        private ISupportManager _iSupportManager;

        public SupportController(ISupportManager iSupportManager)
        {
            _iSupportManager = iSupportManager;
        }

        public ActionResult ContactSupport()
        {
            return View();
        }

        [HttpPost]
        public ActionResult ContactSupport(SupportMessageViewModel supportMessageViewModel)
        {
            if (ModelState.IsValid)
            {
                supportMessageViewModel.UserEmail = User.Identity.Name;

                var isAdded = _iSupportManager.AddSupportMessage(supportMessageViewModel);

                return Json(isAdded);
            }
            else
            {
                return Json(null);
            }            
        }

        [OverrideAuthorization]
        [Authorize(Roles = "Admin")]
        public ActionResult GetSupportMessages()
        {
            try
            {
                var supportMessages = _iSupportManager.GetSupportMessages();
                return View(supportMessages);
            }
            catch(Exception exception)
            {
                Console.WriteLine(exception.Message);
                throw exception;
            }
        }

        [OverrideAuthorization]
        [Authorize(Roles = "Admin")]
        public ActionResult GetSupportMessage(int supportMessageId)
        {
            var supportMessage = _iSupportManager.GetSupportMessageById(supportMessageId);

            return Json(new
            {
                MessageId = supportMessage.SupportMessageId,
                Sender = supportMessage.User.Email,
                CreationDate = supportMessage.CreationDate,
                Subject = supportMessage.Subject,
                Message = supportMessage.Body
            });

        }

        [OverrideAuthorization]
        [Authorize(Roles = "Admin")]
        public ActionResult SendReplySupportMessage(SupportMessageViewModel_Reply replyMessage)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    _iSupportManager.SendReplySupportMessageEmail(replyMessage.EmailTo, replyMessage.Subject, replyMessage.Body);
                    return Json(true);
                }
                catch (Exception exception)
                {
                    Console.WriteLine(exception.Message);
                    return Json(false);
                }
            }
            else
            {
                return Json(null);
            }
        }

        [OverrideAuthorization]
        [Authorize(Roles = "Admin")]
        public ActionResult DeleteSupportMessage(int supportMessageId)
        {
            var isDeleted = _iSupportManager.DeleteSupportMessageById(supportMessageId);

            if (isDeleted)
            {
                TempData["Msg"] = "The message was deleted.";
            }
            else
            {
                TempData["Msg"] = "Deletion of the message failed.";
            }
            return RedirectToAction("GetSupportMessages", "Support");
        }
    }
}