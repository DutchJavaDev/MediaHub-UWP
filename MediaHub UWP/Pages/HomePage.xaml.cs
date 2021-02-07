using MediaHub_UWP.Controls;
using TMDbLib.Client;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Navigation;
using Windows.UI.Xaml.Controls;
using System.Collections.ObjectModel;
using TMDbLib.Objects.Search;
using System.Linq;
using System.Collections.Generic;
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
        private readonly ObservableCollection<MovieWidget> Movies = new ObservableCollection<MovieWidget>();
        private readonly ObservableCollection<ShowWidget> Tv = new ObservableCollection<ShowWidget>();
        private readonly ObservableCollection<MovieWidget> MoviesSugestions = new ObservableCollection<MovieWidget>();
        private readonly ObservableCollection<ShowWidget> TvSugestions = new ObservableCollection<ShowWidget>();

        public HomePage()
        {
            InitializeComponent();
            NavigationCacheMode = NavigationCacheMode.Required;
        }

        private async void Page_Loaded(object sender, RoutedEventArgs e)
        {
            await LoadMovieCollection(LoadAction.Movie);
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
                                Movies.Add(new MovieWidget(item));
                        }
                        break;

                    case LoadAction.MovieSuggestion:
                        {
                            // Bases of of The Age of Adaline (2015)
                            var collection = (await client.GetMovieRecommendationsAsync(293863, page: 1)).Results.Select(i => (SearchMovieTvBase)i).ToList();

                            foreach (var item in collection)
                                MoviesSugestions.Add(new MovieWidget(item));
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
                                Tv.Add(new ShowWidget(item));
                        }
                        break;

                    case LoadAction.TvSuggestion:
                        {
                            // Based off the best show over The Man in the High Castle (2015)
                            var collection = (await client.GetTvShowRecommendationsAsync(62017,page: 1)).Results.Select(i => (SearchMovieTvBase)i).ToList();

                            foreach (var item in collection)
                                TvSugestions.Add(new ShowWidget(item));
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
