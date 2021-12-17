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
        /// <summary>
        /// Permite el acceso a la capa de acceso a datos de editoriales para modificar un registro
        /// </summary>
        /// <param name="edit"></param>
        /// <param name="claveVieja"></param>
        /// <returns></returns>
        public int modificar(EEditorial edit, string claveVieja)
        {
            ADEditorial adE = new ADEditorial(CadConec);
            try
            {
                return adE.modificar(edit, claveVieja);
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }
        /// <summary>
        /// Permite el acceso a la capa de acceso a datos de editoriales para insertar un registro
        /// </summary>
        /// <param name="edit"></param>
        /// <returns></returns>
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
        /// <summary>
        /// Permite el acceso a la capa de acceso a datos de editoriales para eliminar un registro
        /// </summary>
        /// <param name="condicion"></param>
        /// <returns></returns>
        public int eliminar(string condicion)
        {
            ADEditorial adE = new ADEditorial(CadConec);

            try
            {
                return adE.eliminar(condicion);
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }
        /// <summary>
        /// Permite el acceso a la capa de acceso a datos de editoriales para recuperar el nombre en un registro
        /// </summary>
        /// <param name="condicion"></param>
        /// <returns></returns>
        public string recuperarNombre(string condicion)
        {
            ADEditorial adE = new ADEditorial(CadConec);

            try
            {
                return adE.recuperarNombre(condicion);
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }
        /// <summary>
        /// Permite el acceso a la capa de acceso a datos de editoriales para saber si existe un registro
        /// basado en una condición de clave o nombre
        /// </summary>
        /// <param name="condicion"></param>
        /// <returns></returns>
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
        /// <summary>
        /// Permite el acceso a la capa de acceso a datos de editoriales para listar todos los registros
        /// </summary>
        /// <param name="condicion"></param>
        /// <returns></returns>
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
