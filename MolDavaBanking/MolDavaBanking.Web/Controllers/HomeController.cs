using MolDavaBanking.Managers.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MolDavaBanking.Web.Controllers
{
    public class HomeController : Controller
    {
        private ITransactionManager _iTransactionManager;

        public HomeController(ITransactionManager iTransactionManager)
        {
            _iTransactionManager = iTransactionManager;
        }

        public ActionResult Index()
        {
            if (User.Identity.Name != "")
            {
                ViewBag.TransactionAverage = _iTransactionManager.GetAverageOfTransactions(User.Identity.Name);
            }
            return View();
        }

        [Authorize(Roles = "Admin")]
        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        [Authorize(Roles = "Accountant")]
        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }
    }
}