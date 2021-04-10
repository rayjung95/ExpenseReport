using StoreManager.Application.Interfaces.CacheRepositories;
using AspNetCoreHero.Results;
using AutoMapper;
using MediatR;
using System.Threading;
using System.Threading.Tasks;
using StoreManager.Application.Interfaces.Repositories;

namespace StoreManager.Application.Features.ExpenseClaimLineItems.Queries.GetById
{
    public class GetExpenseClaimLineItemByIdQuery : IRequest<Result<GetExpenseClaimLineItemByIdResponse>>
    {
        public int Id { get; set; }

        public class GetExpenseClaimByIdQueryHandler : IRequestHandler<GetExpenseClaimLineItemByIdQuery, Result<GetExpenseClaimLineItemByIdResponse>>
        {
            private readonly IExpenseClaimLineItemRepository _repository;
            private readonly IMapper _mapper;

            public GetExpenseClaimByIdQueryHandler(IExpenseClaimLineItemRepository repository, IMapper mapper)
            {
                _repository = repository;
                _mapper = mapper;
            }

            public async Task<Result<GetExpenseClaimLineItemByIdResponse>> Handle(GetExpenseClaimLineItemByIdQuery query, CancellationToken cancellationToken)
            {
                var expenseClaimLineItem = await _repository.GetByIdAsync(query.Id);
                var mappedExpenseClaimLineItem = _mapper.Map<GetExpenseClaimLineItemByIdResponse>(expenseClaimLineItem);
                return Result<GetExpenseClaimLineItemByIdResponse>.Success(mappedExpenseClaimLineItem);
            }
        }
    }
}