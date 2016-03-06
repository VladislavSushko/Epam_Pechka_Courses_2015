using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Pechka.DLL.ModelsForWEBUI;

namespace Pechka.DLL.Abstract
{
   public interface IUserRepository
    {
        IEnumerable<User> Users { get; }
       bool ValidateUser(string email, string password);
       bool SaveNewUser(RegistrationModel user);
       User GetUser(string email);
       bool ConfirmEmail(string id, string email);
    }
}
