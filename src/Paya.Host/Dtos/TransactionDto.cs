using Paya.Host.Domain.Transfer.Enums;

namespace Paya.Host.Dtos
{
    public class TransactionDto
    {
        public int TransferRequestId { get; set; }
        public decimal Price { get; set; }
        public string Description { get; set; }
        public TransactionType TransactionType { get; set; }
    }
}
