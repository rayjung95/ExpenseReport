namespace StoreManager.Shared.Models
{
    public class BaseAPIResponse
    {
        public bool Failed { get; set; }
        public string Message { get; set; }
        public bool Succeeded { get; set; }
    }

}
