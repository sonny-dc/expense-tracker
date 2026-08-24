using ExpenseTracker.WinForms.Infrastructure.Http;

namespace ExpenseTracker.WinForms.Infrastructure.Dialogs;

public static class ApiErrorDialog
{
    public static void Show(
        IWin32Window owner,
        ApiClientException exception)
    {
        ArgumentNullException.ThrowIfNull(owner);
        ArgumentNullException.ThrowIfNull(exception);

        string message = exception.HasValidationErrors
            ? BuildValidationErrorMessage(
                exception.ValidationErrors)
            : exception.Message;

        MessageBox.Show(
            owner,
            message,
            exception.Title,
            MessageBoxButtons.OK,
            MessageBoxIcon.Error);
    }

    private static string BuildValidationErrorMessage(
        IReadOnlyDictionary<string, string[]> validationErrors)
    {
        IEnumerable<string> messages =
            validationErrors.SelectMany(
                pair => pair.Value.Select(
                    message => $"• {message}"));

        return string.Join(
            Environment.NewLine,
            messages);
    }
}
