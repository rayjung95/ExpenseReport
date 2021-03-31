using AspNetCoreHero.Abstractions.Domain;
using System;
using System.Collections.Generic;

namespace StoreManager.Domain.Entities.Catalog
{
    public class ExpenseClaim : AuditableEntity
    {
        public int Id { get; set; }
        public string Title { get; set; }

        public string RequesterName { get; set; }
        public int RequesterID { get; set; }
        public string ApproverName { get; set; }
        public int ApproverID { get; set; }
        public DateTime SubmitDate { get; set; }
        public DateTime ApprovalDate { get; set; }
        public DateTime ProcessedDate { get; set; }
        public decimal TotalAmount { get; set; }
        public string Status { get; set; }
        public string RequesterComments { get; set; }
        public string ApproverComments { get; set; }
        public string FinanceComments { get; set; }

        public List<ExpenseClaimLineItem> ExpensClaimLineItems { get; set; } = new List<ExpenseClaimLineItem>();


        // GCC Properties

        public DateTime PreparedDate { get; set; }
        public DateTime RecieptDate { get; set; }

        public string Ministry { get; set; }
        public string TeamName { get; set; }
        public int TeamNumber { get; set; }
        public int MyProperty { get; set; }

        public string Payee { get; set; }
        public string ProjectName { get; set; }
        public string BudgetConfirm { get; set; }




    }
}