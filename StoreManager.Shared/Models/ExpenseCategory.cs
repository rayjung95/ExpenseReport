using System;
using System.Collections.Generic;
using System.Text;

namespace StoreManager.Shared.Models
{
    public class ExpenseCategory
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Code { get; set; }

        //public List<ExpenseClaimLineItem> ExpensClaimLineItems { get; set; } = new List<ExpenseClaimLineItem>();


    }
}
