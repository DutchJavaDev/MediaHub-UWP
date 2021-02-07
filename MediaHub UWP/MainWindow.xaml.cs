using MediaHub_UWP.Controls;
using System;
using System.Drawing;
using System.Threading.Tasks;
using TMDbLib.Client;
using MediaHub_UWP.Pages;
using Windows.UI.ViewManagement;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Input;
using System.Collections.Generic;


// The Blank Page item template is documented at https://go.microsoft.com/fwlink/?LinkId=234238

namespace MediaHub_UWP
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class MainWindow : Page
    {
        public static MainWindow Instance;
        private readonly Frame ContentFrame;
        private readonly Dictionary<string, Type> Routes;

        public MainWindow()
        {
            MaximizeWindowOnLoad();
            InitializeComponent();

            ContentFrame = (Frame)NavBar.Content;

            Instance = this;
            Routes = new Dictionary<string, Type> {
                {"home",typeof(HomePage) },
                {"movies", typeof(MoviesPage) },
                {"shows", typeof(ShowsPage) }
            };

            var titleBar = ApplicationView.GetForCurrentView().TitleBar;

            var background = Windows.UI.Color.FromArgb(255, 13, 37, 63);

            titleBar.BackgroundColor = background;
        }

        private static void MaximizeWindowOnLoad()
        {
            // Get how big the window can be in epx.
            var bounds = ApplicationView.GetForCurrentView().VisibleBounds;
            ApplicationView.PreferredLaunchViewSize = new Windows.Foundation.Size(bounds.Width, bounds.Height);
            ApplicationView.PreferredLaunchWindowingMode = ApplicationViewWindowingMode.PreferredLaunchViewSize;
        }

        private void Grid_PointerEntered(object sender, PointerRoutedEventArgs e)
        {
            Window.Current.CoreWindow.PointerCursor = new Windows.UI.Core.CoreCursor(Windows.UI.Core.CoreCursorType.Hand, 0);
        }

        private void Grid_PointerExited(object sender, PointerRoutedEventArgs e)
        {
            Window.Current.CoreWindow.PointerCursor = new Windows.UI.Core.CoreCursor(Windows.UI.Core.CoreCursorType.Arrow, 0);
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            Home.IsSelected = true;
            Home.IsEnabled = true;
            ContentFrame.Navigate(Routes["home"]);
        }

        private bool TryGoBack()
        {
            if (!ContentFrame.CanGoBack)
                return false;

            ContentFrame.GoBack();
            return true;
        }

        private void NavBar_SelectionChanged(NavigationView sender, NavigationViewSelectionChangedEventArgs args)
        {
            var item = ((NavigationViewItem)sender.SelectedItem);

            item.IsSelected = true;
            item.IsEnabled = true;

            ContentFrame.Navigate(Routes[item.Tag.ToString()]);
        }

        private void NavBar_BackRequested(NavigationView sender, NavigationViewBackRequestedEventArgs args)
        {
            // Track history
            // Update navbar
            TryGoBack();
        }
    }
}
