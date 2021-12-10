using System;
using System.Collections.Generic;
using System.Text;
using Entidades;
using System.Data;
using System.Data.SqlClient;

namespace AccesoDatos
{
    public class ADCategoria
    {
        public ADCategoria(string cadConec)
        {
            cadConexion = cadConec;
        }
        public ADCategoria()
        {
            cadConexion = string.Empty;
        }

        private string cadConexion;

        //*******************************************************************
        public ECategoria BuscarRegistro(string condicion)
        {
            ECategoria cate = new ECategoria();
            string sentencia;

            SqlCommand comandoSQL = new SqlCommand();
            SqlConnection conexionSQL = new SqlConnection(cadConexion);
            //Se requiere un objeto para recuperar los datos. 
            SqlDataReader dato;//Solo se define el objeto, no hace falta instanciarlo en este momento

            sentencia = "Select claveCategoria, descripcion From Categoria";
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
                    cate.ClaveCategoria = dato.GetString(0);
                    cate.Descripcion = !dato.IsDBNull(1) ? dato.GetString(1) : "";
                    //cate.ExisteRegistro = true;//Este dato es de App y solo le sirve al programador;
                }
                conexionSQL.Close();
            }
            catch (Exception)
            {
                throw new Exception("Error al recuperar el registro!");
            }
            return cate;
        }//Fin BUSCAR REGISTRO
        //*******************************************************************

        public DataTable ListarRegistros(string condicion)
        {
            DataTable result = new DataTable();
            SqlDataAdapter adaptador;
            SqlConnection conexion = new SqlConnection(cadConexion);

            string sentencia = "Select * From Categoria";

            if (!string.IsNullOrEmpty(condicion))
                sentencia = $"{sentencia} Where {condicion}";

            try
            {
                adaptador = new SqlDataAdapter(sentencia, conexion);
                adaptador.Fill(result);
            }
            catch (Exception)
            {
                throw new Exception("Se ha presentado un error extrayendo la lista de Tablas");
            }

            return result;
        }//Fin Listar Registros

        //*******************************************************************
    }
}
