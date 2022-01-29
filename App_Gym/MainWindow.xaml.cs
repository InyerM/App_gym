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
using System.Windows.Controls.Primitives;
using App_Gym.Clases;

namespace App_Gym
{
    /// <summary>
    /// Lógica de interacción para MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            service = fContainer.NavigationService;
        }

        public static NavigationService service;

        public static Uri actual_page { get; set; }
        public static Uri previous_page { get; set; }

        private void BG_PreviewMouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            Tg_Btn.IsChecked = false;
        }

        private void btnHome_Click(object sender, RoutedEventArgs e)
        {
            fContainer.Navigate(new System.Uri("Paginas/Home.xaml", UriKind.RelativeOrAbsolute));
        }
        private void btnHome_MouseEnter(object sender, MouseEventArgs e)
        {
            if (Tg_Btn.IsChecked == false)
            {
                Popup.PlacementTarget = btnHome;
                Popup.Placement = PlacementMode.Right;
                Popup.IsOpen = true;
                Header.PopupText.Text = "Inicio";
            }
        }

        private void btnHome_MouseLeave(object sender, MouseEventArgs e)
        {
            Popup.Visibility = Visibility.Collapsed;
            Popup.IsOpen = false;
        }

        private void btnSettings_MouseEnter(object sender, MouseEventArgs e)
        {
            if (Tg_Btn.IsChecked == false)
            {
                Popup.PlacementTarget = btnSettings;
                Popup.Placement = PlacementMode.Right;
                Popup.IsOpen = true;
                Header.PopupText.Text = "Opciones";
            }
        }

        private void btnSettings_MouseLeave(object sender, MouseEventArgs e)
        {
            Popup.Visibility = Visibility.Collapsed;
            Popup.IsOpen = false;
        }

        private void btnSettings_Click(object sender, RoutedEventArgs e)
        {
            fContainer.Navigate(new System.Uri("Paginas/Settings.xaml", UriKind.RelativeOrAbsolute));
        }

        private void btnSearch_MouseEnter(object sender, MouseEventArgs e)
        {
            if (Tg_Btn.IsChecked == false)
            {
                Popup.PlacementTarget = btnSearch;
                Popup.Placement = PlacementMode.Right;
                Popup.IsOpen = true;
                Header.PopupText.Text = "Mostrar";
            }
        }

        private void btnSearch_MouseLeave(object sender, MouseEventArgs e)
        {
            Popup.Visibility = Visibility.Collapsed;
            Popup.IsOpen = false;
        }

        private void btnSearch_Click(object sender, RoutedEventArgs e)
        {
            fContainer.Navigate(new System.Uri("Paginas/Search.xaml", UriKind.RelativeOrAbsolute));
        }

        // Start: Button Close | Restore | Minimize 
        private void btnClose_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }

        private void btnRestore_Click(object sender, RoutedEventArgs e)
        {
            if (WindowState == WindowState.Normal)
                WindowState = WindowState.Maximized;
            else
                WindowState = WindowState.Normal;
        }

        private void btnMinimize_Click(object sender, RoutedEventArgs e)
        {
            WindowState = WindowState.Minimized;
        }

        // End: Button Close | Restore | Minimize



        private void btnBack_Click(object sender, RoutedEventArgs e)
        {

            var uri1 = new Uri("Paginas/Menus/AddClient/addArticle.xaml", UriKind.RelativeOrAbsolute);
            var uri2 = new Uri("Paginas/Menus/AddClient/addClient.xaml", UriKind.RelativeOrAbsolute);
            var uri3 = new Uri("Paginas/Menus/AddClient/addSupplement.xaml", UriKind.RelativeOrAbsolute);
            var uri4 = new Uri("Paginas/Menus/AddClient/addMPayment.xaml", UriKind.RelativeOrAbsolute);
            var uri5 = new Uri("Paginas/Menus/AddClient/addPayment.xaml", UriKind.RelativeOrAbsolute);

            if (previous_page == uri1 || previous_page == uri2 || previous_page == uri3 || previous_page == uri4 || previous_page == uri5)
            {
                fContainer.Navigate(new System.Uri("Paginas/Home.xaml", UriKind.RelativeOrAbsolute));
                return;
            }


            if (service.CanGoBack)
            {
                service.GoBack();
            }
        }

        private void btnForward_Click(object sender, RoutedEventArgs e)
        {

            if (service.CanGoForward)
            {
                service.GoForward();
            }
        }
    }
}
