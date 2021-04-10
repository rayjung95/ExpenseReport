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

namespace StoreManager.Application.Features.Currencies.Queries.GetAllPaged
{
    public class GetAllCurrenciesQuery : IRequest<PaginatedResult<GetAllCurrenciesResponse>>
    {
        public int PageNumber { get; set; }
        public int PageSize { get; set; }

        public GetAllCurrenciesQuery(int pageNumber, int pageSize)
        {
            PageNumber = pageNumber;
            PageSize = pageSize;
        }
    }

    public class GGetAllCurrenciesQueryHandler : IRequestHandler<GetAllCurrenciesQuery, PaginatedResult<GetAllCurrenciesResponse>>
    {
        private readonly ICurrencyRepository _repository;

        public GGetAllCurrenciesQueryHandler(ICurrencyRepository repository)
        {
            _repository = repository;
        }

        public async Task<PaginatedResult<GetAllCurrenciesResponse>> Handle(GetAllCurrenciesQuery request, CancellationToken cancellationToken)
        {
            Expression<Func<Currency, GetAllCurrenciesResponse>> expression = e => new GetAllCurrenciesResponse
            {
                Id = e.Id,
                Code = e.Code,
                Name = e.Name,
                Symbol = e.Symbol,
            };
            var paginatedList = await _repository.Currencies
                .Select(expression)
                .ToPaginatedListAsync(request.PageNumber, request.PageSize);
            return paginatedList;
        }
    }
}