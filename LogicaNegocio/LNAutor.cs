using Entidades;
using AccesoDatos;
using System;
using System.Collections.Generic;
using System.Text;
using System.Data;


namespace LogicaNegocio
{
    class LNAutor
    {
        private string cadConexion;
        public LNAutor()
        {
            cadConexion = string.Empty;
        }

        public LNAutor(string cadConexion)
        {
            this.cadConexion = cadConexion;
        }

        public EAutor BuscarRegistro(string condicion)
        {
            EAutor aut;
            ADAutor accesoDatos = new ADAutor(cadConexion);

            try
            {
                aut = accesoDatos.BuscarRegistro(condicion);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return aut;
        }

        public DataTable ListarRegistros(string condicion)
        {
            DataTable result;
            ADAutor daAutor = new ADAutor(cadConexion);

            try
            {
                result = daAutor.ListarRegistros(condicion);
            }
            catch (Exception ex)
            {
                throw ex;
            }

            return result;
        }
    }
}
