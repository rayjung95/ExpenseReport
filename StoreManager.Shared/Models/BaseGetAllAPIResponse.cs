namespace StoreManager.Shared.Models
{
    public class BaseGetAllAPIResponse : BaseAPIResponse
    {
        public int Page { get; set; }
        public int TotalPages { get; set; }
        public int TotalCount { get; set; }
        public bool HasPreviousPage { get; set; }
        public bool HasNextPage { get; set; }

    }
}
