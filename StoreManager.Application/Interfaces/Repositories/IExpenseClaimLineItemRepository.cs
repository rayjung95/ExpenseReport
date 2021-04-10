using StoreManager.Domain.Entities.Catalog;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace StoreManager.Application.Interfaces.Repositories
{
    public interface IExpenseClaimLineItemRepository
    {
        IQueryable<ExpenseClaimLineItem> ExpenseClaimLineItems { get; }

        Task<List<ExpenseClaimLineItem>> GetListAsync();

        Task<ExpenseClaimLineItem> GetByIdAsync(int expenseClaimLineItemId);

        Task<int> InsertAsync(ExpenseClaimLineItem expenseClaimLineItem);

        Task UpdateAsync(ExpenseClaimLineItem expenseClaimLineItem);

        Task DeleteAsync(ExpenseClaimLineItem expenseClaimLineItem);
    }
}