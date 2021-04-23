using StoreManager.Application.Interfaces.Repositories;
using StoreManager.Domain.Entities.Catalog;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Caching.Distributed;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using StoreManager.Application.Features.ExpenseClaims.Commands.Create;
using StoreManager.Infrastructure.DbContexts;
using StoreManager.Application.Interfaces.Shared;
using StoreManager.Application.DTOs.Mail;

namespace StoreManager.Infrastructure.Repositories
{
    public class ExpenseClaimRepository : IExpenseClaimRepository
    {
        private readonly IRepositoryAsync<ExpenseClaim> _repository;
        private readonly IDistributedCache _distributedCache;
        private readonly ApplicationDbContext _dbContext;
        private readonly IMailService _mailService;
        public ExpenseClaimRepository(IDistributedCache distributedCache, IRepositoryAsync<ExpenseClaim> repository, ApplicationDbContext dbContext, IMailService mailService)
        {
            _distributedCache = distributedCache;
            _repository = repository;
            _dbContext = dbContext;
            _mailService = mailService;
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
        public async Task<ExpenseClaim> GetReportByIdAsync(int expenseClaimId)
        {
            return await _repository.Entities.Include(e => e.ExpensClaimLineItems).Where(p => p.Id == expenseClaimId).FirstOrDefaultAsync();
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

        public async Task<int> CreateExpenseReport(CreateExpenseClaimReportCommand request)
        {
            ExpenseClaim expenseClaim = new ExpenseClaim
            {
                Title = request.Title,
                RequesterName = request.RequesterName,
                RequesterID = request.RequesterID,
                ApproverName = request.ApproverName,
                ApproverID = request.ApproverID,
                SubmitDate = request.SubmitDate,
                ApprovalDate = request.ApprovalDate,
                ProcessedDate = request.ProcessedDate,
                TotalAmount = request.TotalAmount,
                Status = "submited",
                RequesterComments = request.RequesterComments,
                ApproverComments = request.ApproverComments,
                FinanceComments = request.FinanceComments
            };

            var result = await _repository.AddAsync(expenseClaim);

            await _dbContext.SaveChangesAsync();

            foreach (var item in request.ExpensClaimLineItems)
            {
                result.ExpensClaimLineItems.Add(new ExpenseClaimLineItem
                {
                    ExpenseClaimID = result.Id,
                    ExpenseCategoryID = item.ExpenseCategoryID,
                    Payee = item.Payee,
                    Date = item.Date,
                    Description = item.Description,
                    Amount = item.Amount,
                    CurrencyCode = item.CurrencyCode,
                    CurrencyID = item.CurrencyID,
                    UsdAmount = item.UsdAmount,
                    Receipt = item.Receipt,
                    Ab = item.Ab,
                    Net = item.Net,
                    Gst = item.Gst,
                    Pst = item.Pst,
                });
            }

            await _dbContext.SaveChangesAsync();
            var route = $"reports/process/{result.Id}";
            await _mailService.SendAsync(new MailRequest() { From = "vivien30@ethereal.email", To = "rayjung95@gmail.com", Body = $"Report is submitted. <a href=https://localhost:44380/{route}> click here to see the report</a>", Subject = "Report is submitted" });
            return result.Id;
        }

    }
}