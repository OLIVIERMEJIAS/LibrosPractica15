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
        /// <summary>
        /// Permite el acceso a la capa de acceso a datos de ejemplares para listar todos los registros
        /// </summary>
        /// <param name="condicion"></param>
        /// <returns></returns>
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
        /// <summary>
        /// Permite el acceso a la capa de acceso a datos de ejemplares para saber si existen 
        /// ejemplares asociados a una editorial
        /// </summary>
        /// <param name="condicion"></param>
        /// <returns></returns>
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
