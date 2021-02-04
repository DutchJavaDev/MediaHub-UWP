using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.ApplicationModel.Resources;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Media.Imaging;
using Windows.UI.Xaml.Navigation;

// The User Control item template is documented at https://go.microsoft.com/fwlink/?LinkId=234236

namespace MediaHub_UWP.Controls
{
    public sealed partial class Widget : UserControl
    {
        public static Random Random = new Random(1337);

        public Widget()
        {
            InitializeComponent();
            Image.Source = new BitmapImage(new Uri($"https://picsum.photos/300/300?random={Random.Next(1,100)}"));
        }

        public Widget(string title, string year, string path)
        {
            InitializeComponent();
            Title.Text = title;
            Year.Text = year;
            Image.Source = new BitmapImage(new Uri($@"https://image.tmdb.org/t/p/original/{path}"));
        }
    }
}
