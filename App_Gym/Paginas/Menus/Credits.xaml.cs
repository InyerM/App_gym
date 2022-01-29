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
        public Conexion con = new Conexion();
        public static int sel { get; set; }

        public static string selection { get; set; }

        public static int sizeChanged { get; set; } = 1;

        public static int cont { get; set; } = 1;


        private void btnDeuda_Click(object sender, RoutedEventArgs e)
        {
            sel = lvCredits.SelectedIndex;

            if (sel == -1)
            {
                MessageBox.Show("Error, no se ha seleccionado ninguna fila");
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
            lvCredits.ItemsSource = null;
            lvCredits.ItemsSource = lista_creditos;
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
                lista_creditos.RemoveAt(sel);
                MessageBox.Show("Se ha eliminado el artículo correctamente");
            }

            lvCredits.ItemsSource = null;
            lvCredits.ItemsSource = lista_creditos;
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
                q = "SELECT        CL.CEDULA, A.ID, TO_DATE(C.FECHA, 'DD/MM/YY'), C.CODIGO_COMPRA, CL.NOMBRE, A.NOMBRE, A.PRECIO, CRE.CODIGO_CREDITO, CRE.DEUDA "
                        + "FROM            ARTICULO A, COMPRAS C, CLIENTE CL, CREDITOS CRE "
                        + "WHERE        A.ID = C.ID AND CL.CEDULA = C.CEDULA AND CRE.ID = C.CODIGO_COMPRA AND C.CODIGO_CREDITO LIKE '%" + busqueda + "%' "
                        + "ORDER BY CRE.CODIGO_CREDITO DESC";
            }
            else
            {
                q = "SELECT        CL.CEDULA, A.ID, TO_DATE(C.FECHA, 'DD/MM/YY'), C.CODIGO_COMPRA, CL.NOMBRE, A.NOMBRE, A.PRECIO, CRE.CODIGO_CREDITO, CRE.DEUDA "
                        + "FROM            ARTICULO A, COMPRAS C, CLIENTE CL, CREDITOS CRE "
                        + "WHERE        A.ID = C.ID AND CL.CEDULA = C.CEDULA AND CRE.ID = C.CODIGO_COMPRA AND CL.NOMBRE LIKE '%" + busqueda + "%' "
                        + "ORDER BY CRE.CODIGO_CREDITO DESC";
            }

            con.s_query("creditos", q);
            lista_creditos = con.l_cre;
            lvCredits.ItemsSource = null;
            lvCredits.ItemsSource = lista_creditos;
        }
    }
}
