using MolDavaBanking.Domain.CardViewModel;
using MolDavaBanking.Managers.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MolDavaBanking.Web.Controllers
{
    [Authorize(Roles = "Accountant")]
    public class CardController : Controller
    {
        private ICardManager _iCardManager;
        private IBankManager _iBankManager;

        public CardController(ICardManager iCardManager, IBankManager iBankManager)
        {
            _iCardManager = iCardManager;
            _iBankManager = iBankManager;
        }

        public ActionResult GetCards()
        {
            var cards = _iCardManager.GetCards();

            return View(cards);
        }

        public ActionResult CreateCard()
        {
            return View();
        }

        [HttpPost]
        public ActionResult CreateCard(CardViewModel_CreateCard cardViewModel)
        {
            if (ModelState.IsValid)
            {
                var isCreated = _iCardManager.CreateCard(cardViewModel);
                if (isCreated)
                {
                    TempData["Msg"] = "A new card was created.";
                    TempData["Color"] = "green";
                    return RedirectToAction("GetCards", "Card");
                }
                else
                {
                    TempData["Msg"] = "The card creation failed.";
                    TempData["Color"] = "red";
                    return View(cardViewModel);
                }
            }
            else
            {
                TempData["Msg"] = "Invalid data provided.";
                TempData["Color"] = "red";
                return View(cardViewModel);
            }
        }

        public ActionResult DeleteCard(int cardId)
        {
            var isDeleted = _iCardManager.DeleteCard(cardId);

            if (isDeleted)
            {
                TempData["Msg"] = "The card was deleted.";
                TempData["Color"] = "green";
            }
            else
            {
                TempData["Msg"] = "The deletion failed";
                TempData["Color"] = "red";
            }
            return RedirectToAction("GetCards", "Card");
        }

        public ActionResult GetCardDetails(int cardId)
        {
            var card = _iCardManager.GetCardById(cardId);

            return Json(new
            {
                BankName = card.Bank.Name,
                BankAddress = card.Bank.Adress,
                UserAddress = card.User.Adress,
                UserIDNP = card.User.IDNP,
                UserBirthDate = card.User.BirthDate.ToString(),
                UserEmail = card.User.Email,
                CardAmount = card.Amount
            });

        }

        [OverrideAuthorization()]
        [Authorize(Roles = "Client")]
        public ActionResult GetMyCards()
        {
            var cards = _iCardManager.GetCardsByEmail(User.Identity.Name);

            return View(cards);
        }
    }
}