using CalculatorApp.Data;
using CalculatorApp.Models;
using CalculatorApp.Results;
using CalculatorApp.Validation;
using Microsoft.EntityFrameworkCore;

namespace CalculatorApp.Services;

public class AuthService
{
    private readonly AppDbContext _dbContext;
    private readonly AuthInputValidator _validator;
    private readonly SessionStateService _sessionState;
    private readonly ILogger<AuthService> _logger;

    public AuthService(
        AppDbContext dbContext,
        AuthInputValidator validator,
        SessionStateService sessionState,
        ILogger<AuthService> logger)
    {
        _dbContext = dbContext;
        _validator = validator;
        _sessionState = sessionState;
        _logger = logger;
    }

    public async Task<AuthResult> RegisterAsync(string? username, string? password)
    {
        try
        {
            AuthValidationResults usernameValidation = _validator.ValidateUsername(username);

            if (!usernameValidation.IsValid)
            {
                return AuthResult.Failure(usernameValidation.ErrorMessage);
            }

            AuthValidationResults passwordValidation = _validator.ValidatePassword(password);

            if (!passwordValidation.IsValid)
            {
                return AuthResult.Failure(passwordValidation.ErrorMessage);
            }

            string cleanedUsername = usernameValidation.CleanedValue;
            string cleanedPassword = passwordValidation.CleanedValue;

            bool usernameAlreadyExists = await _dbContext.Users
                .AnyAsync(user => user.Username == cleanedUsername);

            if (usernameAlreadyExists)
            {
                return AuthResult.Failure("Username already exists.");
            }

            var newUser = new Users
            {
                Username = cleanedUsername,
                Password = cleanedPassword,
                CreatedAtUtc = DateTime.UtcNow
            };

            _dbContext.Users.Add(newUser);
            await _dbContext.SaveChangesAsync();

            _logger.LogInformation("New user registered with username {Username}.", cleanedUsername);

            return AuthResult.Success("Account created successfully.", cleanedUsername);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Unexpected error during user registration.");

            return AuthResult.Failure("An unexpected error occurred while creating the account.");
        }
    }

    public async Task<AuthResult> LoginAsync(string? username, string? password)
    {
        string attemptedUsername = username?.Trim() ?? string.Empty;

        try
        {
            AuthValidationResults usernameValidation = _validator.ValidateUsername(username);

            if (!usernameValidation.IsValid)
            {
                await SaveLoginLogAsync(attemptedUsername, false, usernameValidation.ErrorMessage);

                return AuthResult.Failure(usernameValidation.ErrorMessage);
            }

            AuthValidationResults passwordValidation = _validator.ValidatePassword(password);

            if (!passwordValidation.IsValid)
            {
                await SaveLoginLogAsync(usernameValidation.CleanedValue, false, passwordValidation.ErrorMessage);

                return AuthResult.Failure(passwordValidation.ErrorMessage);
            }

            string cleanedUsername = usernameValidation.CleanedValue;
            string cleanedPassword = passwordValidation.CleanedValue;

            Users? user = await _dbContext.Users
                .AsNoTracking()
                .FirstOrDefaultAsync(user => user.Username == cleanedUsername);

            if (user is null || user.Password != cleanedPassword)
            {
                await SaveLoginLogAsync(cleanedUsername, false, "Invalid credentials.");

                _logger.LogWarning("Failed login attempt for username {Username}.", cleanedUsername);

                return AuthResult.Failure("Invalid username or password.");
            }

            await SaveLoginLogAsync(cleanedUsername, true, null);

            _sessionState.MarkUserAsLoggedIn(cleanedUsername);

            _logger.LogInformation("User {Username} logged in successfully.", cleanedUsername);

            return AuthResult.Success("Login successful.", cleanedUsername);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Unexpected error during login for username {Username}.", attemptedUsername);

            return AuthResult.Failure("An unexpected error occurred while logging in.");
        }
    }

    public void Logout()
    {
        string? username = _sessionState.Username;

        _sessionState.Logout();

        _logger.LogInformation("User {Username} logged out.", username ?? "Unknown");
    }

    private async Task SaveLoginLogAsync(string username, bool wasSuccessful, string? failureReason)
    {
        var loginLog = new LoginLog
        {
            Username = username,
            WasSuccessful = wasSuccessful,
            FailureReason = failureReason,
            AttemptedAtUtc = DateTime.UtcNow
        };

        _dbContext.LoginLogs.Add(loginLog);
        await _dbContext.SaveChangesAsync();
    }
}