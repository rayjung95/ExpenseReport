namespace StoreManager.Infrastructure.CacheKeys
{
    public static class ExpenseCategoryCacheKeys
    {
        public static string ListKey => "ExpenseCategoryList";

        public static string SelectListKey => "ExpenseCategorySelectList";

        public static string GetKey(int expenseCategoryId) => $"ExpenseCategory-{expenseCategoryId}";

        public static string GetDetailsKey(int expenseCategoryId) => $"ExpenseCategoryDetails-{expenseCategoryId}";
    }
}