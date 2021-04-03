using StoreManager.Application.Interfaces.CacheRepositories;
using AspNetCoreHero.Results;
using AutoMapper;
using MediatR;
using System.Threading;
using System.Threading.Tasks;
using StoreManager.Application.Interfaces.Repositories;

namespace StoreManager.Application.Features.ExpenseCategories.Queries.GetById
{
    public class GetExpenseCategoryByIdQuery : IRequest<Result<GetExpenseCategoryByIdResponse>>
    {
        public int Id { get; set; }

        public class GetExpenseCategoryByIdQueryHandler : IRequestHandler<GetExpenseCategoryByIdQuery, Result<GetExpenseCategoryByIdResponse>>
        {
            private readonly IExpenseCategoryRepository _repository;
            private readonly IMapper _mapper;

            public GetExpenseCategoryByIdQueryHandler(IExpenseCategoryRepository repository, IMapper mapper)
            {
                _repository = repository;
                _mapper = mapper;
            }

            public async Task<Result<GetExpenseCategoryByIdResponse>> Handle(GetExpenseCategoryByIdQuery query, CancellationToken cancellationToken)
            {
                var expenseCategory = await _repository.GetByIdAsync(query.Id);
                var mappedExpenseCategory = _mapper.Map<GetExpenseCategoryByIdResponse>(expenseCategory);
                return Result<GetExpenseCategoryByIdResponse>.Success(mappedExpenseCategory);
            }
        }
    }
}