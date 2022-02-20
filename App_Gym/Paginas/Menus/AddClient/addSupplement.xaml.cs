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
    /// Lógica de interacción para addSupplement.xaml
    /// </summary>
    public partial class addSupplement : Page
    {
        public addSupplement()
        {
            InitializeComponent();

            MainWindow.previous_page = MainWindow.actual_page;
            MainWindow.actual_page = new Uri("Paginas/Menus/AddClient/addSupplement.xaml", UriKind.RelativeOrAbsolute);
            cargar();
        }

        public void cargar()
        {
            if (Supplements.selection == "edit")
            {
                txtNombre.Text = Supplements.items[0];
                txtPrecio.Text = Supplements.items[1];
                txtTipo.Text = Supplements.items[2];
            }
        }

        public List<string> values = new List<string>();
        public CD con = new CD();

        private void btnAceptar_Click(object sender, RoutedEventArgs e)
        {

            if (txtNombre.Text == "" || txtPrecio.Text == "" || txtTipo.Text == "")
            {
                MessageBox.Show("Error al insertar, hay algunos espacios vacios");
                return;
            }
            else
            {
                values.Add(txtNombre.Text);
                values.Add(txtPrecio.Text);
                values.Add("SUPLEMENTOS");
                values.Add(txtTipo.Text);
            }


            if (Supplements.selection == "edit")
            {
                try
                {
                    con.update("articulos", Supplements.idSelected, values);
                    NavigationService.Navigate(new System.Uri("Paginas/Menus/Supplements.xaml", UriKind.RelativeOrAbsolute));

                }
                catch (Exception ex)
                {
                    MessageBox.Show("Hubo un error: " + ex.Message);
                }
            }
            else if (Supplements.selection == "add")
            {
                try
                {
                    con.insert("articulos", values);
                    NavigationService.Navigate(new System.Uri("Paginas/Menus/Supplements.xaml", UriKind.RelativeOrAbsolute));

                }
                catch (Exception ex)
                {
                    MessageBox.Show("Hubo un error: " + ex.Message);
                }
            }
        }

        private void btnCancelar_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(new System.Uri("Paginas/Menus/Supplements.xaml", UriKind.RelativeOrAbsolute));
        }
    }
}
