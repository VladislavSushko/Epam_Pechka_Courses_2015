using System.ComponentModel.DataAnnotations;
namespace Pechka.DLL.ModelsForWEBUI
{
    public class LogViewModel
    {
        [Required]
        [Display(Name = "Логин")]
        public string Email { get; set; }

        [Required]
        [DataType(DataType.Password)]
        [Display(Name = "Пароль")]
        public string Password { get; set; }
    }
}
