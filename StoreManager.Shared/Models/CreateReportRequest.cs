using System;
using System.Collections.Generic;

namespace StoreManager.Shared.Models
{
    public class CreateReportRequest
    {
        public string Title { get; set; }
        public string RequesterName { get; set; }
        public int RequesterID { get; set; }
        public string ApproverName { get; set; }
        public int ApproverID { get; set; }
        public DateTime SubmitDate { get; set; }
        public decimal TotalAmount { get; set; }
        public string RequesterComments { get; set; }

        public List<CreateReportRequestClaimItem> ExpensClaimLineItems { get; set; } = new List<CreateReportRequestClaimItem>();
    }


}
