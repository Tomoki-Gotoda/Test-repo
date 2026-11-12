using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace HireMaster
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        /// <summary>
        /// 
        /// </summary>
        public MainWindow()
        {
            InitializeComponent();
            mainControl.Content = new HireScreen();
            btnHires.IsEnabled = false;

        }

        private void btnmembers_Click(object sender, RoutedEventArgs e)
        {
            mainControl.Content = new MemberScreen();
            btnmembers.IsEnabled = false;
            btnItems.IsEnabled = true;
            btnHires.IsEnabled = true;
        }

        private void btnItems_Click(object sender, RoutedEventArgs e)
        {
            mainControl.Content = new ItemScreen();
            
            btnmembers.IsEnabled = true;
            btnItems.IsEnabled = false;
            btnHires.IsEnabled = true;
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnHires_Click(object sender, RoutedEventArgs e)
        {
            mainControl.Content = new HireScreen();
           
            btnmembers.IsEnabled = true;
            btnItems.IsEnabled = true;
            btnHires.IsEnabled = false;
        }

        // Added new method here
    }
}