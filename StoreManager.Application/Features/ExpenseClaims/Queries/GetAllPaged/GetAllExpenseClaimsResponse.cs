namespace StoreManager.Application.Features.ExpenseClaims.Queries.GetAllPaged
{
    public class GetAllExpenseClaimsResponse
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string RequesterName { get; set; }
        public string ApproverName { get; set; }
        public decimal TotalAmount { get; set; }
    }
}