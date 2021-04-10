using Microsoft.AspNetCore.Mvc;
using StoreManager.API.Controllers;
using StoreManager.Application.Features.Currencies.Commands.Create;
using StoreManager.Application.Features.Currencies.Commands.Delete;
using StoreManager.Application.Features.Currencies.Commands.Update;
using StoreManager.Application.Features.Currencies.Queries.GetAllPaged;
using StoreManager.Application.Features.Currencies.Queries.GetById;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace StoreManager.Api.Controllers.v1
{
    public class CurrencyController : BaseApiController<CurrencyController>
    {
        //GET: api/<expenseclaimcontroller>
        [HttpGet]
        public async Task<IActionResult> GetAll(int pageNumber, int pageSize)
        {
            var currencies = await _mediator.Send(new GetAllCurrenciesQuery(pageNumber, pageSize));
            return Ok(currencies);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var currency = await _mediator.Send(new GetCurrencyByIdQuery() { Id = id });
            return Ok(currency);
        }

        // POST api/<controller>
        [HttpPost]
        public async Task<IActionResult> Post(CreateCurrencyCommand command)
        {
            return Ok(await _mediator.Send(command));
        }

        // PUT api/<controller>/5
        [HttpPut("{id}")]
        public async Task<IActionResult> Put(int id, UpdateCurrencyCommand command)
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
            return Ok(await _mediator.Send(new DeleteCurrencyCommand { Id = id }));
        }
    }
}
