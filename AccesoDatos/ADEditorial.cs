using System;
using System.Collections.Generic;
using System.Text;
using System.Data.SqlClient;
using System.Data;
using Entidades;

namespace AccesoDatos
{
    public class ADEditorial
    {
        public string CadConec { get; set; }

        public ADEditorial()
        {
            CadConec = "";
        }

        public ADEditorial(string cad)
        {
            CadConec = cad;
        }
        /// <summary>
        /// Permite listar todas las editoriales
        /// </summary>
        /// <param name="condicion"></param>
        /// <returns></returns>
        public DataTable listarTodos(string condicion)
        {
            DataTable dt = new DataTable();
            SqlDataAdapter ad;
            SqlConnection conexion = new SqlConnection(CadConec);
            string sentencia = "Select * from Editorial";
            if (!string.IsNullOrEmpty(condicion))
            {
                sentencia = $"{sentencia} Where {condicion}";
            }
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
        /// <summary>
        /// Permite insertar una nueva editorial con un objeto entrante de tipo editorial
        /// </summary>
        /// <param name="edit"></param>
        /// <returns></returns>
        public int insertar(EEditorial edit)
        {
            int result = -1;
            string sentencia = "Insert into Editorial(claveEditorial, nombre)" +
                " values(@claveEdit,@nom)";

            SqlConnection conexion = new SqlConnection(CadConec);
            SqlCommand comando = new SqlCommand(sentencia, conexion);

            comando.Parameters.AddWithValue("@claveEdit", edit.ClaveEditorial);
            comando.Parameters.AddWithValue("@nom", edit.Nombre);
            try
            {
                conexion.Open();
                result = comando.ExecuteNonQuery();
                conexion.Close();
            }
            catch (Exception)
            {
                conexion.Close();
                throw new Exception("No se ha logrado insertar la editorial");
            }
            finally
            {
                conexion.Dispose();
                comando.Dispose();
            }

            return result;
        }
        /// <summary>
        /// Permite modificar una editorial a partir de un objeto editorial y
        /// la clave antigua de la editorial que se desea modificar
        /// </summary>
        /// <param name="edit"></param>
        /// <param name="claveVieja"></param>
        /// <returns></returns>
        public int modificar(EEditorial edit,string claveVieja)
        {
            int result = -1;
            string sentencia = "Update Editorial Set claveEditorial = @clave, nombre = @nom " +
                $"where claveEditorial = '{claveVieja}'";

            SqlConnection conexion = new SqlConnection(CadConec);
            SqlCommand comando = new SqlCommand(sentencia, conexion);

            comando.Parameters.AddWithValue("@clave", edit.ClaveEditorial);
            comando.Parameters.AddWithValue("@nom", edit.Nombre);
            try
            {
                conexion.Open();
                result = comando.ExecuteNonQuery();
                conexion.Close();
            }
            catch (Exception)
            {
                conexion.Close();
                throw new Exception("No se ha logrado modificar la editorial");
            }
            finally
            {
                conexion.Dispose();
                comando.Dispose();
            }

            return result;
        }
        /// <summary>
        /// Permite eliminar una editorial a partir de una condición
        /// </summary>
        /// <param name="condicion"></param>
        /// <returns></returns>
        public int eliminar(string condicion)
        {
            int result = -1;
            string sentencia = $"Delete from Editorial where {condicion}";

            SqlConnection conexion = new SqlConnection(CadConec);
            SqlCommand comando = new SqlCommand(sentencia, conexion);
            try
            {
                conexion.Open();
                result = comando.ExecuteNonQuery();
                conexion.Close();
            }
            catch (Exception)
            {
                conexion.Close();
                throw new Exception("No se ha logrado eliminar la editorial");
            }
            finally
            {
                conexion.Dispose();
                comando.Dispose();
            }

            return result;
        }
        /// <summary>
        /// Permite recuperar el nombre de una editorial a partir de una condición
        /// </summary>
        /// <param name="condicion"></param>
        /// <returns></returns>
        public string recuperarNombre(string condicion)
        {
            string result = string.Empty;
            SqlConnection conec = new SqlConnection(CadConec);
            string sentencia = $"Select nombre from Editorial where {condicion}";
            SqlDataReader reader;
            SqlCommand comando = new SqlCommand(sentencia, conec);
            try
            {
                conec.Open();
                reader = comando.ExecuteReader();
                reader.Read();
                result = reader[0].ToString();
                conec.Close();
            }
            catch (Exception)
            {
                conec.Close();
                throw new Exception("No se logró la conexión a datos");
            }
            finally
            {
                conec.Dispose();
                comando.Dispose();
            }
            return result;
        }
        /// <summary>
        /// Permite saber si la clave de uan editorial ya existe o si el 
        /// nombre de una eidtorial ya existe mediante uan condición
        /// </summary>
        /// <param name="condicion"></param>
        /// <returns></returns>
        public bool existeRegistro(string condicion)
        {
        bool result = false;
        object obEscalar;

        //1
        SqlCommand comando = new SqlCommand();
        SqlConnection conexion = new SqlConnection(CadConec);
            

        //2
        comando.CommandText = $"Select 1 from Editorial Where {condicion}";
        comando.Connection = conexion;

        //3
        try
        {
            conexion.Open();
            obEscalar = comando.ExecuteScalar();

            if (obEscalar != null)
                result = true;
            else
                result = false;

            conexion.Close();

        }
        catch (Exception)
        {
            conexion.Close();
            throw new Exception("Error buscando datos de Editorial");
        }
        //4
        finally
        {
            comando.Dispose();
            conexion.Dispose();
        }

        return result;
    }
    }
}
