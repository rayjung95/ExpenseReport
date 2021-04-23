using StoreManager.Application.Features.ExpenseClaims.Commands.Create;
using StoreManager.Domain.Entities.Catalog;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace StoreManager.Application.Interfaces.Repositories
{
    public interface IExpenseClaimRepository
    {
        IQueryable<ExpenseClaim> ExpenseClaims { get; }

        Task<List<ExpenseClaim>> GetListAsync();

        Task<ExpenseClaim> GetByIdAsync(int expenseClaimId);

        Task<ExpenseClaim> GetReportByIdAsync(int expenseClaimId);

        Task<int> InsertAsync(ExpenseClaim expenseClaim);

        Task UpdateAsync(ExpenseClaim expenseClaim);

        Task DeleteAsync(ExpenseClaim expenseClaim);

        Task<int> CreateExpenseReport(CreateExpenseClaimReportCommand request);

    }
}