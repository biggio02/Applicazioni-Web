using System.Globalization;
using FluentValidation;
using MobiShare.Models;
namespace MobiShare.Validators
{
    public class PaymentValidator : AbstractValidator<CartaCredito>
    {
        public PaymentValidator()
        {
            RuleFor(x => x.Scadenza)
                .NotEmpty().WithMessage("La data di scadenza è obbligatoria")
                .Matches(@"^(0[1-9]|1[0-2])\/[0-9]{2}$").WithMessage("Formato scadenza non valido (MM/YY)")
                .Must(IsFutureDate).WithMessage("La carta è scaduta");

            RuleFor(x => x.Numero)
                .NotEmpty().WithMessage("Numero di carta obbligatorio")
                .Length(5, 20).WithMessage("Lunghezza della carta non valida (5-20)");

            RuleFor(x => x.Cvc)
                .NotEmpty().WithMessage("CVC obbligatorio")
                .Length(3, 4).WithMessage("CVC deve essere lungo 3 o 4 cifre");

            RuleFor(x => x.Importo)
                .NotEmpty().WithMessage("Inserire la somma da ricaricare")
                .GreaterThan(1).WithMessage("La somma da ricaricare deve essere maggiore di 1");
        }

        private bool IsFutureDate(string scadenza)
        {
            if (string.IsNullOrEmpty(scadenza)) return false;

            if (DateTime.TryParseExact(scadenza, "MM/yy",
                CultureInfo.InvariantCulture,
                DateTimeStyles.None,
                out var d))
            {
                var lastDay = new DateTime(d.Year, d.Month, DateTime.DaysInMonth(d.Year, d.Month));
                return lastDay >= DateTime.Today;
            }

            return false;
        }


    }
}
