using Paya.Host.Domain.Transfer.Enums;

namespace Paya.Host.Domain.Transfer.Entities
{
    public class TransferRequest : BaseEntity
    {
        public int UserId { get; set; }
        public decimal Price { get; set; }
        public string FromShebaNumber { get; set; }
        public string ToShebaNumber { get; set; }
        public string Note { get; set; }
        public TransferRequestStatus TransferStatus { get; set; }

        public ICollection<Transaction> Transactions { get; set; }
    }
}
