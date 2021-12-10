using System;
using System.Collections.Generic;
using System.Text;

namespace Entidades
{
    class EAutor
    {
        public string ClaveAutor { get; set; }
        public string Nombre { get; set; }
        public string ApPaterno { get; set; }
        public string ApMaterno { get; set; }
        public bool ExiteRegistro { get; set; }

        public EAutor()
        {
            ClaveAutor = string.Empty;
            Nombre = string.Empty;
            ApPaterno = string.Empty;
            ApMaterno = string.Empty;
            ExiteRegistro = false;
        }

        public EAutor(string claveAutor, string nombre, string apPaterno, string apMaterno, bool existeRegistro)
        {
            ClaveAutor = claveAutor;
            Nombre = nombre;
            ApPaterno = apPaterno;
            ApMaterno = apMaterno;
            ExiteRegistro = existeRegistro;
        }
    }
}
