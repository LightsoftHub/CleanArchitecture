using MudBlazor;

namespace CleanArch.ClientApp;

public class WebSettings
{
    public const string APP_NAME = "Light App";

    public const string APP_DESCRIPTION = "Blazor starter app";

    // UI
    public static int[] DEFAULT_TABLE_SIZES => [20, 50, 100];

    public static DialogOptions DEFAULT_DIALOG_OPTIONS => new()
    {
        BackdropClick = false,
        CloseOnEscapeKey = false,
        CloseButton = true,
    };
}
