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
    /// Lógica de interacción para Articles.xaml
    /// </summary>
    public partial class MPayment : Page
    {
        public MPayment()
        {
            InitializeComponent();
            MainWindow.previous_page = MainWindow.actual_page;
            MainWindow.actual_page = new Uri("Paginas/Menus/MPayment.xaml", UriKind.RelativeOrAbsolute);

            con.select("mensualidades");
            lista_m = con.l_m;
            lvMPayments.ItemsSource = lista_m;
        }

        public static List<Mensualidades> lista_m { get; set; } = new List<Mensualidades>();

        public Conexion con = new Conexion();
        public static int sel { get; set; }

        public static double idSelected { get; set; }

        public static string selection { get; set; }

        public static int sizeChanged { get; set; } = 1;

        public static int cont { get; set; } = 1;


        private void refresh()
        {
            con.select("mensualidades");
            lista_m = con.l_m;
            lvMPayments.ItemsSource = null;
            lvMPayments.ItemsSource = lista_m;
            NavigationService.Refresh();
        }

        private void btnEdit_Click(object sender, RoutedEventArgs e)
        {
            selection = "edit";

            sel = lvMPayments.SelectedIndex;
            if (sel == -1)
            {
                MessageBox.Show("Error, no se ha seleccionado ninguna fila");
            }
            else
            {
                idSelected = lista_m[sel].NroRegistro;
                NavigationService.Navigate(new System.Uri("Paginas/Menus/AddClient/addMPayment.xaml", UriKind.RelativeOrAbsolute));
            }
        }

        private void btnReset_Click(object sender, RoutedEventArgs e)
        {
            refresh();
        }

        private void btnAdd_Click(object sender, RoutedEventArgs e)
        {
            selection = "add";
            NavigationService.Navigate(new System.Uri("Paginas/Menus/AddClient/addMPayment.xaml", UriKind.RelativeOrAbsolute));
        }

        private void btnDel_Click(object sender, RoutedEventArgs e)
        {
            sel = lvMPayments.SelectedIndex;

            if (sel == -1)
            {
                MessageBox.Show("Error, no se ha seleccionado ninguna fila");
            }
            else
            {
                idSelected = lista_m[sel].NroRegistro;
                con.delete("mensualidades", idSelected);
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
                q = "SELECT CL.CEDULA, CL.NOMBRE, M.NROREGISTRO, TO_DATE(M.FECHA_INICIO, 'DD/MM/YY'), TO_DATE(M.FECHA_FIN, 'DD/MM/YY'), M.COSTO "
                        + "FROM MENSUALIDAD M INNER JOIN CLIENTE CL ON CL.CEDULA = M.CEDULA WHERE M.NROREGISTRO LIKE '%" + busqueda + "%' "
                        + "ORDER BY NROREGISTRO DESC ";
            }
            else
            {
                q = "SELECT CL.CEDULA, CL.NOMBRE, M.NROREGISTRO, TO_DATE(M.FECHA_INICIO, 'DD/MM/YY'), TO_DATE(M.FECHA_FIN, 'DD/MM/YY'), M.COSTO "
                        + "FROM MENSUALIDAD M INNER JOIN CLIENTE CL ON CL.CEDULA = M.CEDULA WHERE CL.NOMBRE LIKE '%" + busqueda + "%' "
                        + "ORDER BY NROREGISTRO DESC ";
            }

            con.s_query("mensualidades", q);
            lista_m = con.l_m;
            lvMPayments.ItemsSource = null;
            lvMPayments.ItemsSource = lista_m;
        }
    }
}
