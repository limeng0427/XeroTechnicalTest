using System;

namespace XeroTechnicalTest
{
    #region Properties
    [Serializable()]
    public class InvoiceLine
    {
        public int InvoiceLineId { get; set; }
        public string Description { get; set; }
        public int Quantity { get; set; }
        public decimal Cost { get; set; }
        public DateTime CreatedDateTime { get; set; }
        public DateTime UpdatedDateTime { get; set; }
    }
    #endregion

    #region Methods
    #endregion
}