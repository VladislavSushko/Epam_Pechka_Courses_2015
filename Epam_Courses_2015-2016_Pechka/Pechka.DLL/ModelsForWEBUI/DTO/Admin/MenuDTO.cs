using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pechka.DLL.ModelsForWEBUI.DTO.Admin
{
    public class MenuDTO:IComparable
    {
        public int Id { get; set; }
        public DateTime Date { get; set; }
        public string Body { get; set; }
        public decimal PriceWithFirst { get; set; }
        public decimal PriceWithoutFirst { get; set; }
        public int IsCompleted { get; set; }

        public int CompareTo(object obj)
        {
            var casted = obj as MenuDTO;
            return Date.CompareTo(casted.Date);
        }
    }
}
