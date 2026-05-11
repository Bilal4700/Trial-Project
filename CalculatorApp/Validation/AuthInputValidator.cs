public class ValidationResult
{
    public bool IsValid { get; private set; }

    public string CleanedValue { get; private set; } = string.Empty;

    public string ErrorMessage { get; private set; } = string.Empty;

    public static ValidationResult Success(string cleanedValue)
    {
        return new ValidationResult
        {
            IsValid = true,
            CleanedValue = cleanedValue,
            ErrorMessage = string.Empty
        };
    }

    public static ValidationResult Failure(string errorMessage)
    {
        return new ValidationResult
        {
            IsValid = false,
            CleanedValue = string.Empty,
            ErrorMessage = errorMessage
        };
    }
}