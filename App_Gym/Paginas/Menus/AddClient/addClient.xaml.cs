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

namespace App_Gym.Paginas.Menus.AddClient
{
    /// <summary>
    /// Lógica de interacción para addClient.xaml
    /// </summary>
    public partial class addClient : Page
    {
        public addClient()
        {
            InitializeComponent();

            MainWindow.previous_page = MainWindow.actual_page;
            MainWindow.actual_page = new Uri("Paginas/Menus/AddClient/addClient.xaml", UriKind.RelativeOrAbsolute);
            cargar();

        }
        public List<string> values = new List<string>();
        public CD con = new CD();

        public void cargar()
        {
            if (Clients.selection == "edit")
            {
                txtIdentificacion.Text = Clients.items[0];
                txtNombre.Text = Clients.items[1];
                txtTelefono.Text = Clients.items[2];
                txtOcupacion.Text = Clients.items[3];
            }
        }

        private void btnAceptar_Click(object sender, RoutedEventArgs e)
        {


            if (txtNombre.Text == "" || txtIdentificacion.Text == "" || txtOcupacion.Text == "" || txtTelefono.Text == "") 
            {
                MessageBox.Show("Error al insertar, hay algunos espacios vacios");
                return;
            }
            else
            {
                values.Add(txtIdentificacion.Text);
                values.Add(txtNombre.Text);
                values.Add(txtTelefono.Text);
                values.Add(txtOcupacion.Text);
            }

            if(Clients.selection == "edit")
            {
                try
                {
                    con.update("clientes", Clients.idSelected, values);
                    NavigationService.Navigate(new System.Uri("Paginas/Menus/Clients.xaml", UriKind.RelativeOrAbsolute));

                }
                catch (Exception ex)
                {
                    MessageBox.Show("Hubo un error: " + ex.Message);
                }
            }
            else if (Clients.selection == "add")
            {
                try
                {
                    con.insert("clientes", values);
                    NavigationService.Navigate(new System.Uri("Paginas/Menus/Clients.xaml", UriKind.RelativeOrAbsolute));

                }
                catch (Exception ex)
                {
                    MessageBox.Show("Hubo un error: " + ex.Message);
                }
            }
            
            

        }

        private void btnCancelar_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(new System.Uri("Paginas/Menus/Clients.xaml", UriKind.RelativeOrAbsolute));
        }
    }
}
