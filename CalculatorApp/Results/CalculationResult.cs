namespace CalculatorApp.Results;

public class CalculationResult
{
    public bool IsSuccess { get; private set; }

    public decimal? Result { get; private set; }

    public string Message { get; private set; } = string.Empty;

    public static CalculationResult Success(decimal result)
    {
        return new CalculationResult
        {
            IsSuccess = true,
            Result = result,
            Message = "Calculation completed successfully."
        };
    }

    public static CalculationResult Failure(string message)
    {
        return new CalculationResult
        {
            IsSuccess = false,
            Result = null,
            Message = message
        };
    }
}