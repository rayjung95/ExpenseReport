using StoreManager.Application.Interfaces.Repositories;
using AspNetCoreHero.Results;
using MediatR;
using System.Threading;
using System.Threading.Tasks;

namespace StoreManager.Application.Features.ExpenseCategories.Commands.Delete
{
    public class DeleteExpenseCategoryCommand : IRequest<Result<int>>
    {
        public int Id { get; set; }

        public class DeleteExpenseCategoryCommandHandler : IRequestHandler<DeleteExpenseCategoryCommand, Result<int>>
        {
            private readonly IExpenseCategoryRepository _repository;
            private readonly IUnitOfWork _unitOfWork;

            public DeleteExpenseCategoryCommandHandler(IExpenseCategoryRepository repository, IUnitOfWork unitOfWork)
            {
                _repository = repository;
                _unitOfWork = unitOfWork;
            }

            public async Task<Result<int>> Handle(DeleteExpenseCategoryCommand command, CancellationToken cancellationToken)
            {
                var expenseCategory = await _repository.GetByIdAsync(command.Id);
                await _repository.DeleteAsync(expenseCategory);
                await _unitOfWork.Commit(cancellationToken);
                return Result<int>.Success(expenseCategory.Id);
            }
        }
    }
}