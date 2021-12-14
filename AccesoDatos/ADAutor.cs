using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using System.Data.SqlClient;
using Entidades;

namespace AccesoDatos
{
    public class ADAutor
    {

        public ADAutor(string cadConec)
        {
            cadConexion = cadConec;
        }
        public ADAutor()
        {
            cadConexion = string.Empty;
        }

        private string cadConexion;

        //*******************************************************************
        public EAutor BuscarRegistro(string condicion)
        {
            EAutor aut = new EAutor();
            string sentencia;

            SqlCommand comandoSQL = new SqlCommand();
            SqlConnection conexionSQL = new SqlConnection(cadConexion);
            //Se requiere un objeto para recuperar los datos. 
            SqlDataReader dato;//Solo se define el objeto, no hace falta instanciarlo en este momento

            sentencia = "Select claveAutor,nombre,apPaterno,apMaterno From Autor";
            if (!string.IsNullOrEmpty(condicion))
                sentencia = string.Format("{0} Where {1}", sentencia, condicion);

            comandoSQL.Connection = conexionSQL;
            comandoSQL.CommandText = sentencia;

            try
            {
                conexionSQL.Open();
                dato = comandoSQL.ExecuteReader();
                if (dato.HasRows)
                {
                    dato.Read();
                    aut.ClaveAutor = dato.GetString(0);
                    aut.Nombre = !dato.IsDBNull(1) ? dato.GetString(1) : "";
                    aut.ApPaterno = !dato.IsDBNull(2) ? dato.GetString(2) : ""; 
                    aut.ApMaterno = !dato.IsDBNull(3) ? dato.GetString(3) : "";
                    aut.ExiteRegistro = true;//Este dato es de App y solo le sirve al programador;
                }
                conexionSQL.Close();
            }
            catch (Exception)
            {
                throw new Exception("Error al recuperar el registro!");
            }
            return aut;
        }//Fin BUSCAR REGISTRO
        //*******************************************************************

        public DataTable ListarRegistros(string condicion)
        {
            DataTable result = new DataTable();
            SqlDataAdapter adaptador;
            SqlConnection conexion = new SqlConnection(cadConexion);

            string sentencia = "Select * From Autor";

            if (!string.IsNullOrEmpty(condicion))
                sentencia = $"{sentencia} Where  {condicion}";

            try
            {
                adaptador = new SqlDataAdapter(sentencia, conexion);
                adaptador.Fill(result);
            }
            catch (Exception)
            {
                throw new Exception("Se ha presentado un error extrayendo la lista de Autores");
            }

            return result;
        }//Fin Listar Registros

        //*******************************************************************
    }
}
