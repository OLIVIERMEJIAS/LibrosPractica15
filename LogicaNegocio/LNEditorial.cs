using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using AccesoDatos;
using Entidades;

namespace LogicaNegocio
{
    public class LNEditorial
    {
        public string CadConec { get; set; }

        public LNEditorial()
        {
            CadConec = "";
        }

        public LNEditorial(string cad)
        {
            CadConec = cad;
        }

        public int insertar(EEditorial edit)
        {
            ADEditorial adE = new ADEditorial(CadConec);
            try
            {
                return adE.insertar(edit);
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }

        public bool existeRegistro(string condicion)
        {
            bool result = false;
            ADEditorial adE = new ADEditorial(CadConec);

            try
            {
                result = adE.existeRegistro(condicion);
            }
            catch (Exception ex)
            {

                throw ex;
            }

            return result;
        }

        public DataTable listarTodos(string condicion)
        {
            DataTable dt;
            ADEditorial adE = new ADEditorial(CadConec);
            try
            {
                dt = adE.listarTodos(condicion);
            }
            catch (Exception ex)
            {

                throw ex;
            }
            return dt;
        }
    }
}
