using Homework_20.Models;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace Homework_20.Extensions
{
    public class CustomPasswordValidator : IPasswordValidator<ApplicationUser>
    {
        public int RequiredLength { get; set; }

        public CustomPasswordValidator(int length)
        {
            RequiredLength = length;
        }

        public Task<IdentityResult> ValidateAsync(UserManager<ApplicationUser> manager, ApplicationUser user, string password)
        {
            List<IdentityError> errors = new List<IdentityError>();

            //Валидация длины пароля
            if (String.IsNullOrEmpty(password) || password.Length < RequiredLength)
            {
                errors.Add(new IdentityError
                {
                    Description = $"Минимальная длина пароля равна {RequiredLength}"
                });
            }

            string numPattern = "[0-9]";

            if (!Regex.IsMatch(password, numPattern))
            {
                errors.Add(new IdentityError
                {
                    Description = "Пароль должен состоять как минимум из одной цифры («0» - «9»)."
                });
            }

            string cursiveLetterPattern = "[A-Z]";

            if (!Regex.IsMatch(password, cursiveLetterPattern))
            {
                errors.Add(new IdentityError
                {
                    Description = "Пароль должен содержать хотя бы одну заглавную букву («A» - «Z»)."
                });
            }

            string specialCharactersPattern = @"[^\w^\s]";

            if (!Regex.IsMatch(password, specialCharactersPattern))
            {
                errors.Add(new IdentityError
                {
                    Description = "Пароль должен содержать хотя бы один спецсимвол."
                });
            }

            return Task.FromResult(errors.Count == 0 ?
                IdentityResult.Success : IdentityResult.Failed(errors.ToArray()));
        }
    }
}
