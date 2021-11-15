using System;
using System.Collections.Generic;
using System.Text;
using Entidades;
using AccesoDatos;
using System.Data;

namespace LogicaNegocio
{
    public class LNLibro
    {
        private string mensaje;
        private string cadConexion;

        #region Constructores
        public LNLibro() {
            mensaje = string.Empty;
            cadConexion = string.Empty;
        }
        public LNLibro(string cadena) {
            mensaje = string.Empty;
            cadConexion = cadena;
        }
        #endregion

        #region Metrodos
        public bool libroRepetido(ELibro libro) {
            bool result = false;
            ADLibro adLibro = new ADLibro(cadConexion);

            try
            {
                result = adLibro.libroRepetido(libro);
            }
            catch (Exception ex)
            {
                throw ex;
            }

            return result;
        }

        public bool claveLibroRepetida(string clave)
        {
            bool result = false;
            ADLibro adLibro = new ADLibro(cadConexion);

            try
            {
                result = adLibro.claveLibroRepetida(clave);
            }
            catch (Exception ex)
            {
                throw ex;
            }

            return result;
        }

        public int insertar(ELibro libro) {
            int result;
            ADLibro adLibro = new ADLibro(cadConexion);

            try
            {
                result = adLibro.insertar(libro);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return result;
        }

        public DataSet listarTodos(string condicion = "") {
            DataSet setLibros;
            ADLibro adLibro = new ADLibro(cadConexion);

            try
            {
                setLibros = adLibro.listarTodos(condicion);
            }
            catch (Exception ex)
            {

                throw ex;
            }

            return setLibros;
        }

        public ELibro buscarRegistro(string condicion) {
            ELibro libro;
            ADLibro adl = new ADLibro(cadConexion);


            try
            {
                libro = adl.buscarRegistro(condicion);
            }
            catch (Exception ex)
            {
                throw ex;
            }

            return libro;
        }


        public int eliminar(ELibro libro) {
            int result;
            ADLibro adl = new ADLibro(cadConexion);

            try
            {
                result = adl.eliminar(libro);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return result;
        }

        public string eliminarProcedure(ELibro libro) {
            ADLibro adl = new ADLibro(cadConexion);

            try
            {
                mensaje = adl.eliminarProcedure(libro);
            }
            catch (Exception ex)
            {
                mensaje = ex.Message;
            }
            return mensaje;
        }

        public int modificar(ELibro libro, string claveVieja = "") {
            int result;

            ADLibro adl = new ADLibro(cadConexion);

            try
            {
                result = adl.modificar(libro, claveVieja);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return result;
        }
        #endregion
    }
}
