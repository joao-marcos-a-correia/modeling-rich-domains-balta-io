using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using PaymentContext.Domain.Commands;
using PaymentContext.Domain.Entities;
using PaymentContext.Domain.Enums;
using PaymentContext.Domain.Handlers;
using PaymentContext.Domain.ValueObjects;
using PaymentContext.Tests.Mocks;

namespace PaymentContext.Tests
{
    [TestClass]
    [TestCategory("Subscription Handler")]
    public class SubscriptionHandlerTests
    {
        // Red, Green, Refactor

        [TestMethod]
        public void ShouldReturnErrorWhenDocumentsExists()
        {
            var handler = new SubscriptionHandler(new FakeStudentRepository(), new FakeEmailService());
            var command = new CreateBilletSubscriptionCommand();

            command.FirstName = "Joao";
            command.LastName = "Marcos";
            command.Document = "12345678909";
            command.Email = "joao@joao.com";
            command.BarCode = "213455";
            command.BilletNumber = "13213123";
            command.PaymentNumber = "123123";
            command.PaidDate = DateTime.Now;
            command.ExpireDate = DateTime.Now.AddMonths(1);
            command.Total = 60;
            command.TotalPaid = 60;
            command.Payer = "Marcos";
            command.PayerDocument = "2213213213123";
            command.PayerDocumentType = EDocumentType.CPF;
            command.PayerEmail = "joao@joao";
            command.Street = "adsad";
            command.Number = "1321";
            command.Neighborhood = "adsad";
            command.City = "dasd";
            command.State = "as";
            command.Country = "as";
            command.ZipCode = "123456";

            handler.Handle(command);
            Assert.AreNotEqual(true, handler.Invalid);

        }

    }
}
