using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Pechka.DLL.ModelsForWEBUI;

namespace Pechka.DLL.Models
{
    public class Review
    {
        public int Id { get; set; }
        public string Body { get; set; }
        public int UserId { get; set; }
        public int MenuId { get; set; }
        public User User { get; set; }
        public Menu Menu { get; set; }
    }
}
