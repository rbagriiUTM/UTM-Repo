using log4net;
using MolDavaBanking.Domain.UserViewModel;
using MolDavaBanking.Managers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.Mvc;

namespace MolDavaBanking.Web.Controllers
{
    [Authorize(Roles="Admin")]
    public class UserController : Controller
    {
        private static readonly ILog log = LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);

        private IUserManager _iUserManager;

        public UserController(IUserManager iUserManager)
        {
            _iUserManager = iUserManager;
        }

        public ActionResult GetUsers(int page=1)
        {
            var totalRecord = _iUserManager.GetUsers().Count();
            var totalPage = (totalRecord / 5) + ((totalRecord % 5) > 0 ? 1 : 0);
            ViewBag.dbCount = totalPage;
            var usersPaged = _iUserManager.GetPaginatedUsers(page);
            return View(usersPaged);
        }

        public ActionResult EditUser(int userId)
        {
            var user = _iUserManager.GetUsers().FirstOrDefault(u => u.UserId == userId);

            return View(user);
        }

        [HttpPost]
        public ActionResult EditUser(UserViewModel_GetUsers user)
        {
            if (ModelState.IsValid)
            {
                _iUserManager.UpdateUser(user);

                log.Info("An admin updated a user.");

                return RedirectToAction("GetUsers", "User");
            }
            else
            {
                log.Error("An admin tried to edit a user, but attempt failed.");

                ViewBag.Msg = "Invalid Data Provided";
                return View(user);
            }
        }

        public ActionResult DeleteUser(int userId)
        {
            var userToBeDeleted = _iUserManager.GetUsers().FirstOrDefault(u => u.UserId == userId);

            _iUserManager.DeleteUser(userToBeDeleted);

            log.Info("An admin deleted an user");

            return RedirectToAction("GetUsers", "User");
        }

        public ActionResult SendResetPasswordEmail(int userId)
        {
            var userToReset = _iUserManager.GetUsers().Single(u => u.UserId == userId);

            _iUserManager.SendPasswordResetEmail(userToReset.Email, userToReset.FirstName, userToReset.LastName, userId);

            log.Info("An admin sent a reset password link to an user.");

            return RedirectToAction("GetUsers", "User");
        }

        [AllowAnonymous]
        public ActionResult ResetPassword(string userId)
        {
            var decryptedUserId = DecodeFrom64(userId);

            var userToReset = _iUserManager.GetUsers().Single(u => u.UserId == Convert.ToInt32(decryptedUserId));

            log.Info("A user accessed the reset password link.");

            return View(userToReset);
        }

        [AllowAnonymous]
        [HttpPost]
        public ActionResult ResetPassword(UserViewModel_ResetPassword user)
        {
            if (ModelState.IsValid)
            {
                user.UserId = DecodeFrom64(user.UserId);
                _iUserManager.ResetPassword(user);
                TempData["Msg"] = "Your password was successfully reset.";
                TempData["Color"] = "green";

                log.Info("A user reset the password.");
            }
            else
            {
                TempData["Msg"] = "Invalid Data Provided";
                TempData["Color"] = "red";

                log.Error("A user tried to reset the password but the attempt failed.");
            }

            return RedirectToAction("Login", "Authentication");
        }

        public ActionResult CreateUser()
        {
            return View();
        }

        [HttpPost]
        public ActionResult CreateUser(UserViewModel_Create userViewModel)
        {
            if (ModelState.IsValid)
            {
                var isCreated = _iUserManager.CreateUser(userViewModel);

                if (isCreated)
                {
                    log.Info("An admin created an user.");

                    return RedirectToAction("GetUsers", "User");
                }
                else
                {
                    log.Warn("An admin tried to create an user with an already existing email or IDNP.");

                    ViewBag.Msg = "A user with such an email or IDNP already exists.";
                    return View(userViewModel);
                }
            }
            else
            {
                log.Error("An admin tried to create an user but the attempt failed.");

                ViewBag.Msg = "Invalid Data Provided";
                return View(userViewModel);
            }
        }

        [HttpPost]
        public JsonResult ChangeUserLockStatus(int userId, byte isLocked)
        {
            var userToChangeLockStatus = _iUserManager.GetUsers().Single(u => u.UserId == userId);

            userToChangeLockStatus.IsLocked = isLocked;

            _iUserManager.ChangeUserLockStatus(userToChangeLockStatus);

            log.Info("An admin changed the lock status of an user.");

            var returnData = _iUserManager.GetUsers().Single(u => u.UserId == userId).IsLocked;

            return Json(returnData);
        }

        private string DecodeFrom64(string encodedData)
        {
            UTF8Encoding encoder = new UTF8Encoding();
            Decoder utf8Decode = encoder.GetDecoder();
            byte[] todecode_byte = Convert.FromBase64String(encodedData);
            int charCount = utf8Decode.GetCharCount(todecode_byte, 0, todecode_byte.Length);
            char[] decoded_char = new char[charCount];
            utf8Decode.GetChars(todecode_byte, 0, todecode_byte.Length, decoded_char, 0);
            string result = new String(decoded_char);
            return result;
        }
    }
}