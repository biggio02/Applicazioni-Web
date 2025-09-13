using FluentValidation;
using MobiShare.Models;
namespace MobiShare.Validators
{
    public class UserValidator:AbstractValidator<Utente>
    {
        public UserValidator()
        {
            RuleSet("Login", () => {
                RuleFor(x => x.Username)
                   .NotEmpty().WithMessage("Username obbligatorio")
                   .Length(1, 50).WithMessage("Lunghezza dello username non valida (1-50)");

                 RuleFor(x => x.Password)
                    .NotEmpty().WithMessage("Password obbligatoria")
                    .Length(1, 20).WithMessage("Lunghezza della password non valida (1-20)");

            });


            RuleSet("Registration", () => {
                RuleFor(x => x.Username)
                   .NotEmpty().WithMessage("Username obbligatorio")
                   .Length(1, 50).WithMessage("Lunghezza dello username non valida (1-50)");

                RuleFor(x => x.Email)
                    .NotEmpty().WithMessage("Email obbligatoria")
                    .EmailAddress().WithMessage("Indirizzo non valido");

                RuleFor(x => x.Password)
                    .NotEmpty().WithMessage("Password obbligatoria")
                    .Length(1, 20).WithMessage("Lunghezza della password non valida (1-20)");
            });
        }
    }
}
