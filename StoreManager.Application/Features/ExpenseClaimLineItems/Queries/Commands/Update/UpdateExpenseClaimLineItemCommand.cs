using StoreManager.Application.Interfaces.Repositories;
using AspNetCoreHero.Results;
using MediatR;
using System.Threading;
using System.Threading.Tasks;
using System;

namespace StoreManager.Application.Features.ExpenseClaimLineItems.Commands.Update
{
    public class UpdateExpenseClaimLineItemCommand : IRequest<Result<int>>
    {
        public int Id { get; set; }
        public int ExpenseClaimID { get; set; }
        public int ExpenseCategoryID { get; set; }
        public string Payee { get; set; }
        public DateTime Date { get; set; }
        public string Description { get; set; }
        public decimal Amount { get; set; }
        public string CurrencyCode { get; set; }

        public int CurrencyID { get; set; }
        public decimal UsdAmount { get; set; }
        public byte[] Receipt { get; set; }

        public class UpdateExpenseClaimLineItemCommandHandler : IRequestHandler<UpdateExpenseClaimLineItemCommand, Result<int>>
        {
            private readonly IUnitOfWork _unitOfWork;
            private readonly IExpenseClaimLineItemRepository _repository;

            public UpdateExpenseClaimLineItemCommandHandler(IExpenseClaimLineItemRepository repository, IUnitOfWork unitOfWork)
            {
                _repository = repository;
                _unitOfWork = unitOfWork;
            }

            public async Task<Result<int>> Handle(UpdateExpenseClaimLineItemCommand command, CancellationToken cancellationToken)
            {
                var expenseClaimLineItem = await _repository.GetByIdAsync(command.Id);

                if (expenseClaimLineItem == null)
                {
                    return Result<int>.Fail($"Product Not Found.");
                }
                else
                {
                    expenseClaimLineItem.ExpenseCategoryID = (command.ExpenseCategoryID == 0) ? expenseClaimLineItem.ExpenseCategoryID : command.ExpenseCategoryID;
                    expenseClaimLineItem.Payee = command.Payee ?? expenseClaimLineItem.Payee;
                    expenseClaimLineItem.Date = (command.Date == DateTime.MinValue) ? expenseClaimLineItem.Date : command.Date;
                    expenseClaimLineItem.Description = command.Description ?? expenseClaimLineItem.Description;
                    expenseClaimLineItem.Amount = (command.Amount == 0) ? expenseClaimLineItem.Amount : command.Amount;
                    expenseClaimLineItem.CurrencyCode = command.CurrencyCode ?? expenseClaimLineItem.CurrencyCode;
                    expenseClaimLineItem.CurrencyID = (command.CurrencyID == 0) ? expenseClaimLineItem.CurrencyID : command.CurrencyID;
                    expenseClaimLineItem.UsdAmount = (command.UsdAmount == 0) ? expenseClaimLineItem.UsdAmount : command.UsdAmount;
                    expenseClaimLineItem.Receipt = command.Receipt ?? expenseClaimLineItem.Receipt;

                    await _repository.UpdateAsync(expenseClaimLineItem);
                    await _unitOfWork.Commit(cancellationToken);
                    return Result<int>.Success(expenseClaimLineItem.Id);
                }
            }
        }
    }
}