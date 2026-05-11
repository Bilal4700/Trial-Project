namespace CalculatorApp.Services;

public class SessionStateService
{
    public bool IsLoggedIn { get; private set; }

    public string? Username { get; private set; }

    public void MarkUserAsLoggedIn(string username)
    {
        IsLoggedIn = true;
        Username = username;
    }

    public void Logout()
    {
        IsLoggedIn = false;
        Username = null;
    }
}