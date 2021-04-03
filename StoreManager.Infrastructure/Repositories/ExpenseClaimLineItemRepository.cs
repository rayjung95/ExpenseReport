using StoreManager.Application.Interfaces.Repositories;
using StoreManager.Domain.Entities.Catalog;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Caching.Distributed;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace StoreManager.Infrastructure.Repositories
{
    public class ExpenseClaimLineItemRepository : IExpenseClaimLineItemRepository
    {
        private readonly IRepositoryAsync<ExpenseClaimLineItem> _repository;
        private readonly IDistributedCache _distributedCache;

        public ExpenseClaimLineItemRepository(IDistributedCache distributedCache, IRepositoryAsync<ExpenseClaimLineItem> repository)
        {
            _distributedCache = distributedCache;
            _repository = repository;
        }

        public IQueryable<ExpenseClaimLineItem> ExpenseClaimLineItems => _repository.Entities;

        public async Task DeleteAsync(ExpenseClaimLineItem expenseClaimLineItem)
        {
            await _repository.DeleteAsync(expenseClaimLineItem);
            await _distributedCache.RemoveAsync(CacheKeys.ExpenseClaimLineItemCacheKeys.ListKey);
            await _distributedCache.RemoveAsync(CacheKeys.ExpenseClaimLineItemCacheKeys.GetKey(expenseClaimLineItem.Id));
        }

        public async Task<ExpenseClaimLineItem> GetByIdAsync(int expenseClaimLineItemId)
        {
            return await _repository.Entities.Where(p => p.Id == expenseClaimLineItemId).FirstOrDefaultAsync();
        }

        public async Task<List<ExpenseClaimLineItem>> GetListAsync()
        {
            return await _repository.Entities.ToListAsync();
        }

        public async Task<int> InsertAsync(ExpenseClaimLineItem expenseClaimLineItem)
        {
            await _repository.AddAsync(expenseClaimLineItem);
            await _distributedCache.RemoveAsync(CacheKeys.ExpenseClaimLineItemCacheKeys.ListKey);
            return expenseClaimLineItem.Id;
        }

        public async Task UpdateAsync(ExpenseClaimLineItem expenseClaimLineItem)
        {
            await _repository.UpdateAsync(expenseClaimLineItem);
            await _distributedCache.RemoveAsync(CacheKeys.ExpenseClaimLineItemCacheKeys.ListKey);
            await _distributedCache.RemoveAsync(CacheKeys.ExpenseClaimLineItemCacheKeys.GetKey(expenseClaimLineItem.Id));
        }

    }
}