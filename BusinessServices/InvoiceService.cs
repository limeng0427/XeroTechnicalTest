using System;
using System.Collections.Generic;
using XeroTechnicalTest.Interfaces;
using XeroTechnicalTestLibrary;

namespace XeroTechnicalTest.BusinessServices
{
    public class InvoiceService: IInvoiceService
    {
        public decimal CreateInvoiceWithOneItem()
        {
            var invoice = new Invoice();

            invoice.AddInvoiceLine(new InvoiceLine()
            {
                InvoiceLineId = 1,//"1"
                Cost = 6.99m,
                Quantity = 1,
                Description = "Apple"
            });

            decimal total = invoice.GetTotal();
            new LogManager().Log($"CreateInvoiceWithOneItem(). Total: {total}");
            return total;
        }

        public decimal CreateInvoiceWithMultipleItemsAndQuantities()
        {
            var invoice = new Invoice();

            invoice.AddInvoiceLine(new InvoiceLine()
            {
                InvoiceLineId = 1,
                Cost = 10.21m,
                Quantity = 4,
                Description = "Banana"
            });

            invoice.AddInvoiceLine(new InvoiceLine()
            {
                InvoiceLineId = 2,
                Cost = 5.21m,
                Quantity = 1,
                Description = "Orange"
            });

            invoice.AddInvoiceLine(new InvoiceLine()
            {
                InvoiceLineId = 3,
                Cost = 5.21m,
                Quantity = 5,
                Description = "Pineapple"
            });
            var total = invoice.GetTotal();
            new LogManager().Log($"CreateInvoiceWithMultipleItemsAndQuantities(). Total: {total}");
            return total;
        }

        public decimal RemoveItem()
        {
            var invoice = new Invoice();

            invoice.AddInvoiceLine(new InvoiceLine()
            {
                InvoiceLineId = 1,
                Cost = 5.21m,
                Quantity = 1,
                Description = "Orange"
            });

            invoice.AddInvoiceLine(new InvoiceLine()
            {
                InvoiceLineId = 2,
                Cost = 10.99m,
                Quantity = 4,
                Description = "Banana"
            });

            invoice.RemoveInvoiceLine(1);
            var total = invoice.GetTotal();
            new LogManager().Log($"RemoveItem(). Total: {total}");
            return total;
        }

        public decimal MergeInvoices()
        {
            var invoice1 = new Invoice();

            invoice1.AddInvoiceLine(new InvoiceLine()
            {
                InvoiceLineId = 1,
                Cost = 10.33m,
                Quantity = 4,
                Description = "Banana"
            });

            var invoice2 = new Invoice();

            invoice2.AddInvoiceLine(new InvoiceLine()
            {
                InvoiceLineId = 2,
                Cost = 5.22m,
                Quantity = 1,
                Description = "Orange"
            });

            invoice2.AddInvoiceLine(new InvoiceLine()
            {
                InvoiceLineId = 3,
                Cost = 6.27m,
                Quantity = 3,
                Description = "Blueberries"
            });

            invoice1.MergeInvoices(invoice2);
            var total = invoice1.GetTotal();
            new LogManager().Log($"MergeInvoices(). Total: {total}");
            return total;
        }

        public decimal CloneInvoice()
        {
            var invoice = new Invoice();

            invoice.AddInvoiceLine(new InvoiceLine()
            {
                InvoiceLineId = 1,
                Cost = 6.99m,
                Quantity = 1,
                Description = "Apple"
            });

            invoice.AddInvoiceLine(new InvoiceLine()
            {
                InvoiceLineId = 2,
                Cost = 6.27m,
                Quantity = 3,
                Description = "Blueberries"
            });

            var clonedInvoice = invoice.Clone();
            var total = clonedInvoice.GetTotal();
            new LogManager().Log($"CloneInvoice(). Total: {total}");
            return total;
        }

        public string InvoiceToString()
        {
            var result = string.Empty;
            var invoice = new Invoice()
            {
                InvoiceDate = DateTime.Now,
                InvoiceNumber = 1000,
                LineItems = new List<InvoiceLine>()
                {
                    new InvoiceLine()
                    {
                        InvoiceLineId = 1,
                        Cost = 6.99m,
                        Quantity = 1,
                        Description = "Apple"
                    }
                }
            };
            result = invoice.ToString();
            new LogManager().Log($"InvoiceToString(). Result: {result}");
            return result;

        }
    }
}
