using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pechka.DLL.ModelsForWEBUI.DTO.User
{
    public class MenuForUserDTO
    {
        public int MenuId { get;  set; }
        public string Body { get; set; }
        public DateTime Date { get; set; }
        public decimal WithFirst { get; set; }
        public decimal WithoutFirst { get; set; }
        public int OrderWithFirst { get; set; }
        public int OrderWithoutFirst { get; set; }
        public string NewReview { get; set; }
        public List<ReviewDTO> Reviews { get; set; }

    }
}
