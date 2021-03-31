﻿using StoreManager.Domain.Entities.Catalog;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using System.Data;
using System.Threading;
using System.Threading.Tasks;

namespace StoreManager.Application.Interfaces.Contexts
{
    public interface IApplicationDbContext
    {
        IDbConnection Connection { get; }
        bool HasChanges { get; }

        EntityEntry Entry(object entity);

        Task<int> SaveChangesAsync(CancellationToken cancellationToken);

        DbSet<Product> Products { get; set; }

        DbSet<Currency> Currencies { get; set; }

        DbSet<ExpenseCategory> ExpenseCategories { get; set; }

        DbSet<ExpenseClaim> ExpenseClaims { get; set; }

    }
}