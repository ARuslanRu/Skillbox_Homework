using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Homework_20.ViewModels
{
    public class RegisterViewModel
    {
        [Required(ErrorMessage = "Поле Логин обязательное для заполнения.")]
        [Display(Name = "Логин")]
        public string Login { get; set; }

        [Required(ErrorMessage = "Поле Пароль обязательное для заполнения.")]
        [StringLength(100, MinimumLength = 6, ErrorMessage = "{0} должен быть от {2} до {1} символов.")]
        [DataType(DataType.Password)]
        [Display(Name = "Пароль")]
        public string Password { get; set; }

        [DataType(DataType.Password)]
        [Display(Name = "Подтвердить пароль")]
        [Compare("Password", ErrorMessage = "Пароль и пароль подтверждения не совпадают.")]
        public string ConfirmPassword { get; set; }
    }
}