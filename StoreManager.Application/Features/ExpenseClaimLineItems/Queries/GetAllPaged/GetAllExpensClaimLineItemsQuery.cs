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
using StoreManager.Application.Features.ExpenseClaimLineItems.Queries.GetAllPaged;

namespace StoreManager.Application.Features.EpenseClaimLineItems.Queries.GetAllPaged
{
    public class GetAllExpensClaimLineItemsQuery : IRequest<PaginatedResult<GetAllExpenseClaimLineItemsResponse>>
    {
        public int PageNumber { get; set; }
        public int PageSize { get; set; }

        public GetAllExpensClaimLineItemsQuery(int pageNumber, int pageSize)
        {
            PageNumber = pageNumber;
            PageSize = pageSize;
        }
    }

    public class GGetAllExpenseClaimLineItemsQueryHandler : IRequestHandler<GetAllExpensClaimLineItemsQuery, PaginatedResult<GetAllExpenseClaimLineItemsResponse>>
    {
        private readonly IExpenseClaimLineItemRepository _repository;

        public GGetAllExpenseClaimLineItemsQueryHandler(IExpenseClaimLineItemRepository repository)
        {
            _repository = repository;
        }

        public async Task<PaginatedResult<GetAllExpenseClaimLineItemsResponse>> Handle(GetAllExpensClaimLineItemsQuery request, CancellationToken cancellationToken)
        {
            Expression<Func<ExpenseClaimLineItem, GetAllExpenseClaimLineItemsResponse>> expression = e => new GetAllExpenseClaimLineItemsResponse
            {
                Id = e.Id,
                ExpenseClaimID = e.ExpenseClaimID,
                ExpenseCategoryID = e.ExpenseCategoryID,
                Payee = e.Payee,
                Date = e.Date,
                Description = e.Description,
                Amount = e.Amount,
                CurrencyCode = e.CurrencyCode,
                CurrencyID = e.CurrencyID,
                UsdAmount = e.UsdAmount,
                Receipt = e.Receipt
            };
            var paginatedList = await _repository.ExpenseClaimLineItems
                .Select(expression)
                .ToPaginatedListAsync(request.PageNumber, request.PageSize);
            return paginatedList;
        }
    }
}