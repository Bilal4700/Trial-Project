namespace CalculatorApp.Results;

public class AuthValidationResults
{
    public bool IsValid { get; private set; }

    public string CleanedValue { get; private set; } = string.Empty;

    public string ErrorMessage { get; private set; } = string.Empty;

    public static AuthValidationResults Success(string cleanedValue)
    {
        return new AuthValidationResults
        {
            IsValid = true,
            CleanedValue = cleanedValue,
            ErrorMessage = string.Empty
        };
    }

    public static AuthValidationResults Failure(string errorMessage)
    {
        return new AuthValidationResults
        {
            IsValid = false,
            CleanedValue = string.Empty,
            ErrorMessage = errorMessage
        };
    }
}