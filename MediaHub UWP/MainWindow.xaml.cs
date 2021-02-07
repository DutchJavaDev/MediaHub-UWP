using System;
using System.Linq;
using MediaHub_UWP.Pages;
using Windows.UI.ViewManagement;
using Windows.UI.Xaml;
using Windows.UI;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Input;
using System.Collections.Generic;
using Windows.UI.Xaml.Media;


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
        private readonly Queue<string> RouteHistoryQueue;
        private readonly Brush NotSelectedItemBrush = new SolidColorBrush(Color.FromArgb((int)(255f * 0.75f), 145, 145, 145));
        private readonly Brush SelectedItemBrush = new SolidColorBrush(Color.FromArgb(255, 255, 255, 255));

        public MainWindow()
        {
            MaximizeWindowOnLoad();
            InitializeComponent();

            Instance = this;
            ContentFrame = (Frame)NavBar.Content;
            RouteHistoryQueue = new Queue<string>();
            Routes = new Dictionary<string, Type> {
                {"home",typeof(HomePage) },
                {"movies", typeof(MoviesPage) },
                {"shows", typeof(ShowsPage) }
            };

            var titleBar = ApplicationView.GetForCurrentView().TitleBar;

            var background = Color.FromArgb(255, 13, 37, 63);

            titleBar.BackgroundColor = background;
        }

        private static void MaximizeWindowOnLoad()
        {
            // Get how big the window can be in epx.
            var bounds = ApplicationView.GetForCurrentView().VisibleBounds;
            ApplicationView.PreferredLaunchViewSize = new Windows.Foundation.Size(bounds.Width, bounds.Height);
            ApplicationView.PreferredLaunchWindowingMode = ApplicationViewWindowingMode.PreferredLaunchViewSize;
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            Home.IsSelected = true;
        }

        private void NavBar_SelectionChanged(NavigationView sender, NavigationViewSelectionChangedEventArgs args)
        {
            var item = ((NavigationViewItem)sender.SelectedItem);

            var tag = item.Tag.ToString();

            RouteHistoryQueue.Enqueue(tag);

            ContentFrame.Navigate(Routes[tag]);
            
            HighLightByTag(tag);
        }

        private void NavBar_BackRequested(NavigationView sender, NavigationViewBackRequestedEventArgs args)
        {
            // Still buggy
            // TODO: Redo this
            if (ContentFrame.CanGoBack && RouteHistoryQueue.Count >= 2)
            {
                var currentViewItem = (NavigationViewItem)NavBar.SelectedItem;

                currentViewItem.IsSelected = false;

                var previousTextBlock = (TextBlock)currentViewItem.Content;

                previousTextBlock.Foreground = NotSelectedItemBrush;

                var previousTag = RouteHistoryQueue.Dequeue();

                var previousNvi = GetNavigationViewItemByTag(previousTag);

                if (previousNvi != null)
                {
                    var textBlock = (TextBlock)previousNvi.Content;

                    textBlock.Foreground = SelectedItemBrush;

                    previousNvi.IsSelected = true;
                }

                RouteHistoryQueue.Dequeue();
            }
            else return;
        }

        private NavigationViewItem GetNavigationViewItemByTag(string tag)
        {
            return (NavigationViewItem)NavBar.MenuItems.Where(i =>
           {
               var nvi = (NavigationViewItem)i;

               if (nvi.Tag == null)
                   return false;
               else
                   return nvi.Tag.ToString().Equals(tag);

           }).FirstOrDefault();
        }

        private void HighLightByTag(string tag)
        {
            foreach(var item in NavBar.MenuItems)
            {
                if (item is NavigationViewItem nvi)
                {
                    if (nvi.Tag == null) continue;

                    var nviTag = nvi.Tag.ToString();

                    if (string.IsNullOrEmpty(nviTag)) continue;

                    // Highlight the selected item
                    if (nviTag.Equals(tag))
                    {
                        if (nvi.Content is TextBlock textBlock)
                        {
                            textBlock.Foreground = SelectedItemBrush;
                        }
                    }
                    else // remove highlight away from not selected items
                    {
                        if (nvi.Content is TextBlock textBlock)
                        {
                            textBlock.Foreground = NotSelectedItemBrush;
                        }
                    }
                }
            }
        }
    }
}
