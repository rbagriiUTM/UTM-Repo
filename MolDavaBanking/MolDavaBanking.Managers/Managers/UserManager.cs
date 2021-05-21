using AutoMapper;
using MolDavaBanking.Domain.UserViewModel;
using MolDavaBanking.Persistance;
using MolDavaBanking.Repositories;
using MolDavaBanking.Repositories.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mail;
using System.Text;
using System.Threading.Tasks;

namespace MolDavaBanking.Managers
{
    public class UserManager : IUserManager
    {
        private IUserRepository _iUserRepository;

        private IRoleRepository _iRoleRepository;

        public UserManager(IUserRepository iUserRepository, IRoleRepository iRoleRepository)
        {
            _iUserRepository = iUserRepository;
            _iRoleRepository = iRoleRepository;
        }

        public List<UserViewModel_GetUsers> GetUsers()
        {
            var users = _iUserRepository.GetUsers();

            List<UserViewModel_GetUsers> usersDomain = new List<UserViewModel_GetUsers>();

            foreach(var user in users)
            {
                var userDomain = Mapper.Map<User, UserViewModel_GetUsers>(user);
                usersDomain.Add(userDomain);
            }

            return usersDomain;
        }

        public bool LoginUser(UserViewModel_Login userViewModel)
        {
            var userPersistance = Mapper.Map<User>(userViewModel);

            var userExists = _iUserRepository.GetUsers().FirstOrDefault(x => x.Email == userPersistance.Email && DecodeFrom64(x.Password) == userPersistance.Password);

            return (userExists == null ? false : true);
        }

        public bool RegisterUser(UserViewModel_Register userViewModel)
        {
            var userPersistance = Mapper.Map<UserViewModel_Register, User>(userViewModel);

            userPersistance.Roles.Add(_iRoleRepository.GetRoleById(3));

            var userExists = _iUserRepository.GetUsers().FirstOrDefault(x => x.Email == userPersistance.Email || x.IDNP == userPersistance.IDNP);

            if (userExists == null)
            {
                userPersistance.Password = EncodePasswordToBase64(userPersistance.Password);
                _iUserRepository.RegisterUser(userPersistance);
                return true;
            }
            else
            {
                return false;
            }
        }

        public void UpdateUser(UserViewModel_GetUsers userViewModel)
        {
            List<string> rolesName = new List<string>();
            List<Role> roles = new List<Role>();

            if (userViewModel.IsAdmin)
            {
                rolesName.Add("Admin");
            }

            if (userViewModel.IsAccountant)
            {
                rolesName.Add("Accountant");
            }

            if (userViewModel.IsClient)
            {
                rolesName.Add("Client");
            }

            foreach (var role in rolesName)
            {
                roles.Add(_iRoleRepository.GetRoleByName(role));
            }

            userViewModel.Roles = roles;

            var userToBeUpdated = _iUserRepository.GetUsers().FirstOrDefault(u => u.Email == userViewModel.Email || u.UserId == userViewModel.UserId);

            var userPersistance = Mapper.Map<UserViewModel_GetUsers, User>(userViewModel, userToBeUpdated);

            _iUserRepository.UpdateUser(userPersistance);
        }

        public void ChangeUserLockStatus(UserViewModel_GetUsers userViewModel)
        {
            var userToChangeLockStatus = _iUserRepository.GetUserByEmail(userViewModel.Email);

            var userPersistance = Mapper.Map<UserViewModel_GetUsers, User>(userViewModel, userToChangeLockStatus);

            _iUserRepository.UpdateUser(userPersistance);
        }

        public void DeleteUser(UserViewModel_GetUsers userViewModel)
        {
            var userToBeDeleted = _iUserRepository.GetUserByEmail(userViewModel.Email);

            _iUserRepository.DeleteUser(userToBeDeleted);
        }
            
        public void ResetPassword(UserViewModel_ResetPassword userViewModel)
        {
            var userToReset = _iUserRepository.GetUsers().Single(u => u.UserId == Convert.ToInt32(userViewModel.UserId));

            userToReset.Password = EncodePasswordToBase64(userViewModel.Password);

            _iUserRepository.UpdateUser(userToReset);
        }

