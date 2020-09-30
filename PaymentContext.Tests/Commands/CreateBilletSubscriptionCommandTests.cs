using Microsoft.VisualStudio.TestTools.UnitTesting;
using PaymentContext.Domain.Commands;
using PaymentContext.Domain.Entities;
using PaymentContext.Domain.Enums;
using PaymentContext.Domain.ValueObjects;

namespace PaymentContext.Tests
{
    [TestClass]
    [TestCategory("CreateBilletSubscriptionCommand")]
    public class CreateBilletSubscriptionCommandTests
    {
        // Red, Green, Refactor

        [TestMethod]
        public void ShouldReturnErrorWhenNameIsInvalid()
        {
            var command = new CreateBilletSubscriptionCommand();
            command.FirstName = "";

            command.Validate();
            Assert.AreEqual(false, command.Valid);
        }

    }
}
