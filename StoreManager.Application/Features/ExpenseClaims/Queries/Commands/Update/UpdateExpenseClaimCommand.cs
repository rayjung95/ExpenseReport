using StoreManager.Application.Interfaces.Repositories;
using AspNetCoreHero.Results;
using MediatR;
using System.Threading;
using System.Threading.Tasks;
using System;
using System.Collections.Generic;
using StoreManager.Domain.Entities.Catalog;
using StoreManager.Application.Interfaces.Shared;
using StoreManager.Application.DTOs.Mail;

namespace StoreManager.Application.Features.ExpenseClaims.Commands.Update
{
    public class UpdateExpenseClaimCommand : IRequest<Result<int>>
    {
        public int Id { get; set; }
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
        public List<ExpenseClaimLineItem> ExpensClaimLineItems { get; set; } = new List<ExpenseClaimLineItem>();


        public class UpdateExpenseClaimCommandHandler : IRequestHandler<UpdateExpenseClaimCommand, Result<int>>
        {
            private readonly IUnitOfWork _unitOfWork;
            private readonly IExpenseClaimRepository _repository;
            private readonly IMailService _mailService;

            public UpdateExpenseClaimCommandHandler(IExpenseClaimRepository repository, IUnitOfWork unitOfWork, IMailService mailService)
            {
                _repository = repository;
                _unitOfWork = unitOfWork;
                _mailService = mailService;
            }

            public async Task<Result<int>> Handle(UpdateExpenseClaimCommand command, CancellationToken cancellationToken)
            {
                var expenseClaim = await _repository.GetByIdAsync(command.Id);

                if (expenseClaim == null)
                {
                    return Result<int>.Fail($"Product Not Found.");
                }
                else
                {
                    expenseClaim.Title = command.Title ?? expenseClaim.Title;
                    expenseClaim.RequesterName = command.RequesterName ?? expenseClaim.RequesterName;
                    expenseClaim.RequesterID = (command.RequesterID == 0) ? expenseClaim.RequesterID : command.RequesterID;
                    expenseClaim.ApproverName = command.ApproverName ?? expenseClaim.ApproverName;
                    expenseClaim.ApproverID = (command.ApproverID == 0) ? expenseClaim.ApproverID : command.ApproverID;
                    expenseClaim.SubmitDate = (command.SubmitDate == DateTime.MinValue) ? expenseClaim.SubmitDate : command.SubmitDate;
                    expenseClaim.ApprovalDate = (command.ApprovalDate == DateTime.MinValue) ? expenseClaim.ApprovalDate : command.ApprovalDate;
                    expenseClaim.ProcessedDate = (command.ProcessedDate == DateTime.MinValue) ? expenseClaim.ProcessedDate : command.ProcessedDate;
                    expenseClaim.TotalAmount = (command.TotalAmount == 0) ? expenseClaim.TotalAmount : command.TotalAmount;
                    expenseClaim.Status = command.Status ?? expenseClaim.Status;
                    expenseClaim.RequesterComments = command.RequesterComments ?? expenseClaim.RequesterComments;
                    expenseClaim.ApproverComments = command.ApproverComments ?? expenseClaim.ApproverComments;
                    expenseClaim.FinanceComments = command.FinanceComments ?? expenseClaim.FinanceComments;
                    expenseClaim.ExpensClaimLineItems = command.ExpensClaimLineItems ?? expenseClaim.ExpensClaimLineItems;

                    
                    await _repository.UpdateAsync(expenseClaim);
                    if (command.Status == "submitted")
                    {
                        var route = $"reports/process/{expenseClaim.Id}";
                        await _mailService.SendAsync(new MailRequest() { From = "vivien30@ethereal.email", To = "rayjung95@gmail.com", Body = $"Report is resubmitted. <a href=https://localhost:44380/{route}> click here to see the report</a>", Subject = "Report is resubmitted" });
                    }
                    await _unitOfWork.Commit(cancellationToken);
                    return Result<int>.Success(expenseClaim.Id);
                }
            }
        }
    }
}