using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Pechka.DLL.Abstract;
using Pechka.DLL.ModelsForWEBUI;

namespace Pechka.DLL.Cncrete
{
    public class AdminService:IAdminService
    {
        PechkaContext work = new PechkaContext();

        public IEnumerable<User> GetUsersToManageByAdmin(string email)
        {
            return work.Users.Include(u=>u.Role).Where(u=>u.Email!=email);
            
        }
    }
}
