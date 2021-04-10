using StoreManager.Domain.Entities.Catalog;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace StoreManager.Application.Interfaces.Repositories
{
    public interface ICurrencyRepository
    {
        IQueryable<Currency> Currencies { get; }

        Task<List<Currency>> GetListAsync();

        Task<Currency> GetByIdAsync(int currencyId);

        Task<int> InsertAsync(Currency currency);

        Task UpdateAsync(Currency currency);

        Task DeleteAsync(Currency currency);
    }
}