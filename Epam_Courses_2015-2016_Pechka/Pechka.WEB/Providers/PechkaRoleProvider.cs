using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Security;
using Pechka.DLL.Cncrete;
using Pechka.DLL.ModelsForWEBUI;

namespace Pechka.WEB.Providers
{
    public class PechkaRoleProvider:RoleProvider
    {
        public override string[] GetRolesForUser(string email)
        {
            string[] role = new string[] { };
            using (PechkaContext _db = new PechkaContext())
            {
                try
                {
                    User user = (from u in _db.Users
                                 where u.Email == email
                                 select u).FirstOrDefault();
                    if (user != null)
                    {
                        Role userRole = _db.Roles.Find(user.RoleId);

                        if (userRole != null)
                        {
                            role = new string[] { userRole.Name };
                        }
                    }
                }
                catch
                {
                    role = new string[] { };
                }
            }
            return role;
        }
        public override bool IsUserInRole(string email, string roleName)
        {
            bool outputResult = false;
            using (PechkaContext _db = new PechkaContext())
            {
                try
                {
                   User user = (from u in _db.Users
                                 where u.Email == email
                                 select u).FirstOrDefault();
                    if (user != null)
                    {
                        Role userRole = _db.Roles.Find(user.RoleId);

                        if (userRole != null && userRole.Name == roleName)
                        {
                            outputResult = true;
                        }
                    }
                }
                catch
                {
                    outputResult = false;
                }
            }
            return outputResult;
        }
        public override void CreateRole(string roleName)
        {
            throw new NotImplementedException();
        }

        public override bool DeleteRole(string roleName, bool throwOnPopulatedRole)
        {
            throw new NotImplementedException();
        }

        public override bool RoleExists(string roleName)
        {
            throw new NotImplementedException();
        }

        public override void AddUsersToRoles(string[] usernames, string[] roleNames)
        {
            throw new NotImplementedException();
        }

        public override void RemoveUsersFromRoles(string[] usernames, string[] roleNames)
        {
            throw new NotImplementedException();
        }

        public override string[] GetUsersInRole(string roleName)
        {
            throw new NotImplementedException();
        }

        public override string[] GetAllRoles()
        {
            throw new NotImplementedException();
        }

        public override string[] FindUsersInRole(string roleName, string usernameToMatch)
        {
            throw new NotImplementedException();
        }

        public override string ApplicationName { get; set; }

        
    }
}