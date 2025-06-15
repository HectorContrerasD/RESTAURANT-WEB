using FluentValidation;
using FluentValidation.Results;
using System;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Restaurant_Cocineros.Models.Validators
{
    public static class LoginValidator
    {
       public static ValidationResult Validate(LoginDTO login)
        {
            var validator = new InlineValidator<LoginDTO>();
            validator.RuleFor(x => x.Username).NotEmpty().WithMessage("Ingrese un nombre de usuario");
            validator.RuleFor(x => x.Password).NotEmpty().WithMessage("Ingrese una contraseña");
            return validator.Validate(login);
        }
    }
}
