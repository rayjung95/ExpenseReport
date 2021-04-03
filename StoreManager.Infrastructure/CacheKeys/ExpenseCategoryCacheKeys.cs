namespace StoreManager.Infrastructure.CacheKeys
{
    public static class CurrencyCacheKeys
    {
        public static string ListKey => "CurrencyList";

        public static string SelectListKey => "CurrencySelectList";

        public static string GetKey(int currencyId) => $"Currency-{currencyId}";

        public static string GetDetailsKey(int currencyId) => $"CurrencyDetails-{currencyId}";
    }
}