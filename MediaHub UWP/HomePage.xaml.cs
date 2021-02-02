using MediaHub_UWP.Controls;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using System.Windows.Input;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI;
using Windows.UI.Popups;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Media.Imaging;
using Windows.UI.Xaml.Navigation;

// The Blank Page item template is documented at https://go.microsoft.com/fwlink/?LinkId=234238

namespace MediaHub_UWP
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class HomePage : Page
    {
        public HomePage()
        {
            InitializeComponent();
            
            CreateGrids();
        }

        public void CreateGrids()
        {
            var rand = new Random();
            
            for (var i = 0; i < 20; i++)
            {
                //var grid = new Grid
                //{
                //    Margin = new Thickness(8),
                //    Padding = new Thickness(0, 0, 0, 0),
                //    Background = new SolidColorBrush(Colors.Red)
                //};

                //grid.RowDefinitions.Add(new RowDefinition() { Height = GridLength.Auto });

                //var image = new BitmapImage(new Uri($"https://picsum.photos/300/300?random={rand.Next(1,100)}"));

                //grid.Children.Add(new Image { Source = image, Stretch = Stretch.None });

                //grid.PointerPressed += (e, s) => 
                //{
                //    grid.Children.Add(new TextBlock { Text = "Pressed" });
                //};

                WidgetGridView.Items.Add(new Widget());
            }
        }
    }
}
