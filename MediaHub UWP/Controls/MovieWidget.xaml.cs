using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using TMDbLib.Objects.Search;
using Windows.ApplicationModel.Resources;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Media.Imaging;
using Windows.UI.Xaml.Navigation;
using Windows.UI.Xaml.Shapes;

// The User Control item template is documented at https://go.microsoft.com/fwlink/?LinkId=234236

namespace MediaHub_UWP.Controls
{
    public sealed partial class MovieWidget : UserControl
    {
        private readonly SearchMovie Model;

        private string PostPath { get; set; }

        public MovieWidget(string title, string year, string path)
        {
            InitializeComponent();
            Title.Text = title;
            Year.Text = year;
            Image.Source = new BitmapImage(new Uri($@"https://image.tmdb.org/t/p/original/{path}"));
            PointerPressed += (e, s) => ShowInfo();
        }

        public MovieWidget(SearchMovieTvBase movie)
        {
            InitializeComponent();
            Model = (SearchMovie) movie;
            PostPath = $@"https://image.tmdb.org/t/p/original/{Model.BackdropPath}";
            PointerPressed += (e, s) => ShowInfo();
        }

        private async void ShowInfo()
        {
            var contentDialog = new ContentDialog 
            {
                Content = new MovieInfo(),
                Background = new SolidColorBrush(Colors.Transparent)
            };

            await contentDialog.ShowAsync(ContentDialogPlacement.Popup);
        }
    }
}
