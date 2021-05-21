using MolDavaBanking.Persistance;
using MolDavaBanking.Repositories.Interfaces;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MolDavaBanking.Repositories.Repositories
{
    public class UserRepository : IUserRepository
    {
        //MolDavaBankingContext dbContext = new MolDavaBankingContext();

        MolDavaBankingContext dbContext = MolDavaBankingContext.GetInstance();

        public List<User> GetUsers()
        {
            var users = dbContext.Users.ToList();

            return users;
        }

        public User GetUserByEmail(string email)
        {
            var user = dbContext.Users.Single(u => u.Email == email);

            return user;
        }

        public void RegisterUser(User user)
        {
            dbContext.Users.Add(user);
            dbContext.SaveChanges();
        }

        public void UpdateUser(User user)
        {
            dbContext.Entry(user).State = EntityState.Modified;
            dbContext.SaveChanges();
        }

        public void DeleteUser(User user)
        {
            dbContext.Users.Remove(user);
            dbContext.SaveChanges();
        }

        public User GetUserByIDNP(string IDNP)
        {
            var user = dbContext.Users.Single(u => u.IDNP == IDNP);

            return user;
        }

        public bool IsLockedUserByEmail(string userEmail)
        {
            var isLocked = dbContext.Users.Single(u => u.Email == userEmail).IsLocked;

            return (isLocked == 0 ? false : true);
        }

        public List<User> PaginatedUsers(List<User> totalFilteredUsers, int page, int pageSize)
        {
            var filteredDataOnPages = totalFilteredUsers.Skip((page - 1) * pageSize).Take(pageSize).ToList();

            return filteredDataOnPages;
        }
    }
}