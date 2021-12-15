﻿using System;
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
            public bool existeRegistro(string condicion)
        {
            bool result = false;
            object obEscalar;

            //1
            SqlCommand comando = new SqlCommand();
            SqlConnection conexion = new SqlConnection(CadConec);
            SqlDataReader datos;//NO SE INSTANCIA!!!

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