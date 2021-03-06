using Microsoft.AspNetCore.Mvc;
using StoreManager.API.Controllers;
using StoreManager.Application.DTOs.ExpenseClaim;
using StoreManager.Application.Features.ExpenseClaims.Commands.Create;
using StoreManager.Application.Features.ExpenseClaims.Commands.Delete;
using StoreManager.Application.Features.ExpenseClaims.Commands.Update;
using StoreManager.Application.Features.ExpenseClaims.Queries.GetAllPaged;
using StoreManager.Application.Features.ExpenseClaims.Queries.GetById;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace StoreManager.Api.Controllers.v1
{
    public class ExpenseClaimController : BaseApiController<ExpenseClaimController>
    {
        //GET: api/<expenseclaimcontroller>
        // Status will have 'submited' | 'queried' | 'approved'
        [HttpGet]
        public async Task<IActionResult> GetAll(int pageNumber, int pageSize, string status)
        {
            var expenseClaims = await _mediator.Send(new GetAllExpensClaimsQuery(pageNumber, pageSize, status));
            return Ok(expenseClaims);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var expenseClaim = await _mediator.Send(new GetExpenseClaimByIdQuery() { Id = id });
            return Ok(expenseClaim);
        }

        [HttpGet("{id}/Report")]
        public async Task<IActionResult> GetReportById(int id)
        {
            var expenseClaim = await _mediator.Send(new GetExpenseClaimReportByIdQuery() { Id = id });
            return Ok(expenseClaim);
        }

        // POST api/<controller>
        [HttpPost]
        public async Task<IActionResult> Post(CreateExpenseClaimCommand command)
        {
            return Ok(await _mediator.Send(command));
        }
        
        // POST api/<controller>
        [HttpPost("Report")]
        public async Task<IActionResult> CreateExpenseReport(CreateExpenseClaimReportCommand command)
        {
            return Ok(await _mediator.Send(command));
        }

        // PUT api/<controller>/5
        [HttpPut("{id}")]
        public async Task<IActionResult> Put(int id, UpdateExpenseClaimCommand command)
        {
            if (id != command.Id)
            {
                return BadRequest();
            }
            return Ok(await _mediator.Send(command));
        }

        // PUT api/<controller>/5
        [HttpPut("{id}/changeStatus")]
        public async Task<IActionResult> ChangeStatus(int id, ChangeStatusCommand command)
        {
            if (id != command.Id)
            {
                return BadRequest();
            }
            return Ok(await _mediator.Send(command));
        }

        // DELETE api/<controller>/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            return Ok(await _mediator.Send(new DeleteExpenseClaimCommand { Id = id }));
        }
    }
}
