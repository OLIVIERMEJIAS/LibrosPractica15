using System;
using System.Collections.Generic;
using System.Text;
using Entidades;
using AccesoDatos;

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

        #endregion
    }
}
