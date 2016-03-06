using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Migrations;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;
using Pechka.DLL.Abstract;
using Pechka.DLL.Extends;
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

                    if (user != null && user.ConfirmedEmail)
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

        private bool IsEmailUnique(string email)
        {
            return work.Users.FirstOrDefault(u => u.Email==email)==null;
        }

        public bool SaveNewUser(RegistrationModel user)
        {
            if (IsEmailUnique(user.Email))
            {
                try
                {
                    var userToadd = user.ToUser();
                    work.Users.Add(userToadd);
                    work.SaveChanges();
                    return true;
                }
                catch
                {
                    return false;
                }
            }
            return false;
        }

        public User GetUser(string email)
        {
            return work.Users.FirstOrDefault(u => u.Email == email);
        }

        public bool ConfirmEmail(string id,string email)
        {
            
            try
            {
                int userId = int.Parse(id);
                var user = work.Users.Find(userId);
                if (user != null)
                {
                    if (user.Email == email)
                    {
                        user.ConfirmedEmail = true;
                        work.Users.AddOrUpdate(user);
                        work.SaveChanges();
                        return true;
                    }

                }
            }
            catch
            {
                return false;
            }
           return false;
        }
    }
}
