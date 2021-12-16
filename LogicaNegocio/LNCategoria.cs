using System;
using System.Collections.Generic;
using System.Text;
using Entidades;
using AccesoDatos;
using System.Data;


namespace LogicaNegocio
{
    public class LNCategoria
    {
        private string cadConexion;
        public LNCategoria()
        {
            cadConexion = string.Empty;
        }

        public LNCategoria(string cadConexion)
        {
            this.cadConexion = cadConexion;
        }

        public ECategoria BuscarRegistro(string condicion)
        {
            ECategoria cate;
            ADCategoria accesoDatos = new ADCategoria(cadConexion);

            try
            {
                cate = accesoDatos.BuscarRegistro(condicion);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return cate;
        }


        public DataTable ListarRegistros(string condicion)
        {
            DataTable result;
            ADCategoria accesoDatos = new ADCategoria(cadConexion);

            try
            {
                result = accesoDatos.ListarRegistros(condicion);
            }
            catch (Exception ex)
            {
                throw ex;
            }

            return result;
        }
    }
}
