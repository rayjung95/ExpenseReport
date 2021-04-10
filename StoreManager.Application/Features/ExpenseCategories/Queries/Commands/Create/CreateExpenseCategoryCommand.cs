using StoreManager.Application.Interfaces.Repositories;
using StoreManager.Domain.Entities.Catalog;
using AspNetCoreHero.Results;
using AutoMapper;
using MediatR;
using System.Threading;
using System.Threading.Tasks;
using System;

namespace StoreManager.Application.Features.ExpenseCategories.Commands.Create
{
    public partial class CreateExpenseCategoryCommand : IRequest<Result<int>>
    {
        public string Name { get; set; }
        public string Code { get; set; }
    }

    public class CreateExpensCategoryCommandHandler : IRequestHandler<CreateExpenseCategoryCommand, Result<int>>
    {
        private readonly IExpenseCategoryRepository _repository;
        private readonly IMapper _mapper;

        private IUnitOfWork _unitOfWork { get; set; }

        public CreateExpensCategoryCommandHandler(IExpenseCategoryRepository repository, IUnitOfWork unitOfWork, IMapper mapper)
        {
            _repository = repository;
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<Result<int>> Handle(CreateExpenseCategoryCommand request, CancellationToken cancellationToken)
        {
            var expenseCategory = _mapper.Map<ExpenseCategory>(request);
            await _repository.InsertAsync(expenseCategory);
            await _unitOfWork.Commit(cancellationToken);
            return Result<int>.Success(expenseCategory.Id);
        }
    }
}