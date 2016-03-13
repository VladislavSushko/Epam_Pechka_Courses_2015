using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Pechka.DLL.Models;

namespace Pechka.DLL.ModelsForWEBUI.DTO.Admin
{
    public class OrdersDTO
    {
        public DateTime Date { get; set; }
        public List<Order> Orders { get; set; }
        public int CountOrdersWithFirst { get; set; }
        public int CountOrdersWithoutFirst { get; set; }
        public string StartDate { get; set; }
    }
}
