using System;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;

// The User Control item template is documented at http://go.microsoft.com/fwlink/?LinkId=234236

namespace UrlToolkit.UserControls
{
    public sealed partial class PageHeader : UserControl
    {
        public PageHeader()
        {
            this.InitializeComponent();
        }

        public static readonly DependencyProperty PageTitleProperty = DependencyProperty.Register("PageTitle", typeof(String), typeof(UserControl), null);

        public String PageTitle
        {
            get { return (String)GetValue(PageTitleProperty); }
            set
            {
                SetValue(PageTitleProperty, value);
            }
        }
    }
}
