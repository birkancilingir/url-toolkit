using System;
using System.Diagnostics;
using System.Net;
using UrlToolkit.Common;
using UrlToolkit.DataService;
using UrlToolkit.DataService.Entities;
using UrlToolkit.View;
using Windows.ApplicationModel.Resources;
using Windows.Networking.Connectivity;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;

namespace UrlToolkit.ViewModel
{
    public class MainViewModel : BindableBase
    {
        private ILongUrlDataService _dataService;

        public MainViewModel(ILongUrlDataService dataService)
        {
            _dataService = dataService;
        }

        #region Data Members
        private String _shortenedUrlString;

        public String ShortenedUrlString
        {
            get { return this._shortenedUrlString; }
            set
            {
                SetProperty(ref this._shortenedUrlString, value);
            }
        }

        private LongUrl _result;

        public LongUrl Result
        {
            get { return this._result; }
            set
            {
                SetProperty(ref this._result, value);
            }
        }

        private Boolean _isResultsLoading = false;

        public Boolean IsResultsLoading
        {
            get { return this._isResultsLoading; }
            set
            {
                SetProperty(ref this._isResultsLoading, value);
            }
        }
        #endregion

        #region Commands
        private RelayCommand _expandUrlCommand;

        public RelayCommand ExpandUrlCommand
        {
            get
            {
                return _expandUrlCommand
                    ?? (_expandUrlCommand = new RelayCommand(async () =>
                    {
                        Debug.WriteLine("ExpandUrlCommand");

                        if (String.IsNullOrWhiteSpace(ShortenedUrlString))
                            return;

                        ConnectionProfile internetConnectionProfile = NetworkInformation.GetInternetConnectionProfile();
                        if (internetConnectionProfile == null || internetConnectionProfile.GetNetworkConnectivityLevel() != NetworkConnectivityLevel.InternetAccess)
                        {
                            ResourceLoader resourceLoader = new ResourceLoader();
                            await AlertService.ShowAlertAsync(resourceLoader.GetString("ErrorHeader"), resourceLoader.GetString("InternetNotAvailableErrorMessage"));
                            return;
                        }

                        ExpandUrlFilter filter = new ExpandUrlFilter();
                        filter.Url = ShortenedUrlString;
                        filter.Format = ResponseFormat.JSON;

                        try
                        {
                            Result = await _dataService.ExpandUrl(filter, ProjectUtilFunctions.getUserAgent(),
                                () => { IsResultsLoading = true; },
                                () => { IsResultsLoading = false; }
                            );
                        }
                        catch (Exception e)
                        {
                            if (e is LongUrlDataServiceException)
                            {
                                ResourceLoader resourceLoader = new ResourceLoader();
                                IsResultsLoading = false;

                                if (e.Message == HttpStatusCode.InternalServerError.ToString())
                                {                                    
                                    AlertService.ShowAlertAsync(resourceLoader.GetString("ErrorHeader"), resourceLoader.GetString("ServerErrorMessage"));
                                }
                                else
                                {
                                    AlertService.ShowAlertAsync(resourceLoader.GetString("ErrorHeader"), (e as LongUrlDataServiceException).errorMessage);
                                }
                            }
                            else
                            {
                                throw;
                            }
                        }
                    })
                    );
            }
        }

        private RelayCommand _openUrlCommand;

        public RelayCommand OpenUrlCommand
        {
            get
            {
                return _openUrlCommand
                    ?? (_openUrlCommand = new RelayCommand(async () =>
                    {
                        Debug.WriteLine("OpenUrlCommand:" + Result.Url);
                        await Windows.System.Launcher.LaunchUriAsync(new Uri(Result.Url));
                    })
                    );
            }
        }

        private RelayCommand _navigateToAboutCommand;

        public RelayCommand NavigateToAboutCommand
        {
            get
            {
                return _navigateToAboutCommand
                    ?? (_navigateToAboutCommand = new RelayCommand(() =>
                    {
                        Debug.WriteLine("NavigateToAboutCommand");

                        Frame rootFrame = Window.Current.Content as Frame;
                        rootFrame.Navigate(typeof(AboutView));
                    })
                    );
            }
        }
        #endregion
    }
}
