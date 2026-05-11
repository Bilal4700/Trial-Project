using CalculatorApp.Data;
using CalculatorApp.Models;
using CalculatorApp.Results;
using CalculatorApp.Validation;
using Microsoft.EntityFrameworkCore;

namespace CalculatorApp.Services;

public class CalculatorService
{
    private readonly AppDbContext _dbContext;
    private readonly CalculatorInputValidator _validator;
    private readonly SessionStateService _sessionState;
    private readonly ILogger<CalculatorService> _logger;

    public CalculatorService(
        AppDbContext dbContext,
        CalculatorInputValidator validator,
        SessionStateService sessionState,
        ILogger<CalculatorService> logger)
    {
        _dbContext = dbContext;
        _validator = validator;
        _sessionState = sessionState;
        _logger = logger;
    }

    public async Task<CalculationResult> AddAsync(string? firstInput, string? secondInput)
    {
        return await CalculateAsync(firstInput, secondInput, "Add");
    }

    public async Task<CalculationResult> MultiplyAsync(string? firstInput, string? secondInput)
    {
        return await CalculateAsync(firstInput, secondInput, "Multiply");
    }

    public async Task<List<CalculationLog>> GetCalculationHistoryAsync()
    {
        return await _dbContext.CalculationLogs
            .AsNoTracking()
            .OrderByDescending(log => log.CreatedAtUtc)
            .Take(50)
            .ToListAsync();
    }

    private async Task<CalculationResult> CalculateAsync(
        string? firstInput,
        string? secondInput,
        string operation)
    {
        try
        {
            if (!_sessionState.IsLoggedIn)
            {
                return CalculationResult.Failure("You must be logged in to use the calculator.");
            }

            NumberValidationResult firstValidation = _validator.ValidateNumber(firstInput, "First number");

            if (!firstValidation.IsValid)
            {
                return CalculationResult.Failure(firstValidation.ErrorMessage);
            }

            NumberValidationResult secondValidation = _validator.ValidateNumber(secondInput, "Second number");

            if (!secondValidation.IsValid)
            {
                return CalculationResult.Failure(secondValidation.ErrorMessage);
            }

            decimal firstNumber = firstValidation.Value;
            decimal secondNumber = secondValidation.Value;

            decimal result;

            if (operation == "Add")
            {
                result = firstNumber + secondNumber;
            }
            else if (operation == "Multiply")
            {
                result = firstNumber * secondNumber;
            }
            else
            {
                throw new InvalidOperationException("Unsupported calculation operation.");
            }

            var calculationLog = new CalculationLog
            {
                FirstNumber = firstNumber,
                SecondNumber = secondNumber,
                Operation = operation,
                Result = result,
                CreatedAtUtc = DateTime.UtcNow
            };

            _dbContext.CalculationLogs.Add(calculationLog);
            await _dbContext.SaveChangesAsync();

            _logger.LogInformation(
                "Calculation performed by {Username}: {FirstNumber} {Operation} {SecondNumber} = {Result}",
                _sessionState.Username ?? "Unknown",
                firstNumber,
                operation,
                secondNumber,
                result);

            return CalculationResult.Success(result);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Unexpected error during calculation.");

            return CalculationResult.Failure("An unexpected error occurred during calculation.");
        }
    }
}