using System.Text.RegularExpressions;
using CalculatorApp.Results;

namespace CalculatorApp.Validation;

public class AuthInputValidator
{
    private const int MaxUsernameLength = 50;
    private const int MaxPasswordLength = 50;
    private const int MinPasswordLength = 4;

    public AuthValidationResults ValidateUsername(string? username)
    {

        // Check if the username is empty or consists only of whitespace (Initial validation)
        if (string.IsNullOrWhiteSpace(username))
        {
            return AuthValidationResults.Failure("Username is required.");
        }

        string cleanedUsername = username.Trim();

        if (cleanedUsername.Length > MaxUsernameLength)
        {
            return AuthValidationResults.Failure($"Username cannot be longer than {MaxUsernameLength} characters.");
        }

        if (!Regex.IsMatch(cleanedUsername, "^[a-zA-Z0-9_]+$"))
        {
            return AuthValidationResults.Failure("Username can only contain letters, numbers, and underscores.");
        }

        return AuthValidationResults.Success(cleanedUsername);
    }

    public AuthValidationResults ValidatePassword(string? password)

    {
        // Check if the password is empty or consists only of whitespace (Initial validation)
        if (string.IsNullOrWhiteSpace(password))
        {
            return AuthValidationResults.Failure("Password is required.");
        }

        string cleanedPassword = password.Trim();

        if (cleanedPassword.Length < MinPasswordLength)
        {
            return AuthValidationResults.Failure($"Password must be at least {MinPasswordLength} characters long.");
        }

        if (cleanedPassword.Length > MaxPasswordLength)
        {
            return AuthValidationResults.Failure($"Password cannot be longer than {MaxPasswordLength} characters.");
        }

        return AuthValidationResults.Success(cleanedPassword);
    }
}