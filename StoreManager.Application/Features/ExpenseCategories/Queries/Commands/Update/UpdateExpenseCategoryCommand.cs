using StoreManager.Application.Interfaces.Repositories;
using AspNetCoreHero.Results;
using MediatR;
using System.Threading;
using System.Threading.Tasks;
using System;

namespace StoreManager.Application.Features.ExpenseCategories.Commands.Update
{
    public class UpdateExpenseCategoryCommand : IRequest<Result<int>>
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Code { get; set; }

        public class UpdateExpenseCategoryCommandHandler : IRequestHandler<UpdateExpenseCategoryCommand, Result<int>>
        {
            private readonly IUnitOfWork _unitOfWork;
            private readonly IExpenseCategoryRepository _repository;

            public UpdateExpenseCategoryCommandHandler(IExpenseCategoryRepository repository, IUnitOfWork unitOfWork)
            {
                _repository = repository;
                _unitOfWork = unitOfWork;
            }

            public async Task<Result<int>> Handle(UpdateExpenseCategoryCommand command, CancellationToken cancellationToken)
            {
                var expenseCategory = await _repository.GetByIdAsync(command.Id);

                if (expenseCategory == null)
                {
                    return Result<int>.Fail($"Product Not Found.");
                }
                else
                {
                    expenseCategory.Name = command.Name ?? expenseCategory.Name;
                    expenseCategory.Code = command.Code ?? expenseCategory.Code;

                    await _repository.UpdateAsync(expenseCategory);
                    await _unitOfWork.Commit(cancellationToken);
                    return Result<int>.Success(expenseCategory.Id);
                }
            }
        }
    }
}