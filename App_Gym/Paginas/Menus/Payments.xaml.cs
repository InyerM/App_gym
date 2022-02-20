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
    /// Lógica de interacción para Payment.xaml
    /// </summary>
    public partial class Payments : Page
    {
        public Payments()
        {
            InitializeComponent();
            MainWindow.previous_page = MainWindow.actual_page;
            MainWindow.actual_page = new Uri("Paginas/Menus/Payments.xaml", UriKind.RelativeOrAbsolute);

            con.select("compras");
            lista_compras = con.l_p;

            lvPayments.ItemsSource = lista_compras;
        }


        public CD con = new CD();
        public static List<Compras> lista_compras { get; set; } = new List<Compras>();

        public static List<string> items { get; set; } = new List<string>();
        public static int sel { get; set; }

        public static double idSelected { get; set; }
        public static string selection { get; set; }

        public static int sizeChanged { get; set; } = 1;

        public static int cont { get; set; } = 1;



        private void refresh()
        {
            con.select("compras");
            lista_compras = con.l_p;
            lvPayments.ItemsSource = null;
            lvPayments.ItemsSource = lista_compras;
            NavigationService.Refresh();
        }
        private void btnEdit_Click(object sender, RoutedEventArgs e)
        {
            selection = "edit";
            items.Clear();

            sel = lvPayments.SelectedIndex;
            if (sel == -1)
            {
                MessageBox.Show("Error, no se ha seleccionado ninguna fila");
            }
            else
            {
                items.Add(lista_compras[sel].Cedula.ToString());
                items.Add(lista_compras[sel].IdArticulo.ToString());
                items.Add(lista_compras[sel].Fecha.ToString());
                idSelected = lista_compras[sel].CodigoCompra;
                NavigationService.Navigate(new System.Uri("Paginas/Menus/AddClient/addPayment.xaml", UriKind.RelativeOrAbsolute));
            }
        }

        private void btnReset_Click(object sender, RoutedEventArgs e)
        {
            refresh();
        }

        private void btnAdd_Click(object sender, RoutedEventArgs e)
        {
            selection = "add";
            NavigationService.Navigate(new System.Uri("Paginas/Menus/AddClient/addPayment.xaml", UriKind.RelativeOrAbsolute));
        }

        private void btnDel_Click(object sender, RoutedEventArgs e)
        {
            sel = lvPayments.SelectedIndex;

            if (sel == -1)
            {
                MessageBox.Show("Error, no se ha seleccionado ninguna fila");
            }
            else
            {
                idSelected = lista_compras[sel].CodigoCompra;
                con.delete("compras", idSelected);
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

        private void btnSearch_Click(object sender, RoutedEventArgs e)
        {
            TextBox txt = txtSearch.Content as TextBox;
            string busqueda = txt.Text;

            string q = "SELECT C.CEDULA, C.ID, TO_DATE(C.FECHA, 'DD/MM/YY'), C.CODIGO_COMPRA, CL.NOMBRE, A.NOMBRE, A.PRECIO "
                        + "FROM CLIENTE CL "
                        + "INNER JOIN COMPRAS C ON CL.CEDULA = C.CEDULA "
                        + "INNER JOIN ARTICULO A ON C.ID = A.ID WHERE CL.NOMBRE LIKE '%" + busqueda + "%' OR C.CODIGO_COMPRA LIKE '%" + busqueda + "%' OR A.NOMBRE LIKE '%" + busqueda + "%' OR C.FECHA LIKE '%" + busqueda + "%' OR A.PRECIO LIKE '%" + busqueda + "%' "
                        + "ORDER BY CODIGO_COMPRA DESC";

            con.s_query("compras", q);
            lista_compras = con.l_p;
            lvPayments.ItemsSource = null;
            lvPayments.ItemsSource = lista_compras;
        }
    }
}
