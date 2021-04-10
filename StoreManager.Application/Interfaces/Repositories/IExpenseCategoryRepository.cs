using StoreManager.Domain.Entities.Catalog;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace StoreManager.Application.Interfaces.Repositories
{
    public interface IExpenseCategoryRepository
    {
        IQueryable<ExpenseCategory> ExpenseCategories { get; }

        Task<List<ExpenseCategory>> GetListAsync();

        Task<ExpenseCategory> GetByIdAsync(int expenseCategoryId);

        Task<int> InsertAsync(ExpenseCategory expenseCategory);

        Task UpdateAsync(ExpenseCategory expenseCategory);

        Task DeleteAsync(ExpenseCategory expenseCategory);
    }
}