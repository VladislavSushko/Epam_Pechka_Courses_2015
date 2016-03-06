using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pechka.DLL.ModelsForWEBUI
{
    public class RegistrationModel
    {
        [Required]
        [Display(Name = "Имя")]
        public string UserFirstName { get; set; }
        [Required]
        [Display(Name = "Фамилия")]
        public string UserLastName { get; set; }
        [Required]
        [Display(Name = "Пароль")]
        public string Password { get; set; }
        [Required]
        [Display(Name = "Email")]
        public string Email { get; set; }
        public string ImgType { get; set; }
        public byte[] ImgData { get; set; }
    }
}
