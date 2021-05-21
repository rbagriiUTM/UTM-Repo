using MolDavaBanking.Persistance;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MolDavaBanking.Repositories.Interfaces
{
    public interface IUserRepository
    {
        List<User> GetUsers();
        User GetUserByEmail(string email);
        void RegisterUser(User user);
        void UpdateUser(User user);
        void DeleteUser(User user);
        User GetUserByIDNP(string IDNP);
        bool IsLockedUserByEmail(string userEmail);
        List<User> PaginatedUsers(List<User> totalFilteredUsers, int page, int pageSize);
    }
}