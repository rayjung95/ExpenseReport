using StoreManager.Application.Interfaces.Repositories;
using AspNetCoreHero.Results;
using MediatR;
using System.Threading;
using System.Threading.Tasks;

namespace StoreManager.Application.Features.Currencies.Commands.Delete
{
    public class DeleteCurrencyCommand : IRequest<Result<int>>
    {
        public int Id { get; set; }

        public class DeleteCurrencyCommandHandler : IRequestHandler<DeleteCurrencyCommand, Result<int>>
        {
            private readonly ICurrencyRepository _repository;
            private readonly IUnitOfWork _unitOfWork;

            public DeleteCurrencyCommandHandler(ICurrencyRepository repository, IUnitOfWork unitOfWork)
            {
                _repository = repository;
                _unitOfWork = unitOfWork;
            }

            public async Task<Result<int>> Handle(DeleteCurrencyCommand command, CancellationToken cancellationToken)
            {
                var currency = await _repository.GetByIdAsync(command.Id);
                await _repository.DeleteAsync(currency);
                await _unitOfWork.Commit(cancellationToken);
                return Result<int>.Success(currency.Id);
            }
        }
    }
}