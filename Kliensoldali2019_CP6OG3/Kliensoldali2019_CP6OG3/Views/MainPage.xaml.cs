using System;
using System.Diagnostics;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Input;

namespace Kliensoldali2019_CP6OG3.Views
{
    public sealed partial class MainPage : Page
    {
        public MainPage()
        {
            InitializeComponent();
            NavigationCacheMode = Windows.UI.Xaml.Navigation.NavigationCacheMode.Enabled;
        }

        private void btnTranslate_Click(object sender, Windows.UI.Xaml.RoutedEventArgs e)
        {
            ViewModel.Translate();
        }

        private void ComboBox_SelectionChanged_Source(object sender, SelectionChangedEventArgs e)
        {
            var cb = (ComboBox)sender;
            Debug.WriteLine(cb.SelectedValue);
        }

        private void ComboBox_SelectionChanged_Dest(object sender, SelectionChangedEventArgs e)
        {
            var cb = (ComboBox)sender;
            Debug.WriteLine(cb.SelectedValue);
        }
    }
}