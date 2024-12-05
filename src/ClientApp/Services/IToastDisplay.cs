namespace CleanArch.ClientApp.Services;

public interface IToastDisplay
{
    void ShowSuccess(string message);

    void ShowWarning(string message);

    void ShowError(string message);

    void Clear();
}
