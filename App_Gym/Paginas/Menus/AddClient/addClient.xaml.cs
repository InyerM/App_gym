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

        }
        public List<string> values = new List<string>();
        public Conexion con = new Conexion();

        private void btnAceptar_Click(object sender, RoutedEventArgs e)
        {


            try
            {
                TextBox txt = txtIdentificacion.Content as TextBox;
                values.Add(txt.Text);
                txt = txtNombre.Content as TextBox;
                values.Add(txt.Text);
                txt = txtTelefono.Content as TextBox;
                values.Add(txt.Text);
                txt = txtOcupacion.Content as TextBox;
                values.Add(txt.Text);

            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al insertar " + ex.Message);
                NavigationService.Navigate(new System.Uri("Paginas/Menus/Clients.xaml", UriKind.RelativeOrAbsolute));
                return;
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
