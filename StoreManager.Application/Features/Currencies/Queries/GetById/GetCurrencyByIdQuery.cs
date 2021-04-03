using StoreManager.Application.Interfaces.CacheRepositories;
using AspNetCoreHero.Results;
using AutoMapper;
using MediatR;
using System.Threading;
using System.Threading.Tasks;
using StoreManager.Application.Interfaces.Repositories;

namespace StoreManager.Application.Features.Currencies.Queries.GetById
{
    public class GetCurrencyByIdQuery : IRequest<Result<GetCurrencyByIdResponse>>
    {
        public int Id { get; set; }

        public class GetExpenseClaimByIdQueryHandler : IRequestHandler<GetCurrencyByIdQuery, Result<GetCurrencyByIdResponse>>
        {
            private readonly ICurrencyRepository _repository;
            private readonly IMapper _mapper;

            public GetExpenseClaimByIdQueryHandler(ICurrencyRepository repository, IMapper mapper)
            {
                _repository = repository;
                _mapper = mapper;
            }

            public async Task<Result<GetCurrencyByIdResponse>> Handle(GetCurrencyByIdQuery query, CancellationToken cancellationToken)
            {
                var currency = await _repository.GetByIdAsync(query.Id);
                var mappedCurrency = _mapper.Map<GetCurrencyByIdResponse>(currency);
                return Result<GetCurrencyByIdResponse>.Success(mappedCurrency);
            }
        }
    }
}