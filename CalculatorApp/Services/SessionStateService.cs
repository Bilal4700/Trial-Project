namespace CalculatorApp.Services;

public class SessionStateService
{
    public event Action? OnChange;

    public bool IsLoggedIn { get; private set; }

    public string? Username { get; private set; }

    public void MarkUserAsLoggedIn(string username)
    {
        IsLoggedIn = true;
        Username = username;
        NotifyStateChanged();
    }

    public void Logout()
    {
        IsLoggedIn = false;
        Username = null;
        NotifyStateChanged();
    }

    private void NotifyStateChanged()
    {
        OnChange?.Invoke();
    }
}
