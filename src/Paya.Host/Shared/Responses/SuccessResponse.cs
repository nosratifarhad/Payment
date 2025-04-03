using Paya.Host.Dtos;

namespace Paya.Host.Shared.Responses
{
    public class SuccessResponse
    {
        public string Message { get; set; }
        public SuccessTransferResponse request { get; set; }
    }
}
