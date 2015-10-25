using System;
using System.Threading.Tasks;
using Windows.UI.Xaml.Controls;

namespace UrlToolkit.Common
{
    public class AlertService
    {
        private static bool isShowing = false;

        public static async Task ShowAlertAsync(String title, String body)
        {
            // Do not open a new dialog if another one exists
            if (isShowing)
                return;

#if WINDOWS_PHONE_APP
            ContentDialog dialog = new ContentDialog();
            dialog.Title = title;
            dialog.PrimaryButtonText = "OK";

            dialog.Content = new TextBlock()
            {
                Text = body,
                TextWrapping = Windows.UI.Xaml.TextWrapping.WrapWholeWords
            };

            AlertService.isShowing = true;
            await dialog.ShowAsync();
            AlertService.isShowing = false;
#else

#endif
        }
    }
}
