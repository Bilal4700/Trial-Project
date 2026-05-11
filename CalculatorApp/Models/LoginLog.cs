namespace CalculatorApp.Models
{
    public class LoginLog
    {
        public int Id { get; set; }
        public string Username { get; set; } = string.Empty;
        public bool WasSuccessful { get; set; }
        public string? FailureReason { get; set; }
        public DateTime AttemptedAtUtc { get; set; } = DateTime.UtcNow;
        
    }
}