using StoreManager.Domain.Entities.Catalog;
using AutoMapper;
using StoreManager.Application.Features.ExpenseClaims.Commands.Create;
using StoreManager.Application.Features.ExpenseClaims.Queries.GetById;
using StoreManager.Application.Features.ExpenseClaims.Queries.GetAllPaged;

namespace StoreManager.Application.Mappings
{
    internal class ExpenseClaimProfile : Profile
    {
        public ExpenseClaimProfile()
        {
            CreateMap<CreateExpenseClaimCommand, ExpenseClaim>().ReverseMap();
            CreateMap<GetExpenseClaimByIdResponse, ExpenseClaim>().ReverseMap();
            CreateMap<GetAllExpenseClaimsResponse, ExpenseClaim>().ReverseMap();
        }
    }
}