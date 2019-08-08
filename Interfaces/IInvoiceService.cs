using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace XeroTechnicalTest.Interfaces
{
    public interface IInvoiceService:IServiceBase
    {
        decimal CreateInvoiceWithOneItem();

        decimal CreateInvoiceWithMultipleItemsAndQuantities();

        decimal RemoveItem();

        decimal MergeInvoices();

        decimal CloneInvoice();

        string InvoiceToString();
    }
}
