using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StoreManager.Domain.Entities.Catalog
{
    public class ExpenseCategory
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Code { get; set; }

        public List<ExpenseClaimLineItem> ExpensClaimLineItems { get; set; } = new List<ExpenseClaimLineItem>();


    }
}
