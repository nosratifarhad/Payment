namespace Paya.Host.Dtos
{
    public class GetTransferRequestDto
    {
        public string Id { get; set; }
        public decimal Price { get; set; }
        public string Status { get; set; }
        public string FromShebaNumber { get; set; }
        public string ToShebaNumber { get; set; }
        public DateTime CreatedAt { get; set; }
    }
}
