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
    public class UserRepository: IUserRepository
    {
        PechkaContext work=new PechkaContext();
        public IEnumerable<User> Users
        {
            get { return work.Users.Include(u=>u.Role); }
        }

        public bool ValidateUser(string email, string password)
        {
            bool isValid = false;

           try
                {
                    User user = (from u in work.Users
                                 where u.Email == email && u.Password == password
                                 select u).FirstOrDefault();

                    if (user != null)
                    {
                        isValid = true;
                    }
                }
                catch
                {
                    isValid = false;
                }
            
            return isValid;
        }

    }
}
