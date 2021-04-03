using StoreManager.Application.Interfaces.Repositories;
using AspNetCoreHero.Results;
using MediatR;
using System.Threading;
using System.Threading.Tasks;

namespace StoreManager.Application.Features.ExpenseClaimLineItems.Commands.Delete
{
    public class DeleteExpenseClaimLineItemCommand : IRequest<Result<int>>
    {
        public int Id { get; set; }

        public class DeleteExpenseClaimLineItemCommandHandler : IRequestHandler<DeleteExpenseClaimLineItemCommand, Result<int>>
        {
            private readonly IExpenseClaimLineItemRepository _repository;
            private readonly IUnitOfWork _unitOfWork;

            public DeleteExpenseClaimLineItemCommandHandler(IExpenseClaimLineItemRepository repository, IUnitOfWork unitOfWork)
            {
                _repository = repository;
                _unitOfWork = unitOfWork;
            }

            public async Task<Result<int>> Handle(DeleteExpenseClaimLineItemCommand command, CancellationToken cancellationToken)
            {
                var expenseClaimLineItem = await _repository.GetByIdAsync(command.Id);
                await _repository.DeleteAsync(expenseClaimLineItem);
                await _unitOfWork.Commit(cancellationToken);
                return Result<int>.Success(expenseClaimLineItem.Id);
            }
        }
    }
}