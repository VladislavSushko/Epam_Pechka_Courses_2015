using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Pechka.DLL.ModelsForWEBUI;
using Pechka.DLL.ModelsForWEBUI.DTO.Admin;

namespace Pechka.DLL.Abstract
{
    public interface IAdminService
    {
        IEnumerable<UserDto> GetUsersToManageByAdmin(string email);
        void AddUserToBlackList(int id, int temp,int role);
    }
}
