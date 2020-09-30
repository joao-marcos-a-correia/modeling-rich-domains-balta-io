using System;
using Flunt.Notifications;
using Flunt.Validations;
using PaymentContext.Domain.Commands;
using PaymentContext.Domain.Entities;
using PaymentContext.Domain.Enums;
using PaymentContext.Domain.Repositories;
using PaymentContext.Domain.Services;
using PaymentContext.Domain.ValueObjects;
using PaymentContext.Shared.Commands;
using PaymentContext.Shared.Handlers;

namespace PaymentContext.Domain.Handlers
{
    public class SubscriptionHandler:
        Notifiable,
        IHandler<CreateBilletSubscriptionCommand>
        {
            private readonly IStudentRepository _studentRepository;
            private readonly IEmailService _emailService;

            public SubscriptionHandler(
                IStudentRepository studentRepository,
                IEmailService emailService
            )
            {
                this._studentRepository = studentRepository;
                this._emailService = emailService;
            }

            public ICommandResult Handle(CreateBilletSubscriptionCommand command)
            {
                //Fail Fast Validations
                command.Validate();
                if (command.Invalid)
                {
                    AddNotifications(command);
                    return new CommandResult(false, "Não foi possivel realizar seu cadastro");
                }
                //Verificar se documento ja esta cadastrado
                if (_studentRepository.DocumentExists(command.Document))
                {
                    AddNotification("command.Document", "Este CPF já está em uso");
                }

                //Verificar se email ja esta cadastrado
                if (_studentRepository.EmailExists(command.Email))
                {
                    AddNotification("command.Email", "Este E-mail já está em uso");
                }

                //Gerar os VOs
                var name = new Name(command.FirstName, command.LastName);
                var document = new Document(command.Document, EDocumentType.CPF);
                var email = new Email(command.Email);
                var address = new Address(command.Street, command.Number, command.Neighborhood, command.City, command.State, command.Country, command.ZipCode);

                //Gerar as Entidades
                var student = new Student(name, document, email);
                var subscription = new Subscription(DateTime.Now.AddMonths(1));
                var payment = new BilletPayment(
                    command.BarCode,
                    command.BilletNumber,
                    command.PaidDate,
                    command.ExpireDate,
                    command.Total,
                    command.TotalPaid,
                    command.Payer,
                    new Document(command.PayerDocument, command.PayerDocumentType),
                    address,
                    email
                );

                //Relacionamentos
                subscription.AddPayment(payment);
                student.AddSubscription(subscription);

                //Agrupar as validações
                AddNotifications(name, document, email, address, student, subscription, payment);

                //Checar as notificações
                if (Invalid)
                    return new CommandResult(false, "Não foi possível realizar sua assinatura");

                //Salvar as informações
                _studentRepository.CreateSubscription(student);

                //Enviar email de boas vindas
                _emailService.Send(student.Name.ToString(), student.Email.Address, "Bem Vindo ao balta.io", "Sua assinatura foi criada com sucesso");

                //Retornar informações

                return new CommandResult(true, "Assinatura realizada com sucesso");
            }
        }
}
