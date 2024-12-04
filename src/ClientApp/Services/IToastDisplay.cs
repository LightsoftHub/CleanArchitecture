using MudBlazor;

namespace CleanArch.ClientApp.Services;

public interface IToastDisplay
{
    void ShowSuccess(string message);

    void ShowWarning(string message);

    void ShowError(string message);
}

internal class ToastDisplay(ISnackbar snackbar) : IToastDisplay
{
    public void ShowSuccess(string message) => snackbar.Add(message, Severity.Success);

    public void ShowWarning(string message) => snackbar.Add(message, Severity.Warning);

    public void ShowError(string message) => snackbar.Add(message, Severity.Error);
}
