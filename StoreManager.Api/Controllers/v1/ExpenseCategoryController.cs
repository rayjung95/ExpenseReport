using Microsoft.AspNetCore.Mvc;
using StoreManager.API.Controllers;
using StoreManager.Application.Features.ExpenseCategories.Commands.Create;
using StoreManager.Application.Features.ExpenseCategories.Commands.Delete;
using StoreManager.Application.Features.ExpenseCategories.Commands.Update;
using StoreManager.Application.Features.ExpenseCategories.Queries.GetAllPaged;
using StoreManager.Application.Features.ExpenseCategories.Queries.GetById;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace StoreManager.Api.Controllers.v1
{
    public class ExpenseCategoryController : BaseApiController<ExpenseCategoryController>
    {
        //GET: api/<expenseclaimcontroller>
        [HttpGet]
        public async Task<IActionResult> GetAll(int pageNumber, int pageSize)
        {
            var expenseCategories = await _mediator.Send(new GetAllExpenseCategoriesQuery(pageNumber, pageSize));
            return Ok(expenseCategories);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var expenseCategory = await _mediator.Send(new GetExpenseCategoryByIdQuery() { Id = id });
            return Ok(expenseCategory);
        }

        // POST api/<controller>
        [HttpPost]
        public async Task<IActionResult> Post(CreateExpenseCategoryCommand command)
        {
            return Ok(await _mediator.Send(command));
        }

        // PUT api/<controller>/5
        [HttpPut("{id}")]
        public async Task<IActionResult> Put(int id, UpdateExpenseCategoryCommand command)
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
            return Ok(await _mediator.Send(new DeleteExpenseCategoryCommand { Id = id }));
        }
    }
}
