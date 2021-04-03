using Microsoft.AspNetCore.Mvc;
using StoreManager.API.Controllers;
using StoreManager.Application.Features.EpenseClaimLineItems.Queries.GetAllPaged;
using StoreManager.Application.Features.ExpenseClaimLineItems.Commands.Create;
using StoreManager.Application.Features.ExpenseClaimLineItems.Commands.Delete;
using StoreManager.Application.Features.ExpenseClaimLineItems.Commands.Update;
using StoreManager.Application.Features.ExpenseClaimLineItems.Queries.GetById;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace StoreManager.Api.Controllers.v1
{
    public class ExpenseClaimLineItemController : BaseApiController<ExpenseClaimLineItemController>
    {
        //GET: api/<expenseclaimcontroller>
        [HttpGet]
        public async Task<IActionResult> GetAll(int pageNumber, int pageSize)
        {
            var expenseClaimLineItems = await _mediator.Send(new GetAllExpensClaimLineItemsQuery(pageNumber, pageSize));
            return Ok(expenseClaimLineItems);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var expenseClaim = await _mediator.Send(new GetExpenseClaimLineItemByIdQuery() { Id = id });
            return Ok(expenseClaim);
        }

        // POST api/<controller>
        [HttpPost]
        public async Task<IActionResult> Post(CreateExpenseClaimLineItemCommand command)
        {
            return Ok(await _mediator.Send(command));
        }

        // PUT api/<controller>/5
        [HttpPut("{id}")]
        public async Task<IActionResult> Put(int id, UpdateExpenseClaimLineItemCommand command)
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
            return Ok(await _mediator.Send(new DeleteExpenseClaimLineItemCommand { Id = id }));
        }
    }
}
