using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.OracleClient;
using System.Windows;

namespace App_Gym.Clases
{
    public class Conexion
    {

        OracleConnection connection = new OracleConnection("DATA SOURCE = XE; PASSWORD = PFINAL; USER ID = PFINAL;");
        OracleCommand command;
        OracleDataAdapter adapter = new OracleDataAdapter();

        public List<Clientes> l_c = new List<Clientes>();
        public List<Articulos> l_a = new List<Articulos>();
        public List<Creditos> l_cre = new List<Creditos>();
        public List<Mensualidades> l_m = new List<Mensualidades>();
        public List<Compras> l_p = new List<Compras>();
        public List<Articulos> l_s = new List<Articulos>();

        string query;

        public void insert(string table, List<string> values)
        {

            connection.Open();
            
            if(table == "clientes")
            {
                query = "INSERT INTO CLIENTE (CEDULA, NOMBRE, TELEFONO, OCUPACION) VALUES ('" + values[0] + "','" + values[1] + "','" + values[2] + "','" + values[3] + "')";
            }
            if(table == "articulos")
            {
                query = "INSERT INTO ARTICULO (ID, NOMBRE, PRECIO, TIPO, SUBTIPO) VALUES (SEQ_IDARTICULO.NEXTVAL,'" + values[0] + "','" + values[1] + "','" + values[2]  + "','" + values[3] + "')";
            }
            if(table == "creditos")
            {
                query = "INSERT INTO CREDITOS (CODIGO_CREDITO, ID, DEUDA) VALUES (SEQ_CODIGO_CREDITO.NEXTVAL, SEQ_CODIGO_COMPRA.CURRVAL,'" + values[0] + "')";
            }
            if(table == "mensualidades")
            {
                query = "INSERT INTO MENSUALIDAD (NROREGISTRO, FECHA_INICIO, FECHA_FIN, COSTO, CEDULA) VALUES (SEQ_MENSUALIDADES.NEXTVAL,'" + values[0] + "','" + values[1] + "','" + values[2] + "','" + values[3] + "')";
            }
            if(table == "compras")
            {
                query = "INSERT INTO COMPRAS (CODIGO_COMPRA, CEDULA, ID, FECHA) VALUES (SEQ_CODIGO_COMPRA.NEXTVAL,'" + values[0] + "','" + values[1] + "','" + values[2] + "')";
            }

            try
            {
                command = new OracleCommand(query, connection);
                adapter.InsertCommand = command;
                adapter.InsertCommand.ExecuteNonQuery();
                MessageBox.Show("Inserción realizada con éxito");
            }catch(Exception ex)
            {
                MessageBox.Show("Hubo un error, la cédula que ingresó no se encuentra en la base de datos");
            }
            finally
            {
                connection.Close();
            }
        }

        public void select(string table)
        {
            if (table == "clientes")
            {
                query = "SELECT * FROM CLIENTE ORDER BY NOMBRE";
                this.s_query("clientes", query);
            }
            if (table == "articulos")
            {
                query = "SELECT * FROM ARTICULO WHERE TIPO = 'ROPA' ORDER BY SUBTIPO";
                this.s_query("articulos", query);
            }
            if (table == "compras")
            {
                query = "SELECT C.CEDULA, C.ID, TO_DATE(C.FECHA, 'DD/MM/YY'), C.CODIGO_COMPRA, CL.NOMBRE, A.NOMBRE, A.PRECIO "
                        + "FROM CLIENTE CL "
                        + "INNER JOIN COMPRAS C ON CL.CEDULA = C.CEDULA "
                        + "INNER JOIN ARTICULO A ON C.ID = A.ID "
                        + "ORDER BY CODIGO_COMPRA DESC";
                this.s_query("compras", query);
            }
            if (table == "creditos")
            {
                query = "SELECT        CL.CEDULA, A.ID, TO_DATE(C.FECHA, 'DD/MM/YY'), C.CODIGO_COMPRA, CL.NOMBRE, A.NOMBRE, A.PRECIO, CRE.CODIGO_CREDITO, CRE.DEUDA "
                        + "FROM            ARTICULO A, COMPRAS C, CLIENTE CL, CREDITOS CRE "
                        + "WHERE        A.ID = C.ID AND CL.CEDULA = C.CEDULA AND CRE.ID = C.CODIGO_COMPRA "
                        + "ORDER BY CRE.CODIGO_CREDITO DESC";
                this.s_query("creditos", query);
            }
            if (table == "mensualidades")
            {
                query = "SELECT CL.CEDULA, CL.NOMBRE, M.NROREGISTRO, TO_DATE(M.FECHA_INICIO, 'DD/MM/YY'), TO_DATE(M.FECHA_FIN, 'DD/MM/YY'), M.COSTO "
                        + "FROM MENSUALIDAD M INNER JOIN CLIENTE CL ON CL.CEDULA = M.CEDULA "
                        + "ORDER BY NROREGISTRO DESC ";
                this.s_query("mensualidades", query);
            }
            if (table == "suplementos")
            {
                query = "SELECT * FROM ARTICULO WHERE TIPO = 'SUPLEMENTOS' ORDER BY SUBTIPO";
                this.s_query("suplementos", query);
            }
        }

