using MediaHub_UWP.Controls;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Navigation;
using Windows.UI.Xaml.Controls;
using System.Collections.ObjectModel;
using TMDbLib.Objects.Search;
using System.Linq;
using System.Threading.Tasks;

// The Blank Page item template is documented at https://go.microsoft.com/fwlink/?LinkId=234238

namespace MediaHub_UWP.Pages
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    /// 

    public enum LoadAction
    {
        Movie,
        Tv,

        // Suggestion should be based offed movies/shows you liked like on netflix
        MovieSuggestion,
        TvSuggestion
    }

    public sealed partial class HomePage : Page
    {
        private readonly ObservableCollection<MovieWidget> ObservableCollectionForMovies = new ObservableCollection<MovieWidget>();
        private readonly ObservableCollection<ShowWidget> ObservableCollectionForTvShows = new ObservableCollection<ShowWidget>();
        private readonly ObservableCollection<MovieWidget> ObservableCollectionForMoviesSugestions = new ObservableCollection<MovieWidget>();
        private readonly ObservableCollection<ShowWidget> ObservableCollectionForTvSugestions = new ObservableCollection<ShowWidget>();

        private readonly ListViewWidget PopularMovies = new ListViewWidget
        {
            HeaderText = "Popular Movies"
        };

        private readonly ListViewWidget MoviesSuggestions = new ListViewWidget
        {
            HeaderText = "Movies Suggestions"
        };

        private readonly ListViewWidget PopularShows = new ListViewWidget 
        {
            HeaderText = "Popular Shows"
        };

        private readonly ListViewWidget ShowsSuggestions = new ListViewWidget 
        {
            HeaderText = "Shows Suggestion"
        };

        public HomePage()
        {
            InitializeComponent();
            NavigationCacheMode = NavigationCacheMode.Required;

            // Bind the collections to the lists
            PopularMovies.SetItemSource(ObservableCollectionForMovies);
            MoviesSuggestions.SetItemSource(ObservableCollectionForMoviesSugestions);
            PopularShows.SetItemSource(ObservableCollectionForTvShows);
            ShowsSuggestions.SetItemSource(ObservableCollectionForTvSugestions);

            // Add them to the stackpanel
            PageContents.Children.Add(PopularMovies);
            PageContents.Children.Add(MoviesSuggestions);
            PageContents.Children.Add(PopularShows);
            PageContents.Children.Add(ShowsSuggestions);
        }

        private async void Page_Loaded(object sender, RoutedEventArgs e)
        {
            await LoadMovieCollection(LoadAction.Movie);
            await LoadTvCollection(LoadAction.Tv);
            await LoadMovieCollection(LoadAction.MovieSuggestion);
            await LoadTvCollection(LoadAction.TvSuggestion);
        }

        private async Task LoadMovieCollection(LoadAction action)
        {
            using (var client = Helper.CreateClient())
            {
                switch (action)
                {
                    case LoadAction.Movie:
                        {
                            var collection = (await client.GetMoviePopularListAsync(page: 1)).Results.Select(i => (SearchMovieTvBase)i).ToList();

                            foreach (var item in collection)
                                ObservableCollectionForMovies.Add(new MovieWidget(item));
                        }
                        break;

                    case LoadAction.MovieSuggestion:
                        {
                            // Bases of of The Age of Adaline (2015)
                            var collection = (await client.GetMovieRecommendationsAsync(293863, page: 1)).Results.Select(i => (SearchMovieTvBase)i).ToList();

                            foreach (var item in collection)
                                ObservableCollectionForMoviesSugestions.Add(new MovieWidget(item));
                        }
                        break;

                    default:
                        break;
                }
            }
        }

        private async Task LoadTvCollection(LoadAction action)
        {
            using (var client = Helper.CreateClient())
            {
                switch (action)
                {
                    case LoadAction.Tv:
                        {
                            var collection = (await client.GetTvShowPopularAsync(page: 1)).Results.Select(i => (SearchMovieTvBase)i).ToList();

                            foreach (var item in collection)
                                ObservableCollectionForTvShows.Add(new ShowWidget(item));
                        }
                        break;

                    case LoadAction.TvSuggestion:
                        {
                            // Based off the best show over The Man in the High Castle (2015)
                            var collection = (await client.GetTvShowRecommendationsAsync(62017,page: 1)).Results.Select(i => (SearchMovieTvBase)i).ToList();

                            foreach (var item in collection)
                                ObservableCollectionForTvSugestions.Add(new ShowWidget(item));
                        }
                        break;

                    default:
                        break;
                }
            }
        }

        private void Page_Unloaded(object sender, RoutedEventArgs e)
        {
        }
    }
}
