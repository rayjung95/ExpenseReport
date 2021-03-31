using StoreManager.Application.Interfaces.Repositories;
using StoreManager.Domain.Entities.Catalog;
using AspNetCoreHero.Results;
using AutoMapper;
using MediatR;
using System.Threading;
using System.Threading.Tasks;
using System;

namespace StoreManager.Application.Features.ExpenseClaims.Commands.Create
{
    public partial class CreateExpenseClaimCommand : IRequest<Result<int>>
    {
        public string Title { get; set; }
        public string RequesterName { get; set; }
        public int RequesterID { get; set; }
        public string ApproverName { get; set; }
        public int ApproverID { get; set; }
        public DateTime SubmitDate { get; set; }
        public DateTime ApprovalDate { get; set; }
        public DateTime ProcessedDate { get; set; }
        public decimal TotalAmount { get; set; }
        public string Status { get; set; }
        public string RequesterComments { get; set; }
        public string ApproverComments { get; set; }
        public string FinanceComments { get; set; }
    }

    public class CreateExpensClaimCommandHandler : IRequestHandler<CreateExpenseClaimCommand, Result<int>>
    {
        private readonly IExpenseClaimRepository _repository;
        private readonly IMapper _mapper;

        private IUnitOfWork _unitOfWork { get; set; }

        public CreateExpensClaimCommandHandler(IExpenseClaimRepository repository, IUnitOfWork unitOfWork, IMapper mapper)
        {
            _repository = repository;
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<Result<int>> Handle(CreateExpenseClaimCommand request, CancellationToken cancellationToken)
        {
            var expenseClaim = _mapper.Map<ExpenseClaim>(request);
            await _repository.InsertAsync(expenseClaim);
            await _unitOfWork.Commit(cancellationToken);
            return Result<int>.Success(expenseClaim.Id);
        }
    }
}