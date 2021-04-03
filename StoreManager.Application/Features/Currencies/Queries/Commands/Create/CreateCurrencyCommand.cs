using StoreManager.Application.Interfaces.Repositories;
using StoreManager.Domain.Entities.Catalog;
using AspNetCoreHero.Results;
using AutoMapper;
using MediatR;
using System.Threading;
using System.Threading.Tasks;
using System;

namespace StoreManager.Application.Features.Currencies.Commands.Create
{
    public partial class CreateCurrencyCommand : IRequest<Result<int>>
    {
        public string Code { get; set; }
        public string Name { get; set; }
        public string Symbol { get; set; }
    }

    public class CreateCurrencyCommandHandler : IRequestHandler<CreateCurrencyCommand, Result<int>>
    {
        private readonly ICurrencyRepository _repository;
        private readonly IMapper _mapper;

        private IUnitOfWork _unitOfWork { get; set; }

        public CreateCurrencyCommandHandler(ICurrencyRepository repository, IUnitOfWork unitOfWork, IMapper mapper)
        {
            _repository = repository;
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<Result<int>> Handle(CreateCurrencyCommand request, CancellationToken cancellationToken)
        {
            var currency = _mapper.Map<Currency>(request);
            await _repository.InsertAsync(currency);
            await _unitOfWork.Commit(cancellationToken);
            return Result<int>.Success(currency.Id);
        }
    }
}