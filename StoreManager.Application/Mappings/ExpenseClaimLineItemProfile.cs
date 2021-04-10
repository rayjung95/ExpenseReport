using StoreManager.Domain.Entities.Catalog;
using AutoMapper;
using StoreManager.Application.Features.ExpenseClaimLineItems.Commands.Create;
using StoreManager.Application.Features.ExpenseClaimLineItems.Queries.GetById;
using StoreManager.Application.Features.ExpenseClaimLineItems.Queries.GetAllPaged;

namespace StoreManager.Application.Mappings
{
    internal class ExpenseClaimLineItemProfile : Profile
    {
        public ExpenseClaimLineItemProfile()
        {
            CreateMap<CreateExpenseClaimLineItemCommand, ExpenseClaimLineItem>().ReverseMap();
            CreateMap<GetExpenseClaimLineItemByIdResponse, ExpenseClaimLineItem>().ReverseMap();
            CreateMap<GetAllExpenseClaimLineItemsResponse, ExpenseClaimLineItem>().ReverseMap();
        }
    }
}