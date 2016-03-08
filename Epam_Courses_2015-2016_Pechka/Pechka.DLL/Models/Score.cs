using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pechka.DLL.Models
{
    public class Score
    {
        public int Id { get; set; }
        public decimal Money { get; set; }
        public DateTime Date { get; set; }
        public int UserId { get; set; }
    }
}
