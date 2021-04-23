using System;

namespace StoreManager.Shared.Models
{
    public class CreateReportRequestClaimItem
    {
        public int ExpenseCategoryID { get; set; }
        public string Payee { get; set; }
        public DateTime Date { get; set; }
        public string Description { get; set; }
        public decimal Amount { get; set; }
        public string CurrencyCode { get; set; }
        public int CurrencyID { get; set; }
        public decimal UsdAmount { get; set; }
        public byte[] Receipt { get; set; }

        // Gcc properties

        public decimal Ab { get; set; }
        public decimal Net { get; set; }
        public decimal Gst { get; set; }
        public decimal Pst { get; set; }
    }


}
