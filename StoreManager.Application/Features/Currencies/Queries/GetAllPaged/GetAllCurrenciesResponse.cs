﻿using System;

namespace StoreManager.Application.Features.Currencies.Queries.GetAllPaged
{
    public class GetAllCurrenciesResponse
    {
        public int Id { get; set; }
        public string Code { get; set; }
        public string Name { get; set; }
        public string Symbol { get; set; }
    }
}