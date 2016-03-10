using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Pechka.DLL.Models;
using Pechka.DLL.ModelsForWEBUI.DTO.Admin;
using Pechka.DLL.ModelsForWEBUI.DTO.User;

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

        public static List<MenuForUserDTO> ToUserMenuDto(this List<Menu> model)
        {
            var resultList=new List<MenuForUserDTO>();
            foreach (var item in model)
            {
                var newMenuForUser=new MenuForUserDTO();
                newMenuForUser.Date = item.Date;
                newMenuForUser.MenuId = item.Id;
                newMenuForUser.Body = item.Body;
                newMenuForUser.WithFirst = item.PriceWithFirst;
                newMenuForUser.WithoutFirst = item.PriceWithoutFirst;
                newMenuForUser.OrderWithFirst = item.Order==null?0: item.Order[0].WithFirst ? 1 : 0;
                newMenuForUser.OrderWithoutFirst = item.Order!=null? item.Order[0].WithoutFirst ? 1 : 0:0;
                newMenuForUser.Reviews=new List<ReviewDTO>();
                foreach (var item2 in item.Reviews)
                {
                    var newReviews=new ReviewDTO();
                    newReviews.Body = item2.Body;
                    newReviews.User=new UserForReviewDTO();
                    newReviews.User.Id = item2.UserId;
                    newReviews.User.FirstName = item2.User.FirstName;
                    newReviews.User.LastName = item2.User.LastName;
                    newReviews.User.ImgType = item2.User.ImgType;
                    newMenuForUser.Reviews.Add(newReviews);
                }
                newMenuForUser.Reviews.Add(new ReviewDTO());
                resultList.Add(newMenuForUser);
            }
            return resultList;
        }
    }
}
