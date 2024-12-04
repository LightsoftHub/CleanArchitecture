using Light.Contracts;

namespace CleanArch.ClientApp.Services;

public class CallGuardedService(IToastDisplay toastService)
{
    public async Task<Result> ExecuteAsync(Func<Task<Result>> call, string successMessage = "")
    {
        //spinnerService.Show();

        Result result;

        try
        {
            result = await call();

            if (result.Succeeded && !string.IsNullOrEmpty(successMessage))
            {
                toastService.ShowSuccess(successMessage);
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

        //spinnerService.Hide();

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
}
