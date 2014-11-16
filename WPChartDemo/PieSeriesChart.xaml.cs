namespace WPChartDemo
{
    public partial class PieSeriesChart
    {
        public PieSeriesChart()
        {
            InitializeComponent();
            ModelValues.DataContext = App.ViewModel.Models;
        }
    }
}