using StoreManager.Application.Interfaces.Repositories;
using StoreManager.Domain.Entities.Catalog;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Caching.Distributed;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace StoreManager.Infrastructure.Repositories
{
    public class ExpenseClaimRepository : IExpenseClaimRepository
    {
        private readonly IRepositoryAsync<ExpenseClaim> _repository;
        private readonly IDistributedCache _distributedCache;

        public ExpenseClaimRepository(IDistributedCache distributedCache, IRepositoryAsync<ExpenseClaim> repository)
        {
            _distributedCache = distributedCache;
            _repository = repository;
        }

        public IQueryable<ExpenseClaim> ExpenseClaims => _repository.Entities;

        public async Task DeleteAsync(ExpenseClaim expenseClaim)
        {
            await _repository.DeleteAsync(expenseClaim);
            await _distributedCache.RemoveAsync(CacheKeys.ExpenseClaimCacheKeys.ListKey);
            await _distributedCache.RemoveAsync(CacheKeys.ExpenseClaimCacheKeys.GetKey(expenseClaim.Id));
        }

        public async Task<ExpenseClaim> GetByIdAsync(int expenseClaimId)
        {
            return await _repository.Entities.Where(p => p.Id == expenseClaimId).FirstOrDefaultAsync();
        }

        public async Task<List<ExpenseClaim>> GetListAsync()
        {
            return await _repository.Entities.ToListAsync();
        }

        public async Task<int> InsertAsync(ExpenseClaim expenseClaim)
        {
            await _repository.AddAsync(expenseClaim);
            await _distributedCache.RemoveAsync(CacheKeys.ExpenseClaimCacheKeys.ListKey);
            return expenseClaim.Id;
        }

        public async Task UpdateAsync(ExpenseClaim expenseClaim)
        {
            await _repository.UpdateAsync(expenseClaim);
            await _distributedCache.RemoveAsync(CacheKeys.ExpenseClaimCacheKeys.ListKey);
            await _distributedCache.RemoveAsync(CacheKeys.ExpenseClaimCacheKeys.GetKey(expenseClaim.Id));
        }

    }
}