using System.ComponentModel.DataAnnotations;

namespace Homework_20.ViewModels
{
    public class LoginViewModel
    {
        [Required(ErrorMessage = "Поле Логин обязательное для заполнения")]
        [Display(Name = "Логин")]
        public string Login { get; set; }

        [Required(ErrorMessage = "Поле Пароль обязательное для заполнения")]
        [DataType(DataType.Password)]
        [Display(Name = "Пароль")]
        public string Password { get; set; }

        [Display(Name = "Запомнить меня?")]
        public bool RememberMe { get; set; }
    }
}
