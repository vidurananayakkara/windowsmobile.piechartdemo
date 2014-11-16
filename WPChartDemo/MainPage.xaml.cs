using System;
using System.Windows;

namespace WPChartDemo
{
    public partial class MainPage
    {
        public MainPage()
        {
            InitializeComponent();
            DataContext = App.ViewModel;
        }

        private void buttonPie_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(new Uri(@"/PieSeriesChart.xaml", UriKind.RelativeOrAbsolute));
        }
    }
}