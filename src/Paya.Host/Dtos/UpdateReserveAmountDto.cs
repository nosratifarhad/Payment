namespace Paya.Host.Dtos
{
    public class UpdateReserveAmountDto
    {
        public int Id { get; set; }
        public int UserId { get; set; }
        public decimal Price { get; set; }
        public string Status { get; set; }
    }
}
