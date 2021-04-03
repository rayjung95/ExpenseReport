using StoreManager.Application.Interfaces.Repositories;
using AspNetCoreHero.Results;
using MediatR;
using System.Threading;
using System.Threading.Tasks;
using System;

namespace StoreManager.Application.Features.Currencies.Commands.Update
{
    public class UpdateCurrencyCommand : IRequest<Result<int>>
    {
        public int Id { get; set; }
        public string Code { get; set; }
        public string Name { get; set; }
        public string Symbol { get; set; }

        public class UpdateCurrencyCommandHandler : IRequestHandler<UpdateCurrencyCommand, Result<int>>
        {
            private readonly IUnitOfWork _unitOfWork;
            private readonly ICurrencyRepository _repository;

            public UpdateCurrencyCommandHandler(ICurrencyRepository repository, IUnitOfWork unitOfWork)
            {
                _repository = repository;
                _unitOfWork = unitOfWork;
            }

            public async Task<Result<int>> Handle(UpdateCurrencyCommand command, CancellationToken cancellationToken)
            {
                var currency = await _repository.GetByIdAsync(command.Id);

                if (currency == null)
                {
                    return Result<int>.Fail($"Product Not Found.");
                }
                else
                {
                    currency.Code = command.Code ?? currency.Code;
                    currency.Name = command.Name ?? currency.Name;
                    currency.Symbol = command.Symbol ?? currency.Symbol;

                    await _repository.UpdateAsync(currency);
                    await _unitOfWork.Commit(cancellationToken);
                    return Result<int>.Success(currency.Id);
                }
            }
        }
    }
}