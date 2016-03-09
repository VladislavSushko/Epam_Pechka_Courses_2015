using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Pechka.DLL.ModelsForWEBUI;

namespace Pechka.DLL.Models
{
    public class Order
    {
        public int Id { get; set; }
        public int MenuId { get; set; }
        public bool WithFirst { get; set; }
        public bool WithoutFirst { get; set; }
        public int UserId { get; set; }
        public User User { get; set; }
        public Menu Menu { get; set; }
    }
}
