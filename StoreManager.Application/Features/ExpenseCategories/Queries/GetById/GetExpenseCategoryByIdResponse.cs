using System;

namespace StoreManager.Application.Features.ExpenseCategories.Queries.GetById
{
    public class GetExpenseCategoryByIdResponse
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Code { get; set; }
    }
}