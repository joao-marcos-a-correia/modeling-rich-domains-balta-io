using Flunt.Validations;
using PaymentContext.Shared.ValueObjects;

namespace PaymentContext.Domain.ValueObjects
{
    public class Email : ValueObject
    {
        public Email (string email)
        {
            Address = email;

            AddNotifications (new Contract ()
                .Requires ()
                .IsEmail (Address, "Email.Address", "Email Inválido")
            );
        }

        public string Address { get; private set; }
    }
}
