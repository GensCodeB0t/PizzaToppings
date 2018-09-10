using System.Windows;

namespace PizzaToppings
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            getToppings();
        }

        private void EXIT_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void Refresh_Click(object sender, RoutedEventArgs e)
        {
            getToppings();

        }

        /// <summary>
        /// Creates a dictionary by way of a call to the Pizza static class and
        /// binds the results to the dgTopping DataGrid
        /// </summary>
        private void getToppings()
        {
            // Bind dicitionary to dgToppings DataGrid
            dgToppings.ItemsSource = Pizza.GetToppings();
            dgToppings.Items.Refresh();
        }

    }
}
