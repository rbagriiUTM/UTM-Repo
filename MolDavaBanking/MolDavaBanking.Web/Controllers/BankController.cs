using MolDavaBanking.Managers.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MolDavaBanking.Web.Controllers
{
    [Authorize(Roles = "Accountant")]
    public class BankController : Controller
    {
        private IBankManager _iBankManager;

        public BankController(IBankManager iBankManager)
        {
            _iBankManager = iBankManager;
        }

        [HttpPost]
        public ActionResult GetBanksName()
        {
            var banksName = _iBankManager.GetBanks().Select(b => b.Name).ToList();

            return Json(banksName);
        }
    }
}