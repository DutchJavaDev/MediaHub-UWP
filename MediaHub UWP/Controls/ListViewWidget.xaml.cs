using System.Collections.ObjectModel;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Data;

// The User Control item template is documented at https://go.microsoft.com/fwlink/?LinkId=234236

namespace MediaHub_UWP.Controls
{
    public sealed partial class ListViewWidget : UserControl
    {
        public readonly Orientation OrientationView = Orientation.Horizontal;
        public string HeaderText { get; set; } = string.Empty;

        public ListViewWidget()
        {
            InitializeComponent();
        }

        public void SetItemSource<T>(ObservableCollection<T> collection)
        {
            View.SetBinding(ItemsControl.ItemsSourceProperty, new Binding { Source = collection });
        }

        private void ItemsStackPanel_Loaded(object sender, RoutedEventArgs e)
        {
            // I can only reach this like this -.-
            // Try View.FindName(name_of_ui_element) -> cast to ItemsStackPanel and try if it works :)
            var stackPanel = (ItemsStackPanel)sender;
            stackPanel.Orientation = OrientationView;
        }
    }
}
