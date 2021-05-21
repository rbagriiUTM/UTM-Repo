using MolDavaBanking.Persistance;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MolDavaBanking.Repositories.Interfaces
{
    public interface IRoleRepository
    {
        List<Role> GetRoles();
        Role GetRoleById(int roleId);
        Role GetRoleByName(string roleName);
    }
}
