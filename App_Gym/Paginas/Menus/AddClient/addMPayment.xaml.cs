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
    /// Lógica de interacción para addArticle.xaml
    /// </summary>
    public partial class addMPayment : Page
    {
        public addMPayment()
        {
            InitializeComponent();
            MainWindow.previous_page = MainWindow.actual_page;
            MainWindow.actual_page = new Uri("Paginas/Menus/AddClient/addMPayment.xaml", UriKind.RelativeOrAbsolute);
            cargar();

        }

        public void cargar()
        {
            if (MPayment.selection == "edit")
            {
                txtId.Text = MPayment.items[0];
                txtCosto.Text = MPayment.items[1];
                dateInicio.Text = MPayment.items[2];
                dateFin.Text = MPayment.items[3];
            }
        }

        public List<string> values = new List<string>();
        public CD con = new CD();

        private void btnAceptar_Click(object sender, RoutedEventArgs e)
        {

            if (txtCosto.Text == "" || txtId.Text == "" || dateInicio.Text == "" || dateFin.Text == "")
            {
                MessageBox.Show("Error al insertar, hay algunos espacios vacios");
                return;
            }
            else
            {
                values.Add(dateInicio.Text);
                values.Add(dateFin.Text); 
                values.Add(txtCosto.Text);
                values.Add(txtId.Text);

            }


            if (MPayment.selection == "edit")
            {
                try
                {
                    con.update("mensualidades", MPayment.idSelected, values);
                    NavigationService.Navigate(new System.Uri("Paginas/Menus/MPayment.xaml", UriKind.RelativeOrAbsolute));

                }
                catch (Exception ex)
                {
                    MessageBox.Show("Hubo un error: " + ex.Message);
                }
            }
            else if (MPayment.selection == "add")
            {
                try
                {
                    con.insert("mensualidades", values);
                    NavigationService.Navigate(new System.Uri("Paginas/Menus/MPayment.xaml", UriKind.RelativeOrAbsolute));

                }
                catch (Exception ex)
                {
                    MessageBox.Show("Hubo un error: " + ex.Message);
                }
            }
        }

        private void btnCancelar_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(new System.Uri("Paginas/Menus/MPayment.xaml", UriKind.RelativeOrAbsolute));
        }
    }
}
