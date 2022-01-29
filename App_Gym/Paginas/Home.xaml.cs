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

namespace App_Gym.Paginas
{
    /// <summary>
    /// Lógica de interacción para Home.xaml
    /// </summary>
    public partial class Home : Page
    {
        public Home()
        {
            InitializeComponent();
            MainWindow.actual_page = new Uri("Paginas/Home.xaml", UriKind.RelativeOrAbsolute);
        }

        private void btnClients_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(new System.Uri("Paginas/Menus/Clients.xaml", UriKind.RelativeOrAbsolute));
        }

        private void btnStatistics_Click(object sender, RoutedEventArgs e)
        {

        }

        private void btnArticles_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(new System.Uri("Paginas/Menus/Articles.xaml", UriKind.RelativeOrAbsolute));
        }

        private void btnMPayment_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(new System.Uri("Paginas/Menus/MPayment.xaml", UriKind.RelativeOrAbsolute));
        }

        private void btnSuplements_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(new System.Uri("Paginas/Menus/Supplements.xaml", UriKind.RelativeOrAbsolute));
        }

        private void btnPayments_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(new System.Uri("Paginas/Menus/Payments.xaml", UriKind.RelativeOrAbsolute));
        }

        private void btnCredits_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(new System.Uri("Paginas/Menus/Credits.xaml", UriKind.RelativeOrAbsolute));
        }
    }
}
