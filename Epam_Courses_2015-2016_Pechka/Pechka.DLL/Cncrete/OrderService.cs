using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data.Entity;
using System.Data.Entity.Migrations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Pechka.DLL.Abstract;
using Pechka.DLL.Models;
using Pechka.DLL.ModelsForWEBUI.DTO.Admin;
using Pechka.DLL.ModelsForWEBUI.DTO.User;

namespace Pechka.DLL.Cncrete
{
    public class OrderService:IOrderService
    {
        public Order GetUserOrderByDateAndUserId(DateTime date,int userId)
        {
            using (var work = new PechkaContext())
            {
                return
                    work.Orders.Include(u => u.Menu)
                        .Include(u => u.User)
                        .FirstOrDefault(u => u.UserId == userId && u.Menu.Date == date);
            }
        }

        public bool AddNewOrder(MenuForUserDTO model, int userId)
        {
            using (var work = new PechkaContext())
            {
               var orderToAdd=new Order();
                orderToAdd.UserId = userId;
                orderToAdd.MenuId = model.MenuId;
                orderToAdd.WithoutFirst = model.OrderWithoutFirst != 0;
                orderToAdd.WithFirst = model.OrderWithFirst != 0;
                work.Orders.Add(orderToAdd);
                work.SaveChanges();
                return true;

            }
        }

        public bool UpdateUserOrder(MenuForUserDTO model, int userId)
        {
            
            using (var work = new PechkaContext())
            {
                var orderForUpdate = work.Orders.FirstOrDefault(u => u.MenuId == model.MenuId && u.UserId == userId);
                orderForUpdate.WithFirst = model.OrderWithFirst != 0;
                orderForUpdate.WithoutFirst = model.OrderWithoutFirst != 0;
                work.Orders.AddOrUpdate(orderForUpdate);
                work.SaveChanges();
                return true;
            }
        }

        public OrdersDTO GetOrdersForAdmin(DateTime date)
        {
            using (var work = new PechkaContext())
            {
                var result = new OrdersDTO();
                   result.Orders= work.Orders.Include(u=>u.User).Where(u => u.Menu.Date == date).ToList();
                result.Date = date;
                result.CountOrdersWithFirst = result.Orders.Count(u => u.WithFirst == true);
                result.CountOrdersWithoutFirst= result.Orders.Count(u => u.WithoutFirst == true);
                return result;
            }
            
        }
    }
}
