using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Migrations;
using System.IO;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;
using Pechka.DLL.Abstract;
using Pechka.DLL.Extends;
using Pechka.DLL.ModelsForWEBUI;
using Pechka.DLL.ModelsForWEBUI.DTO;


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

                    if (user != null && user.ConfirmedEmail && !user.InBlackList)
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

        public UserForSettingDTO GetUserForsetting(string email)
        {
            return work.Users.FirstOrDefault(u => u.Email == email).ToUserForSetting();
        }

        public bool SaveNewSettings(UserForSettingDTO model,string email)
        {
            var user = work.Users.FirstOrDefault(u => u.Email == email);
            if (user != null && (model.NewPassword == null || model.Password == user.Password)&&(user.Email==model.Email||IsEmailUnique(model.Email)))
            {
                user = model.ToUser(user);
                work.Users.AddOrUpdate(user);
                work.SaveChanges();
                return true;
            }
            return false;
        }
    }
}
