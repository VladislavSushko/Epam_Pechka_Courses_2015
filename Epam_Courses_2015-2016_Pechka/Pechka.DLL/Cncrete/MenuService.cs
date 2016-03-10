using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Migrations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Pechka.DLL.Abstract;
using Pechka.DLL.Extends;
using Pechka.DLL.Models;
using Pechka.DLL.ModelsForWEBUI;
using Pechka.DLL.ModelsForWEBUI.DTO.Admin;
using Pechka.DLL.ModelsForWEBUI.DTO.User;

namespace Pechka.DLL.Cncrete
{
    public class MenuService:IMenuService
    {
        public List<MenuDTO> GetAllMenuForAdmin()
        {
            using (var work = new PechkaContext())
            {
                var intermediateResult = work.Meny;
                    var result = intermediateResult.ToList().Select(item => item.ToMenuDTO()).ToList();
                    result.Sort();
                    result.Reverse();
                result.Add(new MenuDTO());
                return result;
    
            }
        }

        public MenuDTO GetMenu(int id)
        {
            using (var work = new PechkaContext())
            {
                var item= work.Meny.FirstOrDefault(u => u.Id == id);
                if (item != null)
                {
                    return item.ToMenuDTO();
                }
                else
                {
                    return null;
                }
            }
        }

        public bool UpdateMenu(MenuDTO model)
        {
            using (var work =new PechkaContext())
            {
                try
                {
                    var thismenu = work.Meny.FirstOrDefault(u => u.Id == model.Id);
                    thismenu.IsCompleted = model.ToMenu().IsCompleted;
                    work.Meny.AddOrUpdate(thismenu);
                    work.SaveChanges();
                    return true;
                }
                catch
                {
                    return false;
                }
            }
         
        }

        public bool AddNewMenu(MenuDTO model)
        {
            using (var work=new PechkaContext())
            {
                try
                {
                    if (work.Meny.FirstOrDefault(u => u.Date == model.Date) == null)
                    {
                        work.Meny.Add(model.ToMenu());
                        work.SaveChanges();
                        return true;
                    }
                    return false;
                }
                catch
                {
                    return false;
                }
            }
        }

        public List<MenuForUserDTO> GetMenuForUser(string email)
        {
            using (var work = new PechkaContext())
            {
                var userId = work.Users.FirstOrDefault(u => u.Email == email).Id;
                var menu = work.Meny.ToList();
                foreach (var item in menu)
                {
                    var elem = work.Reviews.Include(u => u.User).Where(u => u.MenuId == item.Id);
                    item.Reviews = elem.Count() != 0 ? elem.ToList() : null;
                    var order= work.Orders.FirstOrDefault(u => u.UserId == userId && u.MenuId == item.Id);
                    if(order!=null)item.Order.Add(order);
                }
                return menu.ToUserMenuDto();
            }
        }
    }
}
