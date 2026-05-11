namespace CalculatorApp.Results;

public class AuthResult
{
    public bool IsSuccess { get; private set; }

    public string Message { get; private set; } = string.Empty;

    public string? Username { get; private set; }

    public static AuthResult Success(string message, string username)
    {
        return new AuthResult
        {
            IsSuccess = true,
            Message = message,
            Username = username
        };
    }

    public static AuthResult Failure(string message)
    {
        return new AuthResult
        {
            IsSuccess = false,
            Message = message,
            Username = null
        };
    }
}