        public bool CreateUser(UserViewModel_Create userViewModel)
        {
            List<string> rolesName = new List<string>();
            List<Role> roles = new List<Role>();

            if (userViewModel.IsAdmin)
            {
                rolesName.Add("Admin");
            }

            if (userViewModel.IsAccountant)
            {
                rolesName.Add("Accountant");
            }

            if (userViewModel.IsClient)
            {
                rolesName.Add("Client");
            }

            foreach (var role in rolesName)
            {
                roles.Add(_iRoleRepository.GetRoleByName(role));
            }

            var userPersistance = Mapper.Map<UserViewModel_Create, User>(userViewModel);

            foreach (var role in roles)
            {
                userPersistance.Roles.Add(role);
            }

            var userExists = _iUserRepository.GetUsers().FirstOrDefault(x => x.Email == userPersistance.Email || x.IDNP == userViewModel.IDNP);

            if (userExists == null)
            {
                userPersistance.Password = EncodePasswordToBase64(userPersistance.Password);
                _iUserRepository.RegisterUser(userPersistance);
                return true;
            }
            else
            {
                return false;
            }
        }

        private string EncodePasswordToBase64(string password)
        {
            try
            {
                byte[] encData_byte = new byte[password.Length];
                encData_byte = Encoding.UTF8.GetBytes(password);
                string encodedData = Convert.ToBase64String(encData_byte);
                return encodedData;
            }
            catch (Exception ex)
            {
                throw new Exception("Error in base64Encode" + ex.Message);
            }
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

        public void SendPasswordResetEmail(string toEmail, string firstName, string lastName, int userId)
        {
            var encryptedUserId = EncodePasswordToBase64(Convert.ToString(userId));

            MailMessage mailMessage = new MailMessage("moldavabanking@gmail.com", toEmail);

            StringBuilder emailBody = new StringBuilder();

            emailBody.Append("Dear" + firstName + " " + lastName + ",<br/><br/>");
            emailBody.Append("Please click on the following link to reset your password");
            emailBody.Append("<br/>");
            //emailBody.Append("http://localhost:64845/User/ResetPassword?UserId=" + encryptedUserId);
            emailBody.Append("http://3.208.127.208/User/ResetPassword?UserId=" + encryptedUserId);
            emailBody.Append("<br/><br/>");
            emailBody.Append("Best regards," + "<br/>");
            emailBody.Append("MolDavaBanking Support Team");

            mailMessage.IsBodyHtml = true;

            mailMessage.Body = emailBody.ToString();
            mailMessage.Subject = "Reset Your Password";

            SmtpClient smtpClient = new SmtpClient("smtp.gmail.com", 587);

            smtpClient.Credentials = new System.Net.NetworkCredential()
            {
                UserName = "moldavabanking@gmail.com",
                Password = "molDAVA7!"
            };

            smtpClient.EnableSsl = true;

            //Implement try catch block
            smtpClient.Send(mailMessage);
        }

        public bool IsLockedUserByEmail(string userEmail)
        {
            var isLocked = _iUserRepository.IsLockedUserByEmail(userEmail);

            return isLocked;
        }

        public List<UserViewModel_GetUsers> GetPaginatedUsers (int page)
        {
            var allUsers = _iUserRepository.GetUsers();
            var pageSize = 5;
            var totalRecord = _iUserRepository.GetUsers().Count();
            var totalPage = (totalRecord / pageSize) + ((totalRecord % pageSize) > 0 ? 1 : 0);
            var usersOnPage = _iUserRepository.PaginatedUsers(allUsers, page, pageSize);

            List<UserViewModel_GetUsers> userDomain = new List<UserViewModel_GetUsers>();

            foreach (var user in usersOnPage)
            {
                var userPersistance = Mapper.Map<UserViewModel_GetUsers>(user);
                userDomain.Add(userPersistance);
            }
            return userDomain;
        }
    }
}