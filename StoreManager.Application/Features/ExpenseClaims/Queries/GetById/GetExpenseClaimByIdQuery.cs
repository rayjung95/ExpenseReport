using StoreManager.Application.Interfaces.CacheRepositories;
using AspNetCoreHero.Results;
using AutoMapper;
using MediatR;
using System.Threading;
using System.Threading.Tasks;
using StoreManager.Application.Interfaces.Repositories;

namespace StoreManager.Application.Features.ExpenseClaims.Queries.GetById
{
    public class GetExpenseClaimByIdQuery : IRequest<Result<GetExpenseClaimByIdResponse>>
    {
        public int Id { get; set; }

        public class GetExpenseClaimByIdQueryHandler : IRequestHandler<GetExpenseClaimByIdQuery, Result<GetExpenseClaimByIdResponse>>
        {
            private readonly IExpenseClaimRepository _repository;
            private readonly IMapper _mapper;

            public GetExpenseClaimByIdQueryHandler(IExpenseClaimRepository repository, IMapper mapper)
            {
                _repository = repository;
                _mapper = mapper;
            }

            public async Task<Result<GetExpenseClaimByIdResponse>> Handle(GetExpenseClaimByIdQuery query, CancellationToken cancellationToken)
            {
                var expenseClaim = await _repository.GetByIdAsync(query.Id);
                var mappedExpenseClaim = _mapper.Map<GetExpenseClaimByIdResponse>(expenseClaim);
                return Result<GetExpenseClaimByIdResponse>.Success(mappedExpenseClaim);
            }
        }
    }
}