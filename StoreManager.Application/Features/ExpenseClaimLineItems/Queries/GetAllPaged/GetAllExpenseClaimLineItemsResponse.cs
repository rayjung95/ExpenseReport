using System;

namespace StoreManager.Application.Features.ExpenseClaimLineItems.Queries.GetAllPaged
{
    public class GetAllExpenseClaimLineItemsResponse
    {
        public int Id { get; set; }
        public int ExpenseClaimID { get; set; }
        public int ExpenseCategoryID { get; set; }
        public string Payee { get; set; }
        public DateTime Date { get; set; }
        public string Description { get; set; }
        public decimal Amount { get; set; }
        public string CurrencyCode { get; set; }

        public int CurrencyID { get; set; }
        public decimal UsdAmount { get; set; }
        public byte[] Receipt { get; set; }
    }
}