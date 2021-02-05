using MediaHub_UWP.Controls;
using System.Threading.Tasks;
using TMDbLib.Client;
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

        public MainWindow()
        {
            InitializeComponent();

            Client = Helper.CreateClient();
            
        }

        public async void LoadPopular()
        {
            var popularMovies = await Client.GetMoviePopularListAsync(page: 1);

            foreach (var movie in popularMovies.Results)
            {
                PopularList.Items.Add(new Widget(movie.Title, movie.ReleaseDate.HasValue ? movie.ReleaseDate.Value.ToShortDateString() : "nan", movie.BackdropPath));
            }
        }

        public void CreateGrids()
        {
            //for (var i = 0; i < 20; i++)
            //{
            //    PopularList.Items.Add(new Widget());
            //    TrendingList.Items.Add(new Widget());
            //    FreeToWatchList.Items.Add(new Widget());
            //}
        }

        private void Index_Loaded(object sender, Windows.UI.Xaml.RoutedEventArgs e)
        {
            LoadPopular();
        }
    }
}
