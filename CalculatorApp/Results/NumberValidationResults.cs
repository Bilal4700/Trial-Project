namespace CalculatorApp.Results;

public class NumberValidationResult
{
    public bool IsValid { get; private set; }

    public decimal Value { get; private set; }

    public string ErrorMessage { get; private set; } = string.Empty;

    public static NumberValidationResult Success(decimal value)
    {
        return new NumberValidationResult
        {
            IsValid = true,
            Value = value,
            ErrorMessage = string.Empty
        };
    }

    public static NumberValidationResult Failure(string errorMessage)
    {
        return new NumberValidationResult
        {
            IsValid = false,
            Value = 0,
            ErrorMessage = errorMessage
        };
    }
}