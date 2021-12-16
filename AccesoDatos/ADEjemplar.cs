using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using System.Data.SqlClient;

namespace AccesoDatos
{
    public class ADEjemplar
    {
        public string CadConec { get; set; }

        public ADEjemplar()
        {
            CadConec = "";
        }

        public ADEjemplar(string cad)
        {
            CadConec = cad;
        }

        public bool existenEjemplares(string condicion)
        {
            bool result = false;
            object obEscalar;
            SqlCommand comando = new SqlCommand();
            SqlConnection conexion = new SqlConnection(CadConec);
            comando.CommandText = $"Select 1 from Ejemplar Where claveEditorial = {condicion}";
            comando.Connection = conexion;
            try
            {
                conexion.Open();
                obEscalar = comando.ExecuteScalar();

                if (obEscalar != null)
                    result = true;

                conexion.Close();

            }
            catch (Exception)
            {
                conexion.Close();
                throw new Exception("Error en conexión a datos");
            }
            finally
            {
                comando.Dispose();
                conexion.Dispose();
            }

            return result;
        }

        public DataTable listarTodos(string condicion)
        {
            DataTable dt = new DataTable();
            SqlDataAdapter ad;
            SqlConnection conexion = new SqlConnection(CadConec);
            string sentencia = "Select l.titulo as Titulo, c.descripcion as Condicion, " +
                               "e.descripcion as Estado, ej.edicion as Edicion, ed.nombre as Editorial, "+
                               "ej.numeroPaginas as Paginas from Ejemplar ej inner "+
                                "join Editorial ed On ej.claveEditorial = ed.claveEditorial inner "+
                                "join Libro l On l.claveLibro = ej.claveLibro inner join Condicion c On "+
                                "c.claveCondicion = ej.claveCondicion inner join Estado e On " +
                                $"ej.claveEstado = e.claveEstado Where ej.claveEditorial = {condicion}";
            try
            {
                ad = new SqlDataAdapter(sentencia, conexion);
                ad.Fill(dt);
            }
            catch (Exception)
            {

                throw new Exception("Se ha presentado algún error");
            }
            finally
            {
                conexion.Dispose();
            }
            return dt;
        }

    }
}
