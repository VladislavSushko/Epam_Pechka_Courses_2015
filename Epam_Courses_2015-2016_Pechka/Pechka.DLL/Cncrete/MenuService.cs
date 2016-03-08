using System;
using System.Collections.Generic;
using System.Data.Entity.Migrations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Pechka.DLL.Abstract;
using Pechka.DLL.Extends;
using Pechka.DLL.Models;
using Pechka.DLL.ModelsForWEBUI.DTO.Admin;

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
                    result.Add(new MenuDTO());
                    result.Sort();
                    result.Reverse();
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
    }
}
