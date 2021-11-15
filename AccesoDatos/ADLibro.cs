using Entidades;
using System;
using System.Collections.Generic;
using System.Text;
using System.Data.SqlClient;
using System.Data;

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
            sentencia = $"Select 1 From Libro Where titulo='{libro.Titulo}' and claveAutor='{libro.ClaveAutor}'";

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
                result = datos.HasRows ? true : false;
                conexionSQL.Close();
                //if (datos.HasRows)
                //    result = true;
                //else
                //    result = false;
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
                
                if (obEscalar != null)
                    result = true;
                else
                    result = false;
                
                conexion.Close();

            }
            catch (Exception)
            {
                conexion.Close();
                throw new Exception("Error buscando la clave del libro");
            }
            //4
            finally {
                comando.Dispose();
                conexion.Dispose();
            }

            return result;
        }

        public int insertar(ELibro libro) {
            int result = -1;
            string sentencia = "Insert into Libro(claveLibro, titulo, claveAutor, claveCategoria)" +
                " values(@claveLibro,@titulo,@claveAutor,@claveCategoria)";

            SqlConnection conexion = new SqlConnection(cadConexion);
            SqlCommand comando = new SqlCommand(sentencia, conexion);

            comando.Parameters.AddWithValue("@claveLibro", libro.ClaveLibro);
            comando.Parameters.AddWithValue("@titulo", libro.Titulo);
            comando.Parameters.AddWithValue("@claveAutor", libro.ClaveAutor);
            comando.Parameters.AddWithValue("@claveCategoria", libro.Clavecategoria.ClaveCategoria);

            try
            {
                conexion.Open();
                result = comando.ExecuteNonQuery();
                conexion.Close();
            }
            catch (Exception)
            {
                conexion.Close();
                throw new Exception("No se ha logrado insertar el libro");
            }
            finally {
                conexion.Dispose();
                comando.Dispose();
            }

            return result;
        }

        public DataSet listarTodos(string condicion="") {
            DataSet setLibros = new DataSet();
            string sentencia = "Select claveLibro, titulo, claveAutor, claveCategoria from Libro";

            if (!string.IsNullOrEmpty(condicion))
                sentencia = string.Format("{0} where {1}", sentencia, condicion);

            //sentencia = $"{sentencia} where {condicion}";

            SqlConnection conexion = new SqlConnection(cadConexion);
            SqlDataAdapter adaptador;

            try
            {
                adaptador = new SqlDataAdapter(sentencia, conexion);
                adaptador.Fill(setLibros);

                adaptador.Dispose();
            }
            catch (Exception)
            {
                throw new Exception("Ha ocurrido algo!!!! ;-( ");
            }
            finally {
                conexion.Dispose();
            }

            return setLibros;
        }

        //Otro List<>

        public ELibro buscarRegistro(string condicion) {
            ELibro libro = new ELibro();

            string sentencia = "Select claveLibro, titulo, claveAutor, claveCategoria From Libro";

            sentencia = $"{sentencia} where {condicion}";


            SqlConnection conexion = new SqlConnection(cadConexion);
            SqlCommand comando = new SqlCommand(sentencia,conexion);
            SqlDataReader dato;

            try
            {
                conexion.Open();
                dato = comando.ExecuteReader();
                if (dato.HasRows)
                {
                    dato.Read();//
                    libro.ClaveLibro = dato.GetString(0);
                    libro.Titulo = dato.GetString(1);
                    libro.ClaveAutor = dato.GetString(2);
                    libro.Clavecategoria.ClaveCategoria = dato.GetString(3);
                }

                conexion.Close();
            }
            catch (Exception)
            {
                throw new Exception("No se ha encontrado el libro");
            }
            finally {
                conexion.Dispose();
                comando.Dispose();
            }
            return libro;
        }

        public int eliminar(ELibro libro) {
            int result = -1;
            string sentecia ="Delete from Libro Where claveLibro=@claveLibro";

            SqlConnection conexion = new SqlConnection(cadConexion);
            SqlCommand comando = new SqlCommand(sentecia,conexion);

            comando.Parameters.AddWithValue("@claveLibro", libro.ClaveLibro);

            try
            {
                conexion.Open();
                result = comando.ExecuteNonQuery();//0 1
                conexion.Close();
            }
            catch (Exception)
            {
                result = -1;
                //throw new Exception("Se ha presentado un error eliminando. No se ha logrado!");
            }
            finally {
                conexion.Dispose();
                comando.Dispose();
            }

            return result;
        }

        public string eliminarProcedure(ELibro libro) {

            string sentencia = "EliminarLibro";

            SqlConnection conexion = new SqlConnection(cadConexion);
            SqlCommand comando = new SqlCommand(sentencia, conexion);

            comando.Parameters.AddWithValue("@clave", libro.ClaveLibro).Direction = ParameterDirection.Input;

            //DEFINIR QUE EL TIPO DE COMANDO A EJECUTAR ES UN STORE PROCEDURE
            comando.CommandType = CommandType.StoredProcedure;

            // ↓ Parametro de SALIDA
            comando.Parameters.Add("@msj",SqlDbType.VarChar,100).Direction = ParameterDirection.InputOutput;

            try
            {
                conexion.Open();
                comando.ExecuteNonQuery();
                mensaje = comando.Parameters["@msj"].Value.ToString();
                conexion.Close();
            }
            catch (Exception)
            {

                throw new Exception("Se ha presentando un error con el Procedimiento Almacenado");
            }
            finally {
                conexion.Dispose();
                comando.Dispose();
            }

            return mensaje;
        }

        public int modificar(ELibro libro, string claveVieja="") {
            int result = -1;
            string sentencia;

            SqlConnection conexion = new SqlConnection(cadConexion);
            SqlCommand comando = new SqlCommand();

            if(string.IsNullOrEmpty(claveVieja))
                sentencia = "Update libro set titulo=@titulo, claveAutor=@claveAutor, claveCategoria = @claveCategoria Where claveLibro=@claveLibro";
            else
                sentencia = $"Update libro set claveLibro=@claveLibro, titulo=@titulo, claveAutor=@claveAutor, claveCategoria = @claveCategoria Where claveLibro='{claveVieja}'";

            comando.Connection = conexion;
            comando.CommandText = sentencia;

            comando.Parameters.AddWithValue("@claveLibro", libro.ClaveLibro);
            comando.Parameters.AddWithValue("@titulo", libro.Titulo);
            comando.Parameters.AddWithValue("@claveAutor", libro.ClaveAutor);
            comando.Parameters.AddWithValue("@claveCategoria", libro.Clavecategoria.ClaveCategoria);

            try
            {
                conexion.Open();
                result = comando.ExecuteNonQuery();
                conexion.Close();
            }
            catch (Exception)
            {
                throw new Exception("Error Actualizando");
            }
            finally {
                comando.Dispose();
                conexion.Dispose();
            }
            return result;
        }
        #endregion
    }
}
