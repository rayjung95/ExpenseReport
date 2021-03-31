﻿using System;

namespace StoreManager.Application.Features.ExpenseClaims.Queries.GetById
{
    public class GetExpenseClaimByIdResponse
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
    }
}