using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Pechka.DLL.Models;
using Pechka.DLL.ModelsForWEBUI.DTO.Admin;
using Pechka.DLL.ModelsForWEBUI.DTO.User;

namespace Pechka.DLL.Abstract
{
    public interface IOrderService
    {
        Order GetUserOrderByDateAndUserId(DateTime date, int userId);
        bool AddNewOrder(MenuForUserDTO model, int userId);
        bool UpdateUserOrder(MenuForUserDTO model, int userId);
        OrdersDTO GetOrdersForAdmin(DateTime date);
    }
}
