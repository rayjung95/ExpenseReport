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

        public GetAllExpensClaimsQuery(int pageNumber, int pageSize)
        {
            PageNumber = pageNumber;
            PageSize = pageSize;
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
                //ApproverName = e.ApproverName,
                //RequesterName = e.RequesterName,
                //TotalAmount = e.TotalAmount
            };
            var paginatedList = await _repository.ExpenseClaims
                .Select(expression)
                .ToPaginatedListAsync(request.PageNumber, request.PageSize);
            return paginatedList;
        }
    }
}