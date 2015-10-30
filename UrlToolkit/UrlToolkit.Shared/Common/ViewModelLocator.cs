using Ninject;
using System;
using System.Diagnostics;
using UrlToolkit.DataService;
using UrlToolkit.ViewModel;

namespace UrlToolkit.Common
{
    public class ViewModelLocator
    {
        private static bool? _isInDesignMode;
        private static StandardKernel kernel = new StandardKernel();

        /// <summary>
        /// Gets a value indicating whether the control is in design mode
        /// (running in Blend or Visual Studio).
        /// </summary>
        public static bool IsInDesignMode
        {
            get
            {
                if (!_isInDesignMode.HasValue)
                {
                    _isInDesignMode = Windows.ApplicationModel.DesignMode.DesignModeEnabled;
                }

                return _isInDesignMode.Value;
            }
        }

        static ViewModelLocator()
        {
            if (ViewModelLocator.IsInDesignMode)
            {
                //kernel.Bind<IDictionaryDataService>().To<DictionaryDummyDataService>().InSingletonScope();
            }
            else
            {
                kernel.Bind<ILongUrlDataService>().To<LongUrlDataService>().InSingletonScope();
            }

            kernel.Bind<MainViewModel>().ToSelf();
            kernel.Bind<AboutViewModel>().ToSelf();

        }

        public static ILongUrlDataService DataService
        {
            get
            {
                return kernel.Get<ILongUrlDataService>();
            }
        }

        public MainViewModel Main
        {
            get
            {
                return kernel.Get<MainViewModel>();
            }
        }

        public AboutViewModel About
        {
            get
            {
                return kernel.Get<AboutViewModel>();
            }
        }
    }
}
