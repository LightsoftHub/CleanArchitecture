using Light.Contracts;
using Light.Extensions;
using Microsoft.JSInterop;
using MudBlazor;

namespace BlazorApp.Core;

public interface ICallGuardedService
{
    Task<Result> ExecuteAsync(Func<Task<Result>> call, string successMessage = "");

    Task<Result> ExecuteAsync(Func<Task<Result>> call, string successMessage, Func<Task<Result>> runIfSuccess);

    Task<Result> ExecuteAsync(Func<Task<Result>> call, string successMessage, Func<Task> runIfSuccess);

    Task TryDownloadAsync(Task<Stream> call, string fileName, IJSRuntime jsRuntime);

    // UI library
    Task<Result> ExecuteAsync(Func<Task<Result>> call, string successMessage, IMudDialogInstance dialog);

    Task<Result> GetDialogResultAsync(IDialogReference dialog);

    Task ConfirmBeforeRunAsync(IDialogService dialog,
        Func<Task<Result>> runFunc,
        string confirmMessage,
        string succeededMessage,
        Func<Task> runIfSuccess);
}

public class CallGuardedService(IToastDisplay toastService, SpinnerService spinnerService) : ICallGuardedService
{
    public async Task<Result> ExecuteAsync(Func<Task<Result>> call, string successMessage = "")
    {
        spinnerService.Show();

        Result result;
        //await Task.Delay(10000);
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

    public async Task TryDownloadAsync(Task<Stream> call, string fileName, IJSRuntime jsRuntime)
    {
        spinnerService.Show();

        try
        {
            var file = await call;

            await jsRuntime.InvokeVoidAsync("downloadBase64String", fileName, file.ToBase64String());
        }
        catch (Exception ex)
        {
            toastService.ShowError(ex.Message);
        }

        spinnerService.Hide();
    }

    /* LIBS */

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

        return Result.Error("Error when get dialog result");
    }

    public async Task ConfirmBeforeRunAsync(IDialogService dialog,
        Func<Task<Result>> runFunc,
        string confirmMessage,
        string succeededMessage,
        Func<Task> runIfSuccess)
    {
        bool? isConfirm = await dialog.ShowMessageBox("Warning", confirmMessage);

        if (isConfirm == true)
        {
            await ExecuteAsync(runFunc, succeededMessage, runIfSuccess);
        }
    }
}
