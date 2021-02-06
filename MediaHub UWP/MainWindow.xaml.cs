using MediaHub_UWP.Controls;
using System.Drawing;
using System.Threading.Tasks;
using TMDbLib.Client;
using Windows.UI.ViewManagement;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;


// The Blank Page item template is documented at https://go.microsoft.com/fwlink/?LinkId=234238

namespace MediaHub_UWP
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class MainWindow : Page
    {
        private readonly TMDbClient Client;
        public static MainWindow Instance;

        public MainWindow()
        {
            MaximizeWindowOnLoad();
            InitializeComponent();
            Instance = this;
            Client = Helper.CreateClient();
        }

        private static void MaximizeWindowOnLoad()
        {
            // Get how big the window can be in epx.
            var bounds = ApplicationView.GetForCurrentView().VisibleBounds;
            ApplicationView.PreferredLaunchViewSize = new Windows.Foundation.Size(bounds.Width, bounds.Height);
            ApplicationView.PreferredLaunchWindowingMode = ApplicationViewWindowingMode.PreferredLaunchViewSize;
        }

        public async void LoadPopular()
        {
            var popularMovies = await Client.GetMoviePopularListAsync(page: 1);
            //var populoarShows = await Client.GetTvShowPopularAsync(page: 1);

            //var movie = await Client.GetMovieAsync(47964, extraMethods: TMDbLib.Objects.Movies.MovieMethods.Images);

            //PopularList.Items.Add(new Widget(movie.Title, movie.ReleaseDate.HasValue ? movie.ReleaseDate.Value.ToShortDateString() : "nan", movie.BackdropPath));

            //MainGrid
            foreach (var movie in popularMovies.Results)
            {
                PopularList.Items.Add(new Widget(movie.Title, movie.ReleaseDate.HasValue ? movie.ReleaseDate.Value.ToShortDateString() : "nan", movie.BackdropPath));
            }

            //foreach (var show in populoarShows.Results)
            //{
            //    Bingo.Items.Add(new Widget(title: show.Name, year: show.FirstAirDate.ToString(), path: show.PosterPath));
            //}
        }

        private void Index_Loaded(object sender, Windows.UI.Xaml.RoutedEventArgs e)
        {
            LoadPopular();
        }
    }
}
