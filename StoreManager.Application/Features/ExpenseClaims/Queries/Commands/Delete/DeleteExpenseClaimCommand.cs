using StoreManager.Application.Interfaces.Repositories;
using AspNetCoreHero.Results;
using MediatR;
using System.Threading;
using System.Threading.Tasks;

namespace StoreManager.Application.Features.ExpenseClaims.Commands.Delete
{
    public class DeleteExpenseClaimCommand : IRequest<Result<int>>
    {
        public int Id { get; set; }

        public class DeleteExpenseClaimCommandHandler : IRequestHandler<DeleteExpenseClaimCommand, Result<int>>
        {
            private readonly IExpenseClaimRepository _repository;
            private readonly IUnitOfWork _unitOfWork;

            public DeleteExpenseClaimCommandHandler(IExpenseClaimRepository repository, IUnitOfWork unitOfWork)
            {
                _repository = repository;
                _unitOfWork = unitOfWork;
            }

            public async Task<Result<int>> Handle(DeleteExpenseClaimCommand command, CancellationToken cancellationToken)
            {
                var expenseClaim = await _repository.GetByIdAsync(command.Id);
                await _repository.DeleteAsync(expenseClaim);
                await _unitOfWork.Commit(cancellationToken);
                return Result<int>.Success(expenseClaim.Id);
            }
        }
    }
}