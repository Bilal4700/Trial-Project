using System.Globalization;
using CalculatorApp.Results;

namespace CalculatorApp.Validation;

public class CalculatorInputValidator
{
    private const int MaxInputLength = 30;
    private const decimal MinimumAllowedValue = -1000000m;
    private const decimal MaximumAllowedValue = 1000000m;

    public NumberValidationResult ValidateNumber(string? input, string fieldName)
    {
        if (string.IsNullOrWhiteSpace(input))
        {
            return NumberValidationResult.Failure($"{fieldName} is required.");
        }

        string cleanedInput = input.Trim();

        if (cleanedInput.Length > MaxInputLength)
        {
            return NumberValidationResult.Failure($"{fieldName} is too long.");
        }

        bool isValidNumber = decimal.TryParse(cleanedInput,out decimal number);

        if (!isValidNumber)
        {
            return NumberValidationResult.Failure($"{fieldName} must be a valid number.");
        }

        if (number < MinimumAllowedValue || number > MaximumAllowedValue)
        {
            return NumberValidationResult.Failure(
                $"{fieldName} must be between {MinimumAllowedValue} and {MaximumAllowedValue}.");
        }

        return NumberValidationResult.Success(number);
    }
}