using Light.Contracts;
using MudBlazor;

namespace CleanArch.ClientApp.Services;

public interface ICallGuardedService
{
    Task<Result> ExecuteAsync(Func<Task<Result>> call, string successMessage = "");

    Task<Result> ExecuteAsync(Func<Task<Result>> call, string successMessage, Func<Task<Result>> runIfSuccess);

    Task<Result> ExecuteAsync(Func<Task<Result>> call, string successMessage, Func<Task> runIfSuccess);

    Task<Result> ExecuteAsync(Func<Task<Result>> call, string successMessage, IMudDialogInstance dialog);

    Task<Result> GetDialogResultAsync(IDialogReference dialog);
}

public class CallGuardedService(IToastDisplay toastService, SpinnerService spinnerService) : ICallGuardedService
{
    public async Task<Result> ExecuteAsync(Func<Task<Result>> call, string successMessage = "")
    {
        spinnerService.Show();

        Result result;

        try
        {
            result = await call();

            if (result.Succeeded)
            {
                if (!string.IsNullOrEmpty(successMessage))
                {
                    toastService.ShowSuccess(successMessage);
                }
                else
                {
                    toastService.Clear();
                }
            }
            else
            {
                toastService.ShowError(result.Message);
            }
        }
        catch (Exception ex)
        {
            toastService.ShowError(ex.Message);

            result = Result.Error(ex.Message);
        }

        spinnerService.Hide();

        return result;
    }

    public async Task<Result> ExecuteAsync(Func<Task<Result>> call, string successMessage, Func<Task<Result>> runIfSuccess)
    {
        var result = await ExecuteAsync(call, successMessage);

        if (result.Succeeded)
        {
            await runIfSuccess();
        }

        return result;
    }

    public async Task<Result> ExecuteAsync(Func<Task<Result>> call, string successMessage, Func<Task> runIfSuccess)
    {
        var result = await ExecuteAsync(call, successMessage);

        if (result.Succeeded)
        {
            await runIfSuccess();
        }

        return result;
    }

    public async Task<Result> ExecuteAsync(Func<Task<Result>> call, string successMessage, IMudDialogInstance dialog)
    {
        var result = await ExecuteAsync(call, successMessage);

        if (result.Succeeded)
        {
            dialog.Close(DialogResult.Ok(result));
        }

        return result;
    }

    public async Task<Result> GetDialogResultAsync(IDialogReference dialog)
    {
        var dialogResult = await dialog.Result;

        if (dialogResult != null && dialogResult.Data != null)
        {
            var result = (Result)dialogResult.Data;

            return result;
        }

        return Result.Error("Error when get");
    }
}
