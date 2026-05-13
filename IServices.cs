namespace RaptorTrial.Frontend.Services;

// ─────────────────────────────────────────────────────────────────────────────
// IAuthService
// Implemented by the backend team. The frontend only calls these methods.
// ─────────────────────────────────────────────────────────────────────────────

/// <summary>Result returned by LoginAsync.</summary>
public record LoginResult(bool Success, string? ErrorMessage = null);

/// <summary>
/// Authentication contract consumed by Login.razor.
/// Backend provides the concrete implementation registered in Program.cs.
/// </summary>
public interface IAuthService
{
    /// <summary>True when the current session has an authenticated user.</summary>
    bool IsAuthenticated { get; }

    /// <summary>
    /// Validates credentials and, on success, establishes the session.
    /// Every call must be logged by the implementation (success and failure).
    /// </summary>
    Task<LoginResult> LoginAsync(string username, string password);

    /// <summary>Terminates the current session.</summary>
    Task LogoutAsync();
}


// ─────────────────────────────────────────────────────────────────────────────
// ICalculatorService
// ─────────────────────────────────────────────────────────────────────────────

/// <summary>Result returned by CalculateAsync.</summary>
public record CalculationResult(bool Success, double Value = 0, string? ErrorMessage = null);

/// <summary>
/// Arithmetic contract consumed by Calculator.razor.
/// The implementation must:
///   • Persist each calculation to the database (requirement from interview doc).
///   • Log exceptions.
/// </summary>
public interface ICalculatorService
{
    /// <summary>
    /// Performs the requested operation on a and b.
    /// </summary>
    /// <param name="operation">"add" or "multiply"</param>
    /// <param name="a">First operand (already validated by the caller)</param>
    /// <param name="b">Second operand (already validated by the caller)</param>
    Task<CalculationResult> CalculateAsync(string operation, double a, double b);
}
