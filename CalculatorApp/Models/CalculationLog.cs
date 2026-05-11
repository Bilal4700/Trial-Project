namespace CalculatorApp.Models
{
    public class CalculationLog
    {
        public int Id { get; set; }
        public decimal FirstNumber { get; set; }
        public decimal SecondNumber { get; set; }
        public string Operation { get; set; } = string.Empty;
        public decimal Result { get; set; }
        public DateTime CreatedAtUtc { get; set; } = DateTime.UtcNow;
    }
}