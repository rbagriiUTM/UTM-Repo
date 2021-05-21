using MolDavaBanking.Managers.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MolDavaBanking.Web.Controllers
{
    [Authorize(Roles = "Client")]
    public class StatisticsController : Controller
    {
        private ITransactionManager _iTransactionManager;

        public StatisticsController(ITransactionManager iTransactionManager)
        {
            _iTransactionManager = iTransactionManager;
        }

        public ActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public ActionResult GetMyTransacationInfoNeededForCharts()
        {
            var myTransactions = _iTransactionManager.GetTransactionsByUserEmail(User.Identity.Name);

            var transactionsJanuary = _iTransactionManager.GetTransactionsByYearAndMonth(myTransactions, DateTime.Now.Year, 1);
            var transactionsFebruary = _iTransactionManager.GetTransactionsByYearAndMonth(myTransactions, DateTime.Now.Year, 2);
            var transactionsMarch = _iTransactionManager.GetTransactionsByYearAndMonth(myTransactions, DateTime.Now.Year, 3);
            var transactionsApril = _iTransactionManager.GetTransactionsByYearAndMonth(myTransactions, DateTime.Now.Year, 4);
            var transactionsMay = _iTransactionManager.GetTransactionsByYearAndMonth(myTransactions, DateTime.Now.Year, 5);
            var transactionsJune = _iTransactionManager.GetTransactionsByYearAndMonth(myTransactions, DateTime.Now.Year, 6);
            var transactionsJuly = _iTransactionManager.GetTransactionsByYearAndMonth(myTransactions, DateTime.Now.Year, 7);
            var transactionsAugust = _iTransactionManager.GetTransactionsByYearAndMonth(myTransactions, DateTime.Now.Year, 8);
            var transactionsSeptember = _iTransactionManager.GetTransactionsByYearAndMonth(myTransactions, DateTime.Now.Year, 9);
            var transactionsOctober = _iTransactionManager.GetTransactionsByYearAndMonth(myTransactions, DateTime.Now.Year, 10);
            var transactionsNovember = _iTransactionManager.GetTransactionsByYearAndMonth(myTransactions, DateTime.Now.Year, 11);
            var transactionsDecember = _iTransactionManager.GetTransactionsByYearAndMonth(myTransactions, DateTime.Now.Year, 12);

            var transactionsGroupedByStatuses = _iTransactionManager.GroupTransactionsByStatuses(myTransactions);

            return Json(new
            {
                PieChart = transactionsGroupedByStatuses,
                ColumnChart = new { trJan = transactionsJanuary,
                    trFeb = transactionsFebruary,
                    trMar = transactionsMarch,
                    trApr = transactionsApril,
                    trMay = transactionsMay,
                    trJun = transactionsJune,
                    trJul = transactionsJuly,
                    trAug = transactionsAugust,
                    trSep = transactionsSeptember,
                    trOct = transactionsOctober,
                    trNov = transactionsNovember,
                    trDec = transactionsDecember
                }
            });
        }

        public ActionResult Test()
        {
            return View();
        }
    }
}