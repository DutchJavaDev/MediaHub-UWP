using MediaHub_UWP.Controls;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using TMDbLib.Objects.Discover;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;

// The Blank Page item template is documented at https://go.microsoft.com/fwlink/?LinkId=234238

namespace MediaHub_UWP.Pages
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class ShowsPage : Page
    {
        private readonly ObservableCollection<CardControl> TvCollection = new ObservableCollection<CardControl>();

        public ShowsPage()
        {
            InitializeComponent();
            NavigationCacheMode = NavigationCacheMode.Required;
        }

        private async void Page_Loading(FrameworkElement sender, object args)
        {
            using (var client = Helper.CreateClient())
            {
                var discover = new DiscoverTv(client);

                var tv = discover.WhereVoteAverageIsAtLeast(1)
                                 .WhereAirDateIsBefore(new DateTime(2012, 1, 1));

                foreach (var show in (await tv.Query(page: 1)).Results)
                    TvCollection.Add(new CardControl(show));
            }
        }
    }
}
