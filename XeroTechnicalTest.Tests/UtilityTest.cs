using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using XeroTechnicalTestLibrary;
using Xunit;

namespace XeroTechnicalTest.Tests
{
    public class UnitTest
    {
        [Fact]
        public void DeepClone_ContentEqual()
        {
            //var mock = new Mock<Invoice>();
            //mock.Object.InvoiceNumber = 1;
            //mock.Object.InvoiceDate = DateTime.Today;
            //mock.Object.LineItems = new List<InvoiceLine>() {new InvoiceLine()
            //{
            //    InvoiceLineId = 11,
            //    Description = "Subscription 1 month",
            //    Quantity = 12,
            //    Cost = 120m
            //} }; 

            var invoice = new Invoice()
            {
                InvoiceNumber = 1,
                InvoiceDate = DateTime.Today,
                LineItems = new List<InvoiceLine>()
            };
            invoice.LineItems.Add(new InvoiceLine()
            {
                InvoiceLineId = 11,
                Description = "Subscription 1 month",
                Quantity = 12,
                Cost = 120m
            });
            var expected = invoice;
            var actual = Utility.DeepClone(invoice);

            Assert.NotEqual(expected,actual);
            Assert.Equal(expected.InvoiceNumber, actual.InvoiceNumber);
            Assert.Equal(expected.InvoiceDate, actual.InvoiceDate);
            Assert.NotEqual(expected.LineItems, actual.LineItems);
            Assert.NotEqual(expected.LineItems.First(), actual.LineItems.First());
            Assert.Equal(expected.LineItems.First().InvoiceLineId, actual.LineItems.First().InvoiceLineId);
            Assert.Equal(expected.LineItems.First().Description, actual.LineItems.First().Description);
            Assert.Equal(expected.LineItems.First().Quantity, actual.LineItems.First().Quantity);
            Assert.Equal(expected.LineItems.First().Cost, actual.LineItems.First().Cost);
        }
    }
}
