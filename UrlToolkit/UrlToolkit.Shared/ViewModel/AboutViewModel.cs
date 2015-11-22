using System;
using System.Diagnostics;
using UrlToolkit.Common;
using Windows.ApplicationModel;
using Windows.ApplicationModel.DataTransfer;
using Windows.ApplicationModel.Resources;
using Windows.System;

namespace UrlToolkit.ViewModel
{
    public class AboutViewModel : BindableBase
    {
        private RelayCommand _sendFeedbackCommand;

        public RelayCommand SendFeedbackCommand
        {
            get
            {
                return _sendFeedbackCommand
                    ?? (_sendFeedbackCommand = new RelayCommand(async () =>
                    {
                        Debug.WriteLine("SendFeedbackCommand");
                        PackageVersion version = Package.Current.Id.Version;

                        String appVersion = String.Format("{0}.{1}.{2}.{3}", version.Major, version.Minor, version.Build, version.Revision);

                        ResourceLoader resourceLoader = new ResourceLoader();

                        Uri mailto = new Uri("mailto:" + ProjectConstants.FEEDBACK_MAIL_ADDRESS + "?subject=" + resourceLoader.GetString("ApplicationName") + "(" + appVersion + ")");
                        await Launcher.LaunchUriAsync(mailto);
                    })
                    );
            }
        }

        private RelayCommand _rateAndReviewCommand;

        public RelayCommand RateAndReviewCommand
        {
            get
            {
                return _rateAndReviewCommand
                    ?? (_rateAndReviewCommand = new RelayCommand(async () =>
                    {
                        Debug.WriteLine("RateAndReviewCommand");
#if WINDOWS_PHONE_APP
                        Uri uri = new Uri("ms-windows-store:reviewapp?appid=" + Windows.ApplicationModel.Store.CurrentApp.AppId);
                        await Windows.System.Launcher.LaunchUriAsync(uri);
#else
                        Uri uri = new Uri("ms-windows-store:review?PFN:" + Windows.ApplicationModel.Package.Current.Id);
                        await Windows.System.Launcher.LaunchUriAsync(uri);
#endif
                    })
            );
            }
        }

        private RelayCommand _shareCommand;

        public RelayCommand ShareCommand
        {
            get
            {
                return _shareCommand
                    ?? (_shareCommand = new RelayCommand(() =>
                    {
                        // Create data and show share ui
                        DataTransferManager manager = DataTransferManager.GetForCurrentView();
                        manager.DataRequested += (sender, args) =>
                        {
                            ResourceLoader resourceLoader = new ResourceLoader();
                            args.Request.Data.Properties.Title = resourceLoader.GetString("ApplicationName");
                            args.Request.Data.SetWebLink(Windows.ApplicationModel.Store.CurrentApp.LinkUri);
                        };
                        DataTransferManager.ShowShareUI();
                    })
                    );
            }
        }
    }
}
