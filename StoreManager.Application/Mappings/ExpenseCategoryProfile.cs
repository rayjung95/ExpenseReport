using StoreManager.Domain.Entities.Catalog;
using AutoMapper;
using StoreManager.Application.Features.ExpenseCategories.Commands.Create;
using StoreManager.Application.Features.ExpenseCategories.Queries.GetById;
using StoreManager.Application.Features.ExpenseCategories.Queries.GetAllPaged;

namespace StoreManager.Application.Mappings
{
    internal class ExpenseCategoryProfile : Profile
    {
        public ExpenseCategoryProfile()
        {
            CreateMap<CreateExpenseCategoryCommand, ExpenseCategory>().ReverseMap();
            CreateMap<GetExpenseCategoryByIdResponse, ExpenseCategory>().ReverseMap();
            CreateMap<GetAllExpenseCategoriesResponse, ExpenseCategory>().ReverseMap();
        }
    }
}