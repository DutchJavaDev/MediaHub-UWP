using MediaHub_UWP.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using TMDbLib.Objects.General;
using TMDbLib.Objects.Search;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;

// The User Control item template is documented at https://go.microsoft.com/fwlink/?LinkId=234236

namespace MediaHub_UWP.Controls
{
    public sealed partial class CardControl : UserControl
    {
        public MediaType CardMediaType { get; private set; }
        public CardModel Model { get; private set; }

        private readonly SearchMovieTvBase CardItem;

        private readonly MenuFlyout SubMenu; // TODO
        private readonly MenuFlyoutItem AddToList; // TODO
        private readonly MenuFlyoutItem AddToFavorite; // TODO

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
        }

        private string GetCardPosterPath()
        {
            return string.Empty; // TODO
        }

        private string GetCardTitle()
        {
            return string.Empty; // TODO
        }

        private string GetCardDate()
        {
            return string.Empty; // TODO
        }

        private void Poster_PointerPressed(object sender, PointerRoutedEventArgs e)
        {
            // TODO
        }

        private void MenuFlyoutButton_PointerPressed(object sender, PointerRoutedEventArgs e)
        {
            // TODO
        }
    }
}
