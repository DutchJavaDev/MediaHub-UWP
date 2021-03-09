using MediaHub_UWP.Controls;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Navigation;
using Windows.UI.Xaml.Controls;
using System.Collections.ObjectModel;
using TMDbLib.Objects.Search;
using System.Linq;
using System.Threading.Tasks;
using System.Security.Cryptography;
using System.Collections.Generic;
using System;
using MediaHub_UWP;

// The Blank Page item template is documented at https://go.microsoft.com/fwlink/?LinkId=234238

namespace MediaHub_UWP.Pages
{
    /// <summary>
    /// Random adding to "Cards"
    /// Show shows and movies mixed 
    /// 
    /// </summary>
    public sealed partial class HomePage : Page
    {
        private readonly ObservableCollection<CardControl> Cards = new ObservableCollection<CardControl>();

        public HomePage()
        {
            InitializeComponent();
            NavigationCacheMode = NavigationCacheMode.Required;
        }

        private async void Page_Loaded(object sender, RoutedEventArgs e)
        {
            // WhenAll ?? ->> A drawback here would be an exception handling because when something goes wrong you will get an AggregatedException with possibly multiple exceptions,
            // Meh

            var items = new List<SearchMovieTvBase>();

            using (var client = Helper.CreateClient())
            {
                var collection = (await client.GetMoviePopularListAsync(page: 1, language: client.DefaultLanguage, region: client.DefaultCountry)).Results.Select(i => (SearchMovieTvBase)i).ToList();
                items.AddRange(collection);
                
                collection = (await client.GetTvShowPopularAsync(page: 1, language: client.DefaultLanguage)).Results.Select(i => (SearchMovieTvBase)i).ToList();
                items.AddRange(collection);
            }

            items = items.Shuffle();

            for (var i = 0; i < items.Count; i++)
            {
                Cards.Add(new CardControl(items[i]));
            }
        }

        private void Page_Unloaded(object sender, RoutedEventArgs e)
        {
        }
    }

    
}
