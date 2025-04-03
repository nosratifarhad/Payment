namespace Paya.Host.Dtos
{
    public class AmountDto
    {
        public int Id { get; set; }
        public int UserId { get; set; }
        public decimal Balance { get; set; }
        public decimal ReservedAmount { get; set; }
        public string ShebaNumber { get; set; }
        public DateTime UpdateAt { get; set; }
    }
}