        public void update(string table, double id, List<string> values)
        {
            connection.Open();

            if(table == "clientes")
            {
                query = "UPDATE CLIENTE SET CEDULA = " + values[0] + ", NOMBRE = '" + values[1] + "', " 
                    + "TELEFONO = " + values[2] + ", OCUPACION = '" + values[3] + "' "
                    + "WHERE CEDULA = " + id;
            }
            if(table == "articulos")
            {
                query = "UPDATE ARTICULO SET NOMBRE = '" + values[0] + "', " 
                    + "PRECIO = " + values[1] + ", TIPO = '" + values[2] + "', SUBTIPO = '" + values[3] + "' "
                    + "WHERE ID = " + id;
            }
            if(table == "creditos")
            {
                query = "UPDATE CREDITOS SET DEUDA = '" + values[0] + "'" 
                    + "WHERE ID = " + id;
            }
            if (table == "mensualidades")
            {
                query = "UPDATE MENSUALIDAD SET FECHA_INICIO = '" + values[0] + "', FECHA_FIN = '" + values[1] + "', "
                    + "COSTO = '" + values[2] + "', CEDULA = '" + values[3] + "' "
                    + "WHERE NROREGISTRO = " + id;
            }if (table == "compras")
            {
                query = "UPDATE COMPRAS SET CEDULA = '" + values[0] + "', "
                    + "ID = '" + values[1] + "', FECHA = '" + values[2] + "' "
                    + "WHERE CODIGO_COMPRA = " + id;
            }


            try
            {
                command = new OracleCommand(query, connection);
                adapter.InsertCommand = command;
                adapter.InsertCommand.ExecuteNonQuery();
                MessageBox.Show("Elemento editado con éxito");
            }
            catch (Exception ex)
            {
                System.Windows.MessageBox.Show("Hubo un error, está tratando de modificar un dato que tiene registros en otras tablas");
            }
            finally
            {
                connection.Close();
            }
        }

        public void delete(string table, double id)
        {
            connection.Open();

 
            if (table == "clientes")
            {
                query = "DELETE FROM CLIENTE WHERE CEDULA = " + id;
            }
            if (table == "articulos")
            {
                query = "DELETE FROM ARTICULO WHERE ID = " + id;
            }
            if (table == "creditos")
            {
                query = "DELETE FROM CREDITOS WHERE CODIGO_CREDITO = " + id;
            }
            if (table == "mensualidades")
            {
                query = "DELETE FROM MENSUALIDAD WHERE NROREGISTRO = " + id;
            }
            if (table == "compras")
            {
                query = "DELETE FROM COMPRAS WHERE CODIGO_COMPRA = " + id;
            }

            try
            {
                command = new OracleCommand(query, connection);
                command.CommandType = System.Data.CommandType.StoredProcedure;
                command.ExecuteNonQuery();
                MessageBox.Show("Elemento eliminado con éxito");
            }
            catch (Exception ex)
            {
                System.Windows.MessageBox.Show("Hubo un error, está tratando de eliminar un dato que tiene registros en otras tablas");
            }
            finally
            {
                connection.Close();
            }
        }

        public double personalized_select(string q)
        {
            connection.Open();

            double deuda_total = 0;
            
            command = new OracleCommand(q, connection);
            command.CommandType = System.Data.CommandType.Text;
            OracleDataReader dr = command.ExecuteReader();
            while (dr.Read())
            {
                deuda_total = double.Parse(dr.GetValue(0).ToString());
            }

            connection.Close();
            return deuda_total;
        }

        public double p_query(string q)
        {
            connection.Open();
            double n = 0;

            try
            {
                command = new OracleCommand(q, connection);
                command.CommandType = System.Data.CommandType.Text;
                OracleDataReader dr = command.ExecuteReader();
                while (dr.Read())
                {
                    n = double.Parse(dr.GetValue(0).ToString());
                }
            }
            catch (Exception ex)
            {
                //////////////
            }
            finally
            {
                connection.Close ();
            }
            return n;
        }

