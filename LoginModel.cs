using System.ComponentModel.DataAnnotations;

namespace RaptorTrial.Frontend.Models;

/// <summary>
/// View-model bound to the Login form.
/// DataAnnotations drive Blazor's built-in client-side validation via
/// &lt;DataAnnotationsValidator&gt; and &lt;ValidationMessage&gt;.
/// </summary>
public class LoginModel
{
    [Required(ErrorMessage = "Username is required.")]
    [StringLength(100, MinimumLength = 1, ErrorMessage = "Username must be between 1 and 100 characters.")]
    public string Username { get; set; } = string.Empty;

    [Required(ErrorMessage = "Password is required.")]
    [StringLength(200, MinimumLength = 1, ErrorMessage = "Password must be between 1 and 200 characters.")]
    public string Password { get; set; } = string.Empty;
}
