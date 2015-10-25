using System;
using Windows.ApplicationModel;
using Windows.ApplicationModel.Resources;
using Windows.System;
using Windows.UI.Popups;

namespace UrlToolkit.Common
{
    public static class ExceptionHandler
    {
        public static void ReportException(Exception ex)
        {
            try
            {
                var localSettings = Windows.Storage.ApplicationData.Current.LocalSettings;

                String errorMessage = "\n\n\n" + ex.Message + "\n\n" + ex.StackTrace;

                if (localSettings.Values.ContainsKey("UnhandledException"))
                {
                    localSettings.Values["UnhandledException"] = errorMessage;
                }
                else
                {
                    localSettings.Values.Add("UnhandledException", errorMessage);
                }
            }
            catch (Exception)
            {

            }
        }

        public async static void CheckForPreviousException()
        {
            try
            {
                var localSettings = Windows.Storage.ApplicationData.Current.LocalSettings;
                if (localSettings.Values.ContainsKey("UnhandledException"))
                {
                    object value;
                    localSettings.Values.TryGetValue("UnhandledException", out value);

                    String errorString = value as String;
                    if (errorString != null && errorString.CompareTo(String.Empty) != 0)
                    {
                        ResourceLoader resourceLoader = new ResourceLoader();

                        var dialog = new MessageDialog(resourceLoader.GetString("ErrorWarning"));
                        dialog.Title = resourceLoader.GetString("ErrorHeader");
                        dialog.Commands.Add(new UICommand { Label = resourceLoader.GetString("ErrorYes"), Id = 0 });
                        dialog.Commands.Add(new UICommand { Label = resourceLoader.GetString("ErrorNo"), Id = 1 });
                        var res = await dialog.ShowAsync();

                        if ((int)res.Id == 0)
                        {
                            PackageVersion version = Package.Current.Id.Version;
                            String appVersion = String.Format("{0}.{1}.{2}.{3}", version.Major, version.Minor, version.Build, version.Revision);

                            Uri mailto = new Uri("mailto:" + ProjectConstants.FEEDBACK_MAIL_ADDRESS + "?subject=" + resourceLoader.GetString("ApplicationName") + "(" + appVersion + ")&body=" + errorString);
                            await Launcher.LaunchUriAsync(mailto);
                        }
                    }

                    localSettings.Values.Remove("UnhandledException");
                }
            }
            catch (Exception)
            {

            }
        }
    }
}
