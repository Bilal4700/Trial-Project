namespace CalculatorApp.Models
{
    public class LoginLog
    {
        public int Id { get; set; }
        public int Username { get; set; }
        public bool WasSuccessful { get; set; }
        public string? FailureReason { get; set; }
        public DateTime AttemptedAtUtc { get; set; } = DateTime.UtcNow;
        
    }
}