        public void s_query(string table, string q)
        {
            connection.Open();

            if(table == "articulos")
            {
                l_a.Clear();
                try
                {
                    command = new OracleCommand(q, connection);
                    command.CommandType = System.Data.CommandType.Text;
                    OracleDataReader dr = command.ExecuteReader();
                    while (dr.Read())
                    {
                        double id = double.Parse(dr.GetValue(0).ToString());
                        string nombre = dr.GetValue(1).ToString();
                        double precio = double.Parse(dr.GetValue(2).ToString());
                        string tipo = dr.GetValue(3).ToString();
                        string subtipo = dr.GetValue(4).ToString();
                        l_a.Add(new Articulos() { ArticulosId = id, ArticulosNombre = nombre, ArticulosPrecio = precio, ArticulosTipo = tipo, ArticulosSubtipo = subtipo });
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("No se encontraron coincidencias ");
                }
            }
            if(table == "clientes")
            {
                l_c.Clear();
                try
                {
                    command = new OracleCommand(q, connection);
                    command.CommandType = System.Data.CommandType.Text;
                    OracleDataReader dr = command.ExecuteReader();
                    while (dr.Read())
                    {
                        double cedula = double.Parse(dr.GetValue(0).ToString());
                        string nombre = dr.GetValue(1).ToString();
                        double telefono = double.Parse(dr.GetValue(2).ToString());
                        string ocupacion = dr.GetValue(3).ToString();
                        l_c.Add(new Clientes() { Identificacion = cedula, Nombre = nombre, Telefono = telefono, Ocupacion = ocupacion });
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("No se encontraron coincidencias");
                }
            }
            if(table == "creditos")
            {
                l_cre.Clear();
                try
                {
                    command = new OracleCommand(q, connection);
                    command.CommandType = System.Data.CommandType.Text;
                    OracleDataReader dr = command.ExecuteReader();
                    while (dr.Read())
                    {
                        double cedula = double.Parse(dr.GetValue(0).ToString());
                        double id = double.Parse(dr.GetValue(1).ToString());
                        string fecha = dr.GetValue(2).ToString();
                        double codigo_compra = double.Parse(dr.GetValue(3).ToString());
                        string nombrecl = dr.GetValue(4).ToString();
                        string nombrea = dr.GetValue(5).ToString();
                        double precio = double.Parse(dr.GetValue(6).ToString());
                        double codigo_credito = double.Parse(dr.GetValue(7).ToString());
                        double deuda = double.Parse(dr.GetValue(8).ToString());

                        l_cre.Add(new Creditos() { Cedula = cedula, IdArticulo = id, Fecha = fecha.Substring(0, 10), CodigoCompra = codigo_compra, NombreCliente = nombrecl, NombreArticulo = nombrea, Costo = precio, CodigoCredito = codigo_credito, Deuda = deuda, Abono = (precio - deuda) });
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("No se encontraron coincidencias");
                }
            }
            if (table == "mensualidades")
            {
                l_m.Clear();
                try
                {
                    command = new OracleCommand(q, connection);
                    command.CommandType = System.Data.CommandType.Text;
                    OracleDataReader dr = command.ExecuteReader();
                    while (dr.Read())
                    {
                        double cedula = double.Parse(dr.GetValue(0).ToString());
                        string nombrecl = dr.GetValue(1).ToString();
                        double nroregistro = double.Parse(dr.GetValue(2).ToString());
                        string fecha_inicio = dr.GetValue(3).ToString();
                        string fecha_fin = dr.GetValue(4).ToString();
                        double costo = double.Parse(dr.GetValue(5).ToString());

                        l_m.Add(new Mensualidades() { Cedula = cedula, NombreCliente = nombrecl, NroRegistro = nroregistro, FechaInicio = fecha_inicio.Substring(0, 10), FechaFin = fecha_fin.Substring(0, 10), Costo = costo });
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("No se encontraron coincidencias");
                }
            }
            if (table == "compras")
            {
                l_p.Clear();
                try
                {
                    command = new OracleCommand(q, connection);
                    command.CommandType = System.Data.CommandType.Text;
                    OracleDataReader dr = command.ExecuteReader();
                    while (dr.Read())
                    {
                        double cedula = double.Parse(dr.GetValue(0).ToString());
                        double id = double.Parse(dr.GetValue(1).ToString());
                        string fecha = dr.GetValue(2).ToString();
                        double codigo_compra = double.Parse(dr.GetValue(3).ToString());
                        string nombrecl = dr.GetValue(4).ToString();
                        string nombrea = dr.GetValue(5).ToString();
                        double precio = double.Parse(dr.GetValue(6).ToString());

                        l_p.Add(new Compras() { Cedula = cedula, IdArticulo = id, Fecha = fecha.Substring(0, 10), CodigoCompra = codigo_compra, NombreCliente = nombrecl, NombreArticulo = nombrea, Costo = precio });
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("No se encontraron coincidencias");
                }
            }
            if (table == "suplementos")
            {
                l_s.Clear();
                try
                {
                    command = new OracleCommand(q, connection);
                    command.CommandType = System.Data.CommandType.Text;
                    OracleDataReader dr = command.ExecuteReader();
                    while (dr.Read())
                    {
                        double id = double.Parse(dr.GetValue(0).ToString());
                        string nombre = dr.GetValue(1).ToString();
                        double precio = double.Parse(dr.GetValue(2).ToString());
                        string tipo = dr.GetValue(3).ToString();
                        string subtipo = dr.GetValue(4).ToString();
                        l_s.Add(new Articulos() { ArticulosId = id, ArticulosNombre = nombre, ArticulosPrecio = precio, ArticulosTipo = tipo, ArticulosSubtipo = subtipo });
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("No se encontraron coincidencias");
                }
            }

            connection.Close();
        }

    }
}
