using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pechka.DLL.Models
{
    public class Menu
    {
        public int Id { get; set; }
        public DateTime Date { get; set; }
        public string Body { get; set; }
        public decimal PriceWithFirst { get; set; }
        public decimal PriceWithoutFirst { get; set; }
        public bool IsCompleted { get; set; }
    }
}
