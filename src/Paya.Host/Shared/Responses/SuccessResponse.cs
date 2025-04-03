namespace Paya.Host.Shared.Responses
{
    public class SuccessResponse<T>
    {
        public string Message { get; set; }
        public T request { get; set; }
    }
}
