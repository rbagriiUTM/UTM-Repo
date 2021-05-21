using MolDavaBanking.Domain.TransactionViewModel;
using MolDavaBanking.Managers.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Script.Serialization;

namespace MolDavaBanking.Web.Controllers
{
    public class TransactionController : Controller
    {
        private ITransactionManager _iTransactionManager;

        public TransactionController(ITransactionManager iTransactionManager)
        {
            _iTransactionManager = iTransactionManager;
        }

        [Authorize(Roles = "Accountant")]
        public ActionResult GetTransactions(int id = 1)
        {
            var totalRecord = _iTransactionManager.GetTransactions().Count();
            var totalPage = (totalRecord / 10) + ((totalRecord % 10) > 0 ? 1 : 0);
            ViewBag.dbCount = totalPage;
            ViewBag.minVal = _iTransactionManager.GetMinValueOfAmount();
            ViewBag.maxVal = _iTransactionManager.GetMaxValueOfAmount();
            var transactions = _iTransactionManager.GetMainTransactionsPages(id);
            return View(transactions);
        }


        [Authorize(Roles = "Accountant, Client")]
        [HttpPost]
        public ActionResult GetTransactions(TransactionViewModel_SortData sortRow, TransactionViewModel_FilterData filterData, TransactionViewModel_Pagination getInformationOnPage)
        {
            var appliedFilterToTransactions = _iTransactionManager.Pagination(sortRow, filterData, getInformationOnPage, User.Identity.Name);

            var totalPages = _iTransactionManager.ReturnTotalPages(sortRow, filterData, getInformationOnPage, User.Identity.Name);

            return Json(new { appliedFilterToTransactions, totalPages });
        }

        [HttpPost]
        public ActionResult GetTransactionDetailsByTransactionNumber(string id)
        {
            var transactionInfo = _iTransactionManager.GetFullInformationAboutTransaction(id);

            return PartialView("TransactionDetails", transactionInfo);
        }

        [Authorize(Roles = "Client")]
        public ActionResult GetUserTransactions(int id = 1)
        {
            var totalRecord = _iTransactionManager.GetTransactionsByLoggedUser(User.Identity.Name).Count();
            var totalPage = (totalRecord / 10) + ((totalRecord % 10) > 0 ? 1 : 0);
            ViewBag.dbCount = totalPage;
            ViewBag.minVal = _iTransactionManager.GetMinValueOfAmount();
            ViewBag.maxVal = _iTransactionManager.GetMaxValueOfAmount();
            var transactions = _iTransactionManager.GetMainTransactionsPaged(id, User.Identity.Name);
            return View(transactions);
        }

        [Authorize(Roles = "Accountant")]
        [HttpPost]
        public ActionResult ChangeTransactionStatus(string transactionNumber, string currentStatus)
        {
            if (_iTransactionManager.UpdateTransactionStatusToNew(transactionNumber, currentStatus))
            {
                string message = "Success";
                return Json(new { Message = message, JsonRequestBehavior.AllowGet });
            }
            else
            {
                string message = "Error";
                return Json(new { Message = message, JsonRequestBehavior.DenyGet });
            }
        }

        //[Authorize(Roles = "Client")]
        [HttpPost]
        public JsonResult LastTransactions(int jtStartIndex = 0, int jtPageSize = 0, string jtSorting = null)
        {
            try
            {
                var allUserTransactions = _iTransactionManager.GetTransactionsByLoggedUser(User.Identity.Name);

                var result = allUserTransactions.OrderByDescending(t => t.CreatedDate).Take(10).ToList();

                return Json(new { Result = "OK", Records = result });
            }
            catch (Exception ex)
            {
                return Json(new { Result = "ERROR", Message = ex.Message });
            }
        }
    }
}