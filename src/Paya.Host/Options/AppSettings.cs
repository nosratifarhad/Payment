namespace Paya.Host.Options
{
    public class AppSettings
    {
        public PayaOptions PayaOptions { get; set; } = null!;
    }

    public sealed class PayaOptions
    {
        public string WriteContext { get; set; } = null!;
        public string ReadContext { get; set; } = null!;
    }
}
