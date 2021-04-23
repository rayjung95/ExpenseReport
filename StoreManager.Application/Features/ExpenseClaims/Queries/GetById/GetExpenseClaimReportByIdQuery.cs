using StoreManager.Application.Interfaces.CacheRepositories;
using AspNetCoreHero.Results;
using AutoMapper;
using MediatR;
using System.Threading;
using System.Threading.Tasks;
using StoreManager.Application.Interfaces.Repositories;
using StoreManager.Domain.Entities.Catalog;

namespace StoreManager.Application.Features.ExpenseClaims.Queries.GetById
{
    public class GetExpenseClaimReportByIdQuery : IRequest<Result<ExpenseClaim>>
    {
        public int Id { get; set; }

        public class GetExpenseClaimReportByIdQueryHandler : IRequestHandler<GetExpenseClaimReportByIdQuery, Result<ExpenseClaim>>
        {
            private readonly IExpenseClaimRepository _repository;
            private readonly IMapper _mapper;

            public GetExpenseClaimReportByIdQueryHandler(IExpenseClaimRepository repository, IMapper mapper)
            {
                _repository = repository;
                _mapper = mapper;
            }

            public async Task<Result<ExpenseClaim>> Handle(GetExpenseClaimReportByIdQuery query, CancellationToken cancellationToken)
            {
                var expenseClaim = await _repository.GetReportByIdAsync(query.Id);
                return Result<ExpenseClaim>.Success(expenseClaim);
            }
        }
    }
}