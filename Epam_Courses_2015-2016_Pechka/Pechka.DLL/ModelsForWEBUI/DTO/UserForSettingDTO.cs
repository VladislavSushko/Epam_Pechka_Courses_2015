using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pechka.DLL.ModelsForWEBUI.DTO
{
    public class UserForSettingDTO
    {
        [Required]
        [Display(Name = "Email")]
        public string Email { get; set; }
        [Required]
        [Display(Name = "Имя")]
        public string FirstName { get; set; }
        [Required]
        [Display(Name = "Фамилия")]
        public string LastName { get; set; }
        [Required]
        [Display(Name = "Старый пароль")]
        public string Password { get; set; }
        [Required]
        [Display(Name = "Новый пароль")]
        public string NewPassword { get; set; }
        public string ImgType { get; set; }
        public byte[] ImgData { get; set; }
    }
}
