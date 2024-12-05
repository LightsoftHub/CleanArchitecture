using CleanArch.ClientApp.Services;
using MudBlazor;

namespace CleanArch.ClientApp.Infrastructure;

internal class ToastDisplay(ISnackbar snackbar) : IToastDisplay
{
    public void ShowSuccess(string message) => snackbar.Add(message, Severity.Success);

    public void ShowWarning(string message) => snackbar.Add(message, Severity.Warning);

    public void ShowError(string message) => snackbar.Add(message, Severity.Error);

    public void Clear() => snackbar.Clear();
}
