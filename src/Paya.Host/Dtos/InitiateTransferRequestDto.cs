using Paya.Host.Domain.Transfer.Enums;

namespace Paya.Host.Dtos
{
    public class InitiateTransferRequestDto
    {
        public int UserId { get; set; }
        public decimal Price { get; set; }
        public string FromShebaNumber { get; set; }
        public string ToShebaNumber { get; set; }
        public string Note { get; set; }
        public TransferRequestStatus TransferStatus { get; set; }
        public DateTime CreatedAt { get; set; }

    }
}
