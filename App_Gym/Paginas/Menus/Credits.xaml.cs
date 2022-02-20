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
    /// Lógica de interacción para Credits.xaml
    /// </summary>
    public partial class Credits : Page
    {
        public Credits()
        {
            InitializeComponent();
            MainWindow.previous_page = MainWindow.actual_page;
            MainWindow.actual_page = new Uri("Paginas/Menus/Credits.xaml", UriKind.RelativeOrAbsolute);

            con.select("creditos");
            lista_creditos = con.l_cre;
            lvCredits.ItemsSource = lista_creditos;
        }


        public static List<Creditos> lista_creditos { get; set; } = new List<Creditos>();
        public CD con = new CD();
        public static int sel { get; set; }

        public static double idSelected { get; set; }
        public static string selection { get; set; }

        public static int sizeChanged { get; set; } = 1;

        public static int cont { get; set; } = 1;


        private void btnDeuda_Click(object sender, RoutedEventArgs e)
        {
            sel = lvCredits.SelectedIndex;

            if (sel == -1)
            {
                txtInfoCliente.Text = "Error: seleccione por favor una fila\n\n\n";
            }
            else
            {
                Creditos client = lista_creditos[sel];
                string [] subs = client.NombreCliente.Split(' ');
                string q = "SELECT SUM(DEUDA) "
                    + "FROM CREDITOS CRE "
                    + "INNER JOIN COMPRAS C ON CRE.ID = C.CODIGO_COMPRA "
                    + "INNER JOIN CLIENTE CL ON CL.CEDULA = C.CEDULA "
                    + "WHERE CL.CEDULA = " + client.Cedula + " ";

                txtInfoCliente.Text = "Cedula del cliente: " + client.Cedula + "\n"
                    + "Nombre del cliente: " + subs[0] + " " + subs[1] + "\n"
                    + "Deuda total: " + con.personalized_select(q);
            }
            
        }

        private void btnReset_Click(object sender, RoutedEventArgs e)
        {
            refresh();
        }

        private void refresh()
        {
            con.select("creditos");
            lista_creditos = con.l_cre;
            lvCredits.ItemsSource = null;
            lvCredits.ItemsSource = lista_creditos;
            NavigationService.Refresh();
        }

        private void btnDel_Click(object sender, RoutedEventArgs e)
        {
            sel = lvCredits.SelectedIndex;

            if (sel == -1)
            {
                MessageBox.Show("Error, no se ha seleccionado ninguna fila");
            }
            else
            {
                idSelected = lista_creditos[sel].CodigoCredito;
                con.delete("creditos", idSelected);
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

            string q = "SELECT CL.CEDULA, A.ID, convert(datetime, C.FECHA, 5), C.CODIGO_COMPRA, CL.NOMBRE, A.NOMBRE, A.PRECIO, CRE.CODIGO_CREDITO, CRE.DEUDA "
                        + "FROM ARTICULO A, COMPRAS C, CLIENTE CL, CREDITOS CRE "
                        + "WHERE A.ID = C.ID AND CL.CEDULA = C.CEDULA AND CRE.ID = C.CODIGO_COMPRA AND (CRE.CODIGO_CREDITO LIKE '%" + busqueda + "%' OR CL.NOMBRE LIKE '%" + busqueda + "%' OR C.FECHA LIKE '%" + busqueda + "%' OR A.NOMBRE LIKE '%" + busqueda + "%' OR CRE.DEUDA LIKE '%" + busqueda + "%') "
                        + "ORDER BY CRE.CODIGO_CREDITO DESC";

            con.s_query("creditos", q);
            lista_creditos = con.l_cre;
            lvCredits.ItemsSource = null;
            lvCredits.ItemsSource = lista_creditos;
        }
    }
}
