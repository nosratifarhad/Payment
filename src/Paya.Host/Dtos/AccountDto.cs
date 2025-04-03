namespace Paya.Host.Dtos
{
    public class AccountDto
    {
        public Guid Id { get; set; }
        public Guid UserId { get; set; }
        public decimal Balance { get; set; }
    }
}
