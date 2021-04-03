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

namespace StoreManager.Application.Features.ExpenseCategories.Queries.GetAllPaged
{
    public class GetAllExpenseCategoriesQuery : IRequest<PaginatedResult<GetAllExpenseCategoriesResponse>>
    {
        public int PageNumber { get; set; }
        public int PageSize { get; set; }

        public GetAllExpenseCategoriesQuery(int pageNumber, int pageSize)
        {
            PageNumber = pageNumber;
            PageSize = pageSize;
        }
    }

    public class GGetAllExpenseClaimsQueryHandler : IRequestHandler<GetAllExpenseCategoriesQuery, PaginatedResult<GetAllExpenseCategoriesResponse>>
    {
        private readonly IExpenseCategoryRepository _repository;

        public GGetAllExpenseClaimsQueryHandler(IExpenseCategoryRepository repository)
        {
            _repository = repository;
        }

        public async Task<PaginatedResult<GetAllExpenseCategoriesResponse>> Handle(GetAllExpenseCategoriesQuery request, CancellationToken cancellationToken)
        {
            Expression<Func<ExpenseCategory, GetAllExpenseCategoriesResponse>> expression = e => new GetAllExpenseCategoriesResponse
            {
                Id = e.Id,
                Name = e.Name,
                Code = e.Code,
            };
            var paginatedList = await _repository.ExpenseCategories
                .Select(expression)
                .ToPaginatedListAsync(request.PageNumber, request.PageSize);
            return paginatedList;
        }
    }
}