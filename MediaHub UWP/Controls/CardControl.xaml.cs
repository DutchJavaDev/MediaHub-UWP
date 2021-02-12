using MediaHub_UWP.Models;
using TMDbLib.Objects.General;
using TMDbLib.Objects.Search;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Input;

// The User Control item template is documented at https://go.microsoft.com/fwlink/?LinkId=234236

namespace MediaHub_UWP.Controls
{
    public sealed partial class CardControl : UserControl
    {
        public MediaType CardMediaType { get; private set; }
        public CardModel Model { get; private set; }

        private readonly SearchMovieTvBase CardItem;

        private readonly MenuFlyoutItem AddToList; // TODO
        private readonly MenuFlyoutItem AddToFavorite; // TODO
        private const string POSTER_BASE_URL = "https://image.tmdb.org/t/p/original/";

        public CardControl(SearchMovieTvBase cardItem)
        {
            InitializeComponent();
            CardItem = cardItem;
            
            CardMediaType = CardItem.MediaType;

            Model = new CardModel 
            {
                CardPosterPath = GetCardPosterPath(),
                CardTitle = GetCardTitle(),
                CardDate = GetCardDate()
            };

            AddToList = new MenuFlyoutItem 
            {
                Text = "Add to list",
            };

            AddToFavorite = new MenuFlyoutItem 
            {
                Text = "Favorite ❤️"
            };

            Menu.Items.Add(AddToList);
            Menu.Items.Add(AddToFavorite);
        }

        private string GetCardPosterPath()
        {
            switch (CardMediaType)
            {
                case MediaType.Movie:
                    return $"{POSTER_BASE_URL}{CardItem.BackdropPath}";

                case MediaType.Tv:
                    return $"{POSTER_BASE_URL}{CardItem.BackdropPath}";

                case MediaType.Person:
                    return "https://i.stack.imgur.com/6M513.png";

                case MediaType.Unknown:
                    return "https://i.stack.imgur.com/6M513.png";

                default: return "https://i.stack.imgur.com/6M513.png";
            }
        }

        private string GetCardTitle()
        {
            switch (CardMediaType)
            {
                case MediaType.Movie:
                    return ((SearchMovie) CardItem).Title;

                case MediaType.Tv:
                    return ((SearchTv) CardItem).OriginalName;

                case MediaType.Person:
                    return string.Empty;

                case MediaType.Unknown:
                    return string.Empty;

                default: return string.Empty;
            }
        }

        private string GetCardDate()
        {
            switch (CardMediaType)
            {
                case MediaType.Movie:
                    return ((SearchMovie) CardItem).ReleaseDate.Value.ToShortDateString() ?? "No date set";

                case MediaType.Tv:
                    return ((SearchTv)CardItem).FirstAirDate.Value.ToShortDateString() ?? "No date set";

                case MediaType.Person:
                    return string.Empty;

                case MediaType.Unknown:
                    return string.Empty;

                default: return string.Empty;
            }
        }

        private void Poster_PointerPressed(object sender, PointerRoutedEventArgs e)
        {
            
        }

        private void MenuFlyoutButton_Click(object sender, RoutedEventArgs e)
        {
            Menu.ShowAt(sender as UIElement, new FlyoutShowOptions
            {
                Placement = FlyoutPlacementMode.Right,
                ShowMode = FlyoutShowMode.Auto
            });
        }
    }
}
