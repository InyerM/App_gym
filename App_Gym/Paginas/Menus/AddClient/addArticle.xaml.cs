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
    public partial class addArticle : Page
    {
        public addArticle()
        {
            InitializeComponent();
            MainWindow.previous_page = MainWindow.actual_page;
            MainWindow.actual_page = new Uri("Paginas/Menus/AddClient/addArticle.xaml", UriKind.RelativeOrAbsolute);
            cargar();
        }


        public void cargar()
        {
            if (Articles.selection == "edit")
            {
                txtNombre.Text = Articles.items[0];
                txtPrecio.Text = Articles.items[1];
                txtTipo.Text = Articles.items[2];
            }
        }   
        
        public List<string> values = new List<string>();
        public CD con = new CD();

        private void btnAceptar_Click(object sender, RoutedEventArgs e)
        {
            if (txtNombre.Text == "" || txtPrecio.Text == "" || txtTipo.Text == ""){
                MessageBox.Show("Error al insertar, hay algunos espacios vacios");
                return;
            }
            else
            {
                values.Add(txtNombre.Text);
                values.Add(txtPrecio.Text);
                values.Add("ROPA");
                values.Add(txtTipo.Text);
            }


            if (Articles.selection == "edit")
            {
                try
                {
                    con.update("articulos", Articles.idSelected, values);
                    NavigationService.Navigate(new System.Uri("Paginas/Menus/Articles.xaml", UriKind.RelativeOrAbsolute));

                }
                catch (Exception ex)
                {
                    MessageBox.Show("Hubo un error: " + ex.Message);
                }
            }
            else if (Articles.selection == "add")
            {
                try
                {
                    con.insert("articulos", values);
                    NavigationService.Navigate(new System.Uri("Paginas/Menus/Articles.xaml", UriKind.RelativeOrAbsolute));

                }
                catch (Exception ex)
                {
                    MessageBox.Show("Hubo un error: " + ex.Message);
                }
            }
        }

        private void btnCancelar_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(new System.Uri("Paginas/Menus/Articles.xaml", UriKind.RelativeOrAbsolute));
        }
    }
}
