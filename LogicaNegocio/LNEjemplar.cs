using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using AccesoDatos;

namespace LogicaNegocio
{
    public class LNEjemplar
    {
        public string CadConec { get; set; }

        public LNEjemplar()
        {
            CadConec = string.Empty;
        }

        public LNEjemplar(string cad)
        {
            CadConec = cad;
        }

        public DataTable listarTodos(string condicion)
        {
            ADEjemplar adE = new ADEjemplar(CadConec);
            try
            {
                return adE.listarTodos(condicion);
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }

        public bool existenEjemplares(string condicion)
        {
            ADEjemplar adE = new ADEjemplar(CadConec);

            try
            {
                return adE.existenEjemplares(condicion);
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }

    }
}
