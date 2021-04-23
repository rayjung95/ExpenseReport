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
    public class ChangeStatusCommand : IRequest<Result<int>>
    {
        public int Id { get; set; }
        public string Status { get; set; }
        public string ApproverName { get; set; }
        public int ApproverID { get; set; }
        public DateTime ApprovalDate { get; set; }
        public DateTime ProcessedDate { get; set; }
        public string RequesterComments { get; set; }
        public string ApproverComments { get; set; }
        public string FinanceComments { get; set; }



        public class ChangeStatusCommandHandler : IRequestHandler<ChangeStatusCommand, Result<int>>
        {
            private readonly IUnitOfWork _unitOfWork;
            private readonly IExpenseClaimRepository _repository;
            private readonly IMailService _mailService;

            public ChangeStatusCommandHandler(IExpenseClaimRepository repository, IUnitOfWork unitOfWork, IMailService mailService)
            {
                _repository = repository;
                _unitOfWork = unitOfWork;
                _mailService = mailService;
            }

            public async Task<Result<int>> Handle(ChangeStatusCommand command, CancellationToken cancellationToken)
            {
                var expenseClaim = await _repository.GetByIdAsync(command.Id);

                if (expenseClaim == null)
                {
                    return Result<int>.Fail($"Product Not Found.");
                }
                else
                {
                    expenseClaim.Status = command.Status ?? expenseClaim.Status;
                    expenseClaim.ApproverName = command.ApproverName ?? expenseClaim.ApproverName;
                    expenseClaim.ApproverID = (command.ApproverID == 0) ? expenseClaim.ApproverID : command.ApproverID;
                    expenseClaim.ApprovalDate = (command.ApprovalDate == DateTime.MinValue) ? expenseClaim.ApprovalDate : command.ApprovalDate;
                    expenseClaim.ProcessedDate = (command.ProcessedDate == DateTime.MinValue) ? expenseClaim.ProcessedDate : command.ProcessedDate;
                    expenseClaim.RequesterComments = command.RequesterComments ?? expenseClaim.RequesterComments;
                    expenseClaim.ApproverComments = command.ApproverComments ?? expenseClaim.ApproverComments;
                    expenseClaim.FinanceComments = command.FinanceComments ?? expenseClaim.FinanceComments;

                    await _repository.UpdateAsync(expenseClaim);
                    await _unitOfWork.Commit(cancellationToken);

                    if (command.Status == "approved")
                    {
                        await _mailService.SendAsync(
                            new MailRequest()
                            {
                                From = "vivien30@ethereal.email",
                                To = "rayjung95@gmail.com",
                                Body = "Your Expense Claim is approved by approval",
                                Subject = "Expense Claim is approved"
                            }
                        );
                    }
                    else if (command.Status == "quried")
                    {
                        var route = $"reports/rework/{command.Id}";
                        await _mailService.SendAsync(
                            new MailRequest()
                            {
                                From = "vivien30@ethereal.email",
                                To = "rayjung95@gmail.com",
                                Body = $"Your Expense Claim is queried by approval.                    Rework is required. <a href=https://localhost:44380/{route}>Click here to                          rework</a>",
                                Subject = "Expense Claim is quried"
                            }
                        );
                    }
                    else if (command.Status == "rejected")
                    {
                        await _mailService.SendAsync(
                            new MailRequest()
                            {
                                From = "vivien30@ethereal.email",
                                To = "rayjung95@gmail.com",
                                Body = "Your Expense Claim is rejected by approval",
                                Subject = "Expense Claim is rejected"
                            }
                        );

                    }
                    else if (command.Status == "financeRejected")
                    {
                        await _mailService.SendAsync(
                            new MailRequest()
                            {
                                From = "vivien30@ethereal.email",
                                To = "rayjung95@gmail.com",
                                Body = "Your Expense Claim is rejected by finance",
                                Subject = "Expense Claim is rejected by approval"
                            }
                        );

                    }
                    else if (command.Status == "processed")
                    {
                        await _mailService.SendAsync(
                            new MailRequest()
                            {
                                From = "vivien30@ethereal.email",
                                To = "rayjung95@gmail.com",
                                Body = "Your Expense Claim is processed by finance",
                                Subject = "Expense Claim is processed"
                            }
                        );

                    }

                    return Result<int>.Success(expenseClaim.Id);
                }
            }
        }
    }
}