using UrlToolkit.Common;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.Graphics.Display;
using Windows.UI.ViewManagement;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;

// The Basic Page item template is documented at http://go.microsoft.com/fwlink/?LinkID=390556

namespace UrlToolkit.View
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class MainView : Page
    {
        private NavigationHelper navigationHelper;

        public MainView()
        {
            this.InitializeComponent();

            this.navigationHelper = new NavigationHelper(this);
            this.navigationHelper.LoadState += this.NavigationHelper_LoadState;
            this.navigationHelper.SaveState += this.NavigationHelper_SaveState;
        }

        /// <summary>
        /// Gets the <see cref="NavigationHelper"/> associated with this <see cref="Page"/>.
        /// </summary>
        public NavigationHelper NavigationHelper
        {
            get { return this.navigationHelper; }
        }

        /// <summary>
        /// Populates the page with content passed during navigation.  Any saved state is also
        /// provided when recreating a page from a prior session.
        /// </summary>
        /// <param name="sender">
        /// The source of the event; typically <see cref="NavigationHelper"/>
        /// </param>
        /// <param name="e">Event data that provides both the navigation parameter passed to
        /// <see cref="Frame.Navigate(Type, Object)"/> when this page was initially requested and
        /// a dictionary of state preserved by this page during an earlier
        /// session.  The state will be null the first time a page is visited.</param>
        private void NavigationHelper_LoadState(object sender, LoadStateEventArgs e)
        {
            //if (e.PageState != null)
            //{
            //    try
            //    {
            //        if (e.PageState.ContainsKey("MatchType"))
            //        {
            //            TdkDictionary.ViewModel.MainViewModel.MatchTypeItem MatchType = e.PageState["MatchType"] as TdkDictionary.ViewModel.MainViewModel.MatchTypeItem;
            //            for (int i = 0; i < (this.DataContext as MainViewModel).MatchTypes.Count; i++)
            //            {
            //                if ((this.DataContext as MainViewModel).MatchTypes[i].Key == MatchType.Key)
            //                {
            //                    (this.DataContext as MainViewModel).MatchType = (this.DataContext as MainViewModel).MatchTypes[i];
            //                    break;
            //                }
            //            }
            //        }

            //        (this.DataContext as MainViewModel).SearchString = e.PageState["SearchString"] as String;
            //        (this.DataContext as MainViewModel).IsResultsLoading = ((e.PageState["IsResultsLoading"] as Nullable<Boolean>).HasValue
            //            && (e.PageState["IsResultsLoading"] as Nullable<Boolean>).Value ? true : false);
            //        (this.DataContext as MainViewModel).IsNoResultFound = ((e.PageState["IsNoResultFound"] as Nullable<Boolean>).HasValue
            //            && (e.PageState["IsNoResultFound"] as Nullable<Boolean>).Value ? true : false);
            //        (this.DataContext as MainViewModel).Words = e.PageState["Words"] as ObservableCollection<Word>;
            //    }
            //    catch (Exception ex)
            //    {
            //        Debug.WriteLine(ex.Message);
            //    }

            //}
        }

        /// <summary>
        /// Preserves state associated with this page in case the application is suspended or the
        /// page is discarded from the navigation cache.  Values must conform to the serialization
        /// requirements of <see cref="SuspensionManager.SessionState"/>.
        /// </summary>
        /// <param name="sender">The source of the event; typically <see cref="NavigationHelper"/></param>
        /// <param name="e">Event data that provides an empty dictionary to be populated with
        /// serializable state.</param>
        private void NavigationHelper_SaveState(object sender, SaveStateEventArgs e)
        {
            //e.PageState["MatchType"] = (this.DataContext as MainViewModel).MatchType;
            //e.PageState["SearchString"] = (this.DataContext as MainViewModel).SearchString;
            //e.PageState["IsResultsLoading"] = (this.DataContext as MainViewModel).IsResultsLoading;
            //e.PageState["IsNoResultFound"] = (this.DataContext as MainViewModel).IsNoResultFound;
            //e.PageState["Words"] = (this.DataContext as MainViewModel).Words;
        }

        #region NavigationHelper registration

        /// <summary>
        /// The methods provided in this section are simply used to allow
        /// NavigationHelper to respond to the page's navigation methods.
        /// <para>
        /// Page specific logic should be placed in event handlers for the  
        /// <see cref="NavigationHelper.LoadState"/>
        /// and <see cref="NavigationHelper.SaveState"/>.
        /// The navigation parameter is available in the LoadState method 
        /// in addition to page state preserved during an earlier session.
        /// </para>
        /// </summary>
        /// <param name="e">Provides data for navigation methods and event
        /// handlers that cannot cancel the navigation request.</param>
        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            this.navigationHelper.OnNavigatedTo(e);
        }

        protected override void OnNavigatedFrom(NavigationEventArgs e)
        {
            this.navigationHelper.OnNavigatedFrom(e);
        }

        #endregion
    }
}
