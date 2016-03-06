using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Pechka.DLL.ModelsForWEBUI;

namespace Pechka.DLL.Extends
{
    static class UserExtends
    {
       
        public static User ToUser(this RegistrationModel model)
        {
            User simpleUser=new User();
            simpleUser.Email = model.Email;
            simpleUser.FirstName = model.UserFirstName;
            simpleUser.LastName = model.UserLastName;
            simpleUser.RoleId = 1;
            simpleUser.ConfirmedEmail = false;
            simpleUser.Password = model.Password;
            return simpleUser;

        }
    }
}
