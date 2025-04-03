namespace Paya.Host.Domain.Account.Entities
{
    public class UserAccount : BaseEntity
    {
        public Guid UserId { get; set; }
        public decimal Balance { get; set; }
        public DateTime UpdateAt { get; set; }
    }
}
