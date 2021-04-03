using System;

namespace StoreManager.Application.Features.Currencies.Queries.GetById
{
    public class GetCurrencyByIdResponse
    {
        public int Id { get; set; }
        public string Code { get; set; }
        public string Name { get; set; }
        public string Symbol { get; set; }
    }
}