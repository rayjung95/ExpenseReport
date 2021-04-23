using System;
using System.Collections.Generic;
using System.Text;

namespace StoreManager.Shared.Models
{
    public class ReportsPagingResponse : BaseGetAllAPIResponse
    {
        public ExpenseClaim[] Data { get; set; }
    }

}
