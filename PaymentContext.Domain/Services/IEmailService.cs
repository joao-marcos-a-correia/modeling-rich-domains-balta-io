namespace PaymentContext.Domain.Services
{
    public interface IEmailService
    {
        void Send(string to, string mail, string subject, string body);
    }
}
