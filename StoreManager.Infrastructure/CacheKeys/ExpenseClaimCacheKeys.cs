namespace StoreManager.Infrastructure.CacheKeys
{
    public static class ExpenseClaimCacheKeys
    {
        public static string ListKey => "ExpenseClaimList";

        public static string SelectListKey => "ExpenseClaimSelectList";

        public static string GetKey(int expenseClaimId) => $"ExpenseClaim-{expenseClaimId}";

        public static string GetDetailsKey(int expenseClaimId) => $"ExpenseClaimDetails-{expenseClaimId}";
    }
}