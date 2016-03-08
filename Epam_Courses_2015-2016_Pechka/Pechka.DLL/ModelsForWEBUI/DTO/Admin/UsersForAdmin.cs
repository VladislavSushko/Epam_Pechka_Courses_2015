using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pechka.DLL.ModelsForWEBUI.DTO.Admin
{
    public class UsersForAdmin
    {
        public IEnumerable<UserDto> Users { get; set; }
    }

    public class UserDto
    {
        public int Id { get; set; }

        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public int ConfirmedEmail { get; set; }
        public int RoleId { get; set; }
        public int InBlackList { get; set; }
        public decimal Money { get; set; }
    }
}
