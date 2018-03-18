using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace MinecraftOC
{
    /// <summary>
    /// Interakční logika pro GameOver.xaml
    /// </summary>
    public partial class GameOver : Page
    {
        public GameOver()
        {
            InitializeComponent();
        }

        private void QuitButton_Click(object sender, RoutedEventArgs e)
        {
            Application.Current.Shutdown();
        }

        private void ContinueButton_Click(object sender, RoutedEventArgs e)
        {
            cont_frame.Source = new Uri("GamePage.xaml", UriKind.Relative);
            cont_frame.Visibility = Visibility.Visible;
        }
    }
}
