using System;
using System.Collections.Generic;
using System.Text;
using XeroTechnicalTest.BusinessServices;
using Xunit;

namespace XeroTechnicalTest.Tests
{
    public class ServiceInvoice
    {
        [Fact]
        public void AddInvoiceLine_Add_Success()
        {

            var expected = 6.99m;
            var actual = new InvoiceService().CreateInvoiceWithOneItem();

            Assert.Equal(expected, actual);
        }
        [Fact]
        public void CreateInvoiceWithMultipleItemsAndQuantities()
        {

            var expected = 20.63m;
            var actual = new InvoiceService().CreateInvoiceWithMultipleItemsAndQuantities();

            Assert.Equal(expected, actual);
        }
        [Fact]
        public void RemoveItem()
        {

            var expected = 10.99m;
            var actual = new InvoiceService().RemoveItem();

            Assert.Equal(expected, actual);
        }
        [Fact]
        public void MergeInvoices()
        {

            var expected = 21.82m;
            var actual = new InvoiceService().MergeInvoices();

            Assert.Equal(expected, actual);
        }
        [Fact]
        public void CloneInvoice()
        {

            var expected = 13.26m;
            var actual = new InvoiceService().CloneInvoice();

            Assert.Equal(expected, actual);
        }

        [Fact]
        public void InvoiceToString()
        {

            var expected = $"Invoice Number: 1000, InvoiceDate: {DateTime.Now.ToString("dd/MM/yyyy") }, LineItemCount: 1";
            var actual = new InvoiceService().InvoiceToString();

            Assert.Equal(expected, actual);
        }
    }
}
