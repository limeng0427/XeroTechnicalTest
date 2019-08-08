using System;
using System.Collections.Generic;
using System.Linq;
using XeroTechnicalTest.Interfaces;
using XeroTechnicalTestLibrary;

namespace XeroTechnicalTest
{
    [Serializable()]
    public class Invoice: IEntityBase
    {
        #region Properties
        public int InvoiceNumber { get; set; }
        public DateTime InvoiceDate { get; set; }

        public List<InvoiceLine> LineItems { get; set; }
        public DateTime CreatedDateTime { get; set ; }
        public DateTime UpdatedDateTime { get; set; }
        #endregion

        #region Methods
        /// <summary>
        /// Add InvoiceLine By InvoiceLine
        /// </summary>
        /// <param name="invoiceLine"></param>
        /// <returns></returns>
        public bool AddInvoiceLine(InvoiceLine invoiceLine)
        {
            if(invoiceLine == null)
            {
                //empty item
                new LogManager().Log($"Fail to add invoice line (NULL) to invoice(Id: {InvoiceNumber})");
                return false;
            }
            if (LineItems == null)
            {
                LineItems = new List<InvoiceLine>();
            }
                
            if (!LineItems.Any(i => i.InvoiceLineId == invoiceLine.InvoiceLineId))
            {
                //add
                LineItems.Add(invoiceLine);
                new LogManager().Log($"Add invoice line (Id: {invoiceLine.InvoiceLineId}) to invoice (Id: {InvoiceNumber})");
                return true;
            }
            else
            {
                //update
                var existingItem = LineItems.Where(i => i.InvoiceLineId == invoiceLine.InvoiceLineId).First();
                LineItems.Remove(existingItem);
                LineItems.Add(invoiceLine);
                new LogManager().Log($"Update invoice line (Id: {invoiceLine.InvoiceLineId}) to invoice (Id: {InvoiceNumber})");
                return false;
            }
        }
        /// <summary>
        /// Remove InvoiceLine By InvoiceLineId
        /// </summary>
        /// <param name="invoiceLineId"></param>
        /// <returns></returns>
        public bool RemoveInvoiceLine(int invoiceLineId)
        {
            if (LineItems != null && LineItems.Any(i => i.InvoiceLineId == invoiceLineId))
            {
                LineItems.RemoveAll(i => i.InvoiceLineId == invoiceLineId);
                new LogManager().Log($"Remove invoice line (Id: {invoiceLineId}))");
                return true;
            }
            else
            {
                new LogManager().Log($"Fail to remove invoice line (Id: {invoiceLineId}))");
                return false;

            }
        }

        /// <summary>
        /// GetTotal should return the sum of (Cost * Quantity) for each line item
        /// </summary>
        public decimal GetTotal()
        {
            if (LineItems != null && LineItems.Any())
            {
                new LogManager().Log($"Get invoice total ({InvoiceNumber}): {LineItems.Select(l => l.Cost).Sum()})");
                return LineItems.Select(l => l.Cost).Sum();
            }
            else
            {
                new LogManager().Log($"No Items for invoice ({InvoiceNumber}))");
                return 0;
            }
        }

        /// <summary>
        /// MergeInvoices appends the items from the sourceInvoice to the current invoice
        /// </summary>
        /// <param name="sourceInvoice">Invoice to merge from</param>
        public Invoice MergeInvoices(Invoice sourceInvoice)
        {
            if (LineItems == null)
            {
                LineItems = new List<InvoiceLine>();
            }
            if (sourceInvoice == null)
            {
                new LogManager().Log($"Fail to MergeInvoices: Empty source invoice)");
            }
            else
            {
                foreach(var item in sourceInvoice.LineItems)
                {
                    this.AddInvoiceLine(item);
                }
            }
            return this;
        }

        /// <summary>
        /// Creates a deep clone of the current invoice (all fields and properties)
        /// </summary>
        public Invoice Clone()
        {
            return Utility.DeepClone(this);
        }

        /// <summary>
        /// Outputs string containing the following (replace [] with actual values):
        /// Invoice Number: [InvoiceNumber], InvoiceDate: [DD/MM/YYYY], LineItemCount: [Number of items in LineItems] 
        /// </summary>
        public override string ToString()
        {
            return $"Invoice Number: {InvoiceNumber}, InvoiceDate: {(InvoiceDate == DateTime.MinValue ? string.Empty : InvoiceDate.ToString("dd/MM/yyyy")) }, LineItemCount: {LineItems.Count()}";
        }
        #endregion
    }
}