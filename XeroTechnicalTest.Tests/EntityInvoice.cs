using System;
using System.Collections.Generic;
using System.Linq;
using Xunit;

namespace XeroTechnicalTest.Tests
{
    public class EntityInvoice
    {
        [Fact]
        public void AddInvoiceLine_Add_Success()
        {

            var invoice = new Invoice()
            {
                InvoiceNumber = 1210,
                InvoiceDate = DateTime.Now
            };
            var invoiceLine = new InvoiceLine()
            {
                InvoiceLineId = 1234,
                Description = "Test Invoice Item",
                Quantity = 1,
                Cost = 10
            };
            
            invoice.AddInvoiceLine(invoiceLine);
            var expected = invoiceLine;
            var actual = invoice.LineItems.First();

            Assert.Equal(expected, actual);
        }

        [Fact]
        public void AddInvoiceLine_Update_Success()
        {

            var invoice = new Invoice()
            {
                InvoiceNumber = 1210,
                InvoiceDate = DateTime.Now,
                LineItems = new List<InvoiceLine>() {new InvoiceLine() {
                    InvoiceLineId = 1234,
                    Description = "Test Invoice Item",
                    Quantity = 2,
                    Cost = 20
                    }
                }
            };
            var invoiceLine = new InvoiceLine()
            {
                InvoiceLineId = 1234,
                Description = "Test Invoice Item",
                Quantity = 1,
                Cost = 10
            };

            invoice.AddInvoiceLine(invoiceLine);
            var expected = invoiceLine;
            var actual = invoice.LineItems.First();

            Assert.Equal(expected, actual);
        }

        [Fact]
        public void AddInvoiceLine_EmptyValue_Fail()
        {

            var invoice = new Invoice()
            {
                InvoiceNumber = 1210,
                InvoiceDate = DateTime.Now
            };
            InvoiceLine invoiceLine = null;

            Assert.False(invoice.AddInvoiceLine(invoiceLine));
        }

        [Fact]
        public void RemoveInvoiceLine_NotExist_Fail()
        {

            var invoice = new Invoice()
            {
                InvoiceNumber = 1210,
                InvoiceDate = DateTime.Now
            };
            var invoiceLineId = 9999;

            Assert.False(invoice.RemoveInvoiceLine(invoiceLineId));
        }

        [Fact]
        public void RemoveInvoiceLine_Success()
        {

            var invoice = new Invoice()
            {
                InvoiceNumber = 1210,
                InvoiceDate = DateTime.Now,
                LineItems = new List<InvoiceLine>() { new InvoiceLine(){
                    InvoiceLineId = 1234,
                    Description = "Test Invoice Item",
                    Quantity = 1,
                    Cost = 10
                    }
                }
            };
            var invoiceLine = 1234;

            invoice.RemoveInvoiceLine(invoiceLine);
            Assert.DoesNotContain(invoice.LineItems, l => l.InvoiceLineId == invoiceLine);
        }

        [Fact]
        public void GetTotal_Success()
        {

            var invoice = new Invoice()
            {
                InvoiceNumber = 1210,
                InvoiceDate = DateTime.Now,
                LineItems = new List<InvoiceLine>() {
                    new InvoiceLine(){
                        InvoiceLineId = 1234,
                        Description = "Test Invoice Item1",
                        Quantity = 1,
                        Cost = 10
                    },
                    new InvoiceLine(){
                        InvoiceLineId = 1235,
                        Description = "Test Invoice Item2",
                        Quantity = 2,
                        Cost = 50
                    },
                }
            };
            var expected = 60;
            var actual = invoice.GetTotal();
            Assert.Equal(expected, actual);
        }

        [Fact]
        public void GetTotal_Empty_Zero()
        {

            var invoice = new Invoice()
            {
                InvoiceNumber = 1210,
                InvoiceDate = DateTime.Now,
            };
            var expected = 0;
            var actual = invoice.GetTotal();
            Assert.Equal(expected, actual);
        }

        [Fact]
        public void MergeInvoice_Success()
        {

            var currentInvoice = new Invoice()
            {
                InvoiceNumber = 1210,
                InvoiceDate = DateTime.Now,
                LineItems = new List<InvoiceLine>() {
                    new InvoiceLine(){
                        InvoiceLineId = 1234,
                        Description = "Test Invoice Item1",
                        Quantity = 1,
                        Cost = 10
                    },
                    new InvoiceLine(){
                        InvoiceLineId = 1235,
                        Description = "Test Invoice Item2",
                        Quantity = 2,
                        Cost = 50
                    },
                }
            };
            var sourceInvoice = new Invoice()
            {
                InvoiceNumber = 2250,
                InvoiceDate = DateTime.Now,
                LineItems = new List<InvoiceLine>() {
                    new InvoiceLine(){
                        InvoiceLineId = 3001,
                        Description = "Test Invoice Item1",
                        Quantity = 1,
                        Cost = 10
                    },
                    new InvoiceLine(){
                        InvoiceLineId = 3002,
                        Description = "Test Invoice Item2",
                        Quantity = 2,
                        Cost = 50
                    },
                }
            };
            var expected = 4;
            var actual = currentInvoice.MergeInvoices(sourceInvoice).LineItems.Count();
            Assert.Equal(expected, actual);
        }

        [Fact]
        public void Clone_Success()
        {
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
            var actual = invoice.Clone();

            Assert.NotEqual(expected, actual);
            Assert.Equal(expected.InvoiceNumber, actual.InvoiceNumber);
            Assert.Equal(expected.InvoiceDate, actual.InvoiceDate);
            Assert.NotEqual(expected.LineItems, actual.LineItems);
            Assert.NotEqual(expected.LineItems.First(), actual.LineItems.First());
            Assert.Equal(expected.LineItems.First().InvoiceLineId, actual.LineItems.First().InvoiceLineId);
            Assert.Equal(expected.LineItems.First().Description, actual.LineItems.First().Description);
            Assert.Equal(expected.LineItems.First().Quantity, actual.LineItems.First().Quantity);
            Assert.Equal(expected.LineItems.First().Cost, actual.LineItems.First().Cost);
        }

        [Fact]
        public void ToString_Success()
        {
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
            var expected = $"Invoice Number: {invoice.InvoiceNumber}, InvoiceDate: {(invoice.InvoiceDate == DateTime.MinValue ? string.Empty : invoice.InvoiceDate.ToString("dd/MM/yyyy")) }, LineItemCount: {invoice.LineItems.Count()}"; ;
            var actual = invoice.ToString();


            Assert.Equal(expected, actual);
        }
    }
}
