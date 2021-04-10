using StoreManager.Application.Extensions;
using StoreManager.Application.Interfaces.Repositories;
using StoreManager.Domain.Entities.Catalog;
using AspNetCoreHero.Results;
using MediatR;
using System;
using System.Linq;
using System.Linq.Expressions;
using System.Threading;
using System.Threading.Tasks;

namespace StoreManager.Application.Features.ExpenseClaims.Queries.GetAllPaged
{
    public class GetAllExpensClaimsQuery : IRequest<PaginatedResult<GetAllExpenseClaimsResponse>>
    {
        public int PageNumber { get; set; }
        public int PageSize { get; set; }

        public string Status { get; set; }

        public GetAllExpensClaimsQuery(int pageNumber, int pageSize, string status)
        {
            PageNumber = pageNumber;
            PageSize = pageSize;
            Status = status;
        }
    }

    public class GGetAllExpenseClaimsQueryHandler : IRequestHandler<GetAllExpensClaimsQuery, PaginatedResult<GetAllExpenseClaimsResponse>>
    {
        private readonly IExpenseClaimRepository _repository;

        public GGetAllExpenseClaimsQueryHandler(IExpenseClaimRepository repository)
        {
            _repository = repository;
        }

        public async Task<PaginatedResult<GetAllExpenseClaimsResponse>> Handle(GetAllExpensClaimsQuery request, CancellationToken cancellationToken)
        {
            Expression<Func<ExpenseClaim, GetAllExpenseClaimsResponse>> expression = e => new GetAllExpenseClaimsResponse
            {
                Id = e.Id,
                Title = e.Title,
                RequesterName = e.RequesterName,
                RequesterID = e.RequesterID,
                ApproverName = e.ApproverName,
                ApproverID = e.ApproverID,
                SubmitDate = e.SubmitDate,
                ApprovalDate = e.ApprovalDate,
                ProcessedDate = e.ProcessedDate,
                TotalAmount = e.TotalAmount,
                Status = e.Status,
                RequesterComments = e.RequesterComments,
                ApproverComments = e.ApproverComments,
                FinanceComments = e.FinanceComments,
                ExpensClaimLineItems = e.ExpensClaimLineItems
            };
            if (request.Status == "submited")
            {
                var submitedList = await _repository.ExpenseClaims
                    .Where(e => e.Status == "submited")
                    .Select(expression)
                    .ToPaginatedListAsync(request.PageNumber, request.PageSize);
                return submitedList;
            } else if (request.Status == "quried")
            {
                var quriedList = await _repository.ExpenseClaims
                        .Where(e => e.Status == "quried")
                        .Select(expression)
                        .ToPaginatedListAsync(request.PageNumber, request.PageSize);
                return quriedList;
            } else if (request.Status == "approved")
            {
                var approvedList = await _repository.ExpenseClaims
                   .Where(e => e.Status == "approved")
                   .Select(expression)
                   .ToPaginatedListAsync(request.PageNumber, request.PageSize);
                return approvedList;

            } else {
                var paginatedList = await _repository.ExpenseClaims
                    .Select(expression)
                    .ToPaginatedListAsync(request.PageNumber, request.PageSize);
                return paginatedList;
            }

        }
    }
}