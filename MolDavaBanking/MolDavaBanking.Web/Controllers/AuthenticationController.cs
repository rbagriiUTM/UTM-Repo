using log4net;
using MolDavaBanking.Domain.UserViewModel;
using MolDavaBanking.Managers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;

namespace MolDavaBanking.Web.Controllers
{
    public class AuthenticationController : Controller
    {
        private static readonly ILog log = LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);

        private IUserManager _iUserManager;

        public AuthenticationController(IUserManager iUserManager)
        {
            _iUserManager = iUserManager;
        }

        public ActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Login(UserViewModel_Login userViewModel)
        {
            if (ModelState.IsValid)
            {
                var isLoggedIn = _iUserManager.LoginUser(userViewModel);
                if (isLoggedIn)
                {
                    var isLocked = _iUserManager.IsLockedUserByEmail(userViewModel.Email);
                    if (isLocked)
                    {
                        log.Warn("A blocked user tried to log in.");

                        ViewBag.Msg = "Your account was blocked due to low balance or inactivity.";
                        return View(userViewModel);
                    }

                    FormsAuthentication.SetAuthCookie(userViewModel.Email, false);

                    log.Info("An user logged in.");

                    return RedirectToAction("Index", "Home");
                }
                else
                {
                    log.Warn("An user with bad credentials tried to log in.");

                    ViewBag.Msg = "Invalid password or email. Try again!";
                    return View(userViewModel);
                }
            }
            else
            {
                log.Error("An user tried to log in bu the attempt failed.");

                ViewBag.Msg = "The entered Email or Password is not correct.";
                return View(userViewModel);
            }
            
        }

        public ActionResult Register()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Register(UserViewModel_Register userViewModel)
        {
            if (ModelState.IsValid)
            {
                var isRegistered = _iUserManager.RegisterUser(userViewModel);
                if (isRegistered)
                {
                    log.Info("A new user registered.");

                    return RedirectToAction("Login");
                }
                else
                {
                    log.Warn("A new user attempted to register with an already existing email.");

                    ViewBag.Msg = "A user with such an email already exists.";
                }
            }

            return View(userViewModel);
        }

        public ActionResult Logout()
        {
            FormsAuthentication.SignOut();

            log.Info("An user logged out.");

            return RedirectToAction("Index", "Home");
        }
    }
}