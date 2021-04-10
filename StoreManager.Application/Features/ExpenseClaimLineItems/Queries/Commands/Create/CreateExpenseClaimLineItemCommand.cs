using StoreManager.Application.Interfaces.Repositories;
using StoreManager.Domain.Entities.Catalog;
using AspNetCoreHero.Results;
using AutoMapper;
using MediatR;
using System.Threading;
using System.Threading.Tasks;
using System;

namespace StoreManager.Application.Features.ExpenseClaimLineItems.Commands.Create
{
    public partial class CreateExpenseClaimLineItemCommand : IRequest<Result<int>>
    {
        public int ExpenseCategoryID { get; set; }
        public string Payee { get; set; }
        public DateTime Date { get; set; }
        public string Description { get; set; }
        public decimal Amount { get; set; }
        public string CurrencyCode { get; set; }

        public int CurrencyID { get; set; }
        public decimal UsdAmount { get; set; }
        public byte[] Receipt { get; set; }


        // Gcc properties


        public decimal Ab { get; set; }
        public decimal Net { get; set; }
        public decimal Gst { get; set; }
        public decimal Pst { get; set; }
    }

    public class CreateExpensClaimLineItemCommandHandler : IRequestHandler<CreateExpenseClaimLineItemCommand, Result<int>>
    {
        private readonly IExpenseClaimLineItemRepository _repository;
        private readonly IMapper _mapper;

        private IUnitOfWork _unitOfWork { get; set; }

        public CreateExpensClaimLineItemCommandHandler(IExpenseClaimLineItemRepository repository, IUnitOfWork unitOfWork, IMapper mapper)
        {
            _repository = repository;
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<Result<int>> Handle(CreateExpenseClaimLineItemCommand request, CancellationToken cancellationToken)
        {
            var expenseClaimLineItem = _mapper.Map<ExpenseClaimLineItem>(request);
            await _repository.InsertAsync(expenseClaimLineItem);
            await _unitOfWork.Commit(cancellationToken);
            return Result<int>.Success(expenseClaimLineItem.Id);
        }
    }
}