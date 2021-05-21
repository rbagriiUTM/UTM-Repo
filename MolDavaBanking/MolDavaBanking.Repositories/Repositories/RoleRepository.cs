using MolDavaBanking.Persistance;
using MolDavaBanking.Repositories.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MolDavaBanking.Repositories.Repositories
{
    public class RoleRepository : IRoleRepository
    {
        //MolDavaBankingContext dbContext = new MolDavaBankingContext();

        MolDavaBankingContext dbContext = MolDavaBankingContext.GetInstance();

        public List<Role> GetRoles()
        {
            var roles = dbContext.Roles.ToList();

            return roles;
        }

        public Role GetRoleById(int roleId)
        {
            //using (dbContext)
            //{
                var role = dbContext.Roles.FirstOrDefault(r => r.RoleId == roleId);
                return role;
            //}
        }

        public Role GetRoleByName(string roleName)
        {
            var role = dbContext.Roles.FirstOrDefault(r => r.Name == roleName);

            return role;
        }
    }
}
