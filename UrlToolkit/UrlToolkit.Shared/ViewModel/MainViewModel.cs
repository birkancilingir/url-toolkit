using System;
using System.Collections.ObjectModel;
using System.Diagnostics;
using UrlToolkit.Common;
using UrlToolkit.DataService;
using UrlToolkit.View;
using Windows.ApplicationModel.Resources;
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
            ResourceLoader resourceLoader = new ResourceLoader();

            MatchTypes = new ObservableCollection<MatchTypeItem>
            {
                new MatchTypeItem {Key = "FullMatch", Value = resourceLoader.GetString("ComboBoxItemFullMatch") },
                new MatchTypeItem {Key = "PartialMatch", Value = resourceLoader.GetString("ComboBoxItemPartialMatch") }
            };

            MatchType = MatchTypes[0];

            //Words = new ObservableCollection<Word>();
        }

        public class MatchTypeItem
        {
            public String Key { get; set; }
            public String Value { get; set; }
        }

        #region Data Members
        public ObservableCollection<MatchTypeItem> MatchTypes { get; private set; }

        private MatchTypeItem _matchType;

        public MatchTypeItem MatchType
        {
            get { return this._matchType; }
            set
            {
                SetProperty(ref this._matchType, value);
            }
        }

        private String _searchString;

        public String SearchString
        {
            get { return this._searchString; }
            set
            {
                SetProperty(ref this._searchString, value);
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

        private Boolean _isNoResultFound = false;

        public Boolean IsNoResultFound
        {
            get { return this._isNoResultFound; }
            set
            {
                SetProperty(ref this._isNoResultFound, value);
            }
        }

        private Boolean _isSuggesstion = false;

        public Boolean IsSuggestion
        {
            get { return this._isSuggesstion; }
            set
            {
                SetProperty(ref this._isSuggesstion, value);
            }
        }

        private Boolean _isPartialMatch = false;

        public Boolean IsPartialMatch
        {
            get { return this._isPartialMatch; }
            set
            {
                SetProperty(ref this._isPartialMatch, value);
            }
        }

        //private ObservableCollection<Word> _words;

        //public ObservableCollection<Word> Words
        //{
        //    get { return this._words; }
        //    set
        //    {
        //        SetProperty(ref this._words, value);
        //    }
        //}
        #endregion

        #region Commands
        //private RelayCommand _listWordsCommand;

        //public RelayCommand ListWordsCommand
        //{
        //    get
        //    {
        //        return _listWordsCommand
        //            ?? (_listWordsCommand = new RelayCommand(() =>
        //            {
        //                Debug.WriteLine("ListWordsCommand");
        //                ListWords(null, SearchString);
        //            })
        //            );
        //    }
        //}

        //private RelayCommand<object> _readWordCommand;

        //public RelayCommand<object> ReadWordCommand
        //{
        //    get
        //    {
        //        return _readWordCommand
        //            ?? (_readWordCommand = new RelayCommand<object>((parameter) =>
        //            {
        //                Debug.WriteLine("ReadWordCommand");

        //                Word word = (parameter as Windows.UI.Xaml.Controls.ItemClickEventArgs).ClickedItem as Word;

        //                if (word == null)
        //                    return;

        //                SearchString = word.Name;
        //                MatchType = MatchTypes[0];

        //                ListWords(word.Id, word.Name);
        //            })
        //            );
        //    }
        //}

#if WINDOWS_PHONE_APP
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
#endif
        #endregion

        //private async void ListWords(Nullable<int> id, String name)
        //{
        //    if (String.IsNullOrWhiteSpace(name) || MatchType == null)
        //        return;

        //    ConnectionProfile internetConnectionProfile = NetworkInformation.GetInternetConnectionProfile();
        //    if (internetConnectionProfile == null || internetConnectionProfile.GetNetworkConnectivityLevel() != NetworkConnectivityLevel.InternetAccess)
        //    {
        //        ResourceLoader resourceLoader = new ResourceLoader();
        //        await AlertService.ShowAlertAsync(resourceLoader.GetString("ErrorHeader"), resourceLoader.GetString("InternetNotAvailableErrorMessage"));
        //        return;
        //    }

        //    if (Words != null)
        //        Words.Clear();
        //    IsNoResultFound = false;
        //    IsSuggestion = false;

        //    BigTurkishDictionaryFilter filter = new BigTurkishDictionaryFilter();
        //    filter.SearchString = name;
        //    filter.SearchId = id;

        //    if (MatchType.Key.Equals("FullMatch"))
        //    {
        //        filter.MatchType = BigTurkishDictionaryFilter.MatchTypeFilter.FULL_MATCH;
        //        IsPartialMatch = false;
        //    }
        //    else
        //    {
        //        filter.MatchType = BigTurkishDictionaryFilter.MatchTypeFilter.PARTIAL_MATCH;
        //        IsPartialMatch = true;
        //    }

        //    try
        //    {
        //        SearchResult result = await _dataService.SearchBigTurkishDictionary(filter,
        //            () => { IsResultsLoading = true; },
        //            () => { IsResultsLoading = false; }
        //        );

        //        IsSuggestion = result.IsSuggestion;

        //        Words = new ObservableCollection<Word>(result.Words);
        //        if (Words.Count == 0)
        //            IsNoResultFound = true;
        //    }
        //    catch (Exception e)
        //    {
        //        if (e.Message == HttpStatusCode.InternalServerError.ToString())
        //        {
        //            IsNoResultFound = false;
        //            IsResultsLoading = false;

        //            ResourceLoader resourceLoader = new ResourceLoader();
        //            AlertService.ShowAlertAsync(resourceLoader.GetString("ErrorHeader"), resourceLoader.GetString("ServerErrorMessage"));
        //        }
        //        else
        //        {
        //            throw;
        //        }
        //    }
        //}
    }
}
