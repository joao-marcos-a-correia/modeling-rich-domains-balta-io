using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using PaymentContext.Domain.Commands;
using PaymentContext.Domain.Entities;
using PaymentContext.Domain.Enums;
using PaymentContext.Domain.Handlers;
using PaymentContext.Domain.Queries;
using PaymentContext.Domain.ValueObjects;
using PaymentContext.Tests.Mocks;

namespace PaymentContext.Tests.Queries
{
    [TestClass]
    [TestCategory("Student Queries")]
    public class StudentQueriesTests
    {
        private IList<Student> _students;

        public StudentQueriesTests()
        {
            _students = new List<Student>();

            for (int i = 0; i <= 10; i++)
            {
                _students.Add(new Student(
                    new Name("Joao", i.ToString()),
                    new Document("9999999999" + i.ToString(), EDocumentType.CPF),
                    new Email("Joao@joao.com")
                ));
            }
        }

        // Red, Green, Refactor

        [TestMethod]
        public void ShouldReturnNullWhenDocumentsNotExists()
        {
            var exp = StudentQueries.GetStudentInfo("12345678909");
            var student = _students.AsQueryable().Where(exp).FirstOrDefault();

            Assert.AreEqual(null, student);
        }

        [TestMethod]
        public void ShouldReturnStudentWhenDocumentsExists()
        {
            var exp = StudentQueries.GetStudentInfo("99999999991");
            var student = _students.AsQueryable().Where(exp).FirstOrDefault();

            Assert.AreNotEqual(null, student);
        }
    }
}
