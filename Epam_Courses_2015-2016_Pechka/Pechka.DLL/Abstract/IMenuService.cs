using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Pechka.DLL.ModelsForWEBUI.DTO.Admin;
using Pechka.DLL.ModelsForWEBUI.DTO.User;

namespace Pechka.DLL.Abstract
{
    public interface IMenuService
    {
        List<MenuDTO> GetAllMenuForAdmin();
        MenuDTO GetMenu(int id);
        bool UpdateMenu(MenuDTO model);
        bool AddNewMenu(MenuDTO model);
        List<MenuForUserDTO> GetMenuForUser(string email);
    }
}
