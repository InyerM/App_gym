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
using App_Gym.Clases;
using App_Gym.Paginas.Menus;

namespace App_Gym.Paginas.Menus.AddClient
{
    /// <summary>
    /// Lógica de interacción para addPayments.xaml
    /// </summary>
    public partial class addPayment : Page
    {
        public addPayment()
        {
            InitializeComponent();
            MainWindow.previous_page = MainWindow.actual_page;
            MainWindow.actual_page = new Uri("Paginas/Menus/AddClient/addPayment.xaml", UriKind.RelativeOrAbsolute);
            cargar();

        }


        public void cargar()
        {
            if (Payments.selection == "edit")
            {
                txtCedula.Text = Payments.items[0];
                txtIdArticulo.Text = Payments.items[1];
                dateCompra.Text = Payments.items[2];
            }
        }


        public List<string> values = new List<string>();
        public List<string> items = new List<string>();
        public CD con = new CD();
        string idArticulo;
        double costo;
        double deuda;
        double abono;

        public bool addCredit = false;

        private void btnAceptar_Click(object sender, RoutedEventArgs e)
        {
            if (txtCedula.Text == "" || txtIdArticulo.Text == "" || dateCompra.Text == "")
            {
                MessageBox.Show("Error al insertar, hay algunos espacios vacios");
                return;
            }
            else
            {
                values.Add(txtCedula.Text);
                values.Add(txtIdArticulo.Text);
                values.Add(dateCompra.Text);
            }

            if (Payments.selection == "edit")
            {
                try
                {

                    con.update("compras", Payments.idSelected, values);
                    if (addCredit)
                    {
                        con.update("creditos", Payments.idSelected, items);
                        MessageBox.Show("xsds");
                    }
                    NavigationService.Navigate(new System.Uri("Paginas/Menus/Payments.xaml", UriKind.RelativeOrAbsolute));

                }
                catch (Exception ex)
                {
                    MessageBox.Show("Hubo un error " + ex.Message);
                    MessageBox.Show("xddd");
                }

            }
            else if (Payments.selection == "add")
            {
                try
                {
                    con.insert("compras", values);
                    if (addCredit)
                    {
                        con.insert("creditos", items);
                    }
                    NavigationService.Navigate(new System.Uri("Paginas/Menus/Payments.xaml", UriKind.RelativeOrAbsolute));

                }
                catch (Exception ex)
                {
                    MessageBox.Show("Hubo un error " + ex.Message);
                }
            }
        }

        private void btnCancelar_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(new System.Uri("Paginas/Menus/Payments.xaml", UriKind.RelativeOrAbsolute));
        }

        private void btnDialogAceptar_Click(object sender, RoutedEventArgs e)
        {
            idArticulo = txtIdArticulo.Text;
            items.Clear();
            try
            {
                costo = con.p_query("SELECT PRECIO FROM ARTICULO WHERE ID =  " + idArticulo);
                deuda = costo - double.Parse(txtAbono.Text);
                txtDeuda.Text = "La deuda es de: " + (deuda);
                items.Add(deuda.ToString());
            }
            catch (Exception ex)
            {
                MessageBox.Show("No ha ingresado un valor en el costo");
                deuda = costo;
                items.Add(deuda.ToString());
            }
            
        }

        private void tg_Checked(object sender, RoutedEventArgs e)
        {
            addCredit = true;
        }

        private void tg_Unchecked(object sender, RoutedEventArgs e)
        {
            addCredit = false;
        }
    }
}
