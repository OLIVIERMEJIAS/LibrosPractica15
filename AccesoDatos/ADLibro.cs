using Entidades;
using System;
using System.Collections.Generic;
using System.Text;
using System.Data.SqlClient;

namespace AccesoDatos
{
    public class ADLibro
    {
        private string cadConexion;
        private string mensaje;

        public string Mensaje { get; }

        #region Constructores
        public ADLibro(string cadena) {
            cadConexion = cadena;
            mensaje = string.Empty;
        }
        public ADLibro()
        {
            cadConexion = string.Empty;
            mensaje = string.Empty;
        }
        #endregion

        #region Métodos
        public bool libroRepetido(ELibro libro) {
            bool result=false;
            string sentencia;
            sentencia = $"Select 1 From Libro Where titulo={libro.Titulo} and claveAutor={libro.ClaveAutor}";

            //1 Crear Objetos de Datos de ADO.NET
            SqlCommand comandoSQL = new SqlCommand();
            SqlConnection conexionSQL = new SqlConnection(cadConexion);
            SqlDataReader datos; //No se instancia


            //2 Configurar el Objeto de DATOS
            comandoSQL.Connection = conexionSQL;
            comandoSQL.CommandText = sentencia;

            //3 Abrir Conexión / Ejecutar Comando / Recupar Datos 
            try
            {
                conexionSQL.Open();
                datos = comandoSQL.ExecuteReader();
                conexionSQL.Close();
                //if (datos.HasRows)
                //    result = true;
                //else
                //    result = false;

                result = datos.HasRows ? true : false;
            }
            catch (Exception)
            {
                throw new Exception("Se ha presentado un error realizando la consulta");
            }
            //4 Limpiar Memoria
            finally
            {
                comandoSQL.Dispose();
                conexionSQL.Dispose();
            }

            return result;
        }

        public bool claveLibroRepetida(string clave) {
            bool result=false;
            object obEscalar;

            //1
            SqlCommand comando = new SqlCommand();
            SqlConnection conexion = new SqlConnection(cadConexion);
            SqlDataReader datos;//NO SE INSTANCIA!!!

            //2
            comando.CommandText = "Select 1 from Libro Where claveLibro=@claveLibro";
            comando.Parameters.AddWithValue("@claveLibro", clave);
            comando.Connection = conexion;

            //3
            try
            {
                conexion.Open();
                obEscalar = comando.ExecuteScalar();
                conexion.Close();

                if (obEscalar != null)
                    result = true;
                else
                    result = false;
            }
            catch (Exception)
            {
                throw new Exception("Error buscando la clave del libro");
            }
            //4
            finally {
                comando.Dispose();
                conexion.Dispose();
            }

            return result;
        }
        #endregion
    }
}
