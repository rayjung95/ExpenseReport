namespace StoreManager.Infrastructure.CacheKeys
{
    public static class ExpenseClaimLineItemCacheKeys
    {
        public static string ListKey => "ExpenseClaimLineItemList";

        public static string SelectListKey => "ExpenseClaimLineItemSelectList";

        public static string GetKey(int expenseClaimLineItemId) => $"ExpenseClaimLineItem-{expenseClaimLineItemId}";

        public static string GetDetailsKey(int expenseClaimLineItemId) => $"ExpenseClaimLineItemDetails-{expenseClaimLineItemId}";
    }
}