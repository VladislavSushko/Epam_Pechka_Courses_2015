using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Migrations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Pechka.DLL.Abstract;
using Pechka.DLL.Extends;
using Pechka.DLL.ModelsForWEBUI;
using Pechka.DLL.ModelsForWEBUI.DTO.Admin;

namespace Pechka.DLL.Cncrete
{
    public class AdminService:IAdminService
    {
        PechkaContext work = new PechkaContext();

        public IEnumerable<UserDto> GetUsersToManageByAdmin(string email)
        {
            var temp = work.Users.Include(u => u.Role).Include(u => u.LastScore).Where(u => u.Email != email);
            var listUserDto=new List<UserDto>();
            foreach (var item in temp)
            {
                listUserDto.Add(item.ToUserDTO());
            }
            return listUserDto;

        }

        public void AddUserToBlackList(int Id,int temp,int role)
        {
            var user = work.Users.FirstOrDefault(u => u.Id == Id);
            user.InBlackList = temp != 0;
            user.RoleId = role;
            work.Users.AddOrUpdate(user);
            work.SaveChanges();
        }
    }
}
