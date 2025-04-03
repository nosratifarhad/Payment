using Paya.Host.Domain.Transfer.Enums;

namespace Paya.Host.Domain.Transfer.Entities
{
    public class Transaction : BaseEntity
    {
        public int TransferRequestId { get; set; }
        public decimal Price { get; set; }
        public TransactionType TransactionType { get; set; }
    }
}
