namespace Paya.Host.Dtos
{
    public class TransferRequestDto
    {
        public int Id { get; set; }
        public decimal Price { get; set; }
        public string Status { get; set; }
        public string FromShebaNumber { get; set; }
        public string ToShebaNumber { get; set; }
        public DateTime CreatedAt { get; set; }
    }
}
