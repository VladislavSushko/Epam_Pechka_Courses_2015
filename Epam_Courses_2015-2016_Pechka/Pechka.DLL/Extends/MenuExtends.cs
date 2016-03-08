using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Pechka.DLL.Models;
using Pechka.DLL.ModelsForWEBUI.DTO.Admin;

namespace Pechka.DLL.Extends
{
    public static class MenuExtends
    {
        public static MenuDTO ToMenuDTO(this Menu model)
        {
            var castedMenu=new MenuDTO();
            castedMenu.Id = model.Id;
            castedMenu.Body = model.Body;
            castedMenu.Date = model.Date;
            castedMenu.IsCompleted = model.IsCompleted ? 1 : 0;
            castedMenu.PriceWithFirst = model.PriceWithFirst;
            castedMenu.PriceWithoutFirst = model.PriceWithoutFirst;
            return castedMenu;
        }

        public static Menu ToMenu(this MenuDTO model)
        {
            var castedMenu = new Menu();
            castedMenu.IsCompleted = model.IsCompleted != 0;
            castedMenu.Body = model.Body;
            castedMenu.Date = model.Date;
            castedMenu.PriceWithFirst = model.PriceWithFirst;
            castedMenu.PriceWithoutFirst = model.PriceWithoutFirst;
            return castedMenu;
        }
    }
}
