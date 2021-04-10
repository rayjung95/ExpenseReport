using StoreManager.Application.Interfaces.Repositories;
using StoreManager.Domain.Entities.Catalog;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Caching.Distributed;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace StoreManager.Infrastructure.Repositories
{
    public class ExpenseCategoryRepository : IExpenseCategoryRepository
    {
        private readonly IRepositoryAsync<ExpenseCategory> _repository;
        private readonly IDistributedCache _distributedCache;

        public ExpenseCategoryRepository(IDistributedCache distributedCache, IRepositoryAsync<ExpenseCategory> repository)
        {
            _distributedCache = distributedCache;
            _repository = repository;
        }

        public IQueryable<ExpenseCategory> ExpenseCategories => _repository.Entities;

        public async Task DeleteAsync(ExpenseCategory expenseCategory)
        {
            await _repository.DeleteAsync(expenseCategory);
            await _distributedCache.RemoveAsync(CacheKeys.ExpenseCategoryCacheKeys.ListKey);
            await _distributedCache.RemoveAsync(CacheKeys.ExpenseCategoryCacheKeys.GetKey(expenseCategory.Id));
        }

        public async Task<ExpenseCategory> GetByIdAsync(int expenseCategoryId)
        {
            return await _repository.Entities.Where(p => p.Id == expenseCategoryId).FirstOrDefaultAsync();
        }

        public async Task<List<ExpenseCategory>> GetListAsync()
        {
            return await _repository.Entities.ToListAsync();
        }

        public async Task<int> InsertAsync(ExpenseCategory expenseCategory)
        {
            await _repository.AddAsync(expenseCategory);
            await _distributedCache.RemoveAsync(CacheKeys.ExpenseCategoryCacheKeys.ListKey);
            return expenseCategory.Id;
        }

        public async Task UpdateAsync(ExpenseCategory expenseCategory)
        {
            await _repository.UpdateAsync(expenseCategory);
            await _distributedCache.RemoveAsync(CacheKeys.ExpenseCategoryCacheKeys.ListKey);
            await _distributedCache.RemoveAsync(CacheKeys.ExpenseCategoryCacheKeys.GetKey(expenseCategory.Id));
        }

    }
}