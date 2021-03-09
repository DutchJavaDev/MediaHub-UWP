using MediaHub_UWP.Controls;
using System;
using System.Collections.ObjectModel;
using TMDbLib.Objects.Discover;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Navigation;

// The Blank Page item template is documented at https://go.microsoft.com/fwlink/?LinkId=234238

namespace MediaHub_UWP.Pages
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class MoviesPage : Page
    {
        private readonly ObservableCollection<CardControl> MoviesCollection = new ObservableCollection<CardControl>();

        public MoviesPage()
        {
            InitializeComponent();
            NavigationCacheMode = NavigationCacheMode.Required;
        }

        private async void Page_Loading(FrameworkElement sender, object args)
        {
            using (var client = Helper.CreateClient())
            {
                var discover = new DiscoverMovie(client);

                var movies = discover.IncludeAdultMovies(false)
                                     .WhereVoteAverageIsAtLeast(1)
                                     .WherePrimaryReleaseDateIsBefore(new DateTime(2012, 1, 1));

                foreach (var movie in (await movies.Query(page: 1)).Results)
                    MoviesCollection.Add(new CardControl(movie));
            }
        }
    }
}
