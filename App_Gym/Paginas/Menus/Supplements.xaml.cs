using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
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
using App_Gym.Clases;
using App_Gym.Paginas.Menus.AddClient;

namespace App_Gym.Paginas.Menus
{
    /// <summary>
    /// Lógica de interacción para Supplements.xaml
    /// </summary>
    public partial class Supplements : Page
    {
        public Supplements()
        {
            InitializeComponent();
            MainWindow.previous_page = MainWindow.actual_page;
            MainWindow.actual_page = new Uri("Paginas/Menus/Supplements.xaml", UriKind.RelativeOrAbsolute);

            con.select("suplementos");
            lista_suplementos = con.l_s;
            lvSupplements.ItemsSource = lista_suplementos;

        }

        public static List<Articulos> lista_suplementos { get; set; } = new List<Articulos>();

        public Conexion con = new Conexion();
        public static int sel { get; set; }

        public static double idSelected { get; set; }

        public static string selection { get; set; }

        public static int sizeChanged { get; set; } = 1;

        public static int cont { get; set; } = 1;


        private void refresh()
        {
            con.select("suplementos");
            lista_suplementos = con.l_a;
            lvSupplements.ItemsSource = null;
            lvSupplements.ItemsSource = lista_suplementos;
            NavigationService.Refresh();
        }

        private void btnEdit_Click(object sender, RoutedEventArgs e)
        {
            selection = "edit";

            sel = lvSupplements.SelectedIndex;
            if (sel == -1)
            {
                MessageBox.Show("Error, no se ha seleccionado ninguna fila");
            }
            else
            {
                idSelected = lista_suplementos[sel].ArticulosId;
                NavigationService.Navigate(new System.Uri("Paginas/Menus/AddClient/addSupplement.xaml", UriKind.RelativeOrAbsolute));
            }
        }

        private void btnReset_Click(object sender, RoutedEventArgs e)
        {
            refresh();
        }

        private void btnAdd_Click(object sender, RoutedEventArgs e)
        {
            selection = "add";
            NavigationService.Navigate(new System.Uri("Paginas/Menus/AddClient/addSupplement.xaml", UriKind.RelativeOrAbsolute));
        }

        private void btnDel_Click(object sender, RoutedEventArgs e)
        {
            sel = lvSupplements.SelectedIndex;

            if (sel == -1)
            {
                MessageBox.Show("Error, no se ha seleccionado ninguna fila");
            }
            else
            {
                idSelected = lista_suplementos[sel].ArticulosId;
                con.delete("articulos", idSelected);
            }

            refresh();
        }

        private void Page_SizeChanged(object sender, SizeChangedEventArgs e)
        {
            var n = e.NewSize;
            var p = e.PreviousSize;

            if (p == Size.Parse("0,0") && n != Size.Parse("900, 550"))
            {
                sizeChanged = 1;
            }
            if (p == Size.Parse("0,0") && n == Size.Parse("900, 550"))
            {
                sizeChanged = 2;
            }

            if (sizeChanged == 1)
            {
                gd.Height = 500;
                sizeChanged = 2;
            }
            else if (sizeChanged == 2)
            {
                gd.Height = 400;
                sizeChanged = 1;
            }
        }

        Regex rgx;
        MatchCollection mtch;
        private void btnSearch_Click(object sender, RoutedEventArgs e)
        {
            TextBox txt = txtSearch.Content as TextBox;
            string busqueda = txt.Text;

            string p_cedula = @"\d";
            string q;

            rgx = new Regex(p_cedula);
            mtch = rgx.Matches(busqueda);

            if (mtch.Count > 0)
            {
                q = "SELECT * FROM ARTICULO WHERE ID LIKE '%" + busqueda + "%' AND TIPO = 'SUPLEMENTOS'";
            }
            else
            {
                q = "SELECT * FROM ARTICULO WHERE NOMBRE LIKE '%" + busqueda + "%' AND TIPO = 'SUPLEMENTOS'";
            }

            con.s_query("suplementos", q);
            lista_suplementos = con.l_s;
            lvSupplements.ItemsSource = null;
            lvSupplements.ItemsSource = lista_suplementos;
        }
    }
}
