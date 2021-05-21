using MolDavaBanking.Domain.UserViewModel;
using MolDavaBanking.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MolDavaBanking.Managers
{
    public interface IUserManager
    {
        List<UserViewModel_GetUsers> GetUsers();
        bool RegisterUser(UserViewModel_Register userViewModel);
        bool LoginUser(UserViewModel_Login userViewModel);
        void UpdateUser(UserViewModel_GetUsers userViewModel);
        void DeleteUser(UserViewModel_GetUsers userViewModel);
        void ResetPassword(UserViewModel_ResetPassword userViewModel);
        bool CreateUser(UserViewModel_Create userViewModel);
        void ChangeUserLockStatus(UserViewModel_GetUsers userViewModel);
        void SendPasswordResetEmail(string toEmail, string firstName, string lastName, int userId);
        bool IsLockedUserByEmail(string userEmail);
        List<UserViewModel_GetUsers> GetPaginatedUsers(int page);
    }
}
