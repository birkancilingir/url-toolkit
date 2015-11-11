using System;
using System.Collections.Generic;
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
                        {
                            ResourceLoader resourceLoader = new ResourceLoader();
                            await AlertService.ShowAlertAsync(resourceLoader.GetString("ErrorHeader"), resourceLoader.GetString("WebUrlIsRequiredErrorMessage"));
                            return;
                        }

                        ConnectionProfile internetConnectionProfile = NetworkInformation.GetInternetConnectionProfile();
                        if (internetConnectionProfile == null || internetConnectionProfile.GetNetworkConnectivityLevel() != NetworkConnectivityLevel.InternetAccess)
                        {
                            ResourceLoader resourceLoader = new ResourceLoader();
                            await AlertService.ShowAlertAsync(resourceLoader.GetString("ErrorHeader"), resourceLoader.GetString("InternetNotAvailableErrorMessage"));
                            return;
                        }

                        try
                        {
                            // Try to read supported services file, if none is found download the services
                            IList<Service> supportedServices = await LongUrlDataStore.ReadSupportedServicesFromDataStore();
                            if (supportedServices == null)
                            {
                                ServicesFilter servicesFilter = new ServicesFilter();
                                servicesFilter.Format = ResponseFormat.JSON;

                                supportedServices = await _dataService.GetSupportedServicesList(servicesFilter, ProjectUtilFunctions.getUserAgent(),
                                    () => { IsResultsLoading = true; },
                                    () => { IsResultsLoading = false; }
                                );

                                await LongUrlDataStore.WriteSupportedServicesToDataStore(supportedServices);
                            }

                            Boolean isServiceSupported = false;
                            foreach (Service service in supportedServices)
                            {
                                if (ShortenedUrlString.StartsWith("http://" + service.Domain)
                                    || ShortenedUrlString.StartsWith("https://" + service.Domain))
                                {
                                    isServiceSupported = true;
                                    break;
                                }
                            }

                            if (!isServiceSupported)
                            {
                                ResourceLoader resourceLoader = new ResourceLoader();
                                await AlertService.ShowAlertAsync(resourceLoader.GetString("ErrorHeader"), resourceLoader.GetString("ServiceNotSupportedErrorMessage"));
                                return;
                            }

                            ExpandUrlFilter expandUrlFilter = new ExpandUrlFilter();
                            expandUrlFilter.Url = ShortenedUrlString;
                            expandUrlFilter.Format = ResponseFormat.JSON;

                            Result = await _dataService.ExpandUrl(expandUrlFilter, ProjectUtilFunctions.getUserAgent(),
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
