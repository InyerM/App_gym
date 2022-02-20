using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;
using System.Data;

namespace App_Gym.Clases
{
    public class ConectionServer
    {

        public SqlConnection Conexion;

        public SqlConnection AbrirConexion()
        {
            cargar();
            if (Conexion.State == ConnectionState.Closed)
                Conexion.Open();
            return Conexion;
        }
        public SqlConnection CerrarConexion()
        {
            cargar();
            if (Conexion.State == ConnectionState.Open)
                Conexion.Close();
            return Conexion;
        }

        public void cargar()
        {
            Conexion = new SqlConnection("conection-string"); //enter your connection string
        }
    }
